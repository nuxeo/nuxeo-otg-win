using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nuxeo.Otg.Win.Forms
{
    public partial class FrmBind : Form
    {
        public String Url
        {
            get { return tbxUrl.Text; }
        }

        public String Username
        {
            get { return tbxUsername.Text; }
        }

        public String Password
        {
            get { return tbxPassword.Text; }
        }

        public String RemoteFolder
        {
            get { return tbxRemote.Text; }
        }

        public FrmBind()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnBind_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            CloseForm();
        }

        protected void CloseForm()
        {
            this.Close();
        }
    }
}
