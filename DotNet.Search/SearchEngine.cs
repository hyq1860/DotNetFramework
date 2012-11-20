// -----------------------------------------------------------------------
// <copyright file="SearchEngine.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace DotNet.Search
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;

    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Service;

    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Store;

    using PanGu;

    /// <summary>
    /// 搜索
    /// </summary>
    public class SearchEngine
    {
        public SearchEngine(bool createIndex)
        {
            // 构造索引存放的路径t
            Lucene.Net.Store.Directory indexPath =
                FSDirectory.Open(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Search/")));
            if(createIndex)
            {
                indexWriter = new IndexWriter(indexPath, new PanGuAnalyzer(), createIndex, new IndexWriter.MaxFieldLength(10000));
            }
        }

        private IndexWriter indexWriter = null;

        public void CreateIndex()
        {
            IProductService productService = new ProductService();
            int count = productService.GetProductCount(string.Empty);
            var data = productService.GetProducts(count, 1, string.Empty);

            //设置为多文件索引的格式，默认情况下为true，会建立复合索引的文件结构，这里为了分析，先设置为false，生成多文件的索引结构
            //this.indexWriter.SetUseCompoundFile(false);

            foreach (var productInfo in data)
            {
                var doc = new Document();
                var field1 = new Field("title", productInfo.Title, Field.Store.YES, Field.Index.ANALYZED);
                // 向文档中添加域
                doc.Add(field1);
                field1 = new Field("Category", productInfo.CategoryName, Field.Store.YES, Field.Index.ANALYZED);
                doc.Add(field1);
                field1 = new Field("Desc", productInfo.Desc??"", Field.Store.YES, Field.Index.ANALYZED);
                doc.Add(field1);
                this.indexWriter.AddDocument(doc);
            }

            // 优化索引结构
            this.indexWriter.Optimize();

            this.indexWriter.Commit();
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
            indexDir = HttpContext.Current.Server.MapPath("~/Search/");
            string keywords = q;

            var search = new IndexSearcher(indexDir);

            q = GetKeyWordsSplitBySpace(q, new PanGuTokenizer());

            string[] fields = { "title", "Category", "Desc" };

            QueryParser qp= new MultiFieldQueryParser(fields, new PanGuAnalyzer(true));
            qp.SetDefaultOperator(Lucene.Net.QueryParsers.QueryParser.OR_OPERATOR);

            //var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Desc", new PanGuAnalyzer(true));

            Query query = qp.Parse(q);

            Hits hits = search.Search(query);

            // 新的查询
            TopDocs newHits = search.Search(query, 100);
            ScoreDoc[] scoreDocs = newHits.ScoreDocs;
            for (int i = 0; i < scoreDocs.Length; i++)
            {
                Document document = search.Doc(scoreDocs[i].doc);
                document.GetField("id").StringValue();
            }  


            recCount = hits.Length();
            int j = (pageIndex - 1) * pageSize;

            for (int i = 0; i <= hits.Length() - 1; i++)
            {
                hits.Doc(i).GetField("Desc").StringValue();
            }

            search.Close();
        }

        //http://www.d80.co.uk/post/2011/03/29/LuceneNet-Tutorial.aspx  Lucene.Net Tutorial with Lucene 2.9.2

        public void SearchV2(string indexDir, string q, int pageSize, int pageIndex)
        {
            indexDir = HttpContext.Current.Server.MapPath("~/Search/");
            string keywords = q;
            var search = new IndexSearcher(indexDir);

            q = GetKeyWordsSplitBySpace(q, new PanGuTokenizer());

            // 需要查询的域名称
            string[] fields = { "title", "Category", "Desc" };

            //将每个域Field所查询的结果设为“或”的关系，也就是取并集           
            BooleanClause.Occur[] clauses = { BooleanClause.Occur.SHOULD, BooleanClause.Occur.SHOULD };

            //构造一个多Field查询
            Query query = MultiFieldQueryParser.Parse(Lucene.Net.Util.Version.LUCENE_29, q, fields, clauses, new PanGuAnalyzer(true));

            // 新的查询
            TopDocs newHits = search.Search(query, 100);
            ScoreDoc[] scoreDocs = newHits.ScoreDocs;

            Console.WriteLine("符合条件记录:{0}; 索引库记录总数:{1}", scoreDocs.Length, search.GetIndexReader().NumDocs());

            foreach (var hit in newHits.ScoreDocs)
            {
                var documentFromSearcher = search.Doc(hit.doc);
                Console.WriteLine(documentFromSearcher.Get("Make") + " " + documentFromSearcher.Get("Model"));
            }

            search.Close();
        }

        //更新索引
        /**
        调用UpdateDocument() 方法，传给它一个新的doc来更新数据：
        Term term = new Term(“id”, “1″);
        先去索引文件里查找id为1 的 Document，如果有就更新它（如果有多条，最后更新后只有一条)。如果没有就新增 Document 到索引中。
        数据库更新的时候，我们可以只针对某个列来更新，而lucene只能针对一行数据更新。
        */
        public void UpdateIndex()
        {
            Lucene.Net.Store.Directory indexPath =
                FSDirectory.Open(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Search/")));
            IndexWriter writer = new IndexWriter(indexPath, new PanGuAnalyzer(), false, IndexWriter.MaxFieldLength.LIMITED);

            Term term = new Term("id", "1");
            Document doc = new Document();
            doc.Add(new Field("id", "100", Field.Store.YES, Field.Index.NOT_ANALYZED));

            writer.UpdateDocument(term, doc, new PanGuAnalyzer(true));
            writer.Optimize();
            writer.Close();
        }

        //http://www.entlib.net/?p=1090 删除索引
        public void DeleteIndex()
        {
            
        }

        //http://www.cnblogs.com/mysweet/archive/2012/04/29/2476585.html

        //http://www.cnblogs.com/sophist/archive/2011/05/12/2044380.html
        //http://www.aspxcs.net/HTML/005152948.html
        public string NoHTML(string Htmlstring)  //替换HTML标记
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        //http://www.cnblogs.com/Jesong/archive/2010/04/13/1710994.html 解释器过滤html
    }
}
