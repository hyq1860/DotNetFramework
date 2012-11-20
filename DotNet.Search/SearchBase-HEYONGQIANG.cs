// -----------------------------------------------------------------------
// <copyright file="SearchService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using PanGu;
using PanGu.HighLight;

namespace DotNet.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //http://www.cnblogs.com/guoyuanwei/archive/2012/04/02/2429481.html

    //http://www.cnblogs.com/think_fish/archive/2011/04/15/2017631.html
    //http://www.cnblogs.com/think_fish/archive/2011/06/17/2083861.html

    //http://stackoverflow.com/questions/1079934/how-do-you-implement-a-custom-filter-with-lucene-net 设置自定义过滤器
    //http://weblogs.asp.net/gunnarpeipman/archive/2011/09/02/using-lucene-net-search-engine-library-in-net-applications.aspx

    //http://hrycan.com/2010/02/10/paginating-lucene-search-results/ 分页

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SearchBase
    {
        private IndexWriter indexWriter = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBase"/> class.
        /// </summary>
        public SearchBase(string path)
        {
            // 构造索引存放的路径
            Lucene.Net.Store.Directory indexPath = FSDirectory.Open(new DirectoryInfo(path));

            // 第一个参数为索引存放的路径，
            // 第二个参数为分词处理类实例，
            // 第三个参数为true表示会删掉原有的索引，从新建索引,
            // 第四个参数表示某个域对于不的数据源分词后最大的词条数目     
            indexWriter = new IndexWriter(indexPath, new PanGuAnalyzer(), false, new IndexWriter.MaxFieldLength(10000));
        }

        /// <summary>
        /// 索引文件路径
        /// </summary>
        public string IndexFilePath
        {
            get;
            set;
            
            //get
            //{
            //    //return PanGu.Framework.Path.GetAssemblyPath() + @"SearchIndex";
            //}
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

            // 向文档中添加域
            doc.Add(field1);

            this.indexWriter.AddDocument(doc);
            //
            this.indexWriter.Optimize();
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

            var search = new IndexSearcher(indexDir, true);

            q = GetKeyWordsSplitBySpace(q, new PanGuTokenizer());

            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "title", new PanGuAnalyzer(true));

            Query query = queryParser.Parse(q);

            // 排序
            var sort = new Sort("title", true);

            Hits hits = search.Search(query);


            //TopDocs topDocs = search.Search(query, 10);
            //int results = topDocs.scoreDocs.Length;

            //Console.WriteLine("Found {0} results", results);

            //for (int i = 0; i < results; i++)
            //{
            //    ScoreDoc scoreDoc = topDocs.scoreDocs[i];
            //    float score = scoreDoc.score;
            //    int docId = scoreDoc.doc;
            //    Document doc = search.Doc(docId);
            //    Console.WriteLine("Result num {0}, score {1}", i + 1, score);
            //    Console.WriteLine("ID: {0}", doc.Get("id"));
            //    Console.WriteLine("Text found: {0}\r\n", doc.Get("postBody"));
            //}

            recCount = hits.Length();
            int i = (pageIndex - 1) * pageSize;

            //Console.WriteLine("符合条件记录:{0}; 索引库记录总数:{1}", hits.Length(),search.GetIndexReader().NumDocs());
            search.Close();
        }


        /// <summary>
        /// 构建表达式
        /// </summary>
        /// <param name="s_Analyzer"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public BooleanQuery CreateQuery(Analyzer s_Analyzer, string search)
        {
            //构造布尔查询，用于拼装各查询逻辑
            BooleanQuery query = new BooleanQuery();
            query.Add(new MultiFieldQueryParser(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" }, s_Analyzer).Parse(search), BooleanClause.Occur.SHOULD);

            return query;
        }
    }
}
