using System;
using System.Drawing;
using System.Windows.Forms;

namespace labcenter.Infrastructure
{
    public class TrayIconManager : IDisposable
    {
        private readonly NotifyIcon trayIcon;
        private ToolStripMenuItem estadoItem;

        public TrayIconManager(int pcId)
        {
            trayIcon = new NotifyIcon();
            trayIcon.Icon = SystemIcons.Application;
            trayIcon.Text = "LabCenter";
            trayIcon.Visible = true;
            trayIcon.ContextMenuStrip = BuildMenu(pcId);
            trayIcon.DoubleClick += (s, e) => OnShowRequested();
        }

        public event EventHandler ShowRequested;
        public event EventHandler HideRequested;
        public event EventHandler LogoutRequested;
        public event EventHandler ExitRequested;

        private ContextMenuStrip BuildMenu(int pcId)
        {
            ContextMenuStrip trayMenu = new ContextMenuStrip();

            ToolStripMenuItem mostrarItem = new ToolStripMenuItem("Mostrar");
            mostrarItem.Click += (s, e) => OnShowRequested();
            trayMenu.Items.Add(mostrarItem);

            ToolStripMenuItem ocultarItem = new ToolStripMenuItem("Ocultar");
            ocultarItem.Click += (s, e) => OnHideRequested();
            trayMenu.Items.Add(ocultarItem);

            trayMenu.Items.Add(new ToolStripSeparator());

            estadoItem = new ToolStripMenuItem("PC-" + pcId);
            estadoItem.Enabled = false;
            trayMenu.Items.Add(estadoItem);

            trayMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem cerrarSesionItem = new ToolStripMenuItem("Cerrar Sesión");
            cerrarSesionItem.Click += (s, e) => OnLogoutRequested();
            trayMenu.Items.Add(cerrarSesionItem);

            trayMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem salirItem = new ToolStripMenuItem("Salir");
            salirItem.Click += (s, e) => OnExitRequested();
            trayMenu.Items.Add(salirItem);

            return trayMenu;
        }

        public void UpdateStatus(int pcId, string nombreUsuario, string tiempo)
        {
            string itemText = string.Format("PC-{0} | {1}", pcId, nombreUsuario);
            if (itemText.Length > 50)
                itemText = itemText.Substring(0, 47) + "...";

            estadoItem.Text = itemText;

            string tooltipText = string.Format("PC-{0} | {1}", pcId, tiempo);
            if (tooltipText.Length > 63)
                tooltipText = tooltipText.Substring(0, 60) + "...";

            trayIcon.Text = tooltipText;
        }

        public void ShowSessionStarted(string nombreUsuario)
        {
            trayIcon.ShowBalloonTip(2000, "LabCenter", "Sesión iniciada: " + nombreUsuario, ToolTipIcon.Info);
        }

        public void ShowMinimized()
        {
            trayIcon.ShowBalloonTip(1000, "LabCenter", "Contador minimizado", ToolTipIcon.Info);
        }

        public void Dispose()
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
        }

        private void OnShowRequested()
        {
            if (ShowRequested != null)
                ShowRequested(this, EventArgs.Empty);
        }

        private void OnHideRequested()
        {
            if (HideRequested != null)
                HideRequested(this, EventArgs.Empty);
        }

        private void OnLogoutRequested()
        {
            if (LogoutRequested != null)
                LogoutRequested(this, EventArgs.Empty);
        }

        private void OnExitRequested()
        {
            if (ExitRequested != null)
                ExitRequested(this, EventArgs.Empty);
        }
    }
}
