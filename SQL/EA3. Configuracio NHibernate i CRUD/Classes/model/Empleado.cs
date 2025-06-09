using System;

namespace cat.itb.M6UF2EA3.model
{
    public class Empleado
    {
        public virtual int Id { get; set; }
        public virtual int Empno { get; set; }
        public virtual string Apellido { get; set; }
        public virtual string Oficio { get; set; }
        public virtual int Dir { get; set; }
        public virtual DateTime Fechaalt { get; set; }
        public virtual int Salario { get; set; }
        public virtual int Comision { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}