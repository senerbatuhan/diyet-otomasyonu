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
    public partial class Bilgiler : Form
    {
        SqlConnection baglan = new SqlConnection("Server=.;Database=Kalori;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        public Bilgiler()
        {
            InitializeComponent();
        }
        protected void DatagridYenile()
        {
            string kAdi=Form1.kAdi;
            baglan.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("Select uye_id,kAdi,sifre,adi,soyadi,yas,kilo,boy,cinsiyet,egzersiz from tblBilgiler where kAdi='"+kAdi+"'", baglan);
            baglan.Close();
            adptr.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hesapla asd = new Hesapla();
            asd.Show();
            
        }

        private void Bilgiler_Load(object sender, EventArgs e)
        {
            DatagridYenile();

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cinsiyet"].Value.ToString() == "1")
                {
                    txtKadi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["kAdi"].Value.ToString();
                    txtSifre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifre"].Value.ToString();
                    txtAdi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["adi"].Value.ToString();
                    txtSoyadi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["soyadi"].Value.ToString();
                    txtBoy.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["boy"].Value.ToString();
                    txtKilo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["kilo"].Value.ToString();
                    txtYas.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["yas"].Value.ToString();
                    rbErkek.Checked = true;
                    cmbEgzersiz.SelectedIndex = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["egzersiz"].Value.ToString());


                }
                else
                {
                    txtKadi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["kAdi"].Value.ToString();
                    txtSifre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifre"].Value.ToString();
                    txtAdi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["adi"].Value.ToString();
                    txtSoyadi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["soyadi"].Value.ToString();
                    txtBoy.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["boy"].Value.ToString();
                    txtKilo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["kilo"].Value.ToString();
                    txtYas.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["yas"].Value.ToString();
                    rbKadin.Checked = true;
                    cmbEgzersiz.SelectedIndex = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["egzersiz"].Value.ToString());

                }

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string sifre = txtSifre.Text;
            string adi = txtAdi.Text;
            string soyadi = txtSoyadi.Text;
            string boy = txtBoy.Text;
            string kilo = txtKilo.Text;
            string yas = txtYas.Text;
            string egzersiz = cmbEgzersiz.SelectedIndex.ToString();
            baglan.Open();
            if (rbErkek.Checked)
            {
                int cinsiyet = 1;
                komut = new SqlCommand("Update tblBilgiler set sifre='" + sifre + "',adi='" + adi + "',soyadi='" + soyadi + "',kilo='" + kilo + "',boy='" + boy + "',yas='" + yas + "',cinsiyet='" + cinsiyet + "',egzersiz='" + egzersiz + "'where uye_id=" + dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
                komut.ExecuteNonQuery();

            }
            else
            {
                int cinsiyet = 2;
                komut = new SqlCommand("Update tblBilgiler set sifre='" + sifre + "',adi='" + adi + "',soyadi='" + soyadi + "',kilo='" + kilo + "',boy='" + boy + "',yas='" + yas + "',cinsiyet='" + cinsiyet + "',egzersiz='" + egzersiz + "'where uye_id=" + dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
                komut.ExecuteNonQuery();
            }
            baglan.Close();
            DatagridYenile();
        }
    }
}
