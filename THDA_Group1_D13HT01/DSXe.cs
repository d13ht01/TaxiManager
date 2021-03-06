﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace THDA_Group1_D13HT01
{
    /**
     * Lớp danh sách xe
     * Quản lý tất cả các đối tượng xe
     */
    class DSXe
    {
        private Xe[] dsXE; // Mảng quản lý đối tượng xe
        private int n; // Tổng loại xe
        private int maxsize = Properties.Settings.Default.MAX_SL_XE; // Giới hạn lưu trữ tối đa
        private string datafile = Properties.Settings.Default.FILE_DANHSACHXE; // Địa chỉ file lưu dữ liệu

        public DSXe()
        {
            dsXE = new Xe[maxsize];
        }

        //------------------------------------------------------------------------------------------------------------------------------
        public int getN()
        {
            return n;
        }

        public int getmaxsize()
        {
            return maxsize;
        }

        public Xe[] getDSXe()
        {
            return dsXE;
        }


        public Xe getXEbyID(int id)
        {
            return dsXE[id];
        }

        public Xe getXEbyMX(int maxe)
        {
            for (int i = 0; i < n; i++)
                if (dsXE[i].getMaXe() == maxe)
                    return dsXE[i];

            return new Xe();
        }

        //------------------------------------------------------------------------------------------------------------------------------

        public void Nhap()
        {
            Console.Write("Nhập số lượng xe: ");
            try
            {
                do
                {
                    n = int.Parse(Console.ReadLine());

                    if (n < 0 || n > maxsize)
                        System.Console.WriteLine("Số lượng xe (N)  quá lớn hoặc quá nhỏ, 0 <= N <= {0}.\nNhập lại N:", maxsize);

                } while (n < 0 || n > maxsize);
            }
            catch (Exception ex)
            {
                n = 0;
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Nhập tên thông tin xe thứ {0}: ", i + 1);
                dsXE[i] = new Xe();
                dsXE[i].Nhap();
            }

            Console.WriteLine("Đã nhập {0} xe", n);
        }

        public void Nhap_File()
        {
            try
            {
                StreamReader myfile = File.OpenText(datafile);
                n = int.Parse(myfile.ReadLine());

                Console.WriteLine("Số lượng xe: " + n);

                if (n < 0 || n > maxsize)
                    throw new System.AggregateException("Số lượng xe (N)  quá lớn hoặc quá nhỏ, 0 <= N <= {0}.");

                for (int i = 0; i < n; i++)
                {
                    string[] separators = { "\t" };
                    string value = myfile.ReadLine();
                    string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    // Ví dụ: 1	54T3-6222	Nguyen Dinh Tan	1, mỗi thuộc tính cách nhau bởi phím TAB
                    //string[] _xe = new string[4];

                    //_xe[0] = words[0]; // Mã xe và tài xế
                    //_xe[1] = words[1]; // Biển kiểm soát
                    //_xe[2] = words[2]; // Họ và tên tài xế
                    //_xe[3] = words[3]; // Mã loại xe
                    dsXE[i] = new Xe(int.Parse(words[0]), words[1], words[2], int.Parse(words[3]));
                }

                myfile.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\n{0} không tồn tại", datafile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
            }
        }

        public void Xuat()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện không có xe nào trong danh sách!");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0,-7} | {1,-15} | {2,-32} | {3,-7}", "Mã TX", "Biển KS", "Tên tài xế", "Loại xe");
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                dsXE[i].Xuat();
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public void Xuat2(DSLoaiXe ds)
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện không có xe nào trong danh sách!");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0,-7} | {1,-15} | {2,-32} | {3,-14}", "Mã TX", "Biển KS", "Tên tài xế", "Loại xe");
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                dsXE[i].Xuat2(ds);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public string Xuat2S(DSLoaiXe ds)
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện không có xe nào trong danh sách!");
                return "";
            }

            string s = "";
            s += String.Format("{0,-7} | {1,-15} | {2,-32} | {3,-14}\n", "Mã TX", "Biển KS", "Tên tài xế", "Loại xe");
            for (int i = 0; i < n; i++)
                s += dsXE[i].Xuat2S(ds);

            s += "---------------------------\n";
            s += String.Format("Tổng: {0}.", n);

            return s;
        }

        public void Xuat_File()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện không có xe nào trong danh sách!");
                return;
            }

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(datafile);
                file.WriteLine("{0}", n);
                for (int i = 0; i < n; i++)
                {
                    file.WriteLine("{0}\t{1}\t{2}\t{3}", dsXE[i].getMaXe(), dsXE[i].getSoXe(), dsXE[i].getTenTaiXe(), dsXE[i].getLoaiXe());
                }
                file.Close();

                Console.WriteLine("Đã lưu vào: {0}", Path.GetFullPath(datafile));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
            }
        }
    }
}
