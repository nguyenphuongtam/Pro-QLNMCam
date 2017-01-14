using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Main.BUS;
using KetNoi;
using Main.Properties;
using Main.DTO;
namespace Main.GUI
{
     
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        
        public Login()
        {
            InitializeComponent();
        }
        B_Login ClsB_login = new B_Login();
        DT_Login clsDT_login = new DT_Login();
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtuser.Text.Trim() != "" && txtpass.Text.Trim() != "")
            {

                DataTable dt = new DataTable();
                dt = ClsB_login.GET_Login(txtuser.Text, txtpass.Text);
                if (dt.Rows.Count > 0)
                {
                    if (chkremember.Checked == true)
                    {
                        Settings.Default.username = txtuser.Text;
                        Settings.Default.password = txtpass.Text;
                    }
                    else
                    {
                        Settings.Default.username = string.Empty;
                        Settings.Default.password = string.Empty;
                    }
                    Settings.Default.remember = chkremember.Checked;
                    Settings.Default.Save();
                    clsDT_login.iUserId = clsSystem.GetSafeInt(dt.Rows[0]["Id"]);
                    //ClsParameterSys.ht_userid = clsSystem.GetSafeInt(if dt.Rows[0]["id"]==null , "" , dt.Rows[0]["id"]);
                    //ClsParameterSys.ht_manv = clsSystem.GetSafeString(dt.Rows[0]["manv"]);
                    //ClsParameterSys.ht_MaChuongTrinhLoginSuseccFull = clsSystem.GetSafeInt(dt.Rows[0]["ID_CT"]);
                    MessageBox.Show("Login Successful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Template_Main frm = new Template_Main();
                    frm.Show();
                    // Application.Exit();
                }
                else
                {
                    MessageBox.Show("Username or Password inconrrect", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter username and password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}