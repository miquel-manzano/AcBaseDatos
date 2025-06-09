namespace cat.itb.M6NF2Prac.models
{
    public class Salesperson
    {
        public virtual int Id { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Job { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual decimal Salary { get; set; }
        public virtual decimal? Commission { get; set; }
        public virtual string Department { get; set; }
        public virtual ISet<Product> Products { get; set; } = new HashSet<Product>();
    }
}
