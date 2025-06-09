using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6UF2EA3.model;
using UF2_test.connections;

namespace UF2_test.cruds
{
    public class DepartamentosCRUD
    {
        public IList<Departamento> SelectAll()
        {
            IList<Departamento> departments;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                departments = (from d in session.Query<Departamento>() select d).ToList();
                session.Close();
            }
            return departments;
        }

        public void Insert(Departamento departament)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();
            session.Save(departament);
            tx.Commit();
            Console.WriteLine("Departament {0} inserted", departament.Dnombre);
            session.Close();
        }

        public Departamento SelectById(int pId)
        {
            Departamento departament;
            var session = SessionFactoryHR2Cloud.Open();
            departament = session.Get<Departamento>(pId);
            session.Close();
            return departament;
        }

        public void Update(Departamento departament)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Update(departament);
                tx.Commit();
                Console.WriteLine("Departament {0} updated", departament.Dnombre);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error updating departament : " + ex.Message);
            }

            session.Close();
        }
        public void Delete(Departamento dept)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(dept);
                        tx.Commit();
                        Console.WriteLine("Department {0} deleted", dept.Dnombre);
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }

                        throw new Exception("Error deleting department : " + ex.Message);
                    }
                }

                session.Close();
            }
        }
    }
}
