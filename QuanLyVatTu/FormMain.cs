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
using System.Globalization;
using System.Text.RegularExpressions;

namespace QuanLyVatTu
{
    public partial class FormMain : Form
    {
        DSVatTu dsvt;
        string[] str_dsvt;
        VatTu[] arr_vt;
        string[] str_dshd;
        DSNhanVien dsnv;
        DSHoaDon dshd;
        DSCTHoaDon dscthd;


        public DSVatTu Dsvt { get => dsvt; set => dsvt = value; }
        public string[] Str_dsvt { get => str_dsvt; set => str_dsvt = value; }
        public DSNhanVien Dsnv { get => dsnv; set => dsnv = value; }
        public DSCTHoaDon Dscthd { get => dscthd; set => dscthd = value; }
        public DSHoaDon Dshd { get => dshd; set => dshd = value; }

        public FormMain()
        {
            InitializeComponent();
            loadFileDSVT();
        }

        private void tabCtrlQLVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabCtrlQLVatTu.SelectedIndex)
            {

                case 0:
                    {
                        loadFileDSVT();
                        break;
                    }
                case 1:
                    {
                        loadFileDSHD();
                        loadFileDSNV();
                        loadFileDSCTHD();
                        loadFileNVKetNoiDSHD();
                        loadFileHDKetNoiDSCTHD();
                        refreshDataGVDSNV();
                        break;
                    }
                case 2:
                    {
                        loadFileDSNV();
                        loadFileDSHD();
                        loadFileDSCTHD();
                        loadFileHDKetNoiDSCTHD();
                        loadFileNVKetNoiDSHD();
                        refreshDataGVDSNVKetNoiDSHDKetNoiDSCTHD();
                        break;
                    }
                case 3:
                    {
                        loadFileDSVT();
                        loadFileDSHD();
                        loadFileDSCTHD();
                        loadFileHDKetNoiDSCTHD();
                        refreshDataGVThongKeDT();
                        break;
                    }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?",
                "Exit",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

//Load các file chứa các danh sách
        public void loadFileDSVT()
        {
            dsvt = new DSVatTu();
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsvt.txt", FileMode.OpenOrCreate);
            StreamReader sReader = new StreamReader(fs, Encoding.UTF8);
            string str = "";

            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                str += line;
            }
            sReader.Close();
            fs.Close();
            str_dsvt = str.Split(',');
            for (int i = 0; i < str_dsvt.Length - 1; i += 4)
            {
                VatTu vt = new VatTu(str_dsvt[i], str_dsvt[i + 1], str_dsvt[i + 2], Convert.ToInt32(str_dsvt[i + 3]));
                dsvt.themVatTu(dsvt.root, vt);
            }

            refreshDataGVDSVT();
        }

        public void loadFileDSNV()
        {
            dsnv = new DSNhanVien(500);
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsnv.txt", FileMode.OpenOrCreate);
            StreamReader sReader = new StreamReader(fs, Encoding.UTF8);
            string str = "";

            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                str += line;
            }
            sReader.Close();
            fs.Close();
            string[] str_dsnv = str.Split(',');
            for (int i = 0; i < str_dsnv.Length - 1; i += 4)
            {
                NhanVien nv = new NhanVien(str_dsnv[i], str_dsnv[i + 1], str_dsnv[i + 2], str_dsnv[i + 3]);
                dsnv.themNhanVien(nv);
            }
            dsnv.sapXepNhanVien();
            refreshDataGVDSNV();
        }

        public void loadFileDSHD()
        {
            dshd = new DSHoaDon(null, null);
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dshd.txt", FileMode.OpenOrCreate);
            StreamReader sReader = new StreamReader(fs, Encoding.UTF8);
            string str = "";

            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                str += line;
            }
            sReader.Close();
            fs.Close();
            str_dshd = str.Split(',');
            for (int i = 0; i < str_dshd.Length - 1; i += 3)
            {
                HoaDon hd = new HoaDon(str_dshd[i], str_dshd[i + 1], str_dshd[i + 2]);
                dshd.addLast(hd);
            }
        }

        private void loadFileNVKetNoiDSHD()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsnv-dshd.txt", FileMode.OpenOrCreate);
            StreamReader sReader = new StreamReader(fs, Encoding.UTF8);
            string str = "";

            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                str += line;
            }
            sReader.Close();
            fs.Close();
            string[] str_dsnv_dshd = str.Split('|');
            for (int i = 0; i < str_dsnv_dshd.Length - 1; i++)
            {
                string[] str_nv_hd = str_dsnv_dshd[i].Split(',');
                NhanVien nv = new NhanVien(str_nv_hd[0], str_nv_hd[1], str_nv_hd[2], str_nv_hd[3]);
                if (!dsnv.tonTaiNhanVienCoMa(str_nv_hd[0]))
                {
                    dsnv.themNhanVien(nv);
                    dsnv.sapXepNhanVien();
                }
                if (str_nv_hd.Length > 5)
                {
                    for (int j = 4; j < str_nv_hd.Length - 1; j += 3)
                    {
                        HoaDon hd = new HoaDon(str_nv_hd[j], str_nv_hd[j + 1], str_nv_hd[j + 2]);
                        int index = dsnv.timNhanVien(nv);
                        dsnv.themHoaDon(hd, index);
                    }
                }
            }
        }

        public void loadFileDSCTHD()
        {
            dscthd = new DSCTHoaDon(null, null);
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dscthd.txt", FileMode.OpenOrCreate);
            StreamReader sReader = new StreamReader(fs, Encoding.UTF8);
            string str = "";

            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                str += line;
            }
            sReader.Close();
            fs.Close();
            string[] str_dscthd = str.Split(',');
            for (int i = 0; i < str_dscthd.Length - 1; i += 4)
            {
                CTHoaDon cthd = new CTHoaDon(str_dscthd[i], Convert.ToInt32(str_dscthd[i + 1]), str_dscthd[i + 2], Convert.ToDouble(str_dscthd[i + 3]));
                dscthd.addLast(cthd);
            }
        }

        public void loadFileHDKetNoiDSCTHD()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dshd-dscthd.txt", FileMode.OpenOrCreate);
            StreamReader sReader = new StreamReader(fs, Encoding.UTF8);
            string str = "";
            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                str += line;
            }
            sReader.Close();
            fs.Close();
            string[] str_dshd_dscthd = str.Split('|');
            for (int i = 0; i < str_dshd_dscthd.Length - 1; i++)
            {
                string[] str_hd_cthd = str_dshd_dscthd[i].Split(',');
                HoaDon hd = new HoaDon(str_hd_cthd[0], str_hd_cthd[1], str_hd_cthd[2]);
                if (!dshd.elementWithSoHDExist(str_hd_cthd[0]))
                {
                    dshd.addLast(hd);
                }
                if (str_hd_cthd.Length > 5)
                {
                    int pos = dshd.findElement(hd);
                    NodeHD p = dshd.getNodeAt(pos);
                    for (int j = 3; j < str_hd_cthd.Length - 1; j += 4)
                    {
                        CTHoaDon cthd = new CTHoaDon(str_hd_cthd[j], Convert.ToInt32(str_hd_cthd[j + 1]), str_hd_cthd[j + 2], Convert.ToDouble(str_hd_cthd[j + 3]));
                        dshd.addCTHD(cthd, p.Hd);
                    }
                }
            }
        }

//Làm mới Data Grid View của các danh sách
        public void refreshDataGVDSVT()
        {
            arr_vt = new VatTu[1000];
            string str = "";
            str = dsvt.inorderDisplay(dsvt.root, str);
            str_dsvt = str.Split(',');
            int j = 0;
            for (int i = 0; i < str_dsvt.Length - 1; i += 4)
            {
                arr_vt[j] = new VatTu(str_dsvt[i], str_dsvt[i + 1], str_dsvt[i + 2], Convert.ToInt32(str_dsvt[i + 3]));
                j++;
            }
            arr_vt = sapXepMangVatTu(arr_vt);
            int count = arr_vt.Count(n => n != null);
            dataGViewDSVT.Rows.Clear();
            for (int i = 0; i < count; i++)
            {
                dataGViewDSVT.Rows.Add(arr_vt[i].Mavt, arr_vt[i].Tenvt, arr_vt[i].Dvt, arr_vt[i].Slton);
            }
        }

        private void refreshDataGVDSNV()
        {
            dataGViewDSNV.Rows.Clear();
            for (int i = 0; i < dsnv.Amount; i++)
            {
                if (dsnv.indexAt(i) == null)
                    continue;
                dataGViewDSNV.Rows.Add(dsnv.indexAt(i).Manv, dsnv.indexAt(i).Ho, dsnv.indexAt(i).Ten, dsnv.indexAt(i).Phai);
            }
        }

        public void refreshDataGVDSNVKetNoiDSHDKetNoiDSCTHD()
        {
            string str = dshd.display();
            str_dshd = str.Split(',');
            dataGViewDSHD.Rows.Clear();
            string col4 = "";
            double trigiahd = 0;
            for (int i = 0; i < str_dshd.Length - 1; i += 3)
            {
                string col1 = str_dshd[i];
                string col2 = str_dshd[i + 1];
                string col3 = str_dshd[i + 2];
                
                for (int j = 0; j < dsnv.Amount; j++)
                {
                    if (dsnv.indexAt(j).Dshd.elementWithSoHDExist(str_dshd[i]))
                    {
                        col4 = dsnv.indexAt(j).Ho + " " + dsnv.indexAt(j).Ten;
                        break;
                    }
                }

                int pos = dshd.findElementWithSoHD(col1);
                NodeHD p = dshd.getNodeAt(pos);
                HoaDon hd = p.Hd;
                DSCTHoaDon dscthdTemp = hd.Dscthd;
                str = dscthdTemp.display();
                if (str == "")
                {
                    loadFileHDKetNoiDSCTHD();
                    p = dshd.getNodeAt(pos);
                    hd = p.Hd;
                    dscthdTemp = hd.Dscthd;
                    str = dscthdTemp.display();
                }
                string[] str_dscthd = str.Split(',');
                for (int k = 1; k < str_dscthd.Length - 1; k += 4)
                {
                    double dongia = Convert.ToDouble(str_dscthd[k + 1]);
                    double vat = Convert.ToDouble(str_dscthd[k + 2]);
                    int soluong = Convert.ToInt32(str_dscthd[k]);
                    trigiahd += (dongia + dongia * vat) * soluong;
                }
                string col5 = trigiahd.ToString();
                trigiahd = 0;

                dataGViewDSHD.Rows.Add(col1, col2, col3, col4, col5);
            }
        }

        public void refreshDataGVDSHDTimKiem(HoaDon hd)
        {
            dataGViewDSHD.Rows.Clear();

            string col1 = hd.Sohd;
            string col2 = hd.Ngaylap;
            string col3 = hd.Loai;
            string col4 = "";
            string col5 = "";
            for (int j = 0; j < dsnv.Amount; j++)
            {
                if (dsnv.indexAt(j).Dshd.elementWithSoHDExist(col1))
                {
                    col4 = dsnv.indexAt(j).Ho + " " + dsnv.indexAt(j).Ten;
                    break;
                }
            }

            double trigiahd = 0;
            string str = hd.Dscthd.display();
            if (str == "")
            {
                loadFileHDKetNoiDSCTHD();
                str = hd.Dscthd.display();
            }
            string[] str_dscthd = str.Split(',');
            for (int k = 1; k < str_dscthd.Length - 1; k += 4)
            {
                double dongia = Convert.ToDouble(str_dscthd[k + 1]);
                double vat = Convert.ToDouble(str_dscthd[k + 2]);
                int soluong = Convert.ToInt32(str_dscthd[k]);
                trigiahd += (dongia + dongia * vat) * soluong;
            }
            col5 = trigiahd.ToString();

            dataGViewDSHD.Rows.Add(col1, col2, col3, col4, col5);
        }

        private void refreshDataGViewDSHDFromDateToDate()
        {
            DateTime tuDate = dateTPickerTuNgayHD.Value;
            DateTime denDate = dateTPickerDenNgayHD.Value;

            string str = dshd.display();
            str_dshd = str.Split(',');
            string col4 = "";
            double trigiahd = 0;
            dataGViewDSHD.Rows.Clear();
            for (int i = 0; i < str_dshd.Length - 1; i += 3)
            {
                string col1 = str_dshd[i];
                string col2 = str_dshd[i + 1];
                string col3 = str_dshd[i + 2];
                DateTime datehd = DateTime.ParseExact(col2, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (datehd.Date >= tuDate.Date && datehd.Date <= denDate.Date)
                {
                    for (int j = 0; j < dsnv.Amount; j++)
                    {
                        if (dsnv.indexAt(j).Dshd.elementWithSoHDExist(str_dshd[i]))
                        {
                            col4 = dsnv.indexAt(j).Ho + " " + dsnv.indexAt(j).Ten;
                            break;
                        }
                    }

                    int pos = dshd.findElementWithSoHD(col1);
                    NodeHD p = dshd.getNodeAt(pos);
                    HoaDon hd = p.Hd;
                    DSCTHoaDon dscthdTemp = hd.Dscthd;
                    str = dscthdTemp.display();
                    if (str == "")
                    {
                        loadFileHDKetNoiDSCTHD();
                        p = dshd.getNodeAt(pos);
                        hd = p.Hd;
                        dscthdTemp = hd.Dscthd;
                        str = dscthdTemp.display();
                    }
                    string[] str_dscthd = str.Split(',');
                    for (int k = 1; k < str_dscthd.Length - 1; k += 4)
                    {
                        double dongia = Convert.ToDouble(str_dscthd[k + 1]);
                        double vat = Convert.ToDouble(str_dscthd[k + 2]);
                        int soluong = Convert.ToInt32(str_dscthd[k]);
                        trigiahd += (dongia + dongia * vat) * soluong;
                    }
                    string col5 = trigiahd.ToString();
                    trigiahd = 0;

                    dataGViewDSHD.Rows.Add(col1, col2, col3, col4, col5);
                }
            }
        }

        private void refreshDataGVThongKeDT()
        {
            DateTime tuDate = dateTPickerTuNgayDT.Value;
            DateTime denDate = dateTPickerDenNgayDT.Value;

            string str = "";
            str = dsvt.inorderDisplayMaVT(dsvt.root, str);
            string[] str_dsdoanhthu = str.Split(',');

            str = dshd.display();
            str_dshd = str.Split(',');
            for (int i = 0; i < str_dshd.Length - 1; i += 3)
            {
                if (str_dshd[i + 2] == "X")
                {
                    int pos = dshd.findElementWithSoHD(str_dshd[i]);
                    NodeHD p = dshd.getNodeAt(pos);
                    HoaDon hd = p.Hd;
                    DateTime ngaylap = DateTime.ParseExact(hd.Ngaylap, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (ngaylap.Date >= tuDate.Date && ngaylap.Date <= denDate.Date)
                    {
                        str = hd.Dscthd.display();
                        if (str == "")
                        {
                            loadFileHDKetNoiDSCTHD();
                            p = dshd.getNodeAt(pos);
                            hd = p.Hd;
                            str = hd.Dscthd.display();
                        }
                        string[] str_dscthd = str.Split(',');
                        for (int j = 0; j < str_dscthd.Length - 1; j += 4)
                        {
                            string mavt = str_dscthd[j];
                            int soluong = Convert.ToInt32(str_dscthd[j + 1]);
                            double dongia = Convert.ToDouble(str_dscthd[j + 2]);
                            double vat = Convert.ToDouble(str_dscthd[j + 3]);
                            double trigia = (dongia + dongia * vat) * soluong;
                            for (int k = 0; k < str_dsdoanhthu.Length - 1; k += 2)
                            {
                                if (mavt == str_dsdoanhthu[k])
                                {
                                    str_dsdoanhthu[k + 1] = (Convert.ToDouble(str_dsdoanhthu[k + 1]) + trigia).ToString();
                                }
                            }
                        }
                    }
                }
            }

            for (int i = str_dsdoanhthu.Length - 1; i >= 0; i -= 2)
            {
                bool stop = true;
                for (int j = 0; j < i; j += 2)
                {
                    if (j + 3 < i)
                    {
                        if (Convert.ToDouble(str_dsdoanhthu[j + 3]) > Convert.ToDouble(str_dsdoanhthu[j + 1]))
                        {
                            string temp1 = str_dsdoanhthu[j];
                            string temp2 = str_dsdoanhthu[j + 1];
                            str_dsdoanhthu[j] = str_dsdoanhthu[j + 2];
                            str_dsdoanhthu[j + 1] = str_dsdoanhthu[j + 3];
                            str_dsdoanhthu[j + 2] = temp1;
                            str_dsdoanhthu[j + 3] = temp2;
                            stop = false;
                        }
                    }
                }
                if (stop)
                    break;
            }

            dataGVThongKeDT.Rows.Clear();
            int _10vattu = 10;
            if (str_dsdoanhthu.Length <= 10)
            {
                _10vattu = str_dsdoanhthu.Length - 1;
            }
            for (int i = 0; i < _10vattu * 2; i += 2)
            {
                try
                {
                    VatTu vt = dsvt.timVatTuTheoMa(dsvt.root, str_dsdoanhthu[i]);
                    if (vt != null)
                    {
                        dataGVThongKeDT.Rows.Add(vt.Mavt, vt.Tenvt, vt.Dvt, vt.Slton, str_dsdoanhthu[i + 1]);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
        }

//Lưu file các danh sách
        private void luuDSVT()
        {
            string str = "";
            str = dsvt.inorderDisplay(dsvt.root, str);
            str_dsvt = str.Split(',');
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsvt.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
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

        private void luuDSNV()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dsnv.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < dsnv.Amount; i++)
            {
                if (dsnv.indexAt(i) != null)
                {
                    sWriter.Write(dsnv.indexAt(i).Manv + ",");
                    sWriter.Write(dsnv.indexAt(i).Ho + ",");
                    sWriter.Write(dsnv.indexAt(i).Ten + ",");
                    sWriter.Write(dsnv.indexAt(i).Phai + ",");
                    sWriter.WriteLine();
                }
            }
            sWriter.Flush();
            fs.Close();
        }

        public void luuDSHD()
        {
            string str = dshd.display();
            str_dshd = str.Split(',');
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dshd.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < str_dshd.Length - 1; i += 3)
            {
                sWriter.Write(str_dshd[i] + ",");
                sWriter.Write(str_dshd[i + 1] + ",");
                sWriter.Write(str_dshd[i + 2] + ",");
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
                if (dsnv.indexAt(i) != null)
                {
                    sWriter.Write(dsnv.indexAt(i).Manv + ",");
                    sWriter.Write(dsnv.indexAt(i).Ho + ",");
                    sWriter.Write(dsnv.indexAt(i).Ten + ",");
                    sWriter.Write(dsnv.indexAt(i).Phai + ",");
                    string str = dsnv.indexAt(i).Dshd.display();
                    sWriter.Write(str);
                    sWriter.WriteLine("|");
                }
            }
            sWriter.Flush();
            fs.Close();
        }

        private void luuDSCTHD()
        {
            FileStream fs = new FileStream("C:\\Users\\vdqhu\\Documents\\Study\\PTIT\\Nam 3\\HK2\\CTDL&GT\\QuanLyVatTu\\QuanLyVatTu\\dscthd.txt", FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
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

//Các phương thức liên quan tới DSVT
        public VatTu[] sapXepMangVatTu(VatTu[] arr_dsvt)
        {
            int count = arr_dsvt.Count(n => n != null);
            for (int i = count - 1; i >= 0; i--)
            {
                bool stop = true;
                for (int j = 0; j < i; j++)
                {
                    if (string.Compare(arr_dsvt[j].Tenvt, arr_dsvt[j + 1].Tenvt) > 0)
                    {
                        VatTu temp = arr_dsvt[j];
                        arr_dsvt[j] = arr_dsvt[j + 1];
                        arr_dsvt[j + 1] = temp;
                        stop = false;
                    }
                }
                if (stop)
                    break;
            }
            return arr_dsvt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaVT.Text == "" || txtTenVT.Text == "" || txtTenVT.Text == " " || txtDVT.Text == "" || txtDVT.Text == " " || txtSLTon.Text == ""
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
                    if (txtSLTon.Text == "")
                    {
                        pnlSLTon.BackColor = Color.Red;
                    }
                }
                else
                {
                    if (!dsvt.tonTaiMaVT(dsvt.root, txtMaVT.Text))
                    {
                        VatTu vt = new VatTu(txtMaVT.Text, txtTenVT.Text, txtDVT.Text, Convert.ToInt32(txtSLTon.Text));
                        dsvt.themVatTu(dsvt.root, vt);
                        luuDSVT();
                        refreshDataGVDSVT();
                        MessageBox.Show("Thêm vật tư thành công",
                            "Thêm VT",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Mã VT đã tồn tại. Vui lòng nhập mã VT khác",
                                "Mã vật tư đã tồn tại",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        txtMaVT.Clear();
                        txtTenVT.Clear();
                        txtDVT.Clear();
                        txtSLTon.Clear();
                        txtSLTon.Enabled = true;
                        txtMaVT.Focus();
                    }
                }
            }
            catch (FormatException)
            {
                pnlSLTon.BackColor = Color.Red;
            }
        }

        private void btnXoaVT_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaVT.Text == "" || txtTenVT.Text == "" || txtTenVT.Text == " " || txtDVT.Text == "" || txtDVT.Text == " " || txtSLTon.Text == ""
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
                    if (txtSLTon.Text == "")
                    {
                        pnlSLTon.BackColor = Color.Red;
                    }
                }
                else
                {
                    VatTu vt = new VatTu(txtMaVT.Text, txtTenVT.Text, txtDVT.Text, Convert.ToInt32(txtSLTon.Text));
                    if (dsvt.tonTaiVatTu(dsvt.root, vt))
                    {
                        dsvt.xoaVatTu(vt);
                        luuDSVT();
                        refreshDataGVDSVT();
                        MessageBox.Show("Xóa vật tư thành công",
                            "Xóa VT",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy vật tư",
                            "Vật tư không tồn tại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        txtMaVT.Clear();
                        txtTenVT.Clear();
                        txtDVT.Clear();
                        txtSLTon.Clear();
                        txtMaVT.Focus();
                    }
                }
            }
            catch (FormatException)
            {
                pnlSLTon.BackColor = Color.Red;
            }
        }

        private void btnSuaVT_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaVT.Text == "" || txtTenVT.Text == "" || txtTenVT.Text == " " || txtDVT.Text == "" || txtDVT.Text == " " || txtSLTon.Text == ""
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
                    if (txtSLTon.Text == "")
                    {
                        pnlSLTon.BackColor = Color.Red;
                    }
                }
                else
                {
                    VatTu vt = new VatTu(txtMaVT.Text, txtTenVT.Text, txtDVT.Text, Convert.ToInt32(txtSLTon.Text));
                    if (dsvt.tonTaiMaVT(dsvt.root, txtMaVT.Text))
                    {
                        dsvt.suaTTVT(dsvt.root, vt);
                        luuDSVT();
                        refreshDataGVDSVT();
                        MessageBox.Show("Sửa thông tin vật tư thành công",
                            "Sửa thông VT",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Không tìm thấy mã vật tư. Thêm vật tư?",
                            "Thêm vật tư",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            dsvt.themVatTu(dsvt.root, vt);
                            refreshDataGVDSVT();
                        }
                    }
                }
            }
            catch (FormatException)
            {
                pnlSLTon.BackColor = Color.Red;
            }
        }

        private void txtMaVT_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlMaVT.BackColor == Color.Red)
            {
                pnlMaVT.BackColor = Color.Transparent;
            }
        }

        private void txtTenVT_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlTenVT.BackColor == Color.Red)
            {
                pnlTenVT.BackColor = Color.Transparent;
            }
        }

        private void txtDVT_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlDVT.BackColor == Color.Red)
            {
                pnlDVT.BackColor = Color.Transparent;
            }
        }

        private void txtSLTon_MouseClick(object sender, MouseEventArgs e)
        {
            if (pnlSLTon.BackColor == Color.Red)
            {
                pnlSLTon.BackColor = Color.Transparent;
            }
        }

        private void txtMaVT_TextChanged(object sender, EventArgs e)
        {
            if (dsvt.tonTaiMaVT(dsvt.root, txtMaVT.Text))
            {
                VatTu vt = dsvt.timVatTuTheoMa(dsvt.root, txtMaVT.Text);
                txtTenVT.Text = vt.Tenvt;
                txtDVT.Text = vt.Dvt;
                txtSLTon.Text = vt.Slton.ToString();
                txtSLTon.Enabled = false;
            }
            else
            {
                txtTenVT.Clear();
                txtDVT.Clear();
                txtSLTon.Clear();
                txtSLTon.Enabled = true;
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
                txtSLTon.Text = SelectedRows.Cells[3].Value?.ToString();
                txtSLTon.Enabled = false;
                if (txtSLTon.Text == "") txtSLTon.Enabled = true;

                pnlMaVT.BackColor = Color.Transparent;
                pnlTenVT.BackColor = Color.Transparent;
                pnlDVT.BackColor = Color.Transparent;
                pnlSLTon.BackColor = Color.Transparent;
            }
        }

//Các phương thức liên quan tới DSNV
        private void btnThemNV_Click(object sender, EventArgs e)
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
                if (!dsnv.tonTaiNhanVienCoMa(txtMaNV.Text))
                {
                    string phai;
                    if (rBtnNam.Checked)
                        phai = "Nam";
                    else
                        phai = "Nữ";
                    NhanVien nv = new NhanVien(txtMaNV.Text, txtHoNV.Text, txtTenNV.Text, phai);
                    dsnv.themNhanVien(nv);
                    dsnv.sapXepNhanVien();
                    luuDSNV();
                    refreshDataGVDSNV();
                    MessageBox.Show("Thêm nhân viên thành công",
                        "Thêm nhân viên",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Mã NV đã tồn tại. Vui lòng nhập Mã NV khác",
                        "Mã NV đã tồn tại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtMaNV.Clear();
                }
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
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
                string phai = "";
                if (rBtnNam.Checked)
                    phai = "Nam";
                else
                    phai = "Nữ";
                NhanVien nv = new NhanVien(txtMaNV.Text, txtHoNV.Text, txtTenNV.Text, phai);
                if (dsnv.tonTaiNhanVien(nv))
                {
                    //Xóa nhân viên phải xóa các hóa đơn mà nhân viên đó đã tạo
                    //Xóa các hóa đơn đã tạo thì chi tiết hóa đơn liên quan đến hóa đơn cũng mất
                    bool ran = false;
                    int index = dsnv.timNhanVienTheoMa(txtMaNV.Text);
                    DSHoaDon dshdTemp = dsnv.indexAt(index).Dshd;
                    while (!dshdTemp.isEmpty())
                    {
                        HoaDon hd = dshdTemp.Tail.Hd;
                        int poshd = dshd.findElementWithSoHD(hd.Sohd);
                        if (poshd == -1)
                            break;
                        NodeHD p = dshd.getNodeAt(poshd);
                        DSCTHoaDon dscthdTemp = p.Hd.Dscthd;

                        while (dscthdTemp.Size != 0)
                        {
                            CTHoaDon cthd = dscthdTemp.Tail.Cthd;
                            int poscthd = dscthd.findElementWithMaVT(cthd.Mavt);

                            dscthd.removeAny(poscthd);
                            dscthdTemp.removeLast();
                        }

                        if (poshd == 1)
                        {
                            dshd.removeFirst();
                        }
                        else if (poshd == dshd.Size)
                        {
                            dshd.removeLast();
                        }
                        else
                        {
                            dshd.removeAny(poshd);
                        }
                        ran = true;
                    }

                    dsnv.xoaNhanVien(nv);
                    dsnv.sapXepNhanVien();
                    luuDSNV();
                    if (ran)
                    {
                        luuDSHD();
                        luuDSNVVaHD();
                        luuDSCTHD();
                        luuDSHDVaDSCTHD();
                    }
                    refreshDataGVDSNV();
                    MessageBox.Show("Xóa nhân viên thành công",
                    "Xóa nhân viên",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy nhân viên",
                        "Nhân viên không tồn tại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtMaNV.Clear();
                    txtHoNV.Clear();
                    txtTenNV.Clear();
                    rBtnNam.Checked = true;
                    txtMaNV.Focus();
                }
            }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
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
                string phai = "";
                if (rBtnNam.Checked)
                    phai = "Nam";
                else
                    phai = "Nữ";
                NhanVien nv = new NhanVien(txtMaNV.Text, txtHoNV.Text, txtTenNV.Text, phai);
                if (dsnv.tonTaiNhanVienCoMa(nv.Manv))
                {
                    dsnv.suaTTNV(nv);
                    dsnv.sapXepNhanVien();
                    refreshDataGVDSNV();
                    luuDSNV();
                    MessageBox.Show("Sửa thông tin nhân viên thành công",
                        "Sửa thông tin nhân viên",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("Không tìm thấy mã nhân viên. Thêm nhân viên?",
                        "Thêm nhân viên",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        dsnv.themNhanVien(nv);
                        dsnv.sapXepNhanVien();
                        refreshDataGVDSNV();
                    }
                }
            }
        }

        private void txtMaNV_MouseClick(object sender, MouseEventArgs e)
        {
            pnlMaNV.BackColor = Color.Transparent;
        }

        private void txtHoNV_MouseClick(object sender, MouseEventArgs e)
        {
            pnlHoNV.BackColor = Color.Transparent;
        }

        private void txtTenNV_MouseClick(object sender, MouseEventArgs e)
        {
            pnlTenNV.BackColor = Color.Transparent;
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            if (dsnv.tonTaiNhanVienCoMa(txtMaNV.Text))
            {
                int index = dsnv.timNhanVienTheoMa(txtMaNV.Text);
                NhanVien nv = dsnv.indexAt(index);
                txtTenNV.Text = nv.Ten;
                txtHoNV.Text = nv.Ho;
                string phai = nv.Phai;
                if (phai == "Nam")
                    rBtnNam.Checked = true;
                else
                    rBtnNu.Checked = true;
            }
            else
            {
                txtHoNV.Clear();
                txtTenNV.Clear();
                rBtnNam.Checked = true;
            }
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

//Các phương thức liên quan tới DSHD và doanh thu 10 vật tư cao nhất
    //Nút btnLapHD mở form FormCTHDNhapVT hoặc FormCTHDXuatVT tùy thuộc vào loại hóa đơn
        private void btnLapHD_Click(object sender, EventArgs e)
        {
            if (txtSoHD.Text == "" || Regex.IsMatch(txtSoHD.Text, "\\s")
                || txtSoHD.Text.Contains(",") || txtSoHD.Text.Contains(".") || txtSoHD.Text.Contains(" "))
            {
                pnlSoHD.BackColor = Color.Red;
            }
            else
            {
                if (!dshd.elementWithSoHDExist(txtSoHD.Text))
                {
                    string loai = "";
                    if (rBtnNhap.Checked)
                        loai = "N";
                    else
                        loai = "X";
                    string ngaylap = dateNgayLap.Value.ToString("dd/MM/yyyy");

                    HoaDon hd = new HoaDon(txtSoHD.Text, ngaylap, loai);
                    dshd.addLast(hd);

                    if (loai == "N")
                    {
                        FormCTHDNhapVT formNhapVT = new FormCTHDNhapVT(this);
                        formNhapVT.ShowDialog();
                    }
                    else if (loai == "X")
                    {
                        FormCTHDXuatVT formXuatVT = new FormCTHDXuatVT(this);
                        formXuatVT.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Số HĐ đã tồn tại. Hãy nhập số HĐ khác",
                        "Số HĐ đã tồn tại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtSoHD.Clear();
                    txtSoHD.Focus();
                }

                pnlSoHD.BackColor = Color.Transparent;
            }
        }

        private void btnTimHD_Click(object sender, EventArgs e)
        {

            if (txtSoHDTK.Text == "" || Regex.IsMatch(txtSoHDTK.Text, "\\s")
                || txtSoHDTK.Text.Contains(",") || txtSoHDTK.Text.Contains(".") || txtSoHDTK.Text.Contains(" "))
            {
                pnlSoHDTK.BackColor = Color.Red;
            }
            else if (dshd.elementWithSoHDExist(txtSoHDTK.Text))
            {
                int pos = dshd.findElementWithSoHD(txtSoHDTK.Text);
                NodeHD p = dshd.getNodeAt(pos);
                refreshDataGVDSHDTimKiem(p.Hd);
                pnlSoHDTK.BackColor = Color.Transparent;
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn " + txtSoHDTK.Text);
                txtSoHDTK.Clear();
                txtSoHDTK.Focus();
                refreshDataGVDSNVKetNoiDSHDKetNoiDSCTHD();
                pnlSoHDTK.BackColor = Color.Transparent;
            }
        }

        private void txtSoHD_MouseClick(object sender, MouseEventArgs e)
        {
            pnlSoHD.BackColor = Color.Transparent;
        }

        private void txtSoHDTK_MouseClick(object sender, MouseEventArgs e)
        {
            pnlSoHDTK.BackColor = Color.Transparent;
        }

        private void txtSoHD_TextChanged(object sender, EventArgs e)
        {
            if (dshd.elementWithSoHDExist(txtSoHD.Text))
            {
                pnlSoHD.BackColor = Color.Red;
            }
            else
            {
                pnlSoHD.BackColor = Color.Transparent;
            }
        }

        private void limitDateHD()
        {
            string denngay = dateTPickerDenNgayHD.Value.ToString("dd/MM/yyyy");
            string[] arr_denngay = denngay.Split('/');


            dateTPickerDenNgayHD.MinDate = new DateTime(1999, 1, 1);
            dateTPickerTuNgayHD.MaxDate = new DateTime(Convert.ToInt32(arr_denngay[2]), Convert.ToInt32(arr_denngay[1]), Convert.ToInt32(arr_denngay[0]));
            dateTPickerTuNgayHD.MinDate = new DateTime(1999, 1, 1);
        }

        private void limitDateDT()
        {
            string denngay = dateTPickerDenNgayDT.Value.ToString("dd/MM/yyyy");
            string[] arr_denngay = denngay.Split('/');


            dateTPickerDenNgayDT.MinDate = new DateTime(1999, 1, 1);
            dateTPickerTuNgayDT.MaxDate = new DateTime(Convert.ToInt32(arr_denngay[2]), Convert.ToInt32(arr_denngay[1]), Convert.ToInt32(arr_denngay[0]));
            dateTPickerTuNgayDT.MinDate = new DateTime(1999, 1, 1);
        }

        private void dateTuNgayHD_ValueChanged(object sender, EventArgs e)
        {
            refreshDataGViewDSHDFromDateToDate();
        }

        private void dateTPickerDenNgayHD_ValueChanged(object sender, EventArgs e)
        {
            limitDateHD();
            refreshDataGViewDSHDFromDateToDate();
        }

        private void dateTPickerTuNgayHD_ValueChanged(object sender, EventArgs e)
        {
            refreshDataGViewDSHDFromDateToDate();
        }

        private void dateTPickerTuNgayDT_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTPickerDenNgayDT_ValueChanged(object sender, EventArgs e)
        {
            limitDateDT();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            refreshDataGVThongKeDT();
        }
    }
}
