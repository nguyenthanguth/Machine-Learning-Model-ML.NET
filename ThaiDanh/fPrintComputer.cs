using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SpreadsheetGear;
using SpreadsheetGear.Drawing.Printing;
using SpreadsheetGear.Printing;
using SpreadsheetGear.Windows.Forms;

namespace ThaiDanh
{
    public partial class fPrintComputer : Form
    {
        public fPrintComputer()
        {
            InitializeComponent();

            PrinterSettings.StringCollection printerNames = PrinterSettings.InstalledPrinters;
            cbList.Items.Clear();
            foreach (string printerName in printerNames)
            {
                cbList.Items.Add(printerName);
            }
        }

        private IWorkbook Workbook;
        public fPrintComputer(IWorkbook workbook)
        {
            InitializeComponent();

            this.Workbook = workbook;

            // show full computer print
            PrinterSettings.StringCollection printerNames = PrinterSettings.InstalledPrinters;
            cbList.Items.Clear();
            foreach (string printerName in printerNames)
            {
                cbList.Items.Add(printerName);
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.Workbook.WorkbookSet.GetLock();

                IWorksheet worksheet = this.Workbook.Worksheets["Sample"];

                CheckedListBox.CheckedItemCollection check = cbList.CheckedItems;
                foreach (string computerPrint in check)
                {
                    WorkbookPrintDocument printDocument = new WorkbookPrintDocument(worksheet, PrintWhat.Sheet);
                    printDocument.PrinterSettings.PrinterName = computerPrint; // chọn tên máy in
                    printDocument.Print();
                }
                this.Close();
            }
            finally
            {
                this.Workbook.WorkbookSet.ReleaseLock();
            }

        }
    }
}
