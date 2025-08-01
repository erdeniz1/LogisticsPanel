namespace LogisticsPanel.Models
{
    public class Rota
    {
        public int Id { get; set; }
        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public decimal MesafeKm { get; set; }
        public TimeSpan TahminiSure { get; set; }

        public int? AracId { get; set; }
        public Arac Arac { get; set; }

        public ICollection<Gonderi> Gonderiler { get; set; }
    }
}
