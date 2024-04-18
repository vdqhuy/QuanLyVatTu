using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QuanLyVatTu
{
    public partial class FormCTHDXuatVT : Form
    {
        FormMain frmMain;
        string[] str_dsvt;
        DSVatTu dsvt;
        DSHoaDon dshd;
        DSCTHoaDon dscthd = new DSCTHoaDon(null, null);
        VatTu[] arr_vt = new VatTu[1000];
        bool btnTiepTucWasClicked = false;
        public FormCTHDXuatVT(FormMain frm)
        {
            frmMain = frm;
            InitializeComponent();
            refreshDataGVDSVT();
        }

//Làm mới Data Grid View của các danh sách
        private void refreshDataGVDSVT()
        {
            DSVatTu dsvt = frmMain.Dsvt;
            string str = "";
            str = dsvt.inorderDisplay(dsvt.root, str);
            str_dsvt = str.Split(',');
            int j = 0;
            for (int i = 0; i < str_dsvt.Length - 1; i += 4)
            {
                arr_vt[j] = new VatTu(str_dsvt[i], str_dsvt[i + 1], str_dsvt[i + 2], Convert.ToInt32(str_dsvt[i + 3]));
                j++;
            }
            arr_vt = frmMain.sapXepMangVatTu(arr_vt);
            int count = arr_vt.Count(n => n != null);
            dataGViewDSVT.Rows.Clear();
            for (int i = 0; i < count; i++)
            {
                dataGViewDSVT.Rows.Add(arr_vt[i].Mavt, arr_vt[i].Tenvt, arr_vt[i].Dvt, arr_vt[i].Slton);
            }
        }

        private void refreshDataGVDSCTHD()
        {
            dscthd = frmMain.Dscthd;
            string str = dshd.Tail.Hd.Dscthd.display();
            string[] str_dscthd = str.Split(',');
            dataGViewDSCTHD.Rows.Clear();
            for (int i = 0; i < str_dscthd.Length - 1; i += 4)
            {
                dataGViewDSCTHD.Rows.Add(str_dscthd[i], str_dscthd[i + 1], str_dscthd[i + 2], str_dscthd[i + 3]);
            }
        }

//Kiểm tra xem nút tiếp tục có được nhấn hay không không thì xóa hóa đơn đã lập
        private void FormCTHDXuatVT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnTiepTucWasClicked == false || !Application.OpenForms.OfType<FormNVLapHD>().Any())
            {
                frmMain.Dsvt = new DSVatTu();
                frmMain.loadFileDSVT();
                frmMain.Dscthd = new DSCTHoaDon(null, null);
                frmMain.loadFileDSHD();
                frmMain.Dshd = new DSHoaDon(null, null);
                frmMain.loadFileDSHD();
            }

            frmMain.refreshDataGVDSNVKetNoiDSHDKetNoiDSCTHD();
        }

//Xuất vật tư - thay đổi SLTon, không đủ thì báo lỗi
        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            if (txtMaVT.Text == "" || txtTenVT.Text == "" || txtDVT.Text == ""
                || txtTenVT.Text == " " || txtDVT.Text == " "
                || txtSLXuat.Text == "" || txtDonGia.Text == "" || txtVAT.Text == ""
                || Regex.IsMatch(txtMaVT.Text, "\\s") || !Regex.IsMatch(txtTenVT.Text, "[a-zA-Z0-9 {1,}][^ ]")
                || !Regex.IsMatch(txtDVT.Text, "[a-zA-Z0-9 {1,}][^ ]")
                || txtMaVT.Text.Contains(",") || txtMaVT.Text.Contains("|") || txtMaVT.Text.Contains(" ")
                || txtTenVT.Text.Contains(",") || txtTenVT.Text.Contains("|")
                || txtDVT.Text.Contains(",") || txtDVT.Text.Contains("|"))
            {
                if (txtMaVT.Text == "" || Regex.IsMatch(txtMaVT.Text, "\\s")
                    || txtMaVT.Text.Contains(",") || txtMaVT.Text.Contains("|") || txtMaVT.Text.Contains(" "))
                {
                    pnlMaVT.BackColor = Color.Red;
                }
                if (txtTenVT.Text == "" || txtTenVT.Text == " " || !Regex.IsMatch(txtTenVT.Text, "[a-zA-Z0-9 {1,}][^ ]")
                    || txtTenVT.Text.Contains(",") || txtTenVT.Text.Contains("|"))
                {
                    pnlTenVT.BackColor = Color.Red;
                }
                if (txtDVT.Text == "" || txtDVT.Text == " " || !Regex.IsMatch(txtDVT.Text, "[a-zA-Z0-9 {1,}][^ ]")
                    || txtDVT.Text.Contains(",") || txtDVT.Text.Contains("|"))
                {
                    pnlDVT.BackColor = Color.Red;
                }
                if (txtSLXuat.Text == "")
                {
                    pnlSLXuat.BackColor = Color.Red;
                }
                if (txtDonGia.Text == "")
                {
                    pnlDonGia.BackColor = Color.Red;
                }
                if (txtVAT.Text == "")
                {
                    pnlVAT.BackColor = Color.Red;
                }
            }
            else
            {
                int slxuat;
                double vat;
                if (!int.TryParse(txtSLXuat.Text, out slxuat) || !double.TryParse(txtVAT.Text, out vat) || !int.TryParse(txtDonGia.Text, out _))
                {
                    if (!int.TryParse(txtSLXuat.Text, out slxuat))
                        pnlSLXuat.BackColor = Color.Red;
                    if (!double.TryParse(txtVAT.Text, out vat))
                    {
                        pnlVAT.BackColor = Color.Red;
                    }
                    if (!int.TryParse(txtDonGia.Text, out _))
                    {
                        pnlDonGia.BackColor = Color.Red;
                    }
                }
                else
                {
                    dsvt = frmMain.Dsvt;
                    if (dsvt.tonTaiMaVT(dsvt.root, txtMaVT.Text))
                    {
                        bool daXuat = dsvt.timVatTuDeXuat(dsvt.root, txtMaVT.Text, slxuat);
                        if (daXuat)
                        {
                            refreshDataGVDSVT();

                            dshd = frmMain.Dshd;
                            dscthd = frmMain.Dscthd;

                            CTHoaDon cthd = new CTHoaDon(txtMaVT.Text, slxuat, txtDonGia.Text, vat);
                            HoaDon hd = dshd.Tail.Hd;
                            if (!hd.cTHDExist(cthd))
                            {
                                dscthd.addLast(cthd);
                                hd.Dscthd.addLast(cthd);
                            }
                            else
                            {
                                dscthd.editAny(cthd, hd);
                            }
                            refreshDataGVDSCTHD();
                        }
                        else
                        {
                            MessageBox.Show("Số lượng tồn không đủ để xuất",
                                "Không thể xuất hàng",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy vật tư có mã " + txtMaVT.Text);
                    }
                }
            }
        }

        private void dataGViewDSVT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index != -1)
            {
                DataGridViewRow SelectedRows = dataGViewDSVT.Rows[index];
                txtMaVT.Text = SelectedRows.Cells[0].Value?.ToString();
                txtTenVT.Text = SelectedRows.Cells[1].Value?.ToString();
                txtDVT.Text = SelectedRows.Cells[2].Value?.ToString();
                pnlMaVT.BackColor = Color.Transparent;
                pnlTenVT.BackColor = Color.Transparent;
                pnlDVT.BackColor = Color.Transparent;
                pnlSLXuat.BackColor = Color.Transparent;
            }
        }

//Loại bỏ vật tư đã xuất
        private void btnXoaVT_Click(object sender, EventArgs e)
        {
            dshd = frmMain.Dshd;
            DSCTHoaDon dscthdTemp = dshd.Tail.Hd.Dscthd;
            if (txtXoaMaVT.Text == "" || Regex.IsMatch(txtXoaMaVT.Text, "\\s")
                || txtXoaMaVT.Text.Contains(",") || txtXoaMaVT.Text.Contains(".") || txtXoaMaVT.Text.Contains(" "))
            {
                pnlXoaMaVT.BackColor = Color.Red;
            }
            else if (!dscthdTemp.elementWithMaVTExist(txtXoaMaVT.Text))
            {
                MessageBox.Show("Không tìm thấy vật tư có mã " + txtXoaMaVT.Text,
                    "Vật tư không tồn tại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtXoaMaVT.Clear();
                txtXoaMaVT.Focus();
            }
            else
            {
                dshd = frmMain.Dshd;
                dscthd = frmMain.Dscthd;
                int pos = dscthd.findElementWithMaVT(txtXoaMaVT.Text);
                int pos2 = dshd.Tail.Hd.Dscthd.findElementWithMaVT(txtXoaMaVT.Text);
                if (pos != -1 && pos2 != -1)
                {
                    NodeCTHD p = dshd.Tail.Hd.Dscthd.getNodeAt(pos2);
                    dsvt.timVatTuDeNhap(dsvt.root, txtXoaMaVT.Text, p.Cthd.Soluong);

                    if (dscthd.Size == 1)
                    {
                        dscthd = new DSCTHoaDon(null, null);
                    }
                    else if (pos == dscthd.Size)
                    {
                        dscthd.removeLast();
                    }
                    else
                    {
                        dscthd.removeAny(pos);
                    }

                    if (dshd.Tail.Hd.Dscthd.Size == 1)
                    {
                        dshd.Tail.Hd.Dscthd = new DSCTHoaDon(null, null);
                    }
                    else if (pos2 == dshd.Tail.Hd.Dscthd.Size)
                    {
                        dshd.Tail.Hd.Dscthd.removeLast();
                    }
                    else
                    {
                        dshd.Tail.Hd.Dscthd.removeAny(pos2);
                    }

                    refreshDataGVDSVT();
                    refreshDataGVDSCTHD();
                }
            }
        }

//Tiếp tục tới form FormNVLapHD
        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            dshd = frmMain.Dshd;
            if (!dshd.Tail.Hd.Dscthd.isEmpty())
            {
                btnTiepTucWasClicked = true;
                FormNVLapHD frmNVLapHD = new FormNVLapHD(frmMain);
                frmNVLapHD.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hóa đơn còn trống. Hãy nhập hàng trước khi lưu hóa đơn",
                    "Hóa đơn còn trống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void dataGViewDSCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index != -1)
            {
                DataGridViewRow SelectedRows = dataGViewDSCTHD.Rows[index];
                txtXoaMaVT.Text = SelectedRows.Cells[0].Value?.ToString();
            }
        }

        private void txtMaVT_TextChanged(object sender, EventArgs e)
        {
            dsvt = frmMain.Dsvt;
            if (dsvt.tonTaiMaVT(dsvt.root, txtMaVT.Text))
            {
                VatTu vt = dsvt.timVatTuTheoMa(dsvt.root, txtMaVT.Text);
                txtTenVT.Text = vt.Tenvt;
                txtTenVT.Enabled = false;
                txtDVT.Text = vt.Dvt;
                txtDVT.Enabled = false;
            }
            else
            {
                txtTenVT.Enabled = true;
                txtDVT.Enabled = true;
            }
        }
    }
}
