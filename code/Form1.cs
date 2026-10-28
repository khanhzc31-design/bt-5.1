using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; 

namespace Bai4._1
{
    public partial class Form1 : Form
    {
       
        private string connectionString = @"Server=.;Database=Quanlytaikhoan;Trusted_Connection=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
            SetupFormDefaults();
        }

        private void SetupFormDefaults()
        {

            this.AcceptButton = btnLogin;
            this.CancelButton = btnExit;


            txtPassword.UseSystemPasswordChar = true;
            chkShowPassword.Checked = false;
        }


        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }


        private bool ValidateInputs()
        {
            bool isValid = true;


            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                errorProvider1.SetError(txtLogin, "Vui lòng nhập tên đăng nhập!");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Vui lòng nhập mật khẩu!");
                isValid = false;
            }

            return isValid;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (!ValidateInputs())
            {
                return;
            }

            string user = txtLogin.Text.Trim();
            string pass = txtPassword.Text;
            string query = "SELECT hoten FROM Taikhoan WHERE Username = @user AND Passw = @pass";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);


                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {

                            string hoTen = result.ToString().Trim();

                            MessageBox.Show($"Đăng nhập thành công!\nXin chào: {hoTen}", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Thất bại",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi kết nối CSDL: {ex.Message}", "Lỗi SQL",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }


        private void chkShowPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            chkShowPassword_CheckedChanged(sender, e);
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            btnLogin_Click(sender, e);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            btnExit_Click(sender, e);
        }
    }
}