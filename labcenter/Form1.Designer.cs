using System.Windows.Forms;
using System.Drawing;

namespace labcenter
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnLogin;
        private Label lblUser;
        private Label lblPass;
        private Label lblPC;
        private CheckBox chkMostrar;
        private Panel loginPanel;
        private Panel adminPanel;
        private Label lblAdminTitle;
        private Label lblAdminPc;
        private Label lblAdminLab;
        private NumericUpDown nudAdminPc;
        private ComboBox cmbAdminLab;
        private Button btnAdminSave;
        private Button btnAdminExitMode;
        private Button btnAdminCloseProgram;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.loginPanel = new Panel();
            this.txtUser = new TextBox();
            this.txtPass = new TextBox();
            this.btnLogin = new Button();
            this.lblUser = new Label();
            this.lblPass = new Label();
            this.chkMostrar = new CheckBox();
            this.lblPC = new Label();
            this.adminPanel = new Panel();
            this.lblAdminTitle = new Label();
            this.lblAdminPc = new Label();
            this.lblAdminLab = new Label();
            this.nudAdminPc = new NumericUpDown();
            this.cmbAdminLab = new ComboBox();
            this.btnAdminSave = new Button();
            this.btnAdminExitMode = new Button();
            this.btnAdminCloseProgram = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdminPc)).BeginInit();

            this.loginPanel.Size = new Size(380, 380);
            this.loginPanel.BackColor = Color.FromArgb(180, 0, 0, 0);
            this.loginPanel.BorderStyle = BorderStyle.None;

            this.lblUser.Text = "USUARIO";
            this.lblUser.ForeColor = Color.White;
            this.lblUser.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblUser.Size = new Size(300, 30);
            this.lblUser.Location = new Point(40, 50);
            this.lblUser.BackColor = Color.Transparent;
            this.lblUser.TextAlign = ContentAlignment.MiddleLeft;

            this.txtUser.Location = new Point(40, 85);
            this.txtUser.Font = new Font("Segoe UI", 11);
            this.txtUser.Size = new Size(300, 30);
            this.txtUser.BackColor = Color.FromArgb(50, 50, 70);
            this.txtUser.ForeColor = Color.White;
            this.txtUser.BorderStyle = BorderStyle.FixedSingle;

            this.lblPass.Text = "CONTRASEÑA";
            this.lblPass.ForeColor = Color.White;
            this.lblPass.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblPass.Size = new Size(300, 30);
            this.lblPass.Location = new Point(40, 135);
            this.lblPass.BackColor = Color.Transparent;
            this.lblPass.TextAlign = ContentAlignment.MiddleLeft;

            this.txtPass.Location = new Point(40, 170);
            this.txtPass.Font = new Font("Segoe UI", 11);
            this.txtPass.Size = new Size(300, 30);
            this.txtPass.PasswordChar = '*';
            this.txtPass.BackColor = Color.FromArgb(50, 50, 70);
            this.txtPass.ForeColor = Color.White;
            this.txtPass.BorderStyle = BorderStyle.FixedSingle;

            this.chkMostrar.Text = "Mostrar contraseña";
            this.chkMostrar.ForeColor = Color.White;
            this.chkMostrar.Size = new Size(150, 25);
            this.chkMostrar.Font = new Font("Segoe UI", 9);
            this.chkMostrar.Location = new Point(40, 210);
            this.chkMostrar.BackColor = Color.Transparent;
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);

            this.btnLogin.Text = "INGRESAR";
            this.btnLogin.Size = new Size(300, 45);
            this.btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.btnLogin.Location = new Point(40, 260);
            this.btnLogin.BackColor = Color.FromArgb(0, 123, 255);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            this.btnLogin.MouseEnter += (s, e) => { this.btnLogin.BackColor = Color.FromArgb(0, 100, 200); };
            this.btnLogin.MouseLeave += (s, e) => { this.btnLogin.BackColor = Color.FromArgb(0, 123, 255); };

            this.loginPanel.Controls.Add(this.lblUser);
            this.loginPanel.Controls.Add(this.txtUser);
            this.loginPanel.Controls.Add(this.lblPass);
            this.loginPanel.Controls.Add(this.txtPass);
            this.loginPanel.Controls.Add(this.chkMostrar);
            this.loginPanel.Controls.Add(this.btnLogin);

            this.adminPanel.Size = new Size(420, 420);
            this.adminPanel.BackColor = Color.FromArgb(190, 0, 0, 0);
            this.adminPanel.BorderStyle = BorderStyle.None;
            this.adminPanel.Visible = false;

            this.lblAdminTitle.Text = "MODO ADMINISTRADOR";
            this.lblAdminTitle.ForeColor = Color.White;
            this.lblAdminTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.lblAdminTitle.Size = new Size(340, 35);
            this.lblAdminTitle.Location = new Point(40, 35);
            this.lblAdminTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAdminTitle.BackColor = Color.Transparent;

            this.lblAdminPc.Text = "Número de PC";
            this.lblAdminPc.ForeColor = Color.White;
            this.lblAdminPc.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.lblAdminPc.Size = new Size(160, 28);
            this.lblAdminPc.Location = new Point(40, 95);
            this.lblAdminPc.BackColor = Color.Transparent;

            this.nudAdminPc.Location = new Point(220, 95);
            this.nudAdminPc.Font = new Font("Segoe UI", 11);
            this.nudAdminPc.Minimum = 1;
            this.nudAdminPc.Maximum = 999;
            this.nudAdminPc.Value = 1;
            this.nudAdminPc.Size = new Size(150, 30);

            this.lblAdminLab.Text = "Laboratorio";
            this.lblAdminLab.ForeColor = Color.White;
            this.lblAdminLab.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.lblAdminLab.Size = new Size(160, 28);
            this.lblAdminLab.Location = new Point(40, 145);
            this.lblAdminLab.BackColor = Color.Transparent;

            this.cmbAdminLab.Location = new Point(220, 145);
            this.cmbAdminLab.Font = new Font("Segoe UI", 11);
            this.cmbAdminLab.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAdminLab.Size = new Size(150, 30);

            this.btnAdminSave.Text = "GUARDAR";
            this.btnAdminSave.Size = new Size(330, 42);
            this.btnAdminSave.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnAdminSave.Location = new Point(45, 210);
            this.btnAdminSave.BackColor = Color.FromArgb(40, 167, 69);
            this.btnAdminSave.ForeColor = Color.White;
            this.btnAdminSave.FlatStyle = FlatStyle.Flat;
            this.btnAdminSave.FlatAppearance.BorderSize = 0;
            this.btnAdminSave.Cursor = Cursors.Hand;
            this.btnAdminSave.Click += new System.EventHandler(this.btnAdminSave_Click);

            this.btnAdminExitMode.Text = "SALIR DEL MODO ADMIN";
            this.btnAdminExitMode.Size = new Size(330, 42);
            this.btnAdminExitMode.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnAdminExitMode.Location = new Point(45, 265);
            this.btnAdminExitMode.BackColor = Color.FromArgb(0, 123, 255);
            this.btnAdminExitMode.ForeColor = Color.White;
            this.btnAdminExitMode.FlatStyle = FlatStyle.Flat;
            this.btnAdminExitMode.FlatAppearance.BorderSize = 0;
            this.btnAdminExitMode.Cursor = Cursors.Hand;
            this.btnAdminExitMode.Click += new System.EventHandler(this.btnAdminExitMode_Click);

            this.btnAdminCloseProgram.Text = "CERRAR PROGRAMA";
            this.btnAdminCloseProgram.Size = new Size(330, 42);
            this.btnAdminCloseProgram.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnAdminCloseProgram.Location = new Point(45, 320);
            this.btnAdminCloseProgram.BackColor = Color.FromArgb(220, 53, 69);
            this.btnAdminCloseProgram.ForeColor = Color.White;
            this.btnAdminCloseProgram.FlatStyle = FlatStyle.Flat;
            this.btnAdminCloseProgram.FlatAppearance.BorderSize = 0;
            this.btnAdminCloseProgram.Cursor = Cursors.Hand;
            this.btnAdminCloseProgram.Click += new System.EventHandler(this.btnAdminCloseProgram_Click);

            this.adminPanel.Controls.Add(this.lblAdminTitle);
            this.adminPanel.Controls.Add(this.lblAdminPc);
            this.adminPanel.Controls.Add(this.nudAdminPc);
            this.adminPanel.Controls.Add(this.lblAdminLab);
            this.adminPanel.Controls.Add(this.cmbAdminLab);
            this.adminPanel.Controls.Add(this.btnAdminSave);
            this.adminPanel.Controls.Add(this.btnAdminExitMode);
            this.adminPanel.Controls.Add(this.btnAdminCloseProgram);

            this.lblPC.Text = "PC-1";
            this.lblPC.ForeColor = Color.White;
            this.lblPC.BackColor = Color.Transparent;
            this.lblPC.Font = new Font("Segoe UI", 72, FontStyle.Bold);
            this.lblPC.AutoSize = true;
            this.lblPC.TextAlign = ContentAlignment.MiddleCenter;

            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.adminPanel);
            this.Controls.Add(this.lblPC);
            this.Text = "LabCenter - Control de Ciber";
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.nudAdminPc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
