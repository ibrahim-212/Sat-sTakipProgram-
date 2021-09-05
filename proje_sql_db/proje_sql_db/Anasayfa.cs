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
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=DESKTOP-M5U41NE;Initial Catalog=Satis;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            //Ürünlerin durum seviyesi 
            SqlCommand komut = new SqlCommand("Exec stokdurum", baglantı);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            baglantı.Open();
            SqlCommand ist1 = new SqlCommand("exec kategoriist", baglantı);
            SqlDataReader dr = ist1.ExecuteReader();
            while(dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }

            baglantı.Close();

            baglantı.Open();
            SqlCommand ist2 = new SqlCommand("exec sehirist", baglantı);
            SqlDataReader dr1 = ist2.ExecuteReader();
            while (dr1.Read())
            {
                chart2.Series["Şehirler"].Points.AddXY(dr1[0], dr1[1]);
            }

            baglantı.Close();



        }

        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FrmKategori fr = new FrmKategori();
            fr.Show();
        }

        private void BtnMüsteri_Click(object sender, EventArgs e)
        {
            Müsteri fr2 = new Müsteri();
            fr2.Show();
        }
    }
}
