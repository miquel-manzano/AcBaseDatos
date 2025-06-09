using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using UF2_test.model;

namespace UF2_test.cruds
{
    public class EmpleatCRUD
    {
        // PS: utilitzant el PreparedStatement

        public List<Empleat> SelectAll()
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM emp", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<Empleat> emps = new List<Empleat>();

            while (dr.Read())
            {
                Empleat emp = new Empleat();
                emp.empNo = dr.GetInt32(0);
                emp.cognom = dr.GetString(1);
                emp.ofici = dr.GetString(2);
                emp.cap = dr.IsDBNull(3) ? (int?)null : dr.GetInt32(3);
                emp.dataAlta = dr.GetDateTime(4);
                emp.salari = dr.GetInt32(5);
                emp.comissio = dr.IsDBNull(6) ? (int?)null : dr.GetInt32(6);
                emp.deptNo = dr.GetInt32(7);
                emps.Add(emp);
            }

            conn.Close();
            return emps;
        }

        public Empleat SelectPS(int codi)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("SELECT * FROM emp WHERE emp_no =" + codi, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Empleat emp = new Empleat();

            if (dr.Read())
            {
                emp.empNo = dr.GetInt32(0);
                emp.cognom = dr.GetString(1);
                emp.ofici = dr.GetString(2);
                emp.cap = dr.IsDBNull(3) ? (int?)null : dr.GetInt32(3);
                emp.dataAlta = dr.GetDateTime(4);
                emp.salari = dr.GetInt32(5);
                emp.comissio = dr.IsDBNull(6) ? (int?)null : dr.GetInt32(6);
                emp.deptNo = dr.GetInt32(7);
            }
            else
            {
                emp = null;

            }

            conn.Close();
            return emp;
        }

        public void Insert(Empleat emp)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();
            var sql = $"INSERT INTO EMP VALUES ({emp.empNo}, '{emp.cognom}', '{emp.ofici}',{(emp.cap is null ? "NULL" : emp.cap.ToString())}, '{emp.dataAlta.Year}-{emp.dataAlta.Month}-{emp.dataAlta.Day}', {emp.salari}, {(emp.comissio is null ? "NULL" : emp.comissio.ToString())}, {emp.deptNo})";

            var cmd = new NpgsqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Emp with empNo {0} added", emp.empNo);
            }
            catch
            {
                Console.WriteLine("Couldn't add emp with empNo {0}", emp.empNo);
            }

            conn.Close();
        }
        public void Delete(int id)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            var cmd = new NpgsqlCommand("DELETE FROM emp WHERE emp_no =" + id, conn);

            if (cmd.ExecuteNonQuery() != 0)
            {
                Console.WriteLine($"Employee {id} deleted");
            }
            else
            {
                Console.WriteLine($"Employee {id} doesn't exist");
            }
            conn.Close();
        }

    }
}
