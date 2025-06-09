namespace cat.itb.M6NF2Prac.models
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Client Client { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int Amount { get; set; }
        public virtual DateTime? DeliveryDate { get; set; }
        public virtual decimal Cost { get; set; }
    }
}
