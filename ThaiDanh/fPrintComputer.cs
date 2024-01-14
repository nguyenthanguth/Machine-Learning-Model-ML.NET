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
using ThaiDanh.Properties;

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
                    for (int i = 1; i <= Convert.ToInt32(nSoLanIn.Value); i++)
                    {
                        WorkbookPrintDocument printDocument = new WorkbookPrintDocument(worksheet, PrintWhat.Sheet);
                        printDocument.PrinterSettings.PrinterName = computerPrint; // chọn tên máy in
                        printDocument.Print();
                    }
                }
                this.Close();
            }
            finally
            {
                this.Workbook.WorkbookSet.ReleaseLock();
            }

        }

        private void fPrintComputer_Load(object sender, EventArgs e)
        {
            nSoLanIn.Value = Settings.Default.save_nSoLanIn;
        }

        private void fPrintComputer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.save_nSoLanIn = nSoLanIn.Value;
            Settings.Default.Save();
        }
    }
}
