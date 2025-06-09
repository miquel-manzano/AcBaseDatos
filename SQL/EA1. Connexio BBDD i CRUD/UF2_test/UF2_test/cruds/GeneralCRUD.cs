using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Npgsql;

namespace UF2_test.cruds
{
    public class GeneralCRUD
    {
        public void ExecuteScriptSchool()
        {
            string script = File.ReadAllText(@"../../../files/school.sql");
            // split script on GO command
            IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            CloudConnection db = new CloudConnection();
            
            var conn = db.GetConnection();

            foreach (string commandString in commandStrings)
            {
                if (!string.IsNullOrWhiteSpace(commandString.Trim()))
                {
                    using (var cmd = new NpgsqlCommand(commandString, conn))
                    {
                        cmd.ExecuteNonQuery();
                        //Informo a l'usuari          
                        Console.WriteLine("script run");
                    }
                }
            }
            conn.Close();
        }

        public void DropTable(string table)
        {
            CloudConnection db = new CloudConnection();
            //SchoolConnection db = new SchoolConnection();
            var conn = db.GetConnection();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "DROP TABLE IF EXISTS " + table;

            //Informo a l'usuari si l'eliminació s'ha realitzat         
            Console.WriteLine("table droped");

            // Close connection
            conn.Close();
        }
    }
}
