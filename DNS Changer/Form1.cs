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
        private readonly IUpdateService _updateService;

        public Form1(MainController controller, IUpdateService updateService)
        {
            InitializeComponent();
            _controller = controller;
            _updateService = updateService;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadCurrentDNS();
            LoadAvailableDNSServers();

            // Check for updates (don't await to avoid blocking UI)
            _ = CheckForUpdatesAsync();
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


        private async Task CheckForUpdatesAsync()
        {
            try
            {
                var currentVersion = Application.ProductVersion;

                var updateAvailable = await _updateService.CheckForUpdatesAsync(currentVersion);
                if (updateAvailable)
                {
                    var result = MessageBox.Show(
                        "A new version is available! Would you like to download it now?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        var url = _updateService.GetLatestReleaseUrl();
                        if (!string.IsNullOrEmpty(url))
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = url,
                                UseShellExecute = true
                            });
                        }
                    }
                }
            }
            catch
            {
                // Silently fail - we don't want to bother users with update check errors
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
