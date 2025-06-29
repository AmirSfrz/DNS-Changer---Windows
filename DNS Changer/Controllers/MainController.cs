using DNSChanger.Core.Models;
using DNSChanger.Core.Services;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DNSChanger.WinForms.Controllers
{
    public class MainController
    {
        private readonly IDNSService _dnsService;

        public MainController(IDNSService dnsService)
        {
            _dnsService = dnsService;
        }

        public async Task<DNSItem> GetCurrentDNSAsync()
        {
            return await _dnsService.GetCurrentDNSAsync();
        }

        public NetworkInterface? GetCurrentInterface()
        {
            return _dnsService.GetCurrentInterface();
        }

            public IEnumerable<DNSItem> GetAvailableDNSServers()
        {
            return _dnsService.GetAvailableDNSServers();
        }

        public DNSItem GetMatchedServer(DNSItem currentDNS)
        {
            foreach (var server in _dnsService.GetAvailableDNSServers())
            {
                if (server.PrimaryDNS == currentDNS.PrimaryDNS)
                {
                    if (server.SecondaryDNS == null || server.SecondaryDNS == currentDNS.SecondaryDNS)
                    {
                        return server;
                    }
                }
            }
            return null;
        }

        public async Task<bool> SetDNSAsync(int selectedIndex)
        {
            var servers = _dnsService.GetAvailableDNSServers();
            if (selectedIndex < 0 || selectedIndex >= servers.Count())
            {
                return false;
            }

            var selectedServer = servers.ElementAt(selectedIndex);
            return await _dnsService.SetDNSAsync(selectedServer);
        }
    }
}