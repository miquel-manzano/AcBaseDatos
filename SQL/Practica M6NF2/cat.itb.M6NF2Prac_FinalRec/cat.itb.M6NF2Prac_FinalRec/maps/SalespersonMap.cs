using cat.itb.M6NF2Prac.models;
using FluentNHibernate.Mapping;

namespace cat.itb.M6NF2Prac.maps
{
    public class SalespersonMap : ClassMap<Salesperson>
    {
        public SalespersonMap()
        {
            Table("salesperson");
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Surname).Column("surname").Not.Nullable().Length(25);
            Map(x => x.Job).Column("job").Length(50);
            Map(x => x.StartDate).Column("startdate").Not.Nullable();
            Map(x => x.Salary).Column("salary").Precision(12).Scale(2);
            Map(x => x.Commission).Column("commission").Precision(12).Scale(2).Nullable();
            Map(x => x.Department).Column("dep").Not.Nullable().Length(25);

            HasMany(x => x.Products)
                .KeyColumn("salesp")
                .Inverse()
                .AsSet();
        }
    }
}
