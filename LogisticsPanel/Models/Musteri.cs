﻿namespace LogisticsPanel.Models
{
    public class Musteri
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public ICollection<Gonderi> Gonderiler { get; set; }
    }
}
