﻿namespace LogisticsPanel.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; }  // Admin, Personel, Musteri

        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
