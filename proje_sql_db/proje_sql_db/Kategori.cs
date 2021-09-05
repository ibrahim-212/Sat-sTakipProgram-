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

namespace proje_sql_db
{
    public partial class FrmKategori : Form
    {
        public FrmKategori()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-M5U41NE;Initial Catalog=Satis;Integrated Security=True");
        private void Listele()
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("Select * From kategori", baglantı);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();
        }
        
        private void Btnlistele_Click(object sender, EventArgs e)
        {
            Listele();
        }
        

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtkategoriad.Text) )
                {
                    baglantı.Open();
                    SqlCommand komut2 = new SqlCommand("insert into kategori(Kategoriad) values(@p1)", baglantı);
                    komut2.Parameters.AddWithValue("@p1", txtkategoriad.Text);
                    komut2.ExecuteNonQuery();
                    baglantı.Close();
                    MessageBox.Show("Kategoriye Kayıt Başarıyla Gerçekleşti");
                    Listele();
                    
                }
                else
                    MessageBox.Show("Kayıt Edilcek Alan Yok");
              
            }
            catch (Exception k)
            {
                MessageBox.Show("Kayıt Başarısız"+ k.ToString());
                baglantı.Close();
            }

            
        }

      

    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtkategoriid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtkategoriad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        { 
            
            
            if(!String.IsNullOrEmpty(txtkategoriid.Text) || !String.IsNullOrEmpty(txtkategoriad.Text) )
            {
                try
                {
                    baglantı.Open();
                    SqlCommand sil = new SqlCommand("Delete from kategori Where Kategoriid=@p1", baglantı);
                    sil.Parameters.AddWithValue("@p1", txtkategoriid.Text);
                    sil.ExecuteNonQuery();

                    MessageBox.Show("Başarıyla Silindi");
                    baglantı.Close();
                    Listele();
                }
                catch(Exception v)
                {
                    MessageBox.Show("Silmek İstediğiniz Değer Başka Bir Tabloda Kullanıldığından Silinemiyor"+v.ToString());
                    baglantı.Close();
                }
               
            }
            else
            {
                MessageBox.Show("Bir Alan Seçiniz");
            }
          
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {

            try
            {
                if (!String.IsNullOrEmpty(txtkategoriad.Text) && !String.IsNullOrEmpty(txtkategoriid.Text))
                {
                    baglantı.Open();
                    SqlCommand güncelle = new SqlCommand("Update Kategori set Kategoriad=@p1 where Kategoriid=@p2", baglantı);
                    güncelle.Parameters.AddWithValue("@p1", txtkategoriad.Text);
                    güncelle.Parameters.AddWithValue("@p2", txtkategoriid.Text);
                    güncelle.ExecuteNonQuery();
                    MessageBox.Show("Başarıyla Güncellendi");
                    baglantı.Close();
                    Listele();

                }
                else
                    MessageBox.Show("Boş Alanları Doldur");
            }
            catch (Exception)
            {
                MessageBox.Show("Güncelleme Yapılamadı");
                baglantı.Close();
            }


        }
    }
}
//Data Source=DESKTOP-M5U41NE;Initial Catalog=Satis;Integrated Security=True