using DNSChanger.Core.Models;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DNSChanger.Core.Services
{
    public interface IDNSService
    {
        Task<bool> SetDNSAsync(DNSItem dnsItem);
        Task<DNSItem> GetCurrentDNSAsync();
        IEnumerable<DNSItem> GetAvailableDNSServers();
        NetworkInterface? GetCurrentInterface();
    }
}