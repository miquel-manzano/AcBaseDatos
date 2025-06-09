using cat.itb.M6NF2Prac.models;
using FluentNHibernate.Mapping;

namespace cat.itb.M6NF2Prac.maps
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("orderprod");
            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.Product).Column("product").Not.LazyLoad();
            References(x => x.Client).Column("client").Not.LazyLoad();

            Map(x => x.OrderDate).Column("orderdate").Not.Nullable();
            Map(x => x.Amount).Column("amount").Not.Nullable();
            Map(x => x.DeliveryDate).Column("deliverydate").Nullable();
            Map(x => x.Cost).Column("cost").Precision(12).Scale(2);
        }
    }
}
