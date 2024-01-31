using ConsoleApp2;
using Newtonsoft.Json;
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
using ThaiDanh.Properties;

namespace ThaiDanh
{
    public partial class fGoogleSheet : Form
    {
        public fGoogleSheet()
        {
            InitializeComponent();
        }

        private void fGoogleSheet_Load(object sender, EventArgs e)
        {
            cbSheetTrangThai.Text = Settings.Default.save_cbSheetTrangThai;
            tbSheetURL.Text = Settings.Default.save_tbSheetURL;
            //tbSpreadSheetID.Text = Settings.Default.save_tbSpreadSheetID;

            string folderCre = Path.Combine(Directory.GetCurrentDirectory(), "credentials");
            string[] fileCres = Directory.GetFiles(folderCre);
            string pathToFile = Path.Combine(folderCre, fileCres.First());

            #region Please share with email
            string dataJson = File.ReadAllText(pathToFile);
            CredentialJson credentialJson = JsonConvert.DeserializeObject<CredentialJson>(dataJson);
            // Console.WriteLine("Please share with email: " + credentialJson.client_email);
            tbSheetEmailShare.Text = credentialJson.client_email;
            #endregion
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Settings.Default.save_cbSheetTrangThai = cbSheetTrangThai.Text;
            Settings.Default.save_tbSheetURL = tbSheetURL.Text;
            Settings.Default.save_tbSpreadSheetID = tbSpreadSheetID.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void tbSheetURL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbSpreadSheetID.Text = tbSheetURL.Text.Split('/')[5];
            }
            catch { }
        }
    }
}
