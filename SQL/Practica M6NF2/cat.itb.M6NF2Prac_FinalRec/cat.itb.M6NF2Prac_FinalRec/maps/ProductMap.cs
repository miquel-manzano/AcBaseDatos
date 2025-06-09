using cat.itb.M6NF2Prac.models;
using FluentNHibernate.Mapping;

namespace cat.itb.M6NF2Prac.maps
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("product");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Code).Column("code").Not.Nullable();
            Map(x => x.Description).Column("description").Not.Nullable().Length(30);
            Map(x => x.CurrentStock).Column("currentstock");
            Map(x => x.MinStock).Column("minstock");
            Map(x => x.Price).Column("price").Precision(8).Scale(2);

            References(x => x.Salesperson).Column("salesp").Not.LazyLoad();
            HasOne(x => x.Provider).PropertyRef(x => x.Product).Cascade.All().Not.LazyLoad();

            HasMany(x => x.Orders)
                .KeyColumn("product")
                .Inverse()
                .AsSet();
        }
    }
}
