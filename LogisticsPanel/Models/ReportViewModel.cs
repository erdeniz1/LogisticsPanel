namespace LogisticsPanel.ViewModels
{
    public class ReportViewModel
    {
        public List<GonderiGunlukDto> GunlukGonderiler { get; set; }
        public EnCokTeslimDto EnCokTeslimArac { get; set; }
        public double BasariOrani { get; set; }
    }

    public class GonderiGunlukDto
    {
        public string Tarih { get; set; }
        public int Sayisi { get; set; }
    }

    public class EnCokTeslimDto
    {
        public string Arac { get; set; }
        public int TeslimatSayisi { get; set; }
    }
}
