using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVatTu
{
    public class VatTu
    {
        private string mavt;
        private string tenvt;
        private string dvt;
        private int slton;

        public string Mavt { get => mavt; set => mavt = value; }
        public string Tenvt { get => tenvt; set => tenvt = value; }
        public string Dvt { get => dvt; set => dvt = value; }
        public int Slton { get => slton; set => slton = value; }

        public VatTu()
        {
            mavt = "";
            tenvt = "";
            dvt = "";
            slton = 0;
        }

        public VatTu(string mavt, string tenvt, string dvt, int slton)
        {
            Mavt = mavt;
            Tenvt = tenvt;
            Dvt = dvt;
            Slton = slton;
        }
    }

    public class NodeVT
    {
        public VatTu vattu;
        public NodeVT left;
        public NodeVT right;

        public NodeVT(VatTu vattu, NodeVT left, NodeVT right)
        {
            this.vattu = vattu;
            this.left = left;
            this.right = right;
        }
    }

    public class DSVatTu
    {
        public NodeVT root;
          
        public DSVatTu()
        {
            root = null;
        }

        public void themVatTu(NodeVT temproot, VatTu vt)
        {
            NodeVT temp = temproot;
            while (temproot != null)
            {
                temp = temproot;
                if (string.Compare(vt.Mavt, temproot.vattu.Mavt) == 0)
                {
                    return;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
            NodeVT n = new NodeVT(vt, null, null);
            if (root != null)
            {
                if (string.Compare(vt.Mavt, temp.vattu.Mavt) < 0)
                {
                    temp.left = n;
                }
                else
                    temp.right = n;
            }
            else
                root = n;
        }

        public VatTu timVatTuTheoMa(NodeVT temproot, string mavt)
        {
            while (temproot != null)
            {
                if (string.Compare(mavt, temproot.vattu.Mavt) == 0)
                {
                    return temproot.vattu;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
            return null;
        }

        public bool timVatTuDeXuat(NodeVT temproot, string mavt, int slxuat)
        {
            bool daXuat = false;
            while (temproot != null)
            {
                if (string.Compare(mavt, temproot.vattu.Mavt) == 0)
                {
                    if (temproot.vattu.Slton >= slxuat)
                    {
                        temproot.vattu.Slton -= slxuat;
                        daXuat = true;
                    }    
                    break;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
            return daXuat;
        }

        public void timVatTuDeNhap(NodeVT temproot, string mavt, int slnhap)
        {
            while (temproot != null)
            {
                if (string.Compare(mavt, temproot.vattu.Mavt) == 0)
                {
                    temproot.vattu.Slton += slnhap;
                    break;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
        }

        public bool tonTaiVatTu(NodeVT temproot, VatTu vt)
        {
            bool result = false;
            while (temproot != null)
            {
                if (string.Compare(vt.Mavt, temproot.vattu.Mavt) == 0)
                {
                    if (vt.Slton != temproot.vattu.Slton || (vt.Tenvt != temproot.vattu.Tenvt && vt.Dvt != temproot.vattu.Dvt))
                        break;
                    result = true;
                    break;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
            return result;
        }

        public bool tonTaiMaVT(NodeVT temproot, string mavt)
        {
            bool result = false;
            while (temproot != null)
            {
                if (string.Compare(mavt, temproot.vattu.Mavt) == 0)
                {
                    result = true;
                    break;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
            return result;
        }

        public bool kiemTraSLTon(NodeVT temproot, VatTu vt)
        {
            bool result = true;
            while (temproot != null)
            {
                if (string.Compare(vt.Mavt, temproot.vattu.Mavt) == 0)
                {
                    if (vt.Slton != temproot.vattu.Slton)
                    {
                        result = false;
                        break;
                    }
                    break;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
            return result;
        }

        /*public NodeVT xoaVatTu(NodeVT temproot, VatTu vt)
        {
            if (temproot == null)
                return null;
            else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) < 0)
            {
                temproot.left = xoaVatTu(temproot.left, vt);
            }
            else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) > 0)
            {
                temproot.right = xoaVatTu(temproot.right, vt);
            }
            else
            {
                if (temproot.left == null && temproot.right == null)
                {
                    temproot = null;
                }
                else if (temproot.left != null && temproot.right != null)
                {
                    NodeVT temp = NodeVTTheMang(temproot.right);
                    temproot.vattu = temp.vattu;
                    temproot.right = xoaVatTu(temproot.right, temp.vattu);
                }
                else
                {
                    NodeVT child = temproot.left != null ? temproot.left : temproot.right;
                    temproot = child;
                }
            }
            return temproot;
        }*/

        public bool xoaVatTu(VatTu vt)
        {
            NodeVT p = root;
            NodeVT pp = null;
            while (p != null && p.vattu.Mavt != vt.Mavt)
            {
                pp = p;
                if (string.Compare(vt.Mavt, p.vattu.Mavt) < 0)
                    p = p.left;
                else
                    p = p.right;
            }
            if (p == null)
                return false;
            if (p.left != null && p.right != null)
            {
                NodeVT s = p.left;
                NodeVT ps = p;
                while (s.right != null)
                {
                    ps = s;
                    s = s.right;
                }
                p.vattu = s.vattu;
                p = s;
                pp = ps;
            }
            NodeVT c = null;
            if (p.left != null)
                c = p.left;
            else
                c = p.right;
            if (p == root)
                root = c;
            else
            {
                if (p == pp.left)
                    pp.left = c;
                else
                    pp.right = c;
            }
            return true;
        }

        /*private NodeVT NodeVTTheMang(NodeVT NodeVT)
        {
            while (NodeVT.left != null)
            {
                NodeVT = NodeVT.left;
            }
            return NodeVT;
        }*/

        public void suaTTVT(NodeVT temproot, VatTu vt)
        {
            while (temproot != null)
            {
                if (string.Compare(vt.Mavt, temproot.vattu.Mavt) == 0)
                {
                    temproot.vattu.Tenvt = vt.Tenvt;
                    temproot.vattu.Dvt = vt.Dvt;
                    break;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) < 0)
                {
                    temproot = temproot.left;
                }
                else if (string.Compare(vt.Mavt, temproot.vattu.Mavt) > 0)
                {
                    temproot = temproot.right;
                }
            }
        }

        public string inorderDisplay(NodeVT temproot, String str)
        {
            if (temproot != null)
            {
                str = inorderDisplay(temproot.left, str);
                str += temproot.vattu.Mavt + "," + temproot.vattu.Tenvt + "," + temproot.vattu.Dvt + "," + temproot.vattu.Slton + ",";
                str = inorderDisplay(temproot.right, str);
            }
            return str;
        }

        public string inorderDisplayMaVT(NodeVT temproot, String str)
        {
            if (temproot != null)
            {
                str = inorderDisplayMaVT(temproot.left, str);
                str += temproot.vattu.Mavt + ",0,";
                str = inorderDisplayMaVT(temproot.right, str);
            }
            return str;
        }
    }
}
