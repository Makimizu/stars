namespace HEALTH.Models
{
    public class TemplateProviderClinicalPathwayProv
    {
        public string KODE_PROVIDER { get; set; }
        public string NAMA_PROVIDER { get; set; }

        public string KODE_DIAGNOSA { get; set; }

        public string KODE_TARIF { get; set; }
        public string DIAGNOSA { get; set; }
        public int LOS { get; set; }     
        public decimal KELAS_1 { get; set; }
        public decimal KELAS_2 { get; set; }
        public decimal KELAS_3 { get; set; }         
        public decimal KELAS_UTAMA { get; set; }
        public decimal VIP { get; set; }
        public decimal VVIP { get; set; }
        public decimal SUPER_VIP { get; set; }

        public string PATH_DOCUMENT { get; set; }
    }
}