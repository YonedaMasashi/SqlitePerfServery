using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePerfServery
{
    internal class Col200Row2400
    {
        public void CobineDBInmemory()
        {
            var totalTime = new TimeMeasure();
            totalTime.Start();

            var newDBPath = @"C:\work\git_ws\SqlitePerfServery\TestData\Col200Row2400\Data\New_Col200Row2400.db";
            if (File.Exists(newDBPath) == true)
            {
                File.Delete(newDBPath);
            }
            File.Copy(@"C:\work\git_ws\SqlitePerfServery\TestData\Col200Row2400\Data\1_Col200Row2400.db",
                      newDBPath);

            var con = new SQLiteConnection(@"Data Source =" + newDBPath);
            con.Open();

            for (int i = 2; i < 1001; ++i)
            {
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "ATTACH [C:\\work\\git_ws\\SqlitePerfServery\\TestData\\Col200Row2400\\Data\\" + i.ToString() + "_Col200Row2400.db] AS db" + i.ToString() + ";";
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO main.Col200Row2400 SELECT * FROM db" + i.ToString() + ".Col200Row2400;";
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "detach db" + i.ToString() + ";";
                    cmd.ExecuteNonQuery();
                }
            }

            totalTime.Stop();
            Console.WriteLine("TotalTime:" + totalTime.Elapsed());
        }

        public void CombineDB()
        {
            var totalTime = new TimeMeasure();
            totalTime.Start();

            // 1つ目のDB
            var connectionTime = new TimeMeasure();
            connectionTime.Start();
            var con = new SQLiteConnection(@"Data Source=C:\work\git_ws\SqlitePerfServery\TestData\Col200Row2400\Data\1_Col200Row2400.db");
            con.Open();


            // 2つ目以降のDB
            for (int i = 2; i < 12; ++i)
            {
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "ATTACH [C:\\work\\git_ws\\SqlitePerfServery\\TestData\\Col200Row2400\\Data\\" + i.ToString() + "_Col200Row2400.db] AS db" + i.ToString() + ";";
                    cmd.ExecuteNonQuery();
                }
                connectionTime.Stop();
                Console.WriteLine("DB" + i.ToString() + " attach :" + connectionTime.Elapsed());

                var selectTime = new TimeMeasure();
                selectTime.Start();
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM main.Col200Row2400 "
                                     + "UNION ALL "
                                     + "SELECT * FROM db" + i.ToString() + ".Col200Row2400;";
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    selectTime.Stop();
                    Console.WriteLine("DB" + i.ToString() + " select :" + selectTime.Elapsed());

                    var resultsTime = new TimeMeasure();
                    resultsTime.Start();
                    var result = new StringBuilder();
                    while (reader.Read())
                    {
                        string data = string.Format("{0}\t{1}\t{2}", reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                        //Console.WriteLine(data);
                        result.Append(data);
                    }
                    resultsTime.Stop();
                    Console.WriteLine("DB" + i.ToString() + " result :" + resultsTime.Elapsed());
                }
            }

            totalTime.Stop();
            Console.WriteLine("TotalTime:" + totalTime.Elapsed());
        }
    }
}
