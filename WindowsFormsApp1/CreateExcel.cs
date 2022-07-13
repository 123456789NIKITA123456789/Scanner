using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

using Application = Microsoft.Office.Interop.Excel.Application;

namespace WindowsFormsApp1
{
    public class CreateExcel
    {
        private Application application;
        private Workbook workBook;
        private Worksheet worksheet;
        private List<Division> divisions = new List<Division>();
        private int missingDiv = 0;

        public CreateExcel(List<Division> _divisions)
        {
            divisions.AddRange(_divisions);
        }


        public void ExportDataInExcel()
        {
            application = new Application
            {
                DisplayAlerts = false
            };

            // Файл шаблона
            string template = Path.Combine(Path.GetTempPath(), "template.xlsx");

            // Открываем книгу
            workBook = application.Workbooks.Add(Type.Missing);

            // Получаем активную таблицу
            worksheet = workBook.ActiveSheet as Worksheet;

            // Записываем данные
            worksheet.Columns.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            worksheet.Columns.Font.Size = 14;

            worksheet.Range["B2", "G2"].Merge();
            worksheet.Range["B2"].Value = "Отчет " + DateTime.Now;

            worksheet.Range["B3", "G3"].Merge();
            worksheet.Range["B3"].Value = Program.dBworker.getCurCity().getName() + "," + Program.dBworker.getCurAddress().getName();

            worksheet.Range["B6", "G6"].Merge();
            worksheet.Range["B6"].Value = "Всего подразделений - " + divisions.Count.ToString();


            worksheet.Range["B8", "G8"].Merge();
            worksheet.Range["B8"].Value = "Отсутствующие:";
            worksheet.Range["B9"].Value = "Номер";
            worksheet.Range["C9"].Value = "Подразделение";
            worksheet.Range["D9"].Value = "Этаж";
            worksheet.Range["E9"].Value = "Крыло";
            worksheet.Range["F9"].Value = "Телефон";
            worksheet.Range["G9"].Value = "ФИО";

            worksheet.Range["I8", "N8"].Merge();
            worksheet.Range["I8"].Value = "Присутствующие:";
            worksheet.Range["I9"].Value = "Номер";
            worksheet.Range["J9"].Value = "Подразделение";
            worksheet.Range["K9"].Value = "Этаж";
            worksheet.Range["L9"].Value = "Крыло";
            worksheet.Range["M9"].Value = "Телефон";
            worksheet.Range["N9"].Value = "ФИО";

            //worksheet.Range["I8", "N8"].Interior.Color = Color.FromArgb(226, 239, 218);
            //worksheet.Range["B5"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            //

            WriteDivision();

            worksheet.Range["B5", "G5"].Merge();
            worksheet.Range["B5"].Value = "Отсутствует подразделений - " + missingDiv;

            worksheet.Columns.AutoFit();

            if(File.Exists(template))
            {
                File.Delete(template);
            }

            // Показываем приложение
            application.Visible = true;
            workBook.SaveAs(template);
        }

        private void WriteDivision()
        {
            int miss = 0;
            int check = 0;

            for(int i = 0; i < divisions.Count; i++)
            {
                if(divisions[i].getCheck())
                {
                    worksheet.Range[$"I{10 + check}"].Value = divisions[i].getNumber();
                    worksheet.Range[$"J{10 + check}"].Value = divisions[i].getName();
                    worksheet.Range[$"K{10 + check}"].Value = divisions[i].getFloor();
                    worksheet.Range[$"L{10 + check}"].Value = divisions[i].getWing();
                    worksheet.Range[$"M{10 + check}"].Value = divisions[i].getPhone();
                    worksheet.Range[$"N{10 + check}"].Value = divisions[i].getFIO();
                    check++;
                }
                else
                {
                    worksheet.Range[$"B{10 + miss}"].Value = divisions[i].getNumber();
                    worksheet.Range[$"C{10 + miss}"].Value = divisions[i].getName();
                    worksheet.Range[$"D{10 + miss}"].Value = divisions[i].getFloor();
                    worksheet.Range[$"E{10 + miss}"].Value = divisions[i].getWing();
                    worksheet.Range[$"F{10 + miss}"].Value = divisions[i].getPhone();
                    worksheet.Range[$"G{10 + miss}"].Value = divisions[i].getFIO();
                    miss++;
                }
            }

            missingDiv = miss;
        }

        public void SaveExcel()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "(*.xlsx)|*.xlsx";
            string date = DateTime.Now.ToString();
            date = date.Replace(':', '.');
            save.FileName = $"Report {date}";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                workBook.SaveAs(save.FileName);
            }
        }

        public void CloseExcel()
        {
            if (application != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(application.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.ReleaseComObject(application);

                application = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);
    }
}
