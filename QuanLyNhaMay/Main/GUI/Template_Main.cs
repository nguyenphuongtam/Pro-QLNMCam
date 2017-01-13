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
namespace Main.GUI
{
    public partial class Template_Main : DevExpress.XtraEditors.XtraForm
    {
        public Template_Main()
        {
            InitializeComponent();
        }

        public void BuildMenu()
        {
            var dtPage = new DataTable();
            var dtGroupPage = new DataTable();
            var dtItem = new DataTable();
            int i, j, k;
            DT_Login clsDTO = new DT_Login();
            int iUserId = clsDTO.iUserId;

            try
            {

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

    }
}