using MarketOtomasyon.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MarketOtomasyon.Forms
{
    public partial class FormUrunler : Form
    {
        public FormUrunler()
        {
            InitializeComponent();
        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormUrunler_Load(object sender, EventArgs e)
        {
            VerileriGetir();

        }

        private void VerileriGetir()
        {
            try
            {
                MarketOtomasyonDbEntities db = new MarketOtomasyonDbEntities();

                cmbKategori.DataSource = db.Categories.ToList();
                cmbKategori.DisplayMember = "CategoryName";
                cmbKategori.ValueMember = "CategoryId";
                                
                cmbFirmaAdi.DataSource = db.Suppliers.ToList();
                cmbFirmaAdi.DisplayMember = "CompanyName";
                cmbFirmaAdi.ValueMember = "CompanyId";

                var urunler = db.Products
                    .Select(x => new UrunViewModel()
                    {
                        UrunId = x.ProductId,
                        UrunAdi = x.ProductName,
                        Barkod = x.Barcode,
                        TedarikciFirma = x.Supplier.CompanyName,
                        AlisFiyati = x.UnitPrice ?? 0,
                        SatisFiyati = x.SalesPrice ?? 0,
                        Kategori = x.Category.CategoryName,
                        Stok = x.Stock ?? 0
                    }).ToList();
                lvUrunler.Items.Clear();

                urunler.ForEach(x =>
                {
                    ListViewItem viewItem = new ListViewItem(x.UrunId.ToString());
                    viewItem.SubItems.Add(x.UrunAdi);
                    viewItem.SubItems.Add(x.Barkod);
                    viewItem.SubItems.Add(x.TedarikciFirma);
                    viewItem.SubItems.Add($"{x.AlisFiyati:c2}");
                    
                    viewItem.SubItems.Add(x.Kategori);
                    viewItem.SubItems.Add(x.Stok.ToString());

                    lvUrunler.Items.Add(viewItem);
                });
                                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void lvUrunler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                MarketOtomasyonDbEntities db = new MarketOtomasyonDbEntities();
                Product yeniurun = new Product
                {
                    ProductName = txtUrunAdi.Text,
                    UnitPrice = nAlisFiyati.Value,
                    SalesPrice = nSatisFiyati.Value,
                   // Category = cmbKategori.Text,
                    //CompanyName=cmbFirmaAdi.
                    Stock=Convert.ToInt32(nStok.Value),
                    ProductImage= resimDosyası

                    
                };
                db.Products.Add(yeniurun);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        byte[] resimDosyası;
        private void pbResim_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosyaAc = new OpenFileDialog
            {
                Title = "Bir resim dosyası seçiniz",
                Multiselect = false,
                Filter = "JPG formatlı (*.jpg)|*.jpg;*.jpg;|PNG Formatlı | *.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            DialogResult result = dosyaAc.ShowDialog();
            MemoryStream memoryStream = new MemoryStream();
            var buffer = new byte[64];
            if (result == DialogResult.OK)
            {
                FileStream fileStream = File.Open(dosyaAc.FileName, FileMode.Open);
                while (fileStream.Read(buffer, 0, 64) != 0)
                {
                    memoryStream.Write(buffer, 0, 64);
                }
                resimDosyası = memoryStream.ToArray();
                pbResim.Image = new Bitmap(memoryStream);
            }
        }
    }
}
