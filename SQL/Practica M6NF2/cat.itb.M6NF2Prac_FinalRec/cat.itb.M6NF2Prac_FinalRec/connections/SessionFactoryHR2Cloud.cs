using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using cat.itb.M6NF2Prac.models;

namespace UF2_test.connections
{
    public class SessionFactoryHR2Cloud
    {
        private static string ConnectionString = "Server=postgresql-miquel.alwaysdata.net;Port=5432;Database=miquel_m6nf2;User Id=miquel_admin;Password=Sjo2025!;";
        private static ISessionFactory _session;

        public static ISessionFactory CreateSession()
        {
            if (_session != null) return _session;

            IPersistenceConfigurer configDb = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);
            FluentConfiguration configMap = Fluently.Configure().Database(configDb)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Client>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Order>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Product>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Provider>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Salesperson>());

            _session = configMap.BuildSessionFactory();

            return _session;
        }

        public static ISession Open()
        {
            return CreateSession().OpenSession();
        }
    }
}
