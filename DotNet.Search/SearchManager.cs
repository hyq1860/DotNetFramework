// -----------------------------------------------------------------------
// <copyright file="SearchManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Search
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Lucene.Net.Store;

    using PanGu;

    /// <summary>
    /// 搜索管理
    /// </summary>
    public class SearchManager
    {
        /// <summary>
        /// 索引文件路径
        /// </summary>
        private  static string LuceneIndexFilePath = Path.Combine(Environment.CurrentDirectory,"IndexData");

        private static FSDirectory tempDirectory;

        private static FSDirectory Directory
        {
            get
            {
                if (tempDirectory == null)
                {
                    tempDirectory = FSDirectory.Open(new DirectoryInfo(LuceneIndexFilePath));
                }
                    
                if (IndexWriter.IsLocked(tempDirectory))
                {
                    IndexWriter.Unlock(tempDirectory);
                }
                    
                var lockFilePath = Path.Combine(LuceneIndexFilePath, "write.lock");

                if (File.Exists(lockFilePath))
                {
                    File.Delete(lockFilePath);
                }
                return tempDirectory;
            }
        }

        private IndexWriter indexWriter = null;

        public SearchManager()
        {
            try
            {
                this.indexWriter = new IndexWriter(
                    tempDirectory, new PanGuAnalyzer(), false, IndexWriter.MaxFieldLength.UNLIMITED);
            }
            catch
            {
                this.indexWriter = new IndexWriter(
                    tempDirectory, new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.UNLIMITED);
            }
        }

        #region 索引


        #endregion

        #region 搜索

        /// <summary>
        /// 搜索方法
        /// </summary>
        /// <param name="keyWords"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        public void Search(string keyWords, int pageSize, int pageIndex, ref int recordCount)
        {
            
        }

        #endregion

        #region Helper

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

        /// <summary>
        /// lucene转义方法
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private static string LuceneEscape(string keyWord)
        {
            var result = new StringBuilder();
            TSTree luceneDic = TSTree.Instance();

            int i;
            for (i = 0; i < keyWord.Length; )
            {
                TSTree.Prefix check = luceneDic.checkPrefix(keyWord[i].ToString(), TSTree.rootNode);
                int j = 1;
                while (true)
                {
                    if (j - i > keyWord.Length)
                    {
                        i = i + j;
                        break;
                    }

                    if (check == TSTree.Prefix.MisMatch)
                    {
                        if (j == 1)
                        {
                            result.Append(keyWord.Substring(i, j));
                            i = i + j;
                        }
                        else
                        {
                            result.Append(keyWord.Substring(i, j - 1));
                            i = i + j - 1;
                        }

                        break;
                    }

                    if (check == TSTree.Prefix.Match)
                    {
                        result.Append("\\" + keyWord.Substring(i, j));
                        i = i + j;
                        break;
                    }

                    j++;
                    check = luceneDic.checkPrefix(keyWord.Substring(i, j), TSTree.rootNode);
                }
            }
            return result.ToString();
        }

        #endregion
    }
}
