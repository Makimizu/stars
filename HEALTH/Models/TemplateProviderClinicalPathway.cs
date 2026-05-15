namespace HEALTH.Models
{
    public class TemplateProviderClinicalPathway
    {
        public string KODE_PROVIDER { get; set; }
        public string KODE_DIAGNOSA { get; set; }
        public string DIAGNOSA { get; set; }
        public string TAHUN { get; set; }
        public string LAMA_RAWAT { get; set; }
        public string KODE_TARIF { get; set; }
        public string KODE_KAMAR { get; set; }
        public string KELAS_KAMAR { get; set; }
        public decimal BIAYA_KAMAR { get; set; }
        public decimal TOTAL_BIAYA_CP { get; set; }
        public string PATH_DOCUMENT { get; set; }
    }
}