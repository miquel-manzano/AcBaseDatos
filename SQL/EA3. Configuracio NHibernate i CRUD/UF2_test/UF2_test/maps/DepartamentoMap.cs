using cat.itb.M6UF2EA3.model;
using FluentNHibernate.Mapping;

namespace cat.itb.M6UF2EA3.maps
{
    public class DepartamentoMap : ClassMap<Departamento>
    {
        public DepartamentoMap()
        {
            Table("departamentos");
            Id(x => x.Id);
            Map(x => x.Dnombre).Column("dnombre");
            Map(x => x.Loc).Column("loc");
            HasMany(x => x.Empleados)
                .KeyColumn("deptno")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
        }
    }
}