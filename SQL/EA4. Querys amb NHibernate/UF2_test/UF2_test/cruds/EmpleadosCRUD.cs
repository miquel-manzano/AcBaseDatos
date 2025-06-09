using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6UF2EA3.model;
using NHibernate;
using NHibernate.Criterion;
using UF2_test.connections;

namespace UF2_test.cruds
{
    public class EmpleadosCRUD
    {
        public IList<Empleado> SelectAllUsingCriteria()
        {
            var session = SessionFactoryHR2Cloud.Open();
            ICriteria crit = session.CreateCriteria<Empleado>();
            IList<Empleado> emps = crit.List<Empleado>();
            session.Close();
            return emps;
        }

        public IList<Empleado> SelectBySalaryBiggerThanUsingCriteria(Double sal)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IList<Empleado> emps = session.CreateCriteria<Empleado>()
                .Add(Restrictions.Gt("Salario", sal)).List<Empleado>();
            session.Close();
            return emps;
        }

        public IList<Empleado> SelectByJobSortingBySalaryUsingQueryOver(string job)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IList<Empleado> emps = session.QueryOver<Empleado>()
                .Where(c => c.Oficio == job)
                .OrderBy(c => c.Salario).Desc
                .List<Empleado>();
            session.Close();
            return emps;
        }

        public IList<Empleado> SelectBySurnameStartsWithUsingHql(string letter)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IQuery query = session.CreateQuery($"select c from Empleado c where c.Apellido like '{letter}%'");
            IList<Empleado> emps = query.List<Empleado>();
            session.Close();
            return emps;
        }

        public IList<string> SelectBySalaryBetweenAndSortAscUsingQueryOver(Double sal1, Double sal2)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IList<string> surnames = session.QueryOver<Empleado>()
                .WhereRestrictionOn(c => c.Salario).IsBetween(sal1).And(sal2)
                .Select(c => c.Apellido)
                .OrderBy(c => c.Apellido).Asc
                .List<string>();
            session.Close();
            return surnames;
        }

        public IList<object[]> SelectByJobAndSalaryBiggerThan(string job, Double sal)
        {
            var session = SessionFactoryHR2Cloud.Open();
            IList<object[]> dadesEmps = session.QueryOver<Empleado>()
                .Where(c => c.Oficio == job && c.Salario > sal)
                .SelectList(list => list
                    .Select(c => c.Apellido)
                    .Select(c => c.Salario))
                .List<object[]>();
            session.Close();
            return dadesEmps;
        }

        public Empleado SelectByBiggestSalaryUsingSubqueriesQueryOver()
        {
            var session = SessionFactoryHR2Cloud.Open();
            QueryOver<Empleado> maxSal = QueryOver.Of<Empleado>()
                .SelectList(p => p.SelectMax(c => c.Salario));
            Empleado emp = session.QueryOver<Empleado>()
                .WithSubquery.WhereProperty(c => c.Salario)
                .Eq(maxSal).SingleOrDefault();
            return emp;
        }
    }
}
