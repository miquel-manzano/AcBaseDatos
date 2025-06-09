using System;
using System.Collections.Generic;
using System.IO;
using cat.itb.M6UF2EA2.connections;
using Npgsql;

namespace cat.itb.M6UF2EA2.cruds
{
    /// <summary>
    /// Classe on s'administra l'estructura de la database
    /// </summary>
    public class GeneralCRUD
    {
        /// <summary>
        /// Mètode per esborrar les taules
        /// </summary>
        /// <param name="tables">Llista de strings amb el nom de les taules</param>
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

        /// <summary>
        /// Mètode per executar el script empresa.sql
        /// </summary>
        public void RunScriptEmpresa()
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            string script = File.ReadAllText("../../MyFiles/empresa.sql");
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