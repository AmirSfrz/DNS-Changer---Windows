using DNSChanger.Core.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DNSChanger.Core.Services
{
    public class DNSService : IDNSService
    {
        private readonly DNSItem[] _dnsItems = new DNSItem[]
        {
            new DNSItem{ Name = "Automatic DHCP", PrimaryDNS = null, SecondaryDNS = null},
            new DNSItem{ Name = "Google", PrimaryDNS = "8.8.8.8", SecondaryDNS = "8.8.4.4"},
            new DNSItem{ Name = "Shecan", PrimaryDNS = "185.51.200.2", SecondaryDNS = "178.22.122.100"},
            new DNSItem{ Name = "Begzar", PrimaryDNS = "185.55.226.26", SecondaryDNS = "185.55.225.25"},
            new DNSItem{ Name = "Pishgaman", PrimaryDNS = "5.202.100.100", SecondaryDNS = "5.202.100.101"},
            new DNSItem{ Name = "Beshkan", PrimaryDNS = "181.41.194.177", SecondaryDNS = "181.41.194.186"},
            new DNSItem{ Name = "Electro", PrimaryDNS = "78.157.42.101", SecondaryDNS = "78.157.42.100"},
            new DNSItem{ Name = "Radar", PrimaryDNS = "10.202.10.11", SecondaryDNS = "10.202.10.10"}
        };

        public IEnumerable<DNSItem> GetAvailableDNSServers() => _dnsItems;

        public async Task<DNSItem> GetCurrentDNSAsync()
        {
            return await Task.Run(() =>
            {
                var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up &&
                                ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .OrderByDescending(ni => ni.GetIPProperties().GatewayAddresses.Count);

                foreach (var ni in interfaces)
                {
                    var ipProps = ni.GetIPProperties();
                    var dnsAddresses = ipProps.DnsAddresses;
                    var ipv4Dns = dnsAddresses.Where(d => d.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                              .Take(2)
                                              .ToList();

                    if (ipv4Dns.Count > 0)
                    {
                        return new DNSItem
                        {
                            PrimaryDNS = ipv4Dns[0].ToString(),
                            SecondaryDNS = ipv4Dns.Count > 1 ? ipv4Dns[1].ToString() : null
                        };
                    }
                }
                return null;
            });
        }

        public async Task<bool> SetDNSAsync(DNSItem dnsItem)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var activeInterface = NetworkInterface.GetAllNetworkInterfaces()
                        .Where(ni => ni.OperationalStatus == OperationalStatus.Up &&
                                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                                    ni.GetIPProperties().GatewayAddresses.Count > 0)
                        .FirstOrDefault();

                    if (activeInterface == null) return false;

                    string interfaceName = activeInterface.Name;
                    return SetDNS(interfaceName, dnsItem.PrimaryDNS, dnsItem.SecondaryDNS);
                }
                catch
                {
                    return false;
                }
            });
        }

        private bool SetDNS(string interfaceName, string primaryDNS, string secondaryDNS)
        {
            try
            {
                ProcessStartInfo psi;

                if (string.IsNullOrEmpty(primaryDNS))
                {
                    psi = new ProcessStartInfo
                    {
                        FileName = "netsh",
                        Arguments = $"interface ip set dns \"{interfaceName}\" dhcp",
                        Verb = "runas",
                        UseShellExecute = true,
                        CreateNoWindow = true
                    };
                    Process.Start(psi).WaitForExit();
                    return true;
                }

                psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"interface ip set dns \"{interfaceName}\" static {primaryDNS}",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                };
                Process.Start(psi).WaitForExit();

                if (!string.IsNullOrEmpty(secondaryDNS))
                {
                    psi.Arguments = $"interface ip add dns \"{interfaceName}\" {secondaryDNS} index=2";
                    Process.Start(psi).WaitForExit();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}