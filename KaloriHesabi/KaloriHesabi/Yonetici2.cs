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
    public partial class Yonetici2 : Form
    {
        SqlConnection baglan = new SqlConnection("Server=.;Database=Kalori; Integrated Security = True;");
        SqlCommand komut = new SqlCommand();
        public Yonetici2()
        {
            InitializeComponent();
        }
        protected void DatagridYenile()
        {
            baglan.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("Select id,Tur,Yiyecek,Kalori,Miktar,Tur_id,Miktar_id from tblYiyecekler ", baglan);
           baglan.Close();
            adptr.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }
        private void Yonetici2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            DatagridYenile();
            SqlCommand komut = new SqlCommand("Select Yiyecek from tblYiyecekler ", baglan);
            baglan.Open();
            SqlDataReader reader = komut.ExecuteReader();
            AutoCompleteStringCollection tamamlama = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                tamamlama.Add(reader.GetString(0));
            }
            txtAra.AutoCompleteCustomSource = tamamlama;
            baglan.Close();
        }

     

        private void btnAra_Click(object sender, EventArgs e)
        { string yiyecek = txtAra.Text;
            /*SqlCommand komut = new SqlCommand("Select Yiyecek from tblYiyecek="+yiyecek+"'",baglan);
            SqlDataReader rd = komut.ExecuteReader();
            string veri = rd.GetString(0).ToString();*/
            baglan.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adptr = new SqlDataAdapter("Select id,Tur,Yiyecek,Kalori,Miktar,Tur_id from tblYiyecekler where Yiyecek='" + yiyecek + "'", baglan);
            baglan.Close();
            adptr.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand kmt = new SqlCommand("DELETE  tblYiyecekler where id=" + dataGridView1.CurrentRow.Cells["id"].Value.ToString(), baglan);
            kmt.ExecuteNonQuery();
            baglan.Close();
            DatagridYenile();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label21.Text = label21.Text.Substring(1) + label21.Text.Substring(0, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Yonetici asd = new Yonetici();
            asd.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string tur = cmbTur.SelectedItem.ToString();
            string yiyecek = txtYiyecek.Text;
            int kalori = Convert.ToInt32(txtKalori.Text);
            string tur_id = cmbTur.SelectedIndex.ToString();
            string miktar = txtMiktar.Text;
            baglan.Open();
            if (rbAdet.Checked)
            {
                SqlCommand komut = new SqlCommand("Insert Into tblYiyecekler(Tur,Yiyecek,Kalori,Miktar,Tur_id,Miktar_id) Values('" + tur + "','" + yiyecek + "','" + kalori + "','" + miktar + "','" + tur_id + "','"+"1"+"')", baglan);
                komut.ExecuteNonQuery();
            }
            else
            {
                SqlCommand komut = new SqlCommand("Insert Into tblYiyecekler(Tur,Yiyecek,Kalori,Miktar,Tur_id,Miktar_id) Values('" + tur + "','" + yiyecek + "','" + kalori + "','" + miktar + "','" + tur_id + "', '"+"2"+"')", baglan);
                komut.ExecuteNonQuery();
            }
            baglan.Close();
            DatagridYenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tur = cmbTur.SelectedItem.ToString();
            string yiyecek = txtYiyecek.Text;
            string kalori = txtKalori.Text;
            string tur_id = cmbTur.SelectedIndex.ToString();
            string miktar = txtMiktar.Text;
            baglan.Open();
            if (rbAdet.Checked) {
                komut = new SqlCommand("Update tblYiyecekler set Tur='" + tur + "',Yiyecek='" + yiyecek + "',Kalori='" + kalori + "',Tur_id='" + tur_id +"',Miktar='"+miktar+"',Miktar_id='"+"1"+ "'where id="+ dataGridView1.CurrentRow.Cells["id"].Value.ToString(), baglan);


                komut.ExecuteNonQuery(); }
            else
            {
                komut = new SqlCommand("Update tblYiyecekler set Tur='" + tur + "',Yiyecek='" + yiyecek + "',Kalori='" + kalori + "',Tur_id='" + tur_id + "',Miktar='" + miktar + "',Miktar_id='" + "2" + "' where id=" + dataGridView1.CurrentRow.Cells["id"].Value.ToString(), baglan);


                komut.ExecuteNonQuery();
            }
            baglan.Close();
            DatagridYenile();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Miktar_id"].Value.ToString() == "1")
                {
                    txtYiyecek.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Yiyecek"].Value.ToString();
                    txtKalori.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Kalori"].Value.ToString();
                    txtMiktar.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Miktar"].Value.ToString();
                    rbAdet.Checked = true;
                    cmbTur.SelectedIndex = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Tur_id"].Value.ToString());


                }
                else
                {
                    txtYiyecek.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Yiyecek"].Value.ToString();
                    txtKalori.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Kalori"].Value.ToString();
                    txtMiktar.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Miktar"].Value.ToString();
                    rbGram.Checked = true;
                    cmbTur.SelectedIndex = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Tur_id"].Value.ToString());
                }

            }
            catch { }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand kmt = new SqlCommand("DELETE  tblYiyecekler where id=" + dataGridView1.CurrentRow.Cells["id"].Value.ToString(), baglan);
            kmt.ExecuteNonQuery();
            baglan.Close();
            DatagridYenile();
        }

        private void cmbTur_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtYiyecek.Text = "";
            txtKalori.Text = "";
            txtMiktar.Text = "";
            DatagridYenile();
        }
    }
}
