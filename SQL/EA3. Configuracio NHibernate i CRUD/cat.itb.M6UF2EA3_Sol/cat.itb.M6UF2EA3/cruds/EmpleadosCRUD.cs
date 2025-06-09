using System;
using System.Collections.Generic;
using System.Linq;
using cat.itb.M6UF2EA3.connections;
using cat.itb.M6UF2EA3.model;
using NHibernate;

namespace cat.itb.M6UF2EA3.cruds
{
    public class EmpleadosCRUD
    {
        public IList<Empleado> SelectAll()
        {
            IList<Empleado> emps;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                emps = (from e in session.Query<Empleado>() select e).ToList();
                session.Close();
            }
            return emps;
        }
        public void Insert(Empleado empleat)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(empleat);
                    tx.Commit();
                    Console.WriteLine("Employee {0} inserted", empleat.Apellido);
                    session.Close();
                }
            }
        }
        
        public Empleado SelectByEmpno(int empno)
        {
            Empleado emp;
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery("select c from Empleado c where c.Empno = " + empno);
            emp = query.UniqueResult<Empleado>();
            session.Close();

            return emp;
        }
        
        public void Update(Empleado empleat)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();
                
            try
            {
                session.Update(empleat);
                tx.Commit();
                Console.WriteLine("Empleat {0} actualitzat", empleat.Apellido);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error actualitzant empleat : " + ex.Message);
            }
            
            session.Close();
        }
        
        public Empleado SelectBySurname(string surname)
        {
            Empleado emp;
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery("select c from Empleado c where c.Apellido = '" + surname + "'");
            emp = query.UniqueResult<Empleado>();
            session.Close();

            return emp;
        }

        public void Delete(Empleado empleat)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Delete(empleat);
                tx.Commit();
                Console.WriteLine("Empleat {0} esborrat", empleat.Apellido);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error esborrant empleant : " + ex.Message);
            }

            session.Close();
        }
        
        public IList<Empleado> SelectBySalaryLowerThan(int salary)
        {
            IList<Empleado> empleats;
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery("select c from Empleado c where c.Salario < " + salary);
            empleats = query.List<Empleado>();
            session.Close();

            return empleats;
        }
        
        public Empleado SelectById(int id)
        {
            Empleado empleat;
            var session = SessionFactoryHR2Cloud.Open();
            empleat = session.Get<Empleado>(id);
            session.Close();
            return empleat;
        }
        
    }
}