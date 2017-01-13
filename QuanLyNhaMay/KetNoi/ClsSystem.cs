using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace KetNoi
{
    public static class clsSystem
    {
        public static Image ConvertByeToImage(byte[] byteimage)
        {
            MemoryStream ms = new MemoryStream(byteimage);//create memory stream by passing byte array of the image

            return Image.FromStream(ms);//set image property of the picture box by creating a image from stream 
        }
        public static DateTime GetNgayDauThang(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime GetNgayCuoiThang(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, Maxngaytrongthang(dt));
        }
        public static int Maxngaytrongthang(DateTime dt)
        {
            if (dt.Month == 1 || dt.Month == 3 || dt.Month == 5 || dt.Month == 7 || dt.Month == 8 || dt.Month == 10 || dt.Month == 12)
            {
                return 31;
            }
            else
            {
                if (dt.Month == 4 || dt.Month == 6 || dt.Month == 9 || dt.Month == 11)
                {
                    return 30;
                }
                else
                {
                    if (dt.Year % 4 == 0)
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }

                }
            }

        }
        public static bool sosanhngaythang(DateTime TuNgay, DateTime DenNgay)
        {

            TimeSpan ts = DenNgay.Date - TuNgay.Date;
            if (ts.Days < 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        public static int GetSafeBoolInt(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    if ((bool)value == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    if ((int)value == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

        }
        public static bool GetBool(string value)
        {
            if (value == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool GetSafeBool(object value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                try
                {
                    if ((bool)value == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    if ((int)value == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
        public static string GetSafeString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value.ToString();
        }
        public static string GetSafeFloatString(object value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                if (value.ToString().Trim() == string.Empty)
                {
                    return "0";
                }
                else
                {
                    try
                    {
                        return value.ToString();
                    }
                    catch
                    {
                        return "0";
                    }
                }
            }
        }
        public static object GetSafeDateTime(object value)
        {

            if (value == null)
            {
                return null;
            }
            else
            {
                if (value.ToString().Trim() == string.Empty)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        return Convert.ToDateTime(value);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
        public static float GetSafeFloat(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return float.Parse(value.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static double GetSafeDouble(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return Convert.ToDouble(value.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static int GetSafeInt(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return int.Parse(value.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static decimal GetSafeDecimal(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return decimal.Parse(value.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static string GetSafeIntString(object value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                if (value.ToString().Trim() == string.Empty)
                {
                    return "0";
                }
                else
                {
                    try
                    {
                        return value.ToString();
                    }
                    catch
                    {
                        return "0";
                    }
                }
            }
        }
    }
}
