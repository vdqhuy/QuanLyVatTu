using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVatTu
{
    public class HoaDon
    {
        private string sohd;
        private string ngaylap;
        private string loai;
        private DSCTHoaDon dscthd;

        public string Sohd { get => sohd; set => sohd = value; }
        public string Ngaylap { get => ngaylap; set => ngaylap = value; }
        public string Loai { get => loai; set => loai = value; }
        public DSCTHoaDon Dscthd { get => dscthd; set => dscthd = value; }

        public HoaDon(string sohd, string ngaylap, string loai)
        {
            Sohd = sohd;
            Ngaylap = ngaylap;
            Loai = loai;
            dscthd = new DSCTHoaDon(null, null);
        }

        public bool cTHDExist(CTHoaDon cthd)
        {
            return dscthd.elementExist(cthd);
        }
    }

    public class NodeHD
    {
        private HoaDon hd;
        private NodeHD next;

        public HoaDon Hd { get => hd; set => hd = value; }
        public NodeHD Next { get => next; set => next = value; }

        public NodeHD(HoaDon hd, NodeHD next)
        {
            Hd = hd;
            Next = next;
        }
    }

    public class DSHoaDon
    {
        private NodeHD head;
        private NodeHD tail;
        private int size;

        public NodeHD Tail { get => tail; set => tail = value; }
        public int Size { get => size; set => size = value; }

        public DSHoaDon(NodeHD head, NodeHD tail)
        {
            this.head = head;
            this.tail = tail;
            size = 0;
        }

        public bool isEmpty()
        {
            if (size == 0)
                return true;
            else
                return false;
        }

        public void addLast(HoaDon hd)
        {
            NodeHD newest = new NodeHD(hd, null);
            if (isEmpty())
            {
                head = newest;
                tail = newest;
            }
            else
            {
                tail.Next = newest;
                tail = newest;
            }
            size++;
        }

        public void addFirst(HoaDon hd)
        {
            NodeHD newest = new NodeHD(hd, null);
            if (isEmpty())
            {
                head = newest;
                tail = newest;
            }
            else
            {
                newest.Next = head;
                head = newest;
            }
            size++;
        }

        public void addAny(HoaDon hd, int pos)
        {
            NodeHD newest = new NodeHD(hd, null);
            NodeHD p = head;
            int i = 1;
            while (i < pos - 1)
            {
                p = p.Next;
                i++;
            }
            newest.Next = p.Next;
            p.Next = newest;
            size++;
        }

        public void addCTHD(CTHoaDon cthd, HoaDon hd)
        {
            hd.Dscthd.addLast(cthd);
        }

        public void insertsorted(HoaDon hd)
        {
            NodeHD newest = new NodeHD(hd, null);
            if (isEmpty())
                head = newest;
            else
            {
                NodeHD p = head;
                NodeHD q = head;
                while (p != null && string.Compare(p.Hd.Sohd, hd.Sohd) > 0)
                {
                    q = p;
                    p = p.Next;
                }
                if (p == head)
                {
                    newest.Next = head;
                    head = newest;
                }
                else
                {
                    newest.Next = q.Next;
                    q.Next = newest;
                }
            }
            size++;
        }

        public HoaDon removeFirst()
        {
            HoaDon hd = head.Hd;
            head = head.Next;
            size--;
            return hd;
        }

        public HoaDon removeAny(int pos)
        {
            NodeHD p = head;
            HoaDon hd = p.Hd;
            if (Size == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                int i = 1;
                while (i < pos - 1)
                {
                    p = p.Next;
                    i++;
                }
                hd = p.Next.Hd;
                p.Next = p.Next.Next;
            }
            size--;
            return hd;
        }

        public HoaDon removeLast()
        {
            NodeHD p = head;
            HoaDon hd = p.Hd;
            if (Size == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                while (p.Next.Next != null)
                {
                    p = p.Next;
                }
                hd = p.Next.Hd;
                p.Next = null;
                tail = p;
            }
            size--;
            return hd;
        }

        public int findElement(HoaDon hd)
        {
            int pos = -1;
            NodeHD p = head;
            int i = 1;
            while (i <= size)
            {
                if (p.Hd.Sohd == hd.Sohd)
                {
                    pos = i;
                    break;
                }
                p = p.Next;
                i++;
            }
            return pos;
        }

        public int findElementWithSoHD(string sohd)
        {
            int pos = -1;
            if (!isEmpty())
            {
                NodeHD p = head;
                int i = 1;
                while (i <= size)
                {
                    if (p.Hd.Sohd == sohd)
                    {
                        pos = i;
                        break;
                    }
                    p = p.Next;
                    i++;
                }
            }
            return pos;
        }

        public bool elementWithSoHDExist(string sohd)
        {
            NodeHD p = head;
            while (p != null)
            {
                if (p.Hd.Sohd == sohd)
                    return true;
                p = p.Next;
            }
            return false;
        }

        public NodeHD getNodeAt(int index)
        {
            NodeHD p = head;
            int i = 1;
            while (i < index)
            {
                p = p.Next;
                i++;
            }
            return p;
        }

        public bool elementExist(HoaDon hd)
        {
            NodeHD p = head;
            while (p != null)
            {
                if (p.Hd.Sohd == hd.Sohd)
                    return true;
                p = p.Next;
            }
            return false;
        }

        public string display()
        {
            string str = "";
            NodeHD p = head;
            while (p != null)
            {
                str += p.Hd.Sohd + "," + p.Hd.Ngaylap + "," + p.Hd.Loai + ",";
                p = p.Next;
            }
            return str;
        }
    }
}
