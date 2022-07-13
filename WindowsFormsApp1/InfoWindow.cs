using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WindowsFormsApp1
{
    public partial class InfoWindow : Form,Window
    {

        List<Division> allDiv = new List<Division>();
        public InfoWindow()
        {
            InitializeComponent();
        }

        private void CodeWindow_Load(object sender, EventArgs e)
        {
            Program.dBworker.Attach(this);
        }

        public void Update(List<Division> data)
        {
            UpdateView(data);
            allDiv = data;
        }

        private void UpdateView(List<Division> data)
        {
            Program.ToMain(divListBox, (divListBox) =>
            {
                ((ListBox)divListBox).Items.Clear();
                for (int i = 0; i < data.Count; i++)
                {
                    ((ListBox)divListBox).Items.Add(data[i].getName());
                }
                return true;
            }
                );
        }

        private void UpdateDataInView()
        {
            if(divListBox.SelectedIndex != -1)
            {
                numberLabel.Text = allDiv[divListBox.SelectedIndex].getNumber().ToString();
                floorLabel.Text = allDiv[divListBox.SelectedIndex].getFloor().ToString();
                wingLabel.Text = allDiv[divListBox.SelectedIndex].getWing();
                phoneLabel.Text = allDiv[divListBox.SelectedIndex].getPhone();
                fioLabel.Text = allDiv[divListBox.SelectedIndex].getFIO();
            }
            else
            {
                numberLabel.Text = "";
                floorLabel.Text = "";
                wingLabel.Text = "";
                phoneLabel.Text = "";
                fioLabel.Text = "";
            }
        }

        private void divListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCode();
            UpdateDataInView();

        }

        private void GenerateCode()
        {
            if(qrRadioButton.Checked)
            {
                QRcode();
            }
            else if(barRadioButton.Checked)
            {
                Barcode();
            }
        }

        private void QRcode()
        {
            QRCodeWriter qrEncode = new QRCodeWriter(); //создание QR кода

            string div = allDiv[divListBox.SelectedIndex].getId();  //строка на русском языке

            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();    //для колекции поведений
            hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");   //добавление в коллекцию кодировки utf-8
            BitMatrix qrMatrix = qrEncode.encode(   //создание матрицы QR
                div,                 //кодируемая строка
                BarcodeFormat.QR_CODE,  //формат кода, т.к. используется QRCodeWriter применяется QR_CODE
                400,                    //ширина
                400,                    //высота
                hints);                 //применение колекции поведений

            BarcodeWriter qrWrite = new BarcodeWriter();    //класс для кодирования QR в растровом файле
            Bitmap qrImage = qrWrite.Write(qrMatrix);   //создание изображения

            codeOutputBox.Image = qrImage;
        }

        private void Barcode()
        {
            if(divListBox.SelectedIndex != -1)
            {
                string div = allDiv[divListBox.SelectedIndex].getId();
                BarcodeWriter writer = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 600,
                        Width = 1800,
                        PureBarcode = false,
                        Margin = 10,
                    },
                };

                Bitmap barWrite = writer.Write(div);
                codeOutputBox.Image = barWrite;
            }
        }

        private void saveCodeButton_Click(object sender, EventArgs e)
        {
            SaveCode();
        }

        private void SaveCode()
        {
            if(divListBox.SelectedIndex != -1)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = divListBox.Items[divListBox.SelectedIndex].ToString();
                save.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    codeOutputBox.Image.Save(save.FileName);
                }
                else
                {
                    MessageBox.Show("Выберите подразделение, код которого Вы хотите сохранить");
                }
            }
            
        }

        private void qrRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void CodeWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dBworker.Detach(this);
        }

        private void excelReportButton_Click(object sender, EventArgs e)
        {
            ExcelReport();
        }

        private void ExcelReport()
        {
            CreateExcel cr = new CreateExcel(allDiv);
            cr.ExportDataInExcel();
            cr.SaveExcel();
            //TopMost = true;

            cr.CloseExcel();
        }

        private void pdfExportButton_Click(object sender, EventArgs e)
        {
            PDFReport();
        }

        private void PDFReport()
        {
            string nameCity = Program.dBworker.getCurCity().getName();
            string nameAddress = Program.dBworker.getCurAddress().getName();
            CreaterPDF creat = new CreaterPDF(allDiv);
            creat.CreateFile(nameCity, nameAddress);
        }
    }
}
