﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace UF2_test.cruds
{
    public class GeneralCRUD
    {
        public void DropTables(List<string> tables)
        {
            CloudConnection db = new CloudConnection();
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

        public void RunScriptEmpresa()
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            string script = File.ReadAllText("../../../Files/empresa.sql");
            var cmd = new NpgsqlCommand(script, conn);
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Script executed successfully");
            }
            catch
            {
                Console.WriteLine("Couldn't execute script, try to execute option 12 and then 11 again");
            }

            conn.Close();
        }
    }
}
