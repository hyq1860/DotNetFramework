// -----------------------------------------------------------------------
// <copyright file="SearchService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using PanGu;

namespace DotNet.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //http://www.cnblogs.com/studyzy/archive/2005/06/08/694120.htm 内容很全
    //http://www.cnblogs.com/jeffwongishandsome/archive/2010/12/19/1910697.html
    //http://www.cnblogs.com/jeffwongishandsome/archive/2010/12/18/1910146.html
    //http://www.cnblogs.com/gym_sky/archive/2010/06/30/1768178.html  实现索引生成，修改，查询，删除功能
    //http://www.cnblogs.com/eaglet/archive/2008/11/03/1325350.html
    //http://www.cnblogs.com/wenjl520/archive/2009/07/25/1530679.html
    //http://www.cnblogs.com/jinzhao/archive/2012/05/03/2481018.html
    //http://www.cnblogs.com/kiufei/archive/2012/05/24/2517084.html
    //http://www.cnblogs.com/guoyuanwei/archive/2012/03/30/2425153.html 很全面

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SearchBase
    {
        private IndexWriter indexWriter = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBase"/> class.
        /// </summary>
        public SearchBase()
        {
            // 构造索引存放的路径
            Lucene.Net.Store.Directory indexPath = FSDirectory.Open(new DirectoryInfo(IndexFilePath));

            // 第一个参数为索引存放的路径，
            // 第二个参数为分词处理类实例，
            // 第三个参数为true表示会删掉原有的索引，从新建索引,
            // 第四个参数表示某个域对于不的数据源分词后最大的词条数目     

            if (System.IO.File.Exists(IndexFilePath))
            {
                indexWriter = new IndexWriter(indexPath, new PanGuAnalyzer(), false, new IndexWriter.MaxFieldLength(10000));
            }
            else
            {
                indexWriter = new IndexWriter(indexPath, new PanGuAnalyzer(), true, new IndexWriter.MaxFieldLength(10000));
            }
            
        }

        /// <summary>
        /// 索引文件路径
        /// </summary>
        public string IndexFilePath
        {
            get
            {
                return PanGu.Framework.Path.GetAssemblyPath() + @"SearchIndex";
            }
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        public void CreateIndex()
        {

            var doc = new Document();

            // 构造一个域信息
            /*
             Field.Store.YES:存储字段值（未分词前的字段值）     
             Field.Store.NO:不存储,存储与索引没有关系 
             Field.Store.COMPRESS:压缩存储,用于长文本或二进制，但性能受损 
             Field.Index.ANALYZED:分词建索引 
             Field.Index.ANALYZED_NO_NORMS:分词建索引，但是Field的值不像通常那样被保存，而是只取一个byte，这样节约存储空间 
             Field.Index.NOT_ANALYZED:不分词且索引 
             Field.Index.NOT_ANALYZED_NO_NORMS:不分词建索引，Field的值去一个byte保存 
             TermVector表示文档的条目（由一个Document和Field定位）和它们在当前文档中所出现的次数 
             Field.TermVector.YES:为每个文档（Document）存储该字段的TermVector 
             Field.TermVector.NO:不存储TermVector 
             Field.TermVector.WITH_POSITIONS:存储位置 
             Field.TermVector.WITH_OFFSETS:存储偏移量 
             Field.TermVector.WITH_POSITIONS_OFFSETS:存储位置和偏移量
             */

            var field1 = new Field("title", "笑傲江湖", Field.Store.YES, Field.Index.ANALYZED);

            // Field设置权重     
            field1.SetBoost(1.1f);

            // 向文档中添加域
            doc.Add(field1);

            // 设置文档的权重（默认权重是1.0）
            doc.SetBoost(2);        

            this.indexWriter.AddDocument(doc);

            // 优化索引结构
            this.indexWriter.Optimize();

            // this.indexWriter.Commit();

            // 关闭写入
            this.indexWriter.Close();
        }


        private string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            var result = new StringBuilder();

            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);

            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }

                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }

            return result.ToString().Trim();
        }

        public void Search(string indexDir, string q, int pageSize, int pageIndex, out int recCount)
        {
            string keywords = q;

            var search = new IndexSearcher(indexDir);

            q = GetKeyWordsSplitBySpace(q, new PanGuTokenizer());

            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "title", new PanGuAnalyzer(true));

            Query query = queryParser.Parse(q);

            Hits hits = search.Search(query);


            recCount = hits.Length();
            int i = (pageIndex - 1) * pageSize;


            search.Close();
        }
    }
}
