using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reposotry
{
    public interface ISiteSettings
    {
        public Task<HomePageSettings> GetHomeSettingsAsync(int id);
        public Task Update(HomePageSettings homePageSettings);
        public Task<SiteSettings> GetSiteSettingsAsync();
        public Task UpdateSiteSettingsAsync(SiteSettings siteSettings);

    }
}
