using System;
using System.Drawing;
using System.Windows.Forms;
using labcenter.Services;

namespace labcenter.Views
{
    public class SessionTimerForm : Form
    {
        private readonly SessionTimer sessionTimer;
        private Label lblContador;
        private Label lblNombreUsuario;
        private Button btnCerrarSesion;
        private Button btnMinimizar;

        public SessionTimerForm(string nombreUsuario)
        {
            sessionTimer = new SessionTimer();
            sessionTimer.TimeChanged += SessionTimer_TimeChanged;

            ConfigureWindow();
            BuildControls(nombreUsuario);
        }

        public event EventHandler CloseSessionRequested;
        public event EventHandler MinimizeRequested;
        public event EventHandler TimeChanged;

        public bool AllowClose { get; set; }
        public string TimeText { get { return sessionTimer.TimeText; } }

        public void StartCounter()
        {
            sessionTimer.Start();
        }

        public void StopCounter()
        {
            sessionTimer.Stop();
        }

        public void ShowInFront()
        {
            Show();
            WindowState = FormWindowState.Normal;
            TopMost = true;
            BringToFront();
        }

        private void ConfigureWindow()
        {
            Text = "LabCenter - Control de Tiempo";
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            Size = new Size(320, 160);
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.FromArgb(30, 30, 46);

            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(workingArea.Right - Width - 10, workingArea.Top + 10);
        }

        private void BuildControls(string nombreUsuario)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.FromArgb(40, 40, 55);
            panel.Padding = new Padding(10);

            Panel titleBar = CreateTitleBar();

            lblNombreUsuario = new Label();
            lblNombreUsuario.Text = "👤 " + nombreUsuario;
            lblNombreUsuario.ForeColor = Color.White;
            lblNombreUsuario.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNombreUsuario.Size = new Size(280, 25);
            lblNombreUsuario.TextAlign = ContentAlignment.MiddleCenter;
            lblNombreUsuario.Location = new Point(15, 40);
            lblNombreUsuario.BackColor = Color.Transparent;

            lblContador = new Label();
            lblContador.Text = "00:00:00";
            lblContador.ForeColor = Color.FromArgb(0, 255, 100);
            lblContador.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblContador.Size = new Size(280, 55);
            lblContador.TextAlign = ContentAlignment.MiddleCenter;
            lblContador.Location = new Point(15, 70);
            lblContador.BackColor = Color.Transparent;

            btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "🔒 Cerrar Sesión";
            btnCerrarSesion.Size = new Size(180, 32);
            btnCerrarSesion.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnCerrarSesion.BackColor = Color.FromArgb(220, 53, 69);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Location = new Point(65, 125);
            btnCerrarSesion.Click += (s, e) => OnCloseSessionRequested();
            btnCerrarSesion.MouseEnter += (s, e) => { btnCerrarSesion.BackColor = Color.FromArgb(200, 35, 55); };
            btnCerrarSesion.MouseLeave += (s, e) => { btnCerrarSesion.BackColor = Color.FromArgb(220, 53, 69); };

            panel.Controls.Add(titleBar);
            panel.Controls.Add(lblNombreUsuario);
            panel.Controls.Add(lblContador);
            panel.Controls.Add(btnCerrarSesion);
            Controls.Add(panel);

            MovableWindowBehavior.Attach(this, this);
            MovableWindowBehavior.Attach(this, panel);
            MovableWindowBehavior.Attach(this, titleBar);
        }

        private Panel CreateTitleBar()
        {
            Panel titleBar = new Panel();
            titleBar.Size = new Size(320, 30);
            titleBar.Location = new Point(0, 0);
            titleBar.BackColor = Color.FromArgb(60, 60, 75);

            Label lblTitulo = new Label();
            lblTitulo.Text = "⏱️ Control de Tiempo";
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitulo.Size = new Size(200, 25);
            lblTitulo.Location = new Point(10, 3);
            lblTitulo.BackColor = Color.Transparent;

            btnMinimizar = new Button();
            btnMinimizar.Text = "─";
            btnMinimizar.Size = new Size(30, 25);
            btnMinimizar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnMinimizar.BackColor = Color.FromArgb(0, 123, 255);
            btnMinimizar.ForeColor = Color.White;
            btnMinimizar.FlatStyle = FlatStyle.Flat;
            btnMinimizar.FlatAppearance.BorderSize = 0;
            btnMinimizar.Cursor = Cursors.Hand;
            btnMinimizar.Location = new Point(280, 3);
            btnMinimizar.Click += (s, e) => OnMinimizeRequested();
            btnMinimizar.MouseEnter += (s, e) => { btnMinimizar.BackColor = Color.FromArgb(0, 100, 200); };
            btnMinimizar.MouseLeave += (s, e) => { btnMinimizar.BackColor = Color.FromArgb(0, 123, 255); };

            titleBar.Controls.Add(lblTitulo);
            titleBar.Controls.Add(btnMinimizar);

            return titleBar;
        }

        private void SessionTimer_TimeChanged(object sender, EventArgs e)
        {
            lblContador.Text = sessionTimer.TimeText;
            OnTimeChanged();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
                OnMinimizeRequested();
                return;
            }

            sessionTimer.Dispose();
            base.OnFormClosing(e);
        }

        private void OnCloseSessionRequested()
        {
            if (CloseSessionRequested != null)
                CloseSessionRequested(this, EventArgs.Empty);
        }

        private void OnMinimizeRequested()
        {
            if (MinimizeRequested != null)
                MinimizeRequested(this, EventArgs.Empty);
        }

        private void OnTimeChanged()
        {
            if (TimeChanged != null)
                TimeChanged(this, EventArgs.Empty);
        }
    }
}
