using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6NF2Prac.models;
using NHibernate;
using UF2_test.connections;
using static System.Collections.Specialized.BitVector32;


namespace cat.itb.M6NF2Prac_FinalRec.cruds
{
    public class OrderCRUD
    {
        public IList<Order> SelectAll()
        {
            IList<Order> orders;
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                orders = (from e in session.Query<Order>() select e).ToList();
                session.Close();
            }
            return orders;
        }

        public IList<Order> SelectAllHQL()
        {
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery("select c from Order c");
            IList<Order> ords = query.List<Order>();
            session.Close();
            return ords;
        }

        public Order SelectById(int id)
        {
            Order order;
            var session = SessionFactoryHR2Cloud.Open();
            order = session.Get<Order>(id);
            session.Close();
            return order;
        }

        public void Insert(Order order)
        {
            using (var session = SessionFactoryHR2Cloud.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(order);
                    tx.Commit();
                    Console.WriteLine("Order {0} inserted", order.Id);
                    session.Close();
                }
            }
        }

        public void Update(Order order)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Update(order);
                tx.Commit();
                Console.WriteLine("Order {0} actualitzat", order.Id);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error actualitzant order : " + ex.Message);
            }

            session.Close();
        }

        public void Delete(Order order)
        {
            var session = SessionFactoryHR2Cloud.Open();
            var tx = session.BeginTransaction();

            try
            {
                session.Delete(order);
                tx.Commit();
                Console.WriteLine("Order {0} esborrat", order.Id);
            }
            catch (Exception ex)
            {
                if (!tx.WasCommitted) tx.Rollback();
                throw new Exception("Error esborrant order : " + ex.Message);
            }

            session.Close();
        }

        public IList<Order> SelectByCostHigherThan(int amount, decimal cost)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IList<Order> orders = session.CreateCriteria<Order>()
                .Add(NHibernate.Criterion.Restrictions.Gt("Cost", cost)&& NHibernate.Criterion.Restrictions.Eq("Amount", amount)).List<Order>();
            session.Close();

            return orders;
        }

        public Order SelectLowAmount()
        {
            var sessions = SessionFactoryHR2Cloud.Open();
            NHibernate.Criterion.QueryOver<Order> maxCost = NHibernate.Criterion.QueryOver.Of<Order>()
                .SelectList(p => p.SelectMax(c => c.Cost));
            Order order = sessions.QueryOver<Order>()
                .WithSubquery.WhereProperty(c => c.Cost)
                .Eq(maxCost).SingleOrDefault();
            return order;
        }
    }
}
