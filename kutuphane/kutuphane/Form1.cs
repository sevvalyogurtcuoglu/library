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
namespace kutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // sql bağlantısı sağlanıyor
        SqlConnection baglanti = new SqlConnection("Data Source=****;Initial Catalog=proje;Integrated Security=True");

        private void verilerigoster()
        {

            listView1.Items.Clear();  // her defasında liteyi sil. listeye her deiğişikliksde eskiyi sil en yeniyi göster
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From book", baglanti);
            SqlDataReader oku = komut.ExecuteReader(); //komutlar okunuyor

            while (oku.Read()) //verileri okudukça sırayla listview e aktaracak
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["isbn"].ToString();
                ekle.SubItems.Add(oku["bookNmae"].ToString()); // alt öğeler, oku komutu çalıştıkça
                ekle.SubItems.Add(oku["bookAuthor"].ToString());
                ekle.SubItems.Add(oku["broadcostDate"].ToString());
                ekle.SubItems.Add(oku["placeoftheBook"].ToString());
                ekle.SubItems.Add(oku["theAmount"].ToString());

                listView1.Items.Add(ekle); // ekle çağırılıyor
            }
            baglanti.Close(); // bağlantıyı kapa
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster(); // verileeri alıyor
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into book (isbn,bookNmae,bookAuthor,broadcostDate,placeoftheBook,theAmount) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "')", baglanti);
            komut.ExecuteNonQuery(); // parametreleri geri döner bana
            baglanti.Close();
            verilerigoster();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

        }
        int isbn = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From book where isbn=(" + isbn + ")", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoster();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            isbn = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            // isbn e tıklandığında bilgilari kutucuktaki yerlerine gönderecek
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[5].Text;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update book set isbn='" + textBox1.Text.ToString() + "',bookNmae='" + textBox2.Text.ToString() + "',bookAuthor='" + textBox3.Text.ToString() + "',broadcostDate='" + textBox4.Text.ToString() + "',placeoftheBook='" + textBox5.Text.ToString() + "',theAmount='" + textBox6.Text.ToString() + "'where isbn=" + isbn, baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoster();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();  // her defasında liteyi sil. listeye her deiğişikliksde eskiyi sil en yeniyi göster
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From book where bookNmae like '%" + textBox7.Text + "%'", baglanti);
            SqlDataReader oku = komut.ExecuteReader(); //komutlar okunuyor

            while (oku.Read()) //verileri okudukça sırayla listview e aktaracak
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["isbn"].ToString();
                ekle.SubItems.Add(oku["bookNmae"].ToString()); // alt öğeler, oku komutu çalıştıkça
                ekle.SubItems.Add(oku["bookAuthor"].ToString());
                ekle.SubItems.Add(oku["broadcostDate"].ToString());
                ekle.SubItems.Add(oku["placeoftheBook"].ToString());
                ekle.SubItems.Add(oku["theAmount"].ToString());

                listView1.Items.Add(ekle); // ekle çağırıldı
            }
            baglanti.Close(); 

        }
    }
}