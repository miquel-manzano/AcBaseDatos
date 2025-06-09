using System.Collections.Generic;
using cat.itb.M6UF2EA3.connections;
using cat.itb.M6UF2EA3.model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;


namespace cat.itb.M6UF2EA3.cruds
{
    public class DepartamentosCRUD
    {
        public IList<Departamento> SelectAllHql()
        {
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery("select c from Departamento c");
            IList<Departamento> deps = query.List<Departamento>();
            session.Close();
            return deps;
        }
        
        public Departamento SelectByNameHql(string name)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery("select c from Departamento c where c.Dnombre = '" + name + "'");
            Departamento dep = query.UniqueResult<Departamento>();
            session.Close();
            return dep;
        }
        public IList<Departamento> SelectByLocationUsingQueryOver(string loc1,string loc2 )
        {
            var session = SessionFactoryHR2Cloud.Open();
            var orRestriction = Restrictions.Or(
                Restrictions.Where<Departamento>(dep => dep.Loc == loc1),
                Restrictions.Where<Departamento>(dep => dep.Loc == loc2));
            var deps = session.QueryOver<Departamento>()
                .Where(orRestriction)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            session.Close(); 
            return deps;
        }
        
        public IList<Departamento> SelectByLocationUsingQueryOver2(string loc1,string loc2 )
        {
            var session = SessionFactoryHR2Cloud.Open();
            var deps = session.QueryOver<Departamento>()
                .WhereRestrictionOn(dep => dep.Loc).IsIn(new[] {loc1,loc2})
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            session.Close(); 
            return deps;
        }
    }
}