namespace cat.itb.M6NF2Prac.models
{
    public class Provider
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string StateCode { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int Area { get; set; }
        public virtual string Phone { get; set; }
        public virtual Product Product { get; set; }
        public virtual int Amount { get; set; }
        public virtual decimal Credit { get; set; }
        public virtual string Remark { get; set; }
    }
}
