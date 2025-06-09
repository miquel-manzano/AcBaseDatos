using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF2_test.model
{
    public class Empleat
    {
        public int empNo { get; set; }
        public string cognom { get; set; }
        public string ofici { get; set; }
        public int? cap { get; set; }
        public DateTime dataAlta { get; set; }
        public int salari { get; set; }
        public int? comissio { get; set; }
        public int deptNo { get; set; }
    }
}
