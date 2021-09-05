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
    public partial class Müsteri : Form
    {
        public Müsteri()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-M5U41NE;Initial Catalog=Satis;Integrated Security=True");

        private void Listele()
        {
            baglantı.Open();
            SqlCommand liste = new SqlCommand("Select * From Müsteri",baglantı);
            SqlDataAdapter da = new SqlDataAdapter(liste);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();

        }
        private void Müsteri_Load(object sender, EventArgs e)
        {
            Listele();
            baglantı.Open();
            SqlCommand sehir = new SqlCommand("Select* From sehir", baglantı);
            SqlDataReader dr = sehir.ExecuteReader();
            while (dr.Read())
            {
                CmbSehir.Items.Add(dr["sehir"]);

            }
            baglantı.Close();
            CmbSehir.Items.Add("SEÇİNİZ");
            CmbSehir.SelectedText="SEÇİNİZ";
        }

        private void Btnlistele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(TxtAd.Text) &&
                    !String.IsNullOrEmpty(TxtSoyad.Text) && !String.IsNullOrEmpty(TxtBakiye.Text) &&
                    CmbSehir.SelectedText!="SEÇİNİZ" && CmbSehir.Text!="SEÇİNİZ"
                    
                    )
                {
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("Insert Into müsteri  Values (@s1,@s2,@s3,@s4)", baglantı);
                    komut.Parameters.AddWithValue("@s1", TxtAd.Text);
                    komut.Parameters.AddWithValue("@s2", TxtSoyad.Text);
                    komut.Parameters.AddWithValue("@s3", CmbSehir.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@s4", decimal.Parse(TxtBakiye.Text));
                    komut.ExecuteNonQuery();
                  
                    MessageBox.Show("Başarıyla Eklendi");
                }
                else
                    MessageBox.Show("Boş Alan Bırakmayınız");
            }
           
            catch(FormatException i)
            {
                MessageBox.Show("Lütfen Geçerli Veri Girişi Yapınız"+i.ToString());
            }
            catch(Exception k)
            {
                MessageBox.Show("Kayıt Başarısız" + k.ToString());
            }
            finally
            {
                baglantı.Close();
                Listele();
                
            }

            }

        private void btnsil_Click(object sender, EventArgs e)
        {
            try
            {
               
                baglantı.Open();
                if (!String.IsNullOrEmpty(TxtId.Text))
                {
                    SqlCommand sil = new SqlCommand("delete from müsteri Where müsteriid=@s1 ", baglantı);
                    sil.Parameters.AddWithValue("@s1", TxtId.Text);
                    sil.ExecuteNonQuery();

                    MessageBox.Show("Başarıyla Silindi");

                }
                else
                    MessageBox.Show("Bir Müşteri Seçiniz");
                    
                
            }
            catch (Exception k )
            {
                MessageBox.Show("Beklenmedik Bir Hatayla Karşılaştık " +k.ToString());
            }
            finally
            {
                baglantı.Close();
                Listele();
                
            }
            
            
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(TxtId.Text))
                {
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("Update müsteri set müsteriad=@p1 ,müsterisoyad=@p2,müsterisehir=@p3,bakiye=@p4 where müsteriid=@p5", baglantı);
                    if (!String.IsNullOrEmpty(TxtAd.Text))
                        komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                    else
                        komut.Parameters.AddWithValue("@p1", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    if (!String.IsNullOrEmpty(TxtSoyad.Text))
                        komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                    else
                        komut.Parameters.AddWithValue("@p2", dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    if (!String.IsNullOrEmpty(CmbSehir.SelectedItem.ToString())&& CmbSehir.Text!=("SEÇİNİZ"))
                        komut.Parameters.AddWithValue("@p3", CmbSehir.SelectedItem.ToString());
                    else
                        komut.Parameters.AddWithValue("@p3", dataGridView1.CurrentRow.Cells[3].Value.ToString());
                    if (!String.IsNullOrEmpty(TxtBakiye.Text))
                        komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtBakiye.Text));
                    else
                        komut.Parameters.AddWithValue("@p4", dataGridView1.CurrentRow.Cells[4].Value);
                    komut.Parameters.AddWithValue("@p5", TxtId.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Başarıyla Güncelleme Yapıldı");
                }
                else
                    MessageBox.Show("Güncellenecek Veri Bulunamadı");
               
            }
            catch(FormatException h )
            {
                MessageBox.Show("Format hatası"+h.ToString());
            }
            finally
            {
                baglantı.Close();
                Listele();
            }
            

        }

        private void btnara_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand ara = new SqlCommand("Select * From müsteri where müsteriad  like '%' + @p1 + '%' ", baglantı);
            ara.Parameters.AddWithValue("@p1", TxtAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(ara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglantı.Close();
        }
    }
}
