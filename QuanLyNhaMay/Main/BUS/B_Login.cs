using System;
using KetNoi;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.Skins;
using System.Windows.Forms;
using DevExpress.UserSkins;
using System.Data.SqlClient;
using DevExpress.LookAndFeel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Main.BUS
{
    public class B_Login
    {
        private DataTable dts = new DataTable();
        AccessData ac = new AccessData("TAM-PC", "QUANLYGASAIGON");
        public DataTable Login(string user, string pass)
        {
            SqlParameter[] arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@username", SqlDbType.VarChar);
            arrPara[0].Value = user;
            arrPara[1] = new SqlParameter("@pass", SqlDbType.VarChar);
            arrPara[1].Value = pass;
            //arrPara[2] = new SqlParameter("@CT_ID", SqlDbType.Int);
            //arrPara[2].Value = machuongtrinh;
            return ac.ftblDocDuLieuSP("Login_Main", arrPara);
            //return int.Parse(dt.Rows[0]["ErrCode"].ToString());
        }
        public int CapNhat_NhanVien(string manv, string matkhau)
        {
            SqlParameter[] arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@manv", SqlDbType.VarChar);
            arrPara[0].Value = manv;
            arrPara[1] = new SqlParameter("@matkhau", SqlDbType.VarChar);
            arrPara[1].Value = matkhau;
            DataTable dt = ac.ftblDocDuLieuSP("NhanVien_DoiMK", arrPara);
            return int.Parse(dt.Rows[0]["ErrCode"].ToString());
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "abc12";//abc123
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public DataTable GET_Login(string user, string pass)
        {
            SqlParameter[] arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@username", SqlDbType.VarChar);
            arrPara[0].Value = user;
            arrPara[1] = new SqlParameter("@pass", SqlDbType.VarChar);
            arrPara[1].Value = Encrypt(pass);
            return ac.ftblDocDuLieuSP("sp_User_Login", arrPara);
        }
    }
}