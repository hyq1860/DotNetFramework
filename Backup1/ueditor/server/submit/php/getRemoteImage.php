<?php
/**
 * Created by JetBrains PhpStorm.
 * User: taoqili
 * Date: 11-12-28
 * Time: 上午9:54
 * To change this template use File | Settings | File Templates.
 */
$uri = htmlspecialchars( $_POST[ 'content' ] );
//Ajax提交的网址内容中如果包含了&符号，上述函数会将其转成&amp;导致地址解析不对，这里要转回来
$uri = str_replace( "&amp;" , "&" , $uri );
getRemoteImage( $uri );

/**
 * @param $uri
 */
function getRemoteImage( $uri ){
    //忽略抓取时间限制
    set_time_limit( 0 );
    //远程抓取图片配置
    $config = array(
        "savePath" => "../../upload/uploadimages/" , //保存路径
        "fileType" => array( ".gif" , ".png" , ".jpg" , ".jpeg" , ".bmp" ) , //文件允许格式
        "fileSize" => 3000 //文件大小限制，单位KB
    );
	//ue_separate_ue  ue用于传递数据分割符号
    $imgUrls = explode( "ue_separate_ue" , $uri );
    $tmpNames = array();
    foreach ( $imgUrls as $imgUrl ) {
        //格式验证
        $fileType = strtolower( strrchr( $imgUrl , '.' ) );
        if ( !in_array( $fileType , $config[ 'fileType' ] ) ) {
            array_push($tmpNames,"error" );
            continue;
        }
        //死链检测
        if ( !urlTest( $imgUrl ) ) {
            array_push($tmpNames, "error" );
            continue;
        };

        //打开输出缓冲区并获取远程图片
        ob_start();
        //请确保php.ini中的fopen wrappers已经激活
        readfile( $imgUrl );
        $img = ob_get_contents();
        ob_end_clean();

        //大小验证
        $uriSize = strlen( $img ); //得到图片大小
        $allowSize = 1024 * $config[ 'fileSize' ];
        if ( $uriSize > $allowSize ) {
            array_push($tmpNames,"error" );
            continue;
        }
        //创建保存位置
        $savePath = $config[ 'savePath' ];
        if ( !file_exists( $savePath ) ) {
            mkdir( "$savePath" , 0777 );
        }
        //写入文件
        $tmpName = $savePath . rand( 1 , 10000 ) . time() . strrchr( $imgUrl , '.' );
        try {
            $fp2 = @fopen( $tmpName , "a" );
            fwrite( $fp2 , $img );
            fclose( $fp2 );
            array_push($tmpNames,str_replace('../../','',$tmpName)); //同图片上传一样，去掉容易引起路径混乱的../
	        //array_push($tmpNames,$tmpName);
        } catch ( Exception $e ) {
            array_push($tmpNames, "error" );
        }
    }

    response( "{'url':'" . implode("ue_separate_ue", $tmpNames) . "','tip':'远程图片抓取成功！','srcUrl':'" . $uri . "'}" );
}

/**
 * 返回数据
 * @param $state
 */
function response( $state ){
    echo $state;
    exit();
}

/**
 * 死链检测
 * @param $uri
 * @return bool
 */
function urlTest( $uri ){
    $headerArr = get_headers( $uri );
    return stristr( $headerArr[ 0 ] , "200" ) && stristr( $headerArr[ 0 ] , "OK" );
}

?>