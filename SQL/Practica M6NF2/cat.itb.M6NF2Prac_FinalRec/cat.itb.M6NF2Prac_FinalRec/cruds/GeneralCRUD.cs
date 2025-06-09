using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6UF2EA3.connections;
using Npgsql;

namespace cat.itb.M6NF2Prac_FinalRec.cruds
{
    public class GeneralCRUD
    {
        public void DropTables(List<string> tables)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            foreach (var table in tables)
            {
                var cmd = new NpgsqlCommand("DROP TABLE " + table + " CASCADE", conn);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table {0} succesfully dropped", table);
                }
                catch
                {
                    Console.WriteLine("Table {0} doesn't exist", table);
                }
            }
            conn.Close();
        }

        public void RunScriptStore()
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            string script = File.ReadAllText("../../../data/store.sql");
            var cmd = new NpgsqlCommand(script, conn);
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Script executed successfully");
            }
            catch
            {
                Console.WriteLine("Couldn't execute script, try to execute option 09 and then 10 again");
            }

            conn.Close();
        }
    }
}
