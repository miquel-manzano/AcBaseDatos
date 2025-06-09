using System;
using System.Collections.Generic;
using cat.itb.M6UF2EA2.connections;
using cat.itb.M6UF2EA2.model;
using Npgsql;

namespace cat.itb.M6UF2EA2.cruds
{
    /// <summary>
    /// Classe per gestionar la taula emp
    /// </summary>
    public class EmpleatCRUD
    {
        
        /// <summary>
        /// Mètode que retorna l'Empleat que passes el codi per paràmetre
        /// </summary>
        /// <returns>Un objecte Empleat</returns>
        public Empleat Select(int codi)
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
                emp.cap = dr.IsDBNull(3) ? (int?) null : dr.GetInt32(3);
                emp.dataAlta = dr.GetDateTime(4); 
                emp.salari = dr.GetInt32(5); 
                emp.comissio = dr.IsDBNull(6) ? (int?) null : dr.GetInt32(6);
                emp.deptNo = dr.GetInt32(7);

            }
            else
            {
                emp = null;
               
            }
           
            conn.Close();

            return emp;
        }
        
        /// <summary>
        /// Mètode que inserta un Empleat
        /// </summary>
        /// <param name="emp">Objecte Empleat</param>
        /// <returns>True si s'ha eliminat i false si no es pot eliminar</returns>
        public Boolean Insert(Empleat emp)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();
            var sql = "INSERT INTO EMP VALUES ("
                      + emp.empNo +", " 
                      +"'"+ emp.cognom +"', "
                      +"'"+ emp.ofici +"', "
                      + (emp.cap is null ? "NULL" : emp.cap.ToString()) +", "
                      +"'"+ emp.dataAlta.Year + "-" + emp.dataAlta.Month + "-" + emp.dataAlta.Day + "', "
                      + emp.salari +", "
                      + (emp.comissio is null ? "NULL" : emp.comissio.ToString()) +", "
                      + emp.deptNo
                      +")";

            Boolean ins = false;
            var cmd = new NpgsqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    ins = true;
                    //Console.WriteLine("Emp with empNo {0} added", emp.empNo);
                }
                catch
                {
                    ins = false;
                    //Console.WriteLine("Couldn't add emp with empNo {0}", emp.empNo);
                }
        
            conn.Close();

            return ins;
        }
        
        /// <summary>
        /// Mètode que retorna tots els empleats de la taula
        /// </summary>
        ///<returns>Una llista d'empleats</returns>
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
                emp.cap = dr.IsDBNull(3) ? (int?) null : dr.GetInt32(3);
                emp.dataAlta = dr.GetDateTime(4); 
                emp.salari = dr.GetInt32(5); 
                emp.comissio = dr.IsDBNull(6) ? (int?) null : dr.GetInt32(6);
                emp.deptNo = dr.GetInt32(7);
                emps.Add(emp);
            }

            conn.Close();
            return emps;
        }

        /// <summary>
        /// Mètode que elimina l'Empleat que passem el id per paràmetre
        /// </summary>
        /// <param name="id">Identificador de l'Empleat</param>
        /// <returns>True si s'ha eliminat i false si no es pot eliminar</returns>
        public Boolean Delete(int id)
        {
            CloudConnection db = new CloudConnection();
            var conn = db.GetConnection();

            Boolean del = false;

            var cmd = new NpgsqlCommand("DELETE FROM emp WHERE emp_no =" +id, conn);
            
            if (cmd.ExecuteNonQuery() != 0)  del = true;
            conn.Close();
            return del;
        }

    }
}