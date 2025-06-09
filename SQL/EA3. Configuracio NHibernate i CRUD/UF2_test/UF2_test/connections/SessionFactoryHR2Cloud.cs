using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using cat.itb.M6UF2EA3.model;

namespace UF2_test.connections
{
    public class SessionFactoryHR2Cloud
    {
        private static string ConnectionString = "Server=postgresql-miquel.alwaysdata.net;Port=5432;Database=miquel_ea3;User Id=miquel_admin;Password=Sjo2025!;";
        private static ISessionFactory _session;

        public static ISessionFactory CreateSession()
        {
            if (_session != null) return _session;

            IPersistenceConfigurer configDb = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);
            FluentConfiguration configMap = Fluently.Configure().Database(configDb)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Departamento>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Empleado>());

            _session = configMap.BuildSessionFactory();

            return _session;
        }

        public static ISession Open()
        {
            return CreateSession().OpenSession();
        }
    }
}
