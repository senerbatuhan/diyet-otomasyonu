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
    public partial class Hesapla : Form
    {
        SqlConnection baglan = new SqlConnection("Server=.;Database=Kalori;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        public Hesapla()
        {
            InitializeComponent();
        }

        private void Hesapla_Load(object sender, EventArgs e)
        {
            string kAdi = Form1.kAdi;
            int kilo, boy, yas, cinsiyet, egzersiz;
            baglan.Open();
            SqlCommand SQLKomut = new SqlCommand("select kilo,boy,yas,cinsiyet,egzersiz from tblBilgiler where kAdi='" + kAdi + "'", baglan);
            SqlDataReader sdr = SQLKomut.ExecuteReader();

            while (sdr.Read())
            {
                kilo = Int32.Parse(sdr["kilo"].ToString());
                boy = Int32.Parse(sdr["boy"].ToString());
                yas = Int32.Parse(sdr["yas"].ToString());
                cinsiyet = Int32.Parse(sdr["cinsiyet"].ToString());
                egzersiz = Int32.Parse(sdr["egzersiz"].ToString());
                if (cinsiyet == 1)
                {
                    if (egzersiz == 1)
                    {
                        label5.Text = ((66 + (kilo * 13.7) + (5 * boy) - (6.8 * yas)) * 1.2).ToString() + " kcal'dir.";
                    }
                    else if (egzersiz == 2)
                    {
                        label5.Text = ((66 + (kilo * 13.7) + (5 * boy) - (6.8 * yas)) * 1.375).ToString() + " kcal'dir.";

                    }
                    else if (egzersiz == 3)
                    {
                        label5.Text = ((66 + (kilo * 13.7) + (5 * boy) - (6.8 * yas)) * 1.55).ToString() + " kcal'dir.";
                    }
                    else if (egzersiz == 4)
                    {
                        label5.Text = ((66 + (kilo * 13.7) + (5 * boy) - (6.8 * yas)) * 1.725).ToString() + " kcal'dir.";

                    }
                    else if (egzersiz == 5)
                    {
                        label5.Text = ((66 + (kilo * 13.7) + (5 * boy) - (6.8 * yas)) * 1.9).ToString() + " kcal'dir.";

                    }
                }
                else
                { label5.Text = (655 + (kilo * 9.6) + (1.8 * boy) - (4.7 * yas)).ToString() + " kcal'dir."; }

            }
            baglan.Close();
            komut = new SqlCommand("Select distinct Tur,Tur_id from tblYiyecekler", baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbTur.DataSource = dt;
            cmbTur.ValueMember = "Tur_id";
            cmbTur.DisplayMember = "Tur";
            int tur = int.Parse(cmbTur.SelectedValue.ToString().Trim());
            YiyecekDoldur(tur);
            baglan.Open();
            SqlCommand kmt = new SqlCommand("Select adi,soyadi from tblBilgiler where kAdi='"+kAdi+"'",baglan);
            SqlDataReader rd = kmt.ExecuteReader();
            while (rd.Read())
            {
                lblHos.Text = "Hoşgeldiniz Sayın "+rd["adi"].ToString()+" "+rd["soyadi"].ToString();
            }
            baglan.Close();
        }
        void YiyecekDoldur(int tur)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from tblYiyecekler where Tur_id='" + tur + "'", baglan);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbYiyecek.DataSource = dt;
            cmbYiyecek.ValueMember = "Tur_id";
            cmbYiyecek.DisplayMember = "Yiyecek";
        }
        private void cmbTur_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fid;
            bool parseOK = Int32.TryParse(cmbTur.SelectedValue.ToString(), out fid);
            
            YiyecekDoldur(fid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bilgiler asd = new Bilgiler();
            asd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 asd = new Form1();
            asd.Show();
            
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            if (txtMiktar.Text == "")
            {
                MessageBox.Show("Lütfen miktarı giriniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (rbadet.Checked)
            {
                lbYiyecek.Items.Add(cmbYiyecek.Text + " - " + txtMiktar.Text + " " + rbadet.Text);
            }
            else
            {
                lbYiyecek.Items.Add(cmbYiyecek.Text + " - " + txtMiktar.Text + " " + rbgram.Text);

            }
        }

        private void btnHesapla_Click_1(object sender, EventArgs e)
        {
            if (lbYiyecek.Items.Count == 0)
            {
                MessageBox.Show("Lütfen önce yiyeceklerinizi seçiniz", "YİYECEK SEÇİNİZ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double toplam = 0;
                SqlDataAdapter adap;
                DataTable dt;
                for (int i = 0; i < lbYiyecek.Items.Count; i++)
                {

                    string list_eleman = lbYiyecek.Items[i].ToString();
                    string[] dizi = list_eleman.Split('-');
                    dt = new DataTable();
                    adap = new SqlDataAdapter("select * from tblYiyecekler where Yiyecek = '" + dizi[0].Trim() + "'", baglan);
                    adap.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        string str_miktar = dizi[1].Trim();
                        string[] dizi_miktar = str_miktar.Split(' ');

                        double kalori = double.Parse(item["Kalori"].ToString());
                        double miktar = double.Parse(item["Miktar"].ToString());
                        toplam += (kalori / miktar) * double.Parse(dizi_miktar[0]);
                    }
                }

                lblSonuc.Text = "Yediklerinizin toplam kalorisi " + toplam.ToString() + " cal'dir.";
            }
            }

        private void button3_Click_1(object sender, EventArgs e)
        {
            lbYiyecek.Items.Clear();
            lblSonuc.Text = " ";
            lblmiktar.Text = " ";
        }
    }
}
        
    

