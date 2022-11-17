using SqlitePerfServery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePerfServeryFW452
{
    internal class DataTable_Col200Row2400
    {
        public static void CombineDB()
        {
            var totalTime = new TimeMeasure();
            totalTime.Start();

            DataTable dt = new DataTable();


            for (int i = 1; i < 101; ++i)
            {
                using (var con = new SQLiteConnection(@"Data Source =" + "C:\\work\\git_ws\\SqlitePerfServery\\TestData\\Col200Row2400\\Data\\" + i.ToString() + "_Col200Row2400.db"))
                {
                    con.Open();
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Col200Row2400";

                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }

            Console.WriteLine("dt.Rows.Count:" + dt.Rows.Count);

        }
    }
}
