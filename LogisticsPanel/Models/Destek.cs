namespace LogisticsPanel.Models
{
    public class Destek
    {
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public string Konu { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string Durum { get; set; } = "Beklemede";
        public string Mesaj { get; set; }
        public int? MusteriId { get; set; }
        public Musteri Musteri { get; set; }
    }
}
