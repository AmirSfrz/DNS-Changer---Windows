using System.Threading.Tasks;

namespace DNSChanger.Core.Services
{
    public interface IUpdateService
    {
        Task<bool> CheckForUpdatesAsync(string currentVersion);
        string GetLatestReleaseUrl();
    }
}