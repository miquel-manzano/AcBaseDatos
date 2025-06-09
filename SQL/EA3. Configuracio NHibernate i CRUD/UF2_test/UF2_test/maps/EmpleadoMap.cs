using cat.itb.M6UF2EA3.model;
using FluentNHibernate.Mapping;

namespace cat.itb.M6UF2EA3.maps
{
    public class EmpleadoMap : ClassMap<Empleado>
    {
        public EmpleadoMap()
        {
            Table("empleados");
            Id(x => x.Id);
            Map(x => x.Empno).Column("empno");
            Map(x => x.Apellido).Column("apellido");
            Map(x => x.Oficio).Column("oficio");
            Map(x => x.Dir).Column("dir");
            Map(x => x.Fechaalt).Column("fechaalt");
            Map(x => x.Salario).Column("salario");
            Map(x => x.Comision).Column("comision");
            References(x => x.Departamento)
                .Column("deptno")
                .Not.LazyLoad()
                .Fetch.Join();
        }
    }
}