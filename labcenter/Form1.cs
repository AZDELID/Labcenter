using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using labcenter.Infrastructure;
using labcenter.Models;
using labcenter.Services;
using labcenter.Views;

namespace labcenter
{
    public partial class Form1 : Form
    {
        private readonly bool usarAPI = true;
        private readonly AuthenticationBridge authenticationBridge;
        private readonly IConfigurationService configurationService;
        private readonly IAdminAuthenticationService adminAuthenticationService;
        private readonly ILaboratoryCatalog laboratoryCatalog;
        private LabCenterConfiguration configuration;
        private bool loginAdministradorActivo;
        private bool modoAdministradorActivo;
        private bool permitirCerrar;
        private AuthenticatedUser usuarioActual;
        private SessionTimerForm contadorForm;
        private TrayIconManager trayIconManager;

        public Form1()
        {
            InitializeComponent();
            configurationService = new SettingsConfigurationService();
            adminAuthenticationService = new AdminAuthenticationService();
            laboratoryCatalog = new LaboratoryCatalog();
            authenticationBridge = CreateAuthenticationBridge();
            configuration = configurationService.Load();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            try
            {
                BackgroundImage = Image.FromFile("fondoxd.jpeg");
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                BackColor = Color.FromArgb(30, 30, 46);
            }

            MostrarNumeroPC();
            CargarLaboratoriosDisponibles();
            CentrarElementosLogin();
        }

        private AuthenticationBridge CreateAuthenticationBridge()
        {
            if (usarAPI)
            {
                var apiAuthenticationService =
                    new ApiAuthenticationService("https://backend-lab-pjnt.onrender.com");

                return new AuthenticationBridge(
                    apiAuthenticationService,
                    apiAuthenticationService
                );
            }

            return new AuthenticationBridge(
                new OfflineAuthenticationService(),
                new NullSessionLifecycleService()
            );
        }

        private void CentrarElementosLogin()
        {
            if (loginPanel != null)
            {
                int panelWidth = loginPanel.Width;
                int panelHeight = loginPanel.Height;
                loginPanel.Location = new Point(
                    (ClientSize.Width - panelWidth) / 2,
                    (ClientSize.Height - panelHeight) / 2
                );
            }

            if (adminPanel != null)
            {
                int panelWidth = adminPanel.Width;
                int panelHeight = adminPanel.Height;
                adminPanel.Location = new Point(
                    (ClientSize.Width - panelWidth) / 2,
                    (ClientSize.Height - panelHeight) / 2
                );
            }

            if (lblPC != null)
            {
                lblPC.Location = new Point(
                    (ClientSize.Width - lblPC.Width) / 2,
                    60
                );
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if ((loginPanel != null && loginPanel.Visible) || (adminPanel != null && adminPanel.Visible))
                CentrarElementosLogin();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CentrarElementosLogin();
        }

        private void MostrarNumeroPC()
        {
            if (loginAdministradorActivo || modoAdministradorActivo)
                lblPC.Text = "MODO ADMINISTRADOR";
            else
                lblPC.Text = string.Format("LAB-{0} | PC-{1}", configuration.LaboratoryId, configuration.PcId);

            lblPC.TextAlign = ContentAlignment.MiddleCenter;
            lblPC.AutoSize = true;
            CentrarElementosLogin();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (modoAdministradorActivo || usuarioActual != null)
                return;

            if (loginAdministradorActivo)
            {
                if (adminAuthenticationService.Validate(txtUser.Text, txtPass.Text))
                {
                    IniciarModoAdministrador();
                    return;
                }

                MessageBox.Show("Credenciales de administrador incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                usuarioActual = await authenticationBridge.LoginAsync(txtUser.Text, txtPass.Text);

                if (usuarioActual == null)
                {
                    MessageBox.Show("Credenciales incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(
                    $"Usuario={usuarioActual.Id}\nPC={configuration.PcId}"
                );

                await authenticationBridge.StartSessionAsync(
                    usuarioActual.Id,
                    configuration.PcId
                );
                IniciarInterfazSesion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ActivarLoginAdministrador()
        {
            if (usuarioActual != null || modoAdministradorActivo)
                return;

            loginAdministradorActivo = true;
            loginPanel.Visible = true;
            adminPanel.Visible = false;
            lblUser.Text = "ADMIN";
            btnLogin.Text = "INGRESAR ADMIN";
            LimpiarLogin();
            MostrarNumeroPC();
            CentrarElementosLogin();
        }

        private void ActivarLoginUsuario()
        {
            loginAdministradorActivo = false;
            loginPanel.Visible = true;
            adminPanel.Visible = false;
            lblUser.Text = "USUARIO";
            btnLogin.Text = "INGRESAR";
            LimpiarLogin();
            MostrarNumeroPC();
            CentrarElementosLogin();
        }

        private void IniciarModoAdministrador()
        {
            loginAdministradorActivo = false;
            modoAdministradorActivo = true;
            loginPanel.Visible = false;
            adminPanel.Visible = true;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            btnLogin.Enabled = false;
            nudAdminPc.Value = configuration.PcId;
            cmbAdminLab.SelectedItem = configuration.LaboratoryId;
            LimpiarLogin();
            MostrarNumeroPC();
            CentrarElementosLogin();
        }

        private void SalirModoAdministrador()
        {
            modoAdministradorActivo = false;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            btnLogin.Enabled = true;
            ActivarLoginUsuario();
        }

        private void IniciarInterfazSesion()
        {
            SetupTrayIcon();
            Hide();
            MostrarContadorEnVentanaSeparada();
        }

        private void SetupTrayIcon()
        {
            if (trayIconManager != null)
                return;

            trayIconManager = new TrayIconManager(configuration.PcId);
            trayIconManager.ShowRequested += (s, e) => MostrarContadorVentana();
            trayIconManager.HideRequested += (s, e) => OcultarContadorVentana();
            trayIconManager.LogoutRequested += async (s, e) => await CerrarSesion();
            trayIconManager.ExitRequested += (s, e) => SolicitarSalida();
        }

        private void MostrarContadorEnVentanaSeparada()
        {
            contadorForm = new SessionTimerForm(usuarioActual.Nombre);
            contadorForm.CloseSessionRequested += async (s, e) => await CerrarSesion();
            contadorForm.MinimizeRequested += (s, e) => OcultarContadorVentana();
            contadorForm.TimeChanged += (s, e) => ActualizarTooltipTray();

            contadorForm.Show();
            contadorForm.StartCounter();

            if (trayIconManager != null)
            {
                trayIconManager.ShowSessionStarted(usuarioActual.Nombre);
                ActualizarTooltipTray();
            }
        }

        private void MostrarContadorVentana()
        {
            if (contadorForm != null && !contadorForm.IsDisposed)
                contadorForm.ShowInFront();
        }

        private void OcultarContadorVentana()
        {
            if (contadorForm != null && !contadorForm.IsDisposed)
            {
                contadorForm.Hide();
                if (trayIconManager != null)
                    trayIconManager.ShowMinimized();
            }
        }

        private void ActualizarTooltipTray()
        {
            if (trayIconManager == null || usuarioActual == null || contadorForm == null)
                return;

            trayIconManager.UpdateStatus(configuration.PcId, usuarioActual.Nombre, contadorForm.TimeText);
        }

        private async Task CerrarSesion()
        {
            var confirm = MessageBox.Show(
                "¿Deseas cerrar sesión?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            if (contadorForm != null)
                contadorForm.StopCounter();

            try
            {
                await authenticationBridge.EndSessionAsync(configuration.PcId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finalizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            permitirCerrar = true;
            CerrarInterfazSesion();
            MessageBox.Show("Sesión finalizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Show();
            LimpiarLogin();
            permitirCerrar = false;
            CentrarElementosLogin();
        }

        private void CerrarInterfazSesion()
        {
            if (trayIconManager != null)
            {
                trayIconManager.Dispose();
                trayIconManager = null;
            }

            if (contadorForm != null && !contadorForm.IsDisposed)
            {
                contadorForm.AllowClose = true;
                contadorForm.Close();
                contadorForm = null;
            }
        }

        private void LimpiarLogin()
        {
            txtUser.Text = "";
            txtPass.Text = "";
            usuarioActual = null;
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = chkMostrar.Checked ? '\0' : '*';
        }

        private void CargarLaboratoriosDisponibles()
        {
            cmbAdminLab.Items.Clear();

            foreach (int laboratorio in laboratoryCatalog.GetAvailableLaboratories())
                cmbAdminLab.Items.Add(laboratorio);

            cmbAdminLab.SelectedItem = configuration.LaboratoryId;
        }

        private void btnAdminSave_Click(object sender, EventArgs e)
        {
            int selectedLaboratoryId = configuration.LaboratoryId;

            if (cmbAdminLab.SelectedItem != null)
                selectedLaboratoryId = (int)cmbAdminLab.SelectedItem;

            configuration = new LabCenterConfiguration((int)nudAdminPc.Value, selectedLaboratoryId);
            configurationService.Save(configuration);

            MostrarNumeroPC();
            MessageBox.Show("Configuracion guardada", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAdminExitMode_Click(object sender, EventArgs e)
        {
            SalirModoAdministrador();
        }

        private void btnAdminCloseProgram_Click(object sender, EventArgs e)
        {
            CerrarProgramaDesdeAdministrador();
        }

        private void CerrarProgramaDesdeAdministrador()
        {
            permitirCerrar = true;
            CerrarInterfazSesion();
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.A))
            {
                if (loginAdministradorActivo)
                    ActivarLoginUsuario();
                else
                    ActivarLoginAdministrador();

                return true;
            }

            if (keyData == (Keys.Control | Keys.Shift | Keys.B))
            {
                MessageBox.Show("Cerrando aplicacion por emergencia", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CerrarProgramaDesdeAdministrador();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.F4))
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!permitirCerrar)
            {
                e.Cancel = true;
                if (contadorForm != null && contadorForm.Visible)
                {
                    MessageBox.Show("Debe cerrar sesión antes de salir", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (trayIconManager != null)
                trayIconManager.Dispose();

            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                if (!permitirCerrar)
                    return;
            }

            base.WndProc(ref m);
        }

        private void SolicitarSalida()
        {
            if (!permitirCerrar && !modoAdministradorActivo)
            {
                MessageBox.Show("Debe cerrar sesión antes de salir", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            permitirCerrar = true;
            Application.Exit();
        }
    }
}
