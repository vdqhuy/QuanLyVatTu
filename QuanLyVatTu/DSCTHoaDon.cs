using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVatTu
{
    public class CTHoaDon
    {
        private string mavt;
        private int soluong;
        private string dongia;
        private double vat;

        public string Mavt { get => mavt; set => mavt = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public string Dongia { get => dongia; set => dongia = value; }
        public double Vat { get => vat; set => vat = value; }

        public CTHoaDon(string mavt, int soluong, string dongia, double vat)
        {
            Mavt = mavt;
            Soluong = soluong;
            Dongia = dongia;
            Vat = vat;
        }
    }

    public class NodeCTHD
    {
        private CTHoaDon cthd;
        private NodeCTHD next;

        public CTHoaDon Cthd { get => cthd; set => cthd = value; }
        public NodeCTHD Next { get => next; set => next = value; }

        public NodeCTHD(CTHoaDon cthd, NodeCTHD next)
        {
            Cthd = cthd;
            Next = next;
        }
    }

    public class DSCTHoaDon
    {
        private NodeCTHD head;
        private NodeCTHD tail;
        private int size;

        public NodeCTHD Tail { get => tail; set => tail = value; }
        public int Size { get => size; set => size = value; }

        public DSCTHoaDon(NodeCTHD head, NodeCTHD tail)
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

        public void addLast(CTHoaDon cthd)
        {
            NodeCTHD newest = new NodeCTHD(cthd, null);
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

        public void addLast(CTHoaDon cthd, HoaDon hd)
        {
            NodeCTHD newest = new NodeCTHD(cthd, null);
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
            hd.Dscthd.addLast(cthd);
        }

        public void addFirst(CTHoaDon cthd)
        {
            NodeCTHD newest = new NodeCTHD(cthd, null);
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

        public void addAny(CTHoaDon cthd, int pos)
        {
            NodeCTHD newest = new NodeCTHD(cthd, null);
            NodeCTHD p = head;
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

        public void insertsorted(CTHoaDon cthd)
        {
            NodeCTHD newest = new NodeCTHD(cthd, null);
            if (isEmpty())
                head = newest;
            else
            {
                NodeCTHD p = head;
                NodeCTHD q = head;
                while (p != null && string.Compare(p.Cthd.Mavt, cthd.Mavt) > 0)
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

        public void editAny(CTHoaDon cthd, HoaDon hd)
        {
            NodeCTHD p = hd.Dscthd.head;
            while (p != null)
            {
                if (p.Cthd.Mavt == cthd.Mavt)
                {
                    p.Cthd.Soluong += cthd.Soluong;
                    p.Cthd.Dongia = cthd.Dongia;
                    p.Cthd.Vat = cthd.Vat;
                }
                p = p.Next;
            }
        }

        public CTHoaDon removeFirst()
        {
            CTHoaDon cthd = head.Cthd;
            head = head.Next;
            size--;
            return cthd;
        }

        public CTHoaDon removeAny(int pos)
        {
            NodeCTHD p = head;
            int i = 1;
            while (i < pos - 1)
            {
                p = p.Next;
                i++;
            }
            CTHoaDon cthd = p.Next.Cthd;
            p.Next = p.Next.Next;
            size--;
            return cthd;
        }

        public CTHoaDon removeLast()
        {
            NodeCTHD p = head;
            CTHoaDon cthd = p.Cthd;
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
                cthd = p.Next.Cthd;
                p.Next = null;
                tail = p;
            }
            size--;
            return cthd;
        }

        public int findElement(CTHoaDon cthd)
        {
            int pos = -1;
            NodeCTHD p = head;
            int i = 1;
            while (i <= size)
            {
                if (p.Cthd == cthd)
                {
                    pos = i;
                    break;
                }
                p = p.Next;
                i++;
            }
            return pos;
        }

        public int findElementWithMaVT(string mavt)
        {
            int pos = -1;
            NodeCTHD p = head;
            int i = 1;
            while (i <= size)
            {
                if (p.Cthd.Mavt == mavt)
                {
                    pos = i;
                    break;
                }
                p = p.Next;
                i++;
            }
            return pos;
        }

        public bool elementExist(CTHoaDon cthd)
        {
            NodeCTHD p = head;
            while (p != null)
            {
                if (p.Cthd.Mavt == cthd.Mavt)
                    return true;
                p = p.Next;
            }
            return false;
        }

        public bool elementWithMaVTExist(string mavt)
        {
            NodeCTHD p = head;
            while (p != null)
            {
                if (p.Cthd.Mavt == mavt)
                    return true;
                p = p.Next;
            }
            return false;
        }

        public NodeCTHD getNodeAt(int index)
        {
            NodeCTHD p = head;
            int i = 1;
            while (i < index)
            {
                p = p.Next;
                i++;
            }
            return p;
        }

        public string display()
        {
            string str = "";
            NodeCTHD p = head;
            while (p != null)
            {
                str += p.Cthd.Mavt + "," + p.Cthd.Soluong + "," + p.Cthd.Dongia + "," + p.Cthd.Vat + ",";
                p = p.Next;
            }
            return str;
        }
    }
}
