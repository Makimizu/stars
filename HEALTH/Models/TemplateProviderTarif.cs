namespace HEALTH.Models
{
    public class TemplateProviderTarif
    {
        public string KODE_PROVIDER { get; set; }
        public string KODE_SUB_TARIF { get; set; }
        public string JENIS_TARIF { get; set; }
        public string KELAS_KAMAR { get; set; }
        public string KETERANGAN { get; set; }
        public decimal TARIF { get; set; }
        public string TANGGAL_BERLAKU { get; set; }
    }
}