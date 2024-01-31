using SpreadsheetGear;
using SpreadsheetGear.Drawing.Printing;
using SpreadsheetGear.Printing;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThaiDanh.Properties;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using ConsoleApp2;
using Newtonsoft.Json;
using System.Linq;

namespace ThaiDanh
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private static string pathExcelTemplate = Path.GetFullPath("./SampleThaiDanh.xls");
        string sheetNguoiNhan = "Người nhận";
        string sheetXuatKho = "Xuất kho";
        string sheetHangHoa = "Hàng hoá";

        private void fMain_Load(object sender, EventArgs e)
        {
            workbookView.ActiveWorkbook = Factory.GetWorkbook(pathExcelTemplate);

            SaveSheetSampleDefault();
            ReloadAllData();
            LoadHistory();

            Task.Run(() =>
            {
                while (true)
                {
                    LoopUpdateData();
                    Thread.Sleep(100);
                }
            });
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveHistory();
        }

        private void LoadHistory()
        {
            cbNguoiNhanTen.Text = Settings.Default.save_cbNguoiNhanTen;
            cbNguoiNhanDiaChi.Text = Settings.Default.save_cbNguoiNhanDiaChi;
            cbKhoLyDo.Text = Settings.Default.save_cbKhoLyDo;
            cbKhoXuatTai.Text = Settings.Default.save_cbKhoXuatTai;
            cbKhoDiaDiem.Text = Settings.Default.save_cbKhoDiaDiem;
            cbHHTen.Text = Settings.Default.save_cbHHTen;
            cbHHMaSo.Text = Settings.Default.save_cbHHMaSo;
            cbHHDonVi.Text = Settings.Default.save_cbHHDonVi;
            cbSoLuongYeuCau.Text = Settings.Default.save_cbSoLuongYeuCau;
            cbSoLuongYeuCau.Text = Settings.Default.save_cbSoLuongThucXuat;
            cbHHDonGia.Text = Settings.Default.save_cbHHDonGia;
            cbHHGhiChu.Text = Settings.Default.save_cbHHGhiChu;
            cbAutoFillDataNN.Checked = Settings.Default.save_cbAutoFillDataNN;
            cbAutoFillDataHH.Checked = Settings.Default.save_cbAutoFillDataHH;
            tbSoPhieu.Text = Settings.Default.save_tbSoPhieu;
        }
        private void SaveHistory()
        {
            Settings.Default.save_cbNguoiNhanTen = cbNguoiNhanTen.Text;
            Settings.Default.save_cbNguoiNhanDiaChi = cbNguoiNhanDiaChi.Text;
            Settings.Default.save_cbKhoLyDo = cbKhoLyDo.Text;
            Settings.Default.save_cbKhoXuatTai = cbKhoXuatTai.Text;
            Settings.Default.save_cbKhoDiaDiem = cbKhoDiaDiem.Text;
            Settings.Default.save_cbHHTen = cbHHTen.Text;
            Settings.Default.save_cbHHMaSo = cbHHMaSo.Text;
            Settings.Default.save_cbHHDonVi = cbHHDonVi.Text;
            Settings.Default.save_cbSoLuongYeuCau = cbSoLuongYeuCau.Text;
            Settings.Default.save_cbSoLuongThucXuat = cbSoLuongYeuCau.Text;
            Settings.Default.save_cbHHDonGia = cbHHDonGia.Text;
            Settings.Default.save_cbHHGhiChu = cbHHGhiChu.Text;
            Settings.Default.save_cbAutoFillDataNN = cbAutoFillDataNN.Checked;
            Settings.Default.save_cbAutoFillDataHH = cbAutoFillDataHH.Checked;
            Settings.Default.save_tbSoPhieu = tbSoPhieu.Text;
            Settings.Default.Save();
        }
        private void ReloadAllData()
        {
            // load list data người nhận
            LoadComboBox(cbNguoiNhanTen, ListDataColumn(sheetNguoiNhan, 0));
            LoadComboBox(cbNguoiNhanDiaChi, ListDataColumn(sheetNguoiNhan, 1));
            // load list data kho
            LoadComboBox(cbKhoLyDo, ListDataColumn(sheetXuatKho, 0));
            LoadComboBox(cbKhoXuatTai, ListDataColumn(sheetXuatKho, 1));
            LoadComboBox(cbKhoDiaDiem, ListDataColumn(sheetXuatKho, 2));
            // load list data hàng hoá
            LoadComboBox(cbHHTen, ListDataColumn(sheetHangHoa, 0));
            LoadComboBox(cbHHMaSo, ListDataColumn(sheetHangHoa, 1));
            LoadComboBox(cbHHDonVi, ListDataColumn(sheetHangHoa, 2));
            LoadComboBox(cbHHDonGia, ListDataColumn(sheetHangHoa, 5));
            LoadComboBox(cbHHGhiChu, ListDataColumn(sheetHangHoa, 6));
        }

        private void SaveSheetSampleDefault()
        {
            IWorkbook workbook = workbookView.ActiveWorkbook;
            workbook.WorkbookSet.GetLock();
            IWorksheet worksheet = workbook.Worksheets["Sample"];

            worksheet.Cells["A10"].Value = $"- Họ và tên người nhận hàng: ....     Địa chỉ (bộ phận): ...";
            worksheet.Cells["A11"].Value = $"- Lý do xuất kho: ...";
            worksheet.Cells["A12"].Value = $"- Xuất tại kho (ngăn lô): ....     Địa điểm: ...";

            worksheet.Cells["B17:J22"].ClearContents();

            worksheet.Cells["A25"].Value = $"- Tổng số tiền (viết bằng chữ): ... đồng";
            workbook.Save();
            workbook.WorkbookSet.ReleaseLock();
        }

        private void LoadComboBox(ComboBox comboBox, List<string> list)
        {
            comboBox.Items.Clear();
            foreach (string data in list)
            {
                comboBox.Items.Add(data);
            }
        }

        private List<string> ListDataColumn(string sheet_name, int col)
        {
            List<string> list = new List<string>();

            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets[sheet_name];
                int rowUsed = worksheet.UsedRange.RowCount;
                for (int row = 1; row < rowUsed; row++)
                {
                    string value = Convert.ToString(worksheet.Cells[row, col].Value);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        list.Add(value);
                    }
                }
            }
            finally
            {
                workbookView.ReleaseLock();
            }

            return list;
        }

        private void cbNguoiNhanTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAutoFillDataNN.Checked)
            {
                int index = cbNguoiNhanTen.SelectedIndex;
                cbNguoiNhanDiaChi.SelectedIndex = index;
            }
        }

        private void btNguoiNhanSave_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets[sheetNguoiNhan];

                List<string> listName = ListDataColumn(sheetNguoiNhan, 0);
                if (listName.Contains(cbNguoiNhanTen.Text))
                {
                    DialogResult tiepTuc = MessageBox.Show($"Đã tồn tại người nhận: {cbNguoiNhanTen.Text}\r\nVẫn tiếp tục thêm với một địa chỉ mới?", "Thông báo", MessageBoxButtons.YesNo);
                    if (tiepTuc == DialogResult.No)
                    {
                        return;
                    }

                    List<string> listDiaChi = ListDataColumn(sheetNguoiNhan, 1);
                    if (listDiaChi.Contains(cbNguoiNhanDiaChi.Text))
                    {
                        MessageBox.Show($"Đã tồn tại người nhận: {cbNguoiNhanTen.Text}\r\nĐịa chỉ: {cbNguoiNhanDiaChi.Text}", "Thông báo");
                        return;
                    }
                }

                for (int row = 1; row < worksheet.Range.RowCount; row++)
                {
                    string value = Convert.ToString(worksheet.Cells[row, 0].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[row, 0].Value = cbNguoiNhanTen.Text;
                        worksheet.Cells[row, 1].Value = cbNguoiNhanDiaChi.Text;
                        break;
                    }
                }
                workbook.Save();
                ReloadAllData();
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void btKhoSave_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets[sheetXuatKho];

                for (int row = 1; row < worksheet.Range.RowCount; row++)
                {
                    string value = Convert.ToString(worksheet.Cells[row, 0].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[row, 0].Value = cbKhoLyDo.Text;
                        break;
                    }
                }
                for (int row = 1; row < worksheet.Range.RowCount; row++)
                {
                    string value = Convert.ToString(worksheet.Cells[row, 0].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[row, 1].Value = cbKhoXuatTai.Text;
                        break;
                    }
                }
                for (int row = 1; row < worksheet.Range.RowCount; row++)
                {
                    string value = Convert.ToString(worksheet.Cells[row, 0].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[row, 2].Value = cbKhoDiaDiem.Text;
                        break;
                    }
                }
                workbook.Save();
                SaveSheetSampleDefault();
                ReloadAllData();
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void btHangHoaSave_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets[sheetHangHoa];

                for (int row = 1; row < worksheet.Range.RowCount; row++)
                {
                    string value = Convert.ToString(worksheet.Cells[row, 0].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[row, 0].Value = cbHHTen.Text;
                        worksheet.Cells[row, 1].Value = cbHHMaSo.Text;
                        worksheet.Cells[row, 2].Value = cbHHDonVi.Text;
                        worksheet.Cells[row, 5].Value = cbHHDonGia.Text;
                        worksheet.Cells[row, 6].Value = cbHHGhiChu.Text;
                        break;
                    }
                }
                workbook.Save();
                SaveSheetSampleDefault();
                ReloadAllData();
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void cbHHTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAutoFillDataHH.Checked)
            {
                int index = cbHHTen.SelectedIndex;

                try
                {
                    cbHHMaSo.SelectedIndex = index;
                    cbHHDonVi.SelectedIndex = index;
                    cbHHDonGia.SelectedIndex = index;
                    cbHHGhiChu.SelectedIndex = index;
                }
                catch { }
            }
        }

        private void LoopUpdateData()
        {
            // Get số phiếu
            if (tbSoPhieu.ReadOnly)
            {
                string[] files = Directory.GetFiles("./data");
                int soPhieuMax = 0;
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        string name = fileInfo.Name;
                        int soPhieu = Convert.ToInt32(name.Split('-')[0]);
                        if (soPhieu > soPhieuMax)
                        {
                            soPhieuMax = soPhieu;
                        }
                    }
                    catch { }
                }
                tbSoPhieu.Text = Convert.ToString(soPhieuMax + 1);
            }

            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets["Sample"];

                worksheet.Cells["A6"].Value = $"{DateTime.Now}";
                worksheet.Cells["A7"].Value = $"Số: {tbSoPhieu.Text}";

                worksheet.Cells["A10"].Value = $"- Họ và tên người nhận hàng: {cbNguoiNhanTen.Text}.     Địa chỉ (bộ phận): {cbNguoiNhanDiaChi.Text}";
                worksheet.Cells["A11"].Value = $"- Lý do xuất kho: {cbKhoLyDo.Text}";
                worksheet.Cells["A12"].Value = $"- Xuất tại kho (ngăn lô): {cbKhoXuatTai.Text}.     Địa điểm: {cbKhoDiaDiem.Text}";
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void btHHThem_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets["Sample"];

                int rowStart = 17;
                int rowEnd = 22;
                for (int i = rowStart; i <= rowEnd; i++)
                {
                    string value = Convert.ToString(worksheet.Cells[$"B{i}"].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[$"B{i}"].Value = $"{cbHHTen.Text}\n({cbHHMaSo.Text})";
                        //worksheet.Cells[$"D{i}"].Value = cbHHMaSo.Text;
                        worksheet.Cells[$"E{i}"].Value = cbHHDonVi.Text;
                        worksheet.Cells[$"F{i}"].Value = cbSoLuongYeuCau.Text;
                        worksheet.Cells[$"G{i}"].Value = cbSoLuongThucXuat.Text;
                        worksheet.Cells[$"H{i}"].Value = cbHHDonGia.Text;
                        worksheet.Cells[$"I{i}"].Value = cbHHGhiChu.Text;
                        worksheet.Cells[$"J{i}"].Value = tbHHThanhTien.Text;
                        break;
                    }
                }
                //workbook.Save();
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void btXuatPhieuPDF_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets["Sample"];

                #region lưu pdf
                DialogResult savePdf = MessageBox.Show("Lưu phiếu xuất kho file PDF & Excel ?", "Thông báo", MessageBoxButtons.YesNo);
                if (savePdf == DialogResult.Yes)
                {
                    DialogResult openPdf = MessageBox.Show("Mở file PDF", "Thông báo", MessageBoxButtons.YesNo);

                    IWorkbook wb = Factory.GetWorkbook();
                    IWorksheet ws = wb.Worksheets[0];
                    ws = worksheet;

                    string fileTemp = Path.Combine(Directory.GetCurrentDirectory(), "saveToPdf.xls");
                    ws.SaveAs(fileTemp, FileFormat.Excel8);

                    string filePdf = Path.Combine(Directory.GetCurrentDirectory(), $"data/{tbSoPhieu.Text}-{cbNguoiNhanTen.Text}.pdf");
                    SaveToPDF(fileTemp, filePdf);

                    // xóa
                    if (File.Exists(fileTemp))
                    {
                        File.Delete(fileTemp);
                    }

                    if (DialogResult.Yes == openPdf)
                    {
                        Process.Start(filePdf);
                    }

                    #region lưu excel
                    SaveToExcel(worksheet);
                    #endregion
                }
                #endregion

                fPrintComputer fPrintComputer = new fPrintComputer(workbook);
                this.Hide();
                fPrintComputer.ShowDialog();
                this.Show();
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void SaveToExcel(IWorksheet worksheet)
        {
            string pathFileE = Path.Combine(Directory.GetCurrentDirectory(), $"excel/{DateTime.Now.ToString("yyyy-MM-dd")}.xls");
            if (File.Exists(pathFileE))
            {
                // open and create
                IWorkbook wb = Factory.GetWorkbook(pathFileE);
                IWorksheet ws = wb.Worksheets.Add();

                ws.WindowInfo.Zoom = 55;
                ws.Name = tbSoPhieu.Text;
                worksheet.Cells.Copy(ws.Cells);
                wb.Save();

                // TK
                TKSP(wb);
            }
            else
            {
                // save new file
                IWorkbook wb = Factory.GetWorkbook();
                IWorksheet ws = wb.Worksheets.Add();

                ws.WindowInfo.Zoom = 55;
                worksheet.Cells.Copy(ws.Cells);
                ws.Name = tbSoPhieu.Text;
                wb.SaveAs(pathFileE, FileFormat.Excel8);

                // TK
                TKSP(wb);
            }
        }

        private void TKSP(IWorkbook wb)
        {
            wb.Worksheets[0].Cells["A1:F1000"].Clear();
            wb.Worksheets[0].StandardWidth = 30;

            SanPham sanPham = new SanPham();
            sanPham.GetAllSanPham(wb);

            int r = 0;
            int c = 0;
            List<SanPham> listSanPham = sanPham.ListSanPhamInSheet;
            foreach (SanPham sp in listSanPham)
            {
                if (!string.IsNullOrWhiteSpace(sp.TenSP))
                {
                    wb.Worksheets[0].Cells[r, c++].Value = sp.NguoiNhan;
                    wb.Worksheets[0].Cells[r, c++].Value = sp.TenSP;
                    wb.Worksheets[0].Cells[r, c++].Value = "'" + sp.KhoiLuongGam;
                    wb.Worksheets[0].Cells[r, c++].Value = sp.DVT;
                    wb.Worksheets[0].Cells[r, c++].Value = sp.SoLuongThucXuat;
                    wb.Worksheets[0].Cells[r, c++].Value = sp.GhiChu;
                    wb.Worksheets[0].Cells[r, c++].Value = sp.SoPhieu;

                    r++;
                    c = 0;
                }
            }
            wb.Save();

            if (Settings.Default.save_cbSheetTrangThai == "TẮT")
            {
                return;
            }

            #region post data to google sheet
            List<IList<object>> listlistObj = new List<IList<object>>();
            foreach (SanPham sp in listSanPham)
            {
                if (string.IsNullOrWhiteSpace(sp.TenSP))
                {
                    continue;
                }

                IList<object> listObj = new List<object>(); // ứng với cột
                listObj.Add(sp.NguoiNhan);
                listObj.Add(sp.TenSP);
                listObj.Add(sp.KhoiLuongGam);
                listObj.Add(sp.DVT);
                listObj.Add(sp.SoLuongThucXuat);
                listObj.Add(sp.GhiChu);
                listObj.Add(sp.SoPhieu);

                listlistObj.Add(listObj); // ứng với hàng
            }
            PostDataSheet(listlistObj);
            #endregion
        }

        private void PostDataSheet(List<IList<object>> listlistObj)
        {
            try
            {
                string folderCre = Path.Combine(Directory.GetCurrentDirectory(), "credentials");
                string[] fileCres = Directory.GetFiles(folderCre);
                string pathToFile = Path.Combine(folderCre, fileCres.First());

                #region Connect and Post
                GoogleSheet googleSheet = new GoogleSheet();
                googleSheet.ConnectJsonCredentials(pathToFile);
                googleSheet.SpreadSheetID = Settings.Default.save_tbSpreadSheetID; // thay thế

                string newSheet = DateTime.Now.ToString("yyyy-MM-dd");
                googleSheet.PostData(newSheet, listlistObj);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Post Data To Sheet");
            }
        }

        private void btConvertToWord_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets["Sample"];

                worksheet.Cells["A25"].Value = $"- Tổng số tiền (viết bằng chữ): {ConvertNumberToWord.GetWords(worksheet.Cells["J23"].Value)} đồng";
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void cbHHMaSo_TextChanged(object sender, EventArgs e)
        {
            double data = 0.0;
            foreach (string number in cbHHMaSo.Text.Split(','))
            {
                try
                {
                    double kg = Convert.ToDouble(number) / 10;
                    data += kg;
                }
                catch { }
            }
            cbSoLuongThucXuat.Text = data.ToString();
        }

        private void SaveToPDF(string fileXls, string saveFilePdf)
        {
            // Khởi tạo một ứng dụng Excel
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                // Mở một Workbook hoặc tạo mới nếu không tồn tại
                Workbook wb = excelApp.Workbooks.Open(fileXls);

                Sheets sheets = wb.Sheets;
                Worksheet ws = sheets[1];
                ws.ExportAsFixedFormat2(XlFixedFormatType.xlTypePDF, saveFilePdf);

                // Đóng Workbook mà không lưu thay đổi
                wb.Close(false);
            }
            finally
            {
                // Đóng ứng dụng Excel
                excelApp.Quit();
            }
        }

        private void tbSoPhieu_DoubleClick(object sender, EventArgs e)
        {
            if (tbSoPhieu.ReadOnly == true)
            {
                tbSoPhieu.ReadOnly = false;
            }
            else
            {
                tbSoPhieu.ReadOnly = true;
            }
        }

        private void btXoaHangHoa_Click(object sender, EventArgs e)
        {
            SaveSheetSampleDefault();
        }

        private void btPostDataSheet_Click(object sender, EventArgs e)
        {
            fGoogleSheet fGoogleSheet = new fGoogleSheet();
            fGoogleSheet.ShowDialog();
        }
    }
}
