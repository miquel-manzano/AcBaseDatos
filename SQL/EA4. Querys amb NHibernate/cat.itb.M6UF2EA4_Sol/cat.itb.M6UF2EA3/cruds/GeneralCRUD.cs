using System;
using System.Collections.Generic;
using System.IO;
using cat.itb.M6UF2EA3.connections;
using Npgsql;

namespace cat.itb.M6UF2EA3.cruds
{
    public class GeneralCRUD
    {
        public void DropTables(List<string> tables)
        {
            HR2CloudConnection db = new HR2CloudConnection();
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
        
        public void RunScriptHR()
        {
            HR2CloudConnection db = new HR2CloudConnection();
            var conn = db.GetConnection();

            string script = File.ReadAllText("../../MyFiles/HR.sql");
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

        public void RestoreHR2DBSession()
        {
            string path = @"..\..\MyFiles\hr2.sql";
            string sql = File.ReadAllText(path);
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                session.CreateSQLQuery(sql).ExecuteUpdate();
                Console.WriteLine("DB HR2 restored");
            }
        }
    }
}