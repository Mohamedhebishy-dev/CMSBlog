using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Contracts
{
    public interface ISiteSettingsService
    {
        public Task<HomePageSettings> GetHomeSettingsAsync(int id);
        public Task Update(HomePageSettings homePageSettings);
        public Task<SiteSettings> GetSiteSettingsAsync();
        public Task UpdateSiteSettingsAsync(SiteSettings siteSettings);
    }
}
