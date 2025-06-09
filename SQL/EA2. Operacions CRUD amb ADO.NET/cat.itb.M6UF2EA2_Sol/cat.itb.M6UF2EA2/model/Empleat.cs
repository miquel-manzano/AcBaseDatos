using System;

namespace cat.itb.M6UF2EA2.model
{
    /// <summary>
    /// Clase on s'administren els objectes Empleat
    /// </summary>
    public class Empleat
    {
        /// <summary>
        /// Int amb l'id de l'Empleat
        /// </summary>
        public int empNo { get; set; }
        
        /// <summary>
        /// String amb el cognom de l'Empleat 
        /// </summary>
        public string cognom { get; set; }
        
        /// <summary>
        /// String amb l'ofici de l'Empleat
        /// </summary>
        public string ofici { get; set; }
        
        /// <summary>
        /// Int amb l'id del cap de l'Empleat
        /// </summary>
        public int?  cap { get; set; }
        
        /// <summary>
        /// DateTime amb la data d'alta de l'Empleat
        /// </summary>
        public DateTime dataAlta { get; set; }
        
        /// <summary>
        /// Int amb el salari de l'Empleat
        /// </summary>
        public int salari { get; set; }
        
        /// <summary>
        /// Int amb la comissió de l'Empleat
        /// </summary>
        public int? comissio { get; set; }
        
        /// <summary>
        /// Int amb el número de departament de l'Empleat
        /// </summary>
        public int deptNo { get; set; }
    }
}