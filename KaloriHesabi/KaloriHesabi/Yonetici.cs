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
    public partial class Yonetici : Form
    {
        SqlConnection baglan = new SqlConnection("Server=.;Database=Kalori;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        public Yonetici()
        {
            InitializeComponent();
        }

        private void Yonetici_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 a = new Form1();
            a.Show();
        }
        protected void DatagridYenile()
        {
            baglan.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("Select * from tblBilgiler ", baglan);
            baglan.Close();
            adptr.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }
        protected void DatagridYenile2()
        {
            baglan.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("Select * from tblYonetici ", baglan);
            baglan.Close();
            adptr.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string kAdi = txtKadi.Text;
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
                    komut = new SqlCommand("Insert Into tblBilgiler Values('"+kAdi+ "','"+sifre+ "','"+adi+ "','"+soyadi+ "','"+kilo+ "','"+boy+ "','"+yas+ "','"+cinsiyet+"','"+egzersiz+"')", baglan);
                    komut.ExecuteNonQuery();
                }
                else
                {
                int cinsiyet = 2;
                komut = new SqlCommand("Insert Into tblBilgiler Values('" + kAdi + "','" + sifre + "','" + adi + "','" + soyadi + "','" + kilo + "','" + boy + "','" + yas + "','" + cinsiyet + "','" + egzersiz + "')", baglan);
                komut.ExecuteNonQuery();
                }
                baglan.Close();
            DatagridYenile();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Yonetici2 asd = new Yonetici2();
            asd.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label21.Text = label21.Text.Substring(1) + label21.Text.Substring(0, 1);
        }

        private void cmbkisi_SelectedIndexChanged(object sender, EventArgs e)
        {

          
        }
        

        private void cmbEgzersiz_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand kmt = new SqlCommand("DELETE  tblBilgiler where uye_id=" + dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
            kmt.ExecuteNonQuery();
            baglan.Close();
            DatagridYenile();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string kAdi = txtKadi.Text;
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
                komut = new SqlCommand("Update tblBilgiler set kAdi='" + kAdi + "',sifre='" + sifre + "',adi='" + adi + "',soyadi='" + soyadi + "',kilo='" + kilo + "',boy='" + boy + "',yas='" + yas + "',cinsiyet='" + cinsiyet + "',egzersiz='" + egzersiz + "'where uye_id=" + dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
                komut.ExecuteNonQuery();
                
            }
            else
            {
                int cinsiyet = 2;
                komut = new SqlCommand("Update tblBilgiler set kAdi='" + kAdi + "',sifre='" + sifre + "',adi='" + adi + "',soyadi='" + soyadi + "',kilo='" + kilo + "',boy='" + boy + "',yas='" + yas + "',cinsiyet='" + cinsiyet + "',egzersiz='" + egzersiz + "'where uye_id="+dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
                komut.ExecuteNonQuery();
            }
            baglan.Close();
            DatagridYenile();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try {
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
            try {
                txtYonetKadi.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["kAdi"].Value.ToString();
                txtYonetSifre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifre"].Value.ToString(); }
            catch { }
            }

        private void btnYonetEkle_Click(object sender, EventArgs e)
        {
            string kAdi = txtYonetKadi.Text;
            string sifre = txtYonetSifre.Text;

            baglan.Open();
    
                komut = new SqlCommand("Insert Into tblYonetici Values('" + kAdi + "','" + sifre + "')", baglan);
                komut.ExecuteNonQuery();
            
            baglan.Close();
            DatagridYenile2();
        }

        private void btnYonetSil_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand kmt = new SqlCommand("DELETE  tblYonetici where uye_id=" + dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
            kmt.ExecuteNonQuery();
            baglan.Close();
            DatagridYenile2();
        }

        private void btnYonetGuncelle_Click(object sender, EventArgs e)
        {
            string kAdi = txtYonetKadi.Text;
            string sifre = txtYonetSifre.Text;  
            baglan.Open();              
                komut = new SqlCommand("Update tblYonetici set kAdi='" + kAdi + "',sifre='" + sifre + "'where uye_id=" + dataGridView1.CurrentRow.Cells["uye_id"].Value.ToString(), baglan);
                komut.ExecuteNonQuery();
            baglan.Close();
            DatagridYenile2();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            
            DatagridYenile2();
            txtKadi.Text = "";
            txtSifre.Text = "";
            txtAdi.Text = "";
            txtSoyadi.Text = "";
            txtBoy.Text = "";
            txtKilo.Text = "";
            txtYas.Text = "";
            rbKadin.Checked = false;
            rbErkek.Checked = false;
            cmbEgzersiz.SelectedIndex = 0;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 asd = new Form1();
            asd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DatagridYenile();
            txtYonetKadi.Text = "";
            txtYonetSifre.Text = "";
        }
    }
    }

        
    

