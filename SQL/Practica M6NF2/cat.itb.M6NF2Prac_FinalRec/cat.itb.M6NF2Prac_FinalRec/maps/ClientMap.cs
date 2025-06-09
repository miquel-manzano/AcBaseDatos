using cat.itb.M6NF2Prac.models;
using FluentNHibernate.Mapping;

namespace cat.itb.M6NF2Prac.maps
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table("client");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Code).Column("code").Not.Nullable();
            Map(x => x.Name).Column("name").Not.Nullable().Length(30);
            Map(x => x.Credit).Column("credit").Precision(9).Scale(2);

            HasMany(x => x.Orders)
                .KeyColumn("client")
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .AsSet();
        }
    }
}
