using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Configuration;

namespace KetNoi
{
    public class AccessData
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        private DataSet dts;
        private DataTable tbl;
        private SqlDataReader dr;
        private SqlDataAdapter da;
        private SqlTransaction tran;

        public int server;
        public string ser = "";
        public string db = "";
        public AccessData(string  tensv,string tendb)
        {
            ser = tensv;
            db = tendb;
        }
        public string chuoiketnoi()
        {
            string chuoikn1 = "Data Source=" + ser + ";Initial Catalog=" + db + ";Integrated security=SSPI";
            return chuoikn1;
        }
        public SqlConnection fBolKetNoi()
        {
            return new SqlConnection(chuoiketnoi());
        }
        public OleDbDataReader docdulieuexcel(string extension, string strPath, string sheetname)
        {
            //string connectionstring = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
            OleDbConnection connect = new OleDbConnection() { ConnectionString = getconnectexcel(extension, strPath) };
            ;
            OleDbCommand command = new OleDbCommand(String.Format("select * from [{0}]", sheetname), connect);
            try
            {
                connect.Open();
                OleDbDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                command.Dispose();
                dr.Dispose();
            }

        }
        public DataTable Docdulieutext(string duongdanfile)
        {
            // duong dan dang :@"\\192.1.1.14\acountsystemprogram$\NET_DATA\SapInvoice.txt"
            DataTable dt = new DataTable();
            using (TextReader tr = File.OpenText(duongdanfile))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {

                    string[] items = line.Trim().Split('\t'); //&& line.Trim().Split('\n'));
                    // items = line.Trim().Split('\n');
                    if (dt.Columns.Count == 0)
                    {
                        // Create the data columns for the data table based on the number of items
                        // on the first line of the file
                        for (int i = 0; i < items.Length; i++)
                            dt.Columns.Add(new DataColumn("Column" + i, typeof(string)));
                    }
                    dt.Rows.Add(items);
                }

            }
            dt.Rows[0].Delete();
            return dt;
        }
        public string getconnectexcel(string extension, string strPath)
        {
            string connString = "";
            if (extension.ToLower().Trim() == ".xls")
            {
                connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
            }
            else
            {
                connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", strPath);
            }
            return connString;
        }
        public DataTable docdulieutableexcel(string extension, string strPath, string sheetname)
        {
            DataTable dtexcel = new DataTable();
            //   string connectionstring = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
            OleDbConnection connect = new OleDbConnection() { ConnectionString = getconnectexcel(extension, strPath) };
            OleDbDataAdapter daexcel = new OleDbDataAdapter(String.Format("select * from [{0}]", sheetname), connect);
            try
            {

                connect.Open();
                daexcel.Fill(dtexcel);
                connect.Close();
                return dtexcel;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                daexcel.Dispose();
            }

        }
        public string[] GetExcelSheetNames(string extension, string strPath)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;
            //  string sheetnames = "";
            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                // string connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(getconnectexcel(extension, strPath));
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                string[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop through all of the sheets if you want too...
                /*    for (int j = 0; j < excelSheets.Length; j++)
                    {
                        DataTable dte = docdulieutableexcelpro(strPath, excelSheets[j]);
                       if (dte.Rows.Count > 0)
                       {
                           sheetnames = sheetnames + excelSheets[j] + ",";
                       }
                        // Query each excel sheet.
                    }
                    */
                return excelSheets;
            }
            catch
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public Boolean fBolThucThiSP(string strTenSP, SqlParameter[] arrThamSo)
        {
            int intLoop;
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = strTenSP, Connection = cnn };
                if (arrThamSo != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (intLoop = 0; intLoop < arrThamSo.Length; intLoop++)
                        cmd.Parameters.Add(arrThamSo[intLoop]);
                }
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {

                return false;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
            }
        }
        //Thực thi câu lệnh SQL         
        public Boolean fBolThucThiSQL(string strSQL)
        {
            try
            {
                cnn = fBolKetNoi();
                tran = cnn.BeginTransaction();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn, Transaction = tran };
                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                tran.Dispose();
            }
        }
        // Thuc thi cau lenh co tham so 
        public Boolean fBolThucThiSPSQL(string strTenSP, SqlParameter[] arrThamSo)
        {
            int intLoop;
            try
            {
                cnn = fBolKetNoi();
                tran = cnn.BeginTransaction();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = strTenSP, Connection = cnn, Transaction = tran };
                if (arrThamSo != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (intLoop = 0; intLoop < arrThamSo.Length; intLoop++)
                        cmd.Parameters.Add(arrThamSo[intLoop]);

                }

                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception)
            {
                //  string a = cmd.ExecuteNonQuery().ToString();
                //  string b = ex.ToString();
                tran.Rollback();
                return false;


            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                tran.Dispose();
            }
        }
        //Ðoc dữ liệu vào dataset bằng StoredProcedure có tham số
        public DataSet fdtsDocDuLieuSP(string strTenSP, SqlParameter[] arrThamSo)
        {
            int intLoop;
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = strTenSP, Connection = cnn };
                if (arrThamSo != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (intLoop = 0; intLoop < arrThamSo.Length; intLoop++)
                        cmd.Parameters.Add(arrThamSo[intLoop]);
                }
                da = new SqlDataAdapter(cmd);
                dts = new DataSet();
                da.Fill(dts);
                cnn.Close();
                return dts;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        //Ðoc dữ liệu vào dataset bằng SQL text
        public DataSet fdtsDocDuLieuSQL(string strSQL)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn };
                da = new SqlDataAdapter(cmd);
                dts = new DataSet();
                da.Fill(dts);
                cnn.Close();
                return dts;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        //Ðoc dữ liệu vào datatable bằng StoredProcedure
        public DataTable ftblDocDuLieuSP(string strTenSP, SqlParameter[] arrThamSo)
        {
            int intLoop;
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = strTenSP, Connection = cnn };
                if (arrThamSo != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (intLoop = 0; intLoop < arrThamSo.Length; intLoop++)
                        cmd.Parameters.Add(arrThamSo[intLoop]);
                }
                da = new SqlDataAdapter(cmd);
                tbl = new DataTable(strTenSP);
                da.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        //Ðoc dữ liệu vào datatable bằng SQL text
        public DataTable ftblDocDuLieuSQL(string strSQL)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn };
                da = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                da.Fill(tbl);
                // da.SelectCommand.Parameters.AddRange();
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        //Ðoc dữ liệu vào datareader bằng StoredProcedure
        public SqlDataReader fdreadDocDuLieuSP(string strTenSP, SqlParameter[] arrThamSo)
        {
            int intLoop;
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = strTenSP, Connection = cnn };
                if (arrThamSo != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (intLoop = 0; intLoop < arrThamSo.Length; intLoop++)
                        cmd.Parameters.Add(arrThamSo[intLoop]);
                }
                dr = cmd.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Ðoc dữ liệu vào datareader bằng SQL text
        public SqlDataReader fdreadDocDuLieuSQL(string strSQL)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = strSQL, Connection = cnn };
                dr = cmd.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
