namespace LogisticsPanel.Models
{
    public class Arac
    {
        public int Id { get; set; }
        public string Plaka { get; set; }
        public string SurucuAdi { get; set; }
        public decimal KapasiteKg { get; set; }
        public string Durum { get; set; }
        public DateTime SonServisTarihi { get; set; }

        public ICollection<Gonderi> Gonderiler { get; set; }
    }
}
