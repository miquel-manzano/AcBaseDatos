using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF2_test.model
{
    public class Client
    {
        public int clientCod { get; set; }
        public string nom { get; set; }
        public string adreca { get; set; }
        public string ciutat { get; set; }
        public string estat { get; set; }
        public string codiPostal { get; set; }
        public int Area { get; set; }
        public string telefon { get; set; }
        public int? reprCod { get; set; }
        public double limitCredit { get; set; }
        public string observacions { get; set; }
    }
}
