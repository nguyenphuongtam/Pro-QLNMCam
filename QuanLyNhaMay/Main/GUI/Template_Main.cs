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
using Main.DTO;
using Main.BUS;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
namespace Main.GUI
{
    public partial class Template_Main : DevExpress.XtraEditors.XtraForm
    {
        DT_Login clsDTO = new DT_Login();
        B_Login clsB = new B_Login();
        public Template_Main()
        {
            InitializeComponent();
            BuildMenu();
        }

        public void BuildMenu()
        {
            var dtPage = new DataTable();
            var dtGroupPage = new DataTable();
            var dtItem = new DataTable();
            int i, j, k;
            int IuserID = clsDTO.iUserId; 
            try
            {
                dtPage = clsB.GET_MenuParent(IuserID);
                //RibbonPage page = new RibbonPage((IuserID - 1).ToString());
                if (dtPage.Rows.Count > 0)
                {
                    for (i = 0; i < dtPage.Rows.Count; i++)
                    {
                        RibbonPage page = new RibbonPage(dtPage.Rows[i]["Name"].ToString());
                        ribbonControl1.Pages.Add(page);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

    }
}