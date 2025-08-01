namespace LogisticsPanel.Models
{
    public class Gonderi
    {

        public int Id { get; set; }
        public string GonderiNo { get; set; }
        public int MusteriId { get; set; }
        public Musteri GonderenMusteri { get; set; }

        public string AliciAdresi { get; set; }
        public string Durum { get; set; }
        public DateTime TahminiTeslimTarihi { get; set; }

        public int? AtananAracId { get; set; }
        public Arac AtananArac { get; set; }
    }
}
