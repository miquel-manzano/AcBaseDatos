using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using cat.itb.M6NF2Prac.models;
using cat.itb.M6UF2EA3.connections;
using NHibernate;
using Npgsql;
using UF2_test.connections;

namespace cat.itb.M6NF2Prac_FinalRec.cruds
{
    public class SalespersonCRUD
    {
        public IList<Salesperson> SelectAll()
        {
            IList<Salesperson> salespersons;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                salespersons = (from e in session.Query<Salesperson>() select e).ToList();
                session.Close();
            }
            return salespersons;
        }

        public Salesperson SelectById(int id)
        {
            Salesperson salesperson;
            var session = SessionFactoryHR2Cloud.Open();
            salesperson = session.Get<Salesperson>(id);
            session.Close();
            return salesperson;
        }

        public void Insert(Salesperson salesperson)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(salesperson);
                    tx.Commit();
                    Console.WriteLine("Salesperson {0} inserted", salesperson.Surname);
                    session.Close();
                }
            }
        }

        public void Update(Salesperson salesperson)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Update(salesperson);
                tx.Commit();
                Console.WriteLine("Salesperson {0} actualitzat", salesperson.Surname);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error actualitzant salesperson : " + ex.Message);
            }

            session.Close();
        }

        public void Delete(Salesperson salesperson)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Delete(salesperson);
                tx.Commit();
                Console.WriteLine("Salesperson {0} esborrat", salesperson.Surname);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error esborrant salesperson : " + ex.Message);
            }

            session.Close();
        }

        public Salesperson SelectBySurname(string surename)
        {
            Salesperson salesperson;
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery($"select c from Salesperson c left join fetch c.Products where c.Surname = '{surename}'");
            salesperson = query.UniqueResult<Salesperson>();
            session.Close();

            return salesperson;
        }

        // ADO
        public void InsertADO(List<Salesperson> salespersons)
        {
            StoreCloudConnection db = new StoreCloudConnection();
            var conn = db.GetConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO salesperson (surname, job, startdate, salary, commission, dep) VALUES (@surname, @job, @startdate, @salary, @commission, @dep)", conn);

            foreach (var salesperson in salespersons)
            {
                cmd.Parameters.AddWithValue("surname", salesperson.Surname);
                cmd.Parameters.AddWithValue("job", salesperson.Job);
                cmd.Parameters.AddWithValue("startdate", salesperson.StartDate);
                cmd.Parameters.AddWithValue("salary", salesperson.Salary);
                cmd.Parameters.AddWithValue("commission", salesperson.Commission is null ? 0 : salesperson.Commission);
                cmd.Parameters.AddWithValue("dep", salesperson.Department);
                cmd.Prepare();

                try
                {
                    cmd.ExecuteNonQuery();

                    Console.WriteLine($"Salesperson with Id {salesperson.Id} and surname {salesperson.Surname} added");
                }
                catch
                {
                    Console.WriteLine($"Couldn't add Salesperson with Surename {salesperson.Surname}");
                }

                cmd.Parameters.Clear();
            }

            conn.Close();
        }
    }
}
