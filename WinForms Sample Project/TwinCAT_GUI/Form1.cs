using System.Windows.Forms;
using TcForms;
using static System.Windows.Forms.AxHost;
using System.Net.Sockets;
using System.Diagnostics;
using System.Globalization;
using TwinCAT_GUI.Controls;

namespace TwinCAT_GUI
{
    public partial class Form1 : Form
    {
        private NavigationButton activeButton;
        private int defaultPanelNavigationWidth = 0;
        private readonly TcControlLink tcControlLink = new();

        public Form1()
        {
            InitializeComponent();

            TwincatAdsState.DataBindings.Add("AdsState", tcControlLink, "TwincatAdsState");
            PlcAdsState.DataBindings.Add("AdsState", tcControlLink, "PlcAdsState");

            AddTcControls(panelHeader);

            foreach (TabPage tabPage in tabMainView.TabPages)
            {
                tabPage.Click += RemoveFocus;
            }
            // TabControl-Reiter ausblenden
            tabMainView.ItemSize = new System.Drawing.Size(0, 1); // Höhe auf 0 setzen
            tabMainView.SelectedIndex = 0; // Wechselt zum ersten Tab (Index 0)
            AddTcControls(tabMainView.TabPages[0]);
            ActivateButton(navigationButton0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            defaultPanelNavigationWidth = panelNavigation.Width;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            tcControlLink.Disconnect();
            base.OnFormClosing(e);
        }

        private void RemoveFocus(object? sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        #region -> Menu/Navigation
        private void menuButton_Click(object sender, EventArgs e)
        {
            if (this.panelNavigation.Width == 0)
            {
                this.panelNavigation.Width = defaultPanelNavigationWidth;
            }
            else
            {
                this.panelNavigation.Width = 0;
            }
        }
        private void NavigationButton_Click(object sender, EventArgs e)
        {
            if (sender is NavigationButton btn && int.TryParse(btn.Tag?.ToString(), out int tabIndex))
            {
                if (tabMainView.SelectedIndex != tabIndex)
                {
                    RemoveTcControls(tabMainView.TabPages[tabMainView.SelectedIndex]);
                    ActivateButton(btn);
                    tabMainView.SelectedIndex = tabIndex; // Wechselt zum entsprechenden Tab
                    AddTcControls(tabMainView.TabPages[tabIndex]);
                }
            }
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender is NavigationButton btn) // Sicherstellen, dass btnSender ein Button ist
            {
                if (activeButton != btn)
                {
                    activeButton?.Deactivate();
                    activeButton = btn;
                    btn.Activate();
                }
            }
        }
        #endregion

        #region -> TcControlLink
        private void AddTcControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ITcControl)
                {
                    tcControlLink.Add(control);
                }

                // Falls das Control eine GroupBox ist, rekursiv weitersuchen
                if (control is TcGroupPanel)
                {
                    control.Click += RemoveFocus;
                    AddTcControls(control); // Rekursion für Unterelemente
                }
            }
        }

        private void RemoveTcControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ITcControl)
                {
                    tcControlLink.Remove(control);
                }

                // Falls das Control eine GroupBox ist, rekursiv weitersuchen
                if (control is TcGroupPanel)
                {
                    control.Click -= RemoveFocus;
                    RemoveTcControls(control); // Rekursion für Unterelemente
                }
            }
        }

#endregion
    }
}
