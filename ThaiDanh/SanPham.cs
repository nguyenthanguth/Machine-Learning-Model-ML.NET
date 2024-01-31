using SpreadsheetGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDanh
{
    internal class SanPham
    {
        public string NguoiNhan { get; set; }

        public string TenSP { get; set; }
        public string DVT { get; set; }
        public string KhoiLuongGam { get; set; }

        public double SoLuongThucXuat { get; set; }
        public string GhiChu { get; set; }

        public string SoPhieu { get; set; }

        public List<SanPham> ListSanPhamInSheet = new List<SanPham> { };
        public SanPham() { }

        public void GetAllSanPham(IWorkbook workbook)
        {
            IWorksheets worksheets = workbook.Worksheets;
            foreach (IWorksheet worksheet in worksheets)
            {
                if (worksheet.Index == 0)
                {
                    continue;
                }

                for (int i = 16; i <= 21; i++)
                {
                    SanPham sanPham = new SanPham();
                    sanPham.NguoiNhan = Convert.ToString(worksheet.Cells[9, 0].Value).Split(':')[1].TrimStart(' ');

                    string name = Convert.ToString(worksheet.Cells[i, 1].Value);
                    if (name.Contains('\n'))
                    {
                        sanPham.TenSP = name.Split('\n')[0];
                        sanPham.KhoiLuongGam = name.Split('\n')[1];
                    }
                    else
                    {
                        sanPham.TenSP = name;
                    }
                    sanPham.DVT = Convert.ToString(worksheet.Cells[i, 4].Value);
                    sanPham.SoLuongThucXuat = Convert.ToDouble(worksheet.Cells[i, 6].Value);
                    sanPham.GhiChu = Convert.ToString(worksheet.Cells[i, 8].Value);
                    sanPham.SoPhieu = worksheet.Name;

                    ListSanPhamInSheet.Add(sanPham);
                }
            }
        }
    }
}
