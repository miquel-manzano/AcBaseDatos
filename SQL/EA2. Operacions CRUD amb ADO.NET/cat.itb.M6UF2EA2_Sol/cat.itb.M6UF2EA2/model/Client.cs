namespace cat.itb.M6UF2EA2.model
{
    /// <summary>
    /// Classe on s'administra l'objecte de tipus client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Int amb l'id del client
        /// </summary>
        public int clientCod { get; set; }
        
        /// <summary>
        /// String amb el nom del client
        /// </summary>
        public string nom { get; set; }
        
        /// <summary>
        /// String amb l'adreça del client
        /// </summary>
        public string adreca { get; set; }
        
        /// <summary>
        /// String amb el nom de la ciutat del client
        /// </summary>
        public string ciutat { get; set; }
        
        /// <summary>
        /// String amb el nom de l'estat del client
        /// </summary>
        public string estat { get; set; }
        
        /// <summary>
        /// String amb el codi postal de la població on resideix el client
        /// </summary>
        public string codiPostal { get; set; }
        
        /// <summary>
        /// Int amb l'area del client
        /// </summary>
        public int Area { get; set; }
        
        /// <summary>
        /// String amb el número de telèfon del client
        /// </summary>
        public string telefon { get; set; }
        
        /// <summary>
        /// Int amb el codi del treballador que li pertoca
        /// </summary>
        public int? reprCod { get; set; }
        
        /// <summary>
        /// Double amb el limit de crèdit del client
        /// </summary>
        public double limitCredit { get; set; }
        
        /// <summary>
        /// String amb les observacions del client
        /// </summary>
        public string observacions { get; set; }
    }
}