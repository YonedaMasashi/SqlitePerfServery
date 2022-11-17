using SqlitePerfServery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePerfServeryFW452
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var col200Row2400 = new NewSQLiteDBFile_Col200Row2400();
            //col200Row2400.CombineDB();
            //col200Row2400.CobineDBInmemory();


            DataTable_Col200Row2400.CombineDB();
            

        }
    }
}
