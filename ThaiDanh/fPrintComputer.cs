using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ThaiDanh
{
    public partial class fPrintComputer : Form
    {
        public static List<string> listPrint = new List<string>();

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

        private void btOK_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection check = cbList.CheckedItems;
            foreach (var c in check)
            {
                listPrint.Add(c.ToString());
            }
            this.Close();
        }
    }
}
