using SpreadsheetGear;
using SpreadsheetGear.Drawing.Printing;
using SpreadsheetGear.Printing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThaiDanh.Properties;

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

            worksheet.Cells["B17:J21"].ClearContents();

            worksheet.Cells["A24"].Value = $"- Tổng số tiền (viết bằng chữ): ... đồng";
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

                cbHHMaSo.SelectedIndex = index;
                cbHHDonVi.SelectedIndex = index;
                cbHHDonGia.SelectedIndex = index;
                cbHHGhiChu.SelectedIndex = index;
            }
        }

        private void LoopUpdateData()
        {
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
                int rowEnd = 21;
                for (int i = rowStart; i <= rowEnd; i++)
                {
                    string value = Convert.ToString(worksheet.Cells[$"B{i}"].Value);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        worksheet.Cells[$"B{i}"].Value = cbHHTen.Text;
                        worksheet.Cells[$"D{i}"].Value = cbHHMaSo.Text;
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

                fPrintComputer fPrintComputer = new fPrintComputer();
                fPrintComputer.ShowDialog();
                foreach (string print in fPrintComputer.listPrint)
                {
                    WorkbookPrintDocument printDocument = new WorkbookPrintDocument(worksheet, PrintWhat.Sheet);
                    printDocument.PrinterSettings.PrinterName = print;
                    printDocument.Print();
                }
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }

        private void btConvertToWord_Click(object sender, EventArgs e)
        {
            try
            {
                workbookView.GetLock();

                IWorkbook workbook = workbookView.ActiveWorkbook;
                IWorksheet worksheet = workbook.Worksheets["Sample"];

                worksheet.Cells["A24"].Value = $"- Tổng số tiền (viết bằng chữ): {ConvertNumberToWord.GetWords(worksheet.Cells["J22"].Value)} đồng";
            }
            finally
            {
                workbookView.ReleaseLock();
            }
        }
    }
}
