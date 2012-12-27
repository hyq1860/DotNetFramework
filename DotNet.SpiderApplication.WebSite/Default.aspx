<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotNet.SpiderApplication.WebSite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<title>黄金价格走势图</title>

		<script type="text/javascript" src="script/jquery-1.8.2.js"></script>
		<script type="text/javascript">
		    $(function () {
		        var chart;
		        $(document).ready(function () {
		            chart = new Highcharts.Chart({
		                chart: {
		                    renderTo: 'container',
		                    type: 'spline'
		                },
		                title: {
		                    text: 'AU9999价格走势图'
		                },
//		                subtitle: {
//		                    text: 'An example of irregular time data in Highcharts JS'
//		                },
		                xAxis: {
		                    type: 'datetime',
		                    dateTimeLabelFormats: { // don't display the dummy year
		                        month: '%e. %b',
		                        year: '%b'
		                    }
		                },
		                yAxis: {
		                    title: {
		                        text: '价格 (元)'
		                    },
		                    min: 0
		                },
		                tooltip: {
		                    formatter: function () {
		                        return '<b>' + this.series.name + '</b><br/>' +
                        Highcharts.dateFormat('%e. %b', this.x) + ': ' + this.y + ' m';
		                    }
		                },

		                series: [{
		                    name: '<%=First %>-<%=Latest %>',
		                    data: [
//                    [Date.UTC(1970, 9, 9), 0],
//                    [Date.UTC(1970, 9, 14), 0.15]
		                        <%=Data %>
                ]
		                }]
		            });
		        });

		    });
		</script>
	</head>
	<body>
<script src="script/highcharts.js"></script>
<script src="script/modules/exporting.js"></script>

<div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>

	</body>
</html>