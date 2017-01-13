using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Main.DTO
{
    public class DT_Login
    {
        private static int _iUserId;

        public int iUserId
        {
            get { return _iUserId; }
            set { _iUserId = value; }
        }
        private static string _GetTaiKhoan;

        public string GetTaiKhoan
        {
            get { return _GetTaiKhoan; }
            set { _GetTaiKhoan = value; }
        }
        private static String _GetCapBac;

        public String GetCapBac
        {
            get { return _GetCapBac; }
            set { _GetCapBac = value; }
        }
        private static String _GetDonVi;

        public String GetDonVi
        {
            get { return _GetDonVi; }
            set { _GetDonVi = value; }
        }

    }
}
