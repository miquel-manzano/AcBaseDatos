namespace cat.itb.M6NF2Prac.models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual int Code { get; set; }
        public virtual string Description { get; set; }
        public virtual int CurrentStock { get; set; }
        public virtual int MinStock { get; set; }
        public virtual decimal Price { get; set; }
        public virtual Salesperson Salesperson { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual ISet<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
