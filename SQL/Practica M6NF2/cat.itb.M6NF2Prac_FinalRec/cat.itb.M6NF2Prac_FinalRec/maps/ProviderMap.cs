using cat.itb.M6NF2Prac.models;
using FluentNHibernate.Mapping;

namespace cat.itb.M6NF2Prac.maps
{
    public class ProviderMap : ClassMap<Provider>
    {
        public ProviderMap()
        {
            Table("provider");
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name).Column("name").Not.Nullable().Length(45);
            Map(x => x.Address).Column("address").Not.Nullable().Length(40);
            Map(x => x.City).Column("city").Not.Nullable().Length(30);
            Map(x => x.StateCode).Column("stcode").Length(2);
            Map(x => x.ZipCode).Column("zipcode").Not.Nullable().Length(10);
            Map(x => x.Area).Column("area").Not.Nullable();
            Map(x => x.Phone).Column("phone").Length(10);
            Map(x => x.Amount).Column("amount");
            Map(x => x.Credit).Column("credit").Precision(9).Scale(2);
            Map(x => x.Remark).Column("remark").CustomType("StringClob");

            References(x => x.Product).Column("product").Unique().Not.Nullable().Not.LazyLoad();
        }
    }
}
