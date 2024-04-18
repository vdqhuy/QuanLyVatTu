using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QuanLyVatTu
{
    public partial class FormNVLapHD : Form
    {
        FormMain frmMain;
        DSVatTu dsvt;
        DSNhanVien dsnv;
        DSHoaDon dshd;
        DSCTHoaDon dscthd;
        public FormNVLapHD(FormMain frm)
        {
            frmMain = frm;
            InitializeComponent();
            refreshDataGVDSNV();
        }

//Làm mới Data Grid View của các danh sách
        private void refreshDataGVDSNV()
        {
            dsnv = frmMain.Dsnv;
            dataGViewDSNV.Rows.Clear();
            for (int i = 0; i < dsnv.Amount; i++)
            {
                if (dsnv.indexAt(i) == null)
                    continue;
                dataGViewDSNV.Rows.Add(dsnv.indexAt(i).Manv, dsnv.indexAt(i).Ho, dsnv.indexAt(i).Ten, dsnv.indexAt(i).Phai);
            }
        }

        private void refreshDataGVDSNVTimKiem(NhanVien nv)
        {
            dataGViewDSNV.Rows.Clear();
            dataGViewDSNV.Rows.Add(nv.Manv, nv.Ho, nv.Ten, nv.Phai);
        }

        private void dataGViewDSNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index != -1)
            {
                DataGridViewRow SelectedRows = dataGViewDSNV.Rows[index];
                txtMaNV.Text = SelectedRows.Cells[0].Value?.ToString();
                txtHoNV.Text = SelectedRows.Cells[1].Value?.ToString();
                txtTenNV.Text = SelectedRows.Cells[2].Value?.ToString();
                string phai = SelectedRows.Cells[3].Value?.ToString();
                if (phai == rBtnNam.Text)
                    rBtnNam.Checked = true;
                else
                    rBtnNu.Checked = true;

                pnlMaNV.BackColor = Color.Transparent;
                pnlHoNV.BackColor = Color.Transparent;
                pnlTenNV.BackColor = Color.Transparent;
            }
        }

        private void luuDSNV()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsnv.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < dsnv.Amount; i++)
            {
                sWriter.Write(dsnv.indexAt(i).Manv + ",");
                sWriter.Write(dsnv.indexAt(i).Ho + ",");
                sWriter.Write(dsnv.indexAt(i).Ten + ",");
                sWriter.Write(dsnv.indexAt(i).Phai + ",");
                sWriter.WriteLine();
            }
            sWriter.Flush();
            fs.Close();
        }

        private void luuDSNVVaHD()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsnv-dshd.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < dsnv.Amount; i++)
            {
                sWriter.Write(dsnv.indexAt(i).Manv + ",");
                sWriter.Write(dsnv.indexAt(i).Ho + ",");
                sWriter.Write(dsnv.indexAt(i).Ten + ",");
                sWriter.Write(dsnv.indexAt(i).Phai + ",");
                string str = dsnv.indexAt(i).Dshd.display();
                sWriter.Write(str);
                sWriter.WriteLine("|");
            }
            sWriter.Flush();
            fs.Close();
        }

        private void luuDSVT()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsvt.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            dsvt = frmMain.Dsvt;
            string str = "";
            str = dsvt.inorderDisplay(dsvt.root, str);
            string[] str_dsvt = str.Split(',');
            for (int i = 0; i < str_dsvt.Length - 1; i += 4)
            {
                sWriter.Write(str_dsvt[i] + ",");
                sWriter.Write(str_dsvt[i + 1] + ",");
                sWriter.Write(str_dsvt[i + 2] + ",");
                sWriter.Write(str_dsvt[i + 3] + ",");
                sWriter.WriteLine();
            }
            sWriter.Flush();
            fs.Close();
        }

        private void luuDSCTHD()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dscthd.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            dscthd = frmMain.Dscthd;
            string str = dscthd.display();
            string[] str_dscthd = str.Split(',');
            for (int i = 0; i < str_dscthd.Length - 1; i += 4)
            {
                sWriter.Write(str_dscthd[i] + ",");
                sWriter.Write(str_dscthd[i + 1] + ",");
                sWriter.Write(str_dscthd[i + 2] + ",");
                sWriter.Write(str_dscthd[i + 3] + ",");
                sWriter.WriteLine();
            }
            sWriter.Flush();
            fs.Close();
        }

        private void luuDSHDVaDSCTHD()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dshd-dscthd.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            dshd = frmMain.Dshd;
            string str = dshd.display();
            string[] str_dshd = str.Split(',');
            for (int i = 0; i < str_dshd.Length - 1; i += 3)
            {
                sWriter.Write(str_dshd[i] + ",");
                sWriter.Write(str_dshd[i + 1] + ",");
                sWriter.Write(str_dshd[i + 2] + ",");

                HoaDon hd = new HoaDon(str_dshd[i], str_dshd[i + 1], str_dshd[i + 2]);
                int pos = dshd.findElement(hd);
                NodeHD p = dshd.getNodeAt(pos);
                str = p.Hd.Dscthd.display();
                sWriter.Write(str);
                sWriter.WriteLine("|");
            }
            sWriter.Flush();
            fs.Close();
        }

// Lưu tất cả các thông tin liên quan đến hóa đơn đang lập
        private void btnLuuTatCa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" || txtHoNV.Text == "" || txtHoNV.Text == " "
                || txtTenNV.Text == "" || txtTenNV.Text == " "
                || Regex.IsMatch(txtMaNV.Text, "\\s") || !Regex.IsMatch(txtHoNV.Text, "[a-zA-Z0-9 {1,}][^ ]")
                || !Regex.IsMatch(txtTenNV.Text, "[a-zA-Z0-9 {1,}][^ ]")
                || txtMaNV.Text.Contains(",") || txtMaNV.Text.Contains("|") || txtMaNV.Text.Contains(" ")
                || txtHoNV.Text.Contains(",") || txtHoNV.Text.Contains("|")
                || txtTenNV.Text.Contains(",") || txtTenNV.Text.Contains("|"))
            {
                if (txtMaNV.Text == "" || Regex.IsMatch(txtMaNV.Text, "\\s")
                    || txtMaNV.Text.Contains(",") || txtMaNV.Text.Contains("|") || txtMaNV.Text.Contains(" "))
                {
                    pnlMaNV.BackColor = Color.Red;
                }
                if (txtHoNV.Text == "" || txtHoNV.Text == " " || !Regex.IsMatch(txtHoNV.Text, "[a-zA-Z0-9 {1,}][^ ]")
                    || txtHoNV.Text.Contains(",") || txtHoNV.Text.Contains("|"))
                {
                    pnlHoNV.BackColor = Color.Red;
                }
                if (txtTenNV.Text == "" || txtTenNV.Text == " " || !Regex.IsMatch(txtTenNV.Text, "[a-zA-Z0-9 {1,}][^ ]")
                    || txtTenNV.Text.Contains(",") || txtTenNV.Text.Contains("|"))
                {
                    pnlTenNV.BackColor = Color.Red;
                }
            }
            else
            {
                dshd = frmMain.Dshd;
                HoaDon hd = dshd.Tail.Hd;
                string phai = "";
                if (rBtnNam.Checked)
                    phai = "Nam";
                else
                    phai = "Nữ";
                NhanVien nv = new NhanVien(txtMaNV.Text, txtHoNV.Text, txtTenNV.Text, phai);
                if (!dsnv.tonTaiNhanVien(nv))
                {
                    dsnv.themNhanVien(nv);
                    dsnv.sapXepNhanVien();
                }
                int index = dsnv.timNhanVien(nv);
                dsnv.themHoaDon(hd, index);
                luuDSVT();
                frmMain.luuDSHD();
                luuDSCTHD();
                luuDSHDVaDSCTHD();
                luuDSNV();
                luuDSNVVaHD();
                frmMain.refreshDataGVDSNVKetNoiDSHDKetNoiDSCTHD();

                MessageBox.Show("Lưu hóa đơn thành công thành công",
                    "Lưu vào file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                if (Application.OpenForms.OfType<FormCTHDNhapVT>().Any())
                {
                    Application.OpenForms["FormCTHDNhapVT"].Close();
                }
                else
                {
                    Application.OpenForms["FormCTHDXuatVT"].Close();
                }
                Close();
            }
        }

//Tìm kiếm nhân viên
        private void btnTK_Click(object sender, EventArgs e)
        {
            dsnv = frmMain.Dsnv;
            if (txtMaNVTK.Text == "" || Regex.IsMatch(txtMaNVTK.Text, "\\s")
                || txtMaNVTK.Text.Contains(",") || txtMaNVTK.Text.Contains(".") || txtMaNVTK.Text.Contains(" "))
            {
                pnlMaNVTK.BackColor = Color.Red;
            }
            else if (dsnv.tonTaiNhanVienCoMa(txtMaNVTK.Text))
            {
                int index = dsnv.timNhanVienTheoMa(txtMaNVTK.Text);
                NhanVien nv = dsnv.indexAt(index);
                refreshDataGVDSNVTimKiem(nv);
                pnlMaNVTK.BackColor = Color.Transparent;
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã NV " + txtMaNVTK.Text,
                    "Mã NV không tồn tại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtMaNVTK.Clear();
                txtMaNVTK.Focus();
                refreshDataGVDSNV();
                pnlMaNVTK.BackColor = Color.Transparent;
            }
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            pnlMaNV.BackColor = Color.Transparent;
            if (dsnv.tonTaiNhanVienCoMa(txtMaNV.Text))
            {
                int index = dsnv.timNhanVienTheoMa(txtMaNV.Text);
                NhanVien nv = dsnv.indexAt(index);
                txtTenNV.Text = nv.Ten;
                txtTenNV.Enabled = false;
                txtHoNV.Text = nv.Ho;
                txtHoNV.Enabled = false;
                string phai = nv.Phai;
                if (phai == "Nam")
                    rBtnNam.Checked = true;
                else
                    rBtnNu.Checked = true;
                rBtnNam.Enabled = false;
                rBtnNu.Enabled = false;
            }
            else
            {
                txtHoNV.Clear();
                txtHoNV.Enabled = true;
                txtTenNV.Clear();
                txtTenNV.Enabled = true;
                rBtnNam.Checked = true;
                rBtnNam.Enabled = true;
                rBtnNu.Enabled = true;
            }
        }

        private void txtHoNV_TextChanged(object sender, EventArgs e)
        {
            pnlHoNV.BackColor = Color.Transparent;
        }

        private void txtTenNV_TextChanged(object sender, EventArgs e)
        {
            pnlTenNV.BackColor = Color.Transparent;
        }
    }
}
