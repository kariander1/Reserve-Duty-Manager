using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
namespace DarkDemo
{
    class ExcelForm
    {
        public Excel.Application appExl { get; set; }
        public Excel.Workbook workBook { get; set; }
        public Excel.Worksheet workSheet { get; set; }
        public  System.Globalization.CultureInfo prevCulture{ get; set; }
        public string path { get; set; }
        public ExcelForm (string path)
        {
            this.prevCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            this.path = path;
        }
        public void OpenWorkBook()
        {
            appExl = new Excel.Application();
            workBook = appExl.Workbooks.Open(this.path);
        }
        public bool SetWorksheet (string worksheet)
        {
            foreach (Excel.Worksheet sheet in workBook.Worksheets)
            {
                if(sheet.Name == worksheet)
                {
                    workSheet = sheet;
                    return true;
                }
            }
            return false;
        }
        public void CopyWorkBook(string path,string name)
        {
            // Excel.Workbook xlWorkBook = appExl.Workbooks.Add(ex);
            //  xlWorkBook.SaveAs(path);
            this.workBook.SaveAs(path + "\\" + name);
        }
        public void UpdateCell(int row,int column,string s)
        {
            workSheet.Cells[row, column] = s;
        }
        public void AppendCell(int row, int column, string s)
        {
            try
            {
                object temp = (workSheet.Cells[row, column] as Excel.Range).Value;
                if (temp != null && temp.ToString() != "")
                {
                    workSheet.Cells[row, column] = (workSheet.Cells[row, column] as Excel.Range).Value + " " + s;
                    return;
                }
            }
            catch (Exception e)
            {
               
                
            }
            workSheet.Cells[row, column] = s;
        }
        public void ColorCell(int row, int column, System.Drawing.Color color)
        {
            (workSheet.Cells[row,column] as Excel.Range).Interior.Color =System.Drawing.ColorTranslator.ToOle(color);
        }
        public void ColorCellFont(int row, int column, System.Drawing.Color color)
        {
            (workSheet.Cells[row, column] as Excel.Range).Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        public bool Save()
        {
            try
            {
                workBook.Save();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public object GetCellVal(int row,int column)
        {
            object ob = (workSheet.Cells[row, column] as Excel.Range).Value;
            return ob;
        }
        public void runMacro(string MacroName)
        {
            appExl.Run(MacroName);
        }
        public bool Close()
        {
            try
            {
                workBook.Close();
            }
            catch (Exception)
            {

                
                return false;
            }
            try
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(appExl);
                appExl = null;
            }
            catch (Exception)
            {
                appExl = null;
               
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = this.prevCulture;
                GC.Collect();

            }
            return true;
        }

    }
}
