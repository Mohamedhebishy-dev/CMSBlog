using Bl.Contracts;
using Domain;
using Domain.Reposotry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        ISiteSettings _repoSiteSettings;
        public SiteSettingsService(ISiteSettings repoSiteSettings)
        {
             _repoSiteSettings= repoSiteSettings;
        }
        public async Task<HomePageSettings> GetHomeSettingsAsync(int id)
        {
        var settings = await _repoSiteSettings.GetHomeSettingsAsync(id);
            return settings;
        }

        public async Task Update(HomePageSettings homePageSettings)
        {
            await _repoSiteSettings.Update(homePageSettings);
        }
        public async Task<SiteSettings> GetSiteSettingsAsync()
        {
            var SiteSettings = await _repoSiteSettings.GetSiteSettingsAsync();
            return SiteSettings;
        }
        public async Task UpdateSiteSettingsAsync(SiteSettings siteSettings)
        {
            await _repoSiteSettings.UpdateSiteSettingsAsync(siteSettings);

        }
    }
}
