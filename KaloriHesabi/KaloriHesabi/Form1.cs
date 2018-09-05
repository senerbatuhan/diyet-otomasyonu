using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KaloriHesabi
{
    public partial class Form1 : Form
    {
        SqlConnection baglan = new SqlConnection("Server=.; Database=Kalori; Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }
        public static string kAdi;
        private void button3_Click(object sender, EventArgs e)
        {
            string adi = txtkAdi.Text.Trim();
            string sifre = textBox2.Text.Trim();
            komut = new SqlCommand("select count (*) from tblYonetici where kAdi = @adi and sifre = @sifre", baglan);
            komut.Parameters.AddWithValue("@adi", adi);
            komut.Parameters.AddWithValue("@sifre", sifre);
            baglan.Open();
            int sayi = int.Parse(komut.ExecuteScalar().ToString());
            baglan.Close();

            
            if (sayi == 0)
                MessageBox.Show("Yönetici Girişi Hatalı. Tekrar Deneyin!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.Hide();
                Yonetici asd = new Yonetici();
                asd.Show();
            }
            
                }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kAdi1= txtkAdi.Text.Trim();
            kAdi = kAdi1;
            string adi = txtkAdi.Text.Trim();
            string sifre = textBox2.Text.Trim();
            komut = new SqlCommand("select count (*) from tblBilgiler where kAdi = @adi and sifre = @sifre", baglan);
            komut.Parameters.AddWithValue("@adi", adi);
            komut.Parameters.AddWithValue("@sifre", sifre);
            baglan.Open();
            int sayi = int.Parse(komut.ExecuteScalar().ToString());
            baglan.Close();
            if (sayi == 0)
                MessageBox.Show("Kullanıcı Girişi Hatalı. Tekrar Deneyin!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show("Başarıyla Giriş Yaptınız Sayın "+txtkAdi.Text, "GİRİŞ BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Hesapla asd = new Hesapla();
                asd.Show();
            }
        }
    }
}
