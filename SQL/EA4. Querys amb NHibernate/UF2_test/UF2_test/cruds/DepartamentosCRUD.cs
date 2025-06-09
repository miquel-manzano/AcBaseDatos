using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6UF2EA3.model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using UF2_test.connections;

namespace UF2_test.cruds
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
            IQuery query = session.CreateQuery($"select c from Departamento c where c.Dnombre = '{name}'");
            Departamento dep = query.UniqueResult<Departamento>();
            session.Close();
            return dep;
        }
        public IList<Departamento> SelectByLocationUsingQueryOver(string loc1, string loc2)
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

    }
}
