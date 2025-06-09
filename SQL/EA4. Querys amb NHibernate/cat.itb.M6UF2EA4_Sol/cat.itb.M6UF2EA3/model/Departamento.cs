using System.Collections.Generic;

namespace cat.itb.M6UF2EA3.model
{
    public class Departamento
    {
        public virtual int Id { get; set; }
        public virtual string Dnombre { get; set; }
        public virtual string Loc { get; set; }
        public virtual IList<Empleado> Empleados { get; set; } 
    }
}