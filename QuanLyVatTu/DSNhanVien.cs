using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVatTu
{

    public class NhanVien
    {
        private string manv;
        private string ho, ten;
        private string phai;
        private DSHoaDon dshd;

        public string Manv { get => manv; set => manv = value; }
        public string Ho { get => ho; set => ho = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Phai { get => phai; set => phai = value; }
        public DSHoaDon Dshd { get => dshd; set => dshd = value; }

        public NhanVien(string manv, string ho, string ten, string phai)
        {
            Manv = manv;
            Ho = ho;
            Ten = ten;
            Phai = phai;
            dshd = new DSHoaDon(null, null);
        }
    }

    public class DSNhanVien
    {
        private NhanVien[] dsnv;
        private int upper;
        private int amount;

        public int Amount { get => amount; set => amount = value; }

        public DSNhanVien(int size)
        {
            dsnv = new NhanVien[size];
            upper = size - 1;
            amount = 0;
        }

        public NhanVien indexAt(int i)
        {
            return dsnv[i];
        }

        public void themNhanVien(NhanVien nv)
        {
            bool added = false;
            if (amount < upper)
            {
                for (int i = 0; i < amount; i++)
                {
                    if (dsnv[i] == null)
                    {
                        dsnv[i] = nv;
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    dsnv[amount] = nv;
                    amount++;
                }
            }
        }

        public void themHoaDon(HoaDon hd, int index)
        {
            dsnv[index].Dshd.addLast(hd);
        }

        public void xoaNhanVien(NhanVien nv)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dsnv[i] == null)
                    continue;
                if (dsnv[i].Manv == nv.Manv && dsnv[i].Ten == nv.Ten 
                    && dsnv[i].Ho == nv.Ho && dsnv[i].Phai == nv.Phai)
                {
                    dsnv[i] = null;
                    break;
                }
            }
        }

        public bool tonTaiNhanVien(NhanVien nv)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dsnv[i] == null)
                    continue;
                else if (dsnv[i].Manv == nv.Manv && dsnv[i].Ten == nv.Ten 
                    && dsnv[i].Ho == nv.Ho && dsnv[i].Phai == nv.Phai)
                {
                    return true;
                }
            }
            return false;
        }

        public int timNhanVien(NhanVien nv)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dsnv[i] == null)
                    continue;
                else if (dsnv[i].Manv == nv.Manv)
                {
                    return i;
                }
            }
            return -1;
        }

        public int timNhanVienTheoMa(string manv)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dsnv[i] == null)
                    continue;
                else if (dsnv[i].Manv == manv)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool tonTaiNhanVienCoMa(string manv)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dsnv[i] == null)
                    continue;
                else if (dsnv[i].Manv == manv)
                {
                    return true;
                }
            }
            return false;
        }

        public void suaTTNV(NhanVien nv)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dsnv[i] == null)
                    continue;
                else if (dsnv[i].Manv == nv.Manv)
                {
                    dsnv[i].Ten = nv.Ten;
                    dsnv[i].Ho = nv.Ho;
                    dsnv[i].Phai = nv.Phai;
                }
            }
        }

        public void sapXepNhanVien()
        {
            for (int i = amount - 1; i >= 0; i--)
            {
                bool stop = true;
                for (int j = 0; j < i; j++)
                {
                    if (dsnv[j] == null || dsnv[j + 1] == null)
                    {
                        continue;
                    }
                    else if (string.Compare(dsnv[j].Ten, dsnv[j + 1].Ten) > 0)
                    {
                        NhanVien temp = dsnv[j];
                        dsnv[j] = dsnv[j + 1];
                        dsnv[j + 1] = temp;
                        stop = false;
                    }
                    else if (string.Compare(dsnv[j].Ten, dsnv[j + 1].Ten) == 0)
                    {
                        if (string.Compare(dsnv[j].Ho, dsnv[j + 1].Ho) > 0)
                        {
                            NhanVien temp = dsnv[j];
                            dsnv[j] = dsnv[j + 1];
                            dsnv[j + 1] = temp;
                            stop = false;
                        }
                    }
                }
                if (stop)
                    break;
            }
        }
    }
}
