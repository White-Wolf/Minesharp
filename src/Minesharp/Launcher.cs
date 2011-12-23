using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Minesharp
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            Console.Write("Initializing Launcher Window...");
            InitializeComponent();
            Console.WriteLine("Done");
            this.Focus();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Console.WriteLine("Closing Launcher Window");
            base.OnClosing(e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUser.Text) && !String.IsNullOrEmpty(txtPass.Text))
            {
                lblUser.Visible = false;
                lblPass.Visible = false;
                txtUser.Visible = false;
                txtPass.Visible = false;
                btnLogin.Visible = false;
                chkRemember.Visible = false;

                if (Network.Authenticate(txtUser.Text, txtPass.Text))
                {
                    this.Close();
                }
                else
                {
                    lblLoginFailed.Visible = true;
                    btnTryAgain.Visible = true;
                }
            }
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            lblLoginFailed.Visible = false;
            btnTryAgain.Visible = false;
            lblUser.Visible = true;
            lblPass.Visible = true;
            txtUser.Visible = true;
            txtPass.Visible = true;
            btnLogin.Visible = true;
            chkRemember.Visible = true;

        }
    }
}
