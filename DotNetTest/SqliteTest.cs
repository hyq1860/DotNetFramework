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
                conn.ConnectionString = "Data Source=e:\\sqlite.db3";
                conn.Open();

                // 创建数据表
                string sql = "create table [test1] ([id] INTEGER PRIMARY KEY, [s] TEXT COLLATE NOCASE)";
                DbCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // 添加参数
                cmd.Parameters.Add(cmd.CreateParameter());

                // 开始计时
                Stopwatch watch = new Stopwatch();
                watch.Start();

                DbTransaction trans = conn.BeginTransaction(); // <-------------------
                Random r = new Random();
                string data =
                    "为什么只是简单启用了一个事务会有这么大的差距呢？很简单，SQLite 缺省为每个操作启动一个事务，那么原代码 1000 次插入起码开启了 1000 个事务，事务开启 + SQL 执行 + 事务关闭 自然耗费了大量的时间";
                try
                {
                    // 连续插入1000条记录
                    for (int i = 0; i < 2000000; i++)
                    {
                        cmd.CommandText = "insert into [test1] ([s]) values (?)";
                        cmd.Parameters[0].Value = data
                            .Substring(0,r.Next(1,data.Length-1));

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

        public static void Query(string sql,string connectionstring)
        {
            // 创建数据库文件
            //SQLiteConnection.CreateFile("z:\\test1.db3");

            DbProviderFactory factory = SQLiteFactory.Instance;
            using (DbConnection conn = factory.CreateConnection())
            {
                // 连接数据库
                conn.ConnectionString = connectionstring;// "Data Source=e:\\sqlite.db3";
                conn.Open();

               
                DbCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                // 开始计时
                Stopwatch watch = new Stopwatch();
                watch.Start();
                cmd.ExecuteReader();
                // 停止计时
                watch.Stop();
                Console.WriteLine(watch.Elapsed);
            }

        }

        public static void Memory(int count)
        {
            // 创建数据库文件
            //SQLiteConnection.CreateFile("z:\\test1.db3");

            DbProviderFactory factory = SQLiteFactory.Instance;
            using (DbConnection conn = factory.CreateConnection())
            {
                // 连接数据库
                conn.ConnectionString = "Data Source=:memory:;Version=3;New=True";
                conn.Open();

                // 创建数据表
                string sql = "create table [test1] ([id] INTEGER PRIMARY KEY, [s] TEXT COLLATE NOCASE)";
                DbCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // 添加参数
                cmd.Parameters.Add(cmd.CreateParameter());

                // 开始计时
                Stopwatch watch = new Stopwatch();
                watch.Start();

                DbTransaction trans = conn.BeginTransaction(); // <-------------------
                Random r = new Random();
                string data =
                    "为什么只是简单启用了一个事务会有这么大的差距呢？很简单，SQLite 缺省为每个操作启动一个事务，那么原代码 1000 次插入起码开启了 1000 个事务，事务开启 + SQL 执行 + 事务关闭 自然耗费了大量的时间";
                try
                {
                    // 连续插入1000条记录
                    for (int i = 0; i < count; i++)
                    {
                        cmd.CommandText = "insert into [test1] ([s]) values (?)";
                        cmd.Parameters[0].Value = data
                            .Substring(0,r.Next(1,data.Length-1));

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit(); // <-------------------
                }
                catch
                {
                    trans.Rollback(); // <-------------------
                    throw; // <-------------------
                }
                cmd.CommandText = "select id from test1 limit 0,10000";
                cmd.Parameters.Clear();
                cmd.ExecuteReader();
                
                // 停止计时
                watch.Stop();
                Console.WriteLine(watch.Elapsed);
            }

        }

        public static void MemoryQuery(string sql)
        {
            // 创建数据库文件
            //SQLiteConnection.CreateFile("z:\\test1.db3");

            DbProviderFactory factory = SQLiteFactory.Instance;
            using (DbConnection conn = factory.CreateConnection())
            {
                // 连接数据库
                conn.ConnectionString = "Data Source=:memory:;Version=3;New=False";
                conn.Open();

               
                DbCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                


                // 开始计时
                Stopwatch watch = new Stopwatch();
                watch.Start();

                cmd.ExecuteReader();

                // 停止计时
                watch.Stop();
                Console.WriteLine(watch.Elapsed);
            }

        }
    }
}
