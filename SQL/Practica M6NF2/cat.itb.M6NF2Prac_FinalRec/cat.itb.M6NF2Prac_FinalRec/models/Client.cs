using NHibernate.Criterion;

namespace cat.itb.M6NF2Prac.models
{
    public class Client
    {
        public virtual int Id { get; set; }
        public virtual int Code { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Credit { get; set; }
        public virtual ISet<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
