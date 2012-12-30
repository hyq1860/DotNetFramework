// -----------------------------------------------------------------------
// <copyright file="SearchManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SearchEngine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Store;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SearchManager
    {
        public PanGuAnalyzer analyzer;
        public string indexDirectory = string.Empty;

        public int NumberHits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void IndexCreate()
        {
            analyzer = new PanGuAnalyzer();
            
            IndexWriter writer = new IndexWriter(FSDirectory.Open(new DirectoryInfo(indexDirectory)),analyzer, true, IndexWriter.MaxFieldLength.LIMITED);


        }

        public void Search(string keyword)
        {
            IndexReader reader = null;
            IndexSearcher searcher = null;
            try
            {
                reader = IndexReader.Open(FSDirectory.Open(new DirectoryInfo(indexDirectory)), true);
                searcher = new IndexSearcher(reader);
                //创建查询
                PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(analyzer);
                wrapper.AddAnalyzer("FileName", analyzer);
                wrapper.AddAnalyzer("Author", analyzer);
                wrapper.AddAnalyzer("Content", analyzer);
                string[] fields = { "FileName", "Author", "Content" };

                QueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fields, wrapper);
                Query query = parser.Parse(keyword);
                TopScoreDocCollector collector = TopScoreDocCollector.Create(NumberHits, true);

                searcher.Search(query, collector);
                var hits = collector.TopDocs().ScoreDocs;

                int numTotalHits = collector.TotalHits;


                //以后就可以对获取到的collector数据进行操作
                for (int i = 0; i < hits.Count(); i++)
                {
                    var hit = hits[i];
                    Document doc = searcher.Doc(hit.Doc);
                    Field fileNameField = doc.GetField("FileName");
                    Field authorField = doc.GetField("Author");
                    Field pathField = doc.GetField("Path");


                }
            }
            finally
            {
                if (searcher != null)
                    searcher.Dispose();

                if (reader != null)
                    reader.Dispose();
            }
        }
    }
}
