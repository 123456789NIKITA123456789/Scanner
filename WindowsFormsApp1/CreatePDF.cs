using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CreaterPDF
    {
        private List<Division> divisions = new List<Division>();
        private int totalDiv;
        private int misDiv;
        public CreaterPDF(List<Division> _divisions)
        {
            List <Division> oldDiv = new List<Division>();
            oldDiv = _divisions;
            totalDiv = oldDiv.Count;
            for (int i = 0; i < oldDiv.Count; i++)
            {
                if(!oldDiv[i].getCheck())
                {
                    this.divisions.Add(oldDiv[i]);
                }
            }
            
            misDiv = divisions.Count;
        }

        public void CreateFile(string nameCity, string nameAddress)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF|*.pdf";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(save.FileName, FileMode.Create)))
                {
                    document.Open();

                    CreateText(document, writer, nameCity, nameAddress);

                    document.Close();
                    writer.Close();
                }
            }
        }

        private void CreateText(iTextSharp.text.Document document, PdfWriter writer, string nameCity, string nameAddress)
        {
           
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            writer.DirectContent.BeginText();

            writer.DirectContent.SetFontAndSize(baseFont, 18f);
            writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"Отчет {DateTime.Now}", 1120 / 8 + 60, 820, 0);
            writer.DirectContent.SetFontAndSize(baseFont, 15f);
            writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"{nameCity}, {nameAddress}", 1120/8+60, 800, 0);
            writer.DirectContent.SetFontAndSize(baseFont, 11f);
            writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"Отсутствует подразделений - {misDiv}", 20, 770, 0);
            writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"Всего подразделений - {totalDiv}", 20, 755, 0);
            writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"Отсутствующие: ", 20, 725, 0);

            for(int i = 0; (i < divisions.Count) && (i < 47); i++)
            {
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"{i+1}. {divisions[i].getName()}", 20, 725 - 15*(i + 1), 0);
            }
            writer.DirectContent.EndText();

            if (divisions.Count >= 47)
            {
                int n = 47;

                while(n < divisions.Count)
                {
                    document.NewPage();
                    writer.DirectContent.BeginText();
                    writer.DirectContent.SetFontAndSize(baseFont, 11f);
                    for (int i = 0; (i+n < divisions.Count) && (i < 53); i++)
                    {
                        writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT, $"{i + n + 1}. {divisions[i + n].getName()}", 20, 820 - 15 * (i + 1), 0);
                    }
                    n += 53;
                    writer.DirectContent.EndText();
                }
                
            }

        }
    }
}
