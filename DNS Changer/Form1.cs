using DNSChanger.Core.Services;
using DNSChanger.WinForms.Controllers;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Text;
using System.Net.NetworkInformation;
namespace DNS_Changer
{


    public partial class Form1 : Form
    {
        private readonly MainController _controller;

        public Form1()
        {
            InitializeComponent();
            _controller = new MainController(new DNSService());
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadCurrentDNS();
            LoadAvailableDNSServers();
        }

        private async Task LoadCurrentDNS()
        {
            try
            {
                var currentDNS = await _controller.GetCurrentDNSAsync();
                if (currentDNS != null)
                {
                    currentDns1.Text = currentDNS.PrimaryDNS ?? "Not set";
                    currentDns2.Text = currentDNS.SecondaryDNS ?? "Not set";

                    var matchedServer = _controller.GetMatchedServer(currentDNS);
                    CurrentDnsNameTxt.Text = matchedServer?.Name ?? "Custom";
                    CurrentDnsNameTxt.ForeColor = matchedServer != null ? Color.Green : Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading current DNS: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAvailableDNSServers()
        {
            DnsList.Items.Clear();
            foreach (var server in _controller.GetAvailableDNSServers())
            {
                DnsList.Items.Add(server.Name);
            }
        }

        private async void setDnsButton_Click(object sender, EventArgs e)
        {
            if (DnsList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a DNS provider from the list.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                setDnsButton.Enabled = false;
                DnsList.Enabled = false;

                var success = await _controller.SetDNSAsync(DnsList.SelectedIndex);
                if (success)
                {
                    MessageBox.Show("DNS set successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadCurrentDNS();
                }
                else
                {
                    MessageBox.Show("Failed to set DNS.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting DNS: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                setDnsButton.Enabled = true;
                DnsList.Enabled = true;
            }
        }
    }
}
