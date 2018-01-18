using MarketOtomasyon.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Ürün Yönetimi";
        }

        FormUrunler urunlerForm;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnUrunYonetimi_Click(object sender, EventArgs e)
        {
            
            urunlerForm = new FormUrunler();
            
            urunlerForm.Text = "Ürünler";
            urunlerForm.Show();

        }
    }
}
