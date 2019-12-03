using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TiptopLogin {
    public partial class frmLogin : Form {
        public frmLogin() {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            try {
                #region 檢查
                if (txtHost.Text == "") {
                    throw new Exception("[" + lblHost.Text + "]" + "要有值");
                }

                if (txtAccount.Text == "") {
                    throw new Exception("[" + lblAccount.Text + "]" + "要有值");
                }

                if (txtPassword.Text == "") {
                    throw new Exception("[" + lblPassword.Text + "]" + "要有值");
                }

                if (txtPort.Text == "") {
                    throw new Exception("[" + lblPort.Text + "]" + "要有值");
                }
                #endregion

                var mode = rbxProd.Checked ? "1" : "2";

                StartDosCommand(@"C:\\PROGRAM FILES (X86)\\FOURJS\\GDC\\BIN\\GDC.EXE", " -M");

                var connection = new Telnet(txtHost.Text, Convert.ToInt32(txtPort.Text));

                if (connection.ResponseIncludes("login:")) {
                    connection.SendKeys(txtAccount.Text);
                }

                if (connection.ResponseIncludes("Password:")) {
                    connection.SendKeys(txtPassword.Text);
                }

                if (connection.ResponseIncludes("Please Select [1]topprod[2]toptest [*]Exit [1]:")) {
                    connection.SendKeys(mode);
                }
                else {
                    throw new Exception("無法登入,請確認帳密是否正確");
                }

                Hide();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 背景執行Dos指令(無視窗)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        private void StartDosCommand(string fileName, string arguments) {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = fileName;
            processStartInfo.Arguments = arguments;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.Start();
        }

        private void icoNotify_BalloonTipClicked(object sender, EventArgs e) {
            ShowForm();
        }

        private void ShowForm() {
            Show();
            Activate();
            Focus();
        }

        private void icoNotify_DoubleClick(object sender, EventArgs e) {
            ShowForm();
        }

        private void icoNotify_Click(object sender, EventArgs e) {
            ShowForm();
        }
    }
}