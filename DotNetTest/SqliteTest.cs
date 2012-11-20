        using System.Data;
using System.Data.Common;
using System.Data.SQLite;
namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SqliteTest
    {
        public static void Test()
        {
            // 创建数据库文件
            //SQLiteConnection.CreateFile("z:\\test1.db3");

            DbProviderFactory factory = SQLiteFactory.Instance;
            using (DbConnection conn = factory.CreateConnection())
            {
                // 连接数据库
                conn.ConnectionString = "Data Source=z:\\test1.db3";
                conn.Open();

                // 创建数据表
                string sql = "create table [test1] ([id] INTEGER PRIMARY KEY, [s] TEXT COLLATE NOCASE)";
                DbCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

                // 添加参数
                cmd.Parameters.Add(cmd.CreateParameter());

                // 开始计时
                Stopwatch watch = new Stopwatch();
                watch.Start();

                DbTransaction trans = conn.BeginTransaction(); // <-------------------
                try
                {
                    // 连续插入1000条记录
                    for (int i = 0; i < 2000000; i++)
                    {
                        cmd.CommandText = "insert into [test1] ([s]) values (?)";
                        cmd.Parameters[0].Value = "为什么只是简单启用了一个事务会有这么大的差距呢？很简单，SQLite 缺省为每个操作启动一个事务，那么原代码 1000 次插入起码开启了 1000 个事务，事务开启 + SQL 执行 + 事务关闭 自然耗费了大量的时间";

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit(); // <-------------------
                }
                catch
                {
                    trans.Rollback(); // <-------------------
                    throw; // <-------------------
                }

                // 停止计时
                watch.Stop();
                Console.WriteLine(watch.Elapsed);
            }

        }


    }
}
