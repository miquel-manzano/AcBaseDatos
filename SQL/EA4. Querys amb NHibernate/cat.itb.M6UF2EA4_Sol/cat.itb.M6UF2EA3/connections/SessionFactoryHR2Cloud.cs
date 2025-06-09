using cat.itb.M6UF2EA3.model;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace cat.itb.M6UF2EA3.connections
{
    public class SessionFactoryHR2Cloud
    {
        private static string ConnectionString = "Server=postgresql-joancolomer.alwaysdata.net;Port=5432;Database=joancolomer_hr2;User Id=joancolomer;Password=itbPassITB;";
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