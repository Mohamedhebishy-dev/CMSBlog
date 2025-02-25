using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reposotry
{
    public class repoSiteSettings : ISiteSettings
    {
      private readonly  BlogContext _BlogContext;
    
        public repoSiteSettings(BlogContext _blogContext)
        { 
            _BlogContext = _blogContext ?? throw new ArgumentNullException(nameof(_blogContext)); ;
        
          
        }
        public async Task< HomePageSettings> GetHomeSettingsAsync(int id)
        {
            var  HomeSettings = await _BlogContext.HomePageSettings.FirstOrDefaultAsync(a=>a.Id == id);
            return  HomeSettings;
        }
        public async  Task Update(HomePageSettings homePageSettings)
        {
            var old = await GetHomeSettingsAsync(homePageSettings.Id);
            old.key = homePageSettings.key;
            old.value = homePageSettings.value;
     
           await _BlogContext.SaveChangesAsync();

        }
        public async Task<SiteSettings> GetSiteSettingsAsync() 
        {
            var SiteSettings = await _BlogContext.SiteSettings.FirstOrDefaultAsync();
            return SiteSettings;
        }
        public async Task UpdateSiteSettingsAsync(SiteSettings siteSettings)
        {
            var SiteSettingsOld = await GetSiteSettingsAsync();
            SiteSettingsOld.Id = siteSettings.Id;
            SiteSettingsOld.WebSiteName = siteSettings.WebSiteName;
            SiteSettingsOld.Description = siteSettings.Description;
            SiteSettingsOld.LogoName = siteSettings.LogoName;
            SiteSettingsOld.Adress = siteSettings.Adress;
            SiteSettingsOld.ContactNumber = siteSettings.ContactNumber;
            SiteSettingsOld.FacebookLink = siteSettings.FacebookLink;
            SiteSettingsOld.InstagramLink = siteSettings.InstagramLink;
            SiteSettingsOld.XLink = siteSettings.XLink;
            SiteSettingsOld.LinkedInLink = siteSettings.LinkedInLink;
            SiteSettingsOld.ColumnLinks1 = siteSettings.ColumnLinks1;
            SiteSettingsOld.ColumnLinks2 = siteSettings.ColumnLinks2;
            SiteSettingsOld.EmailAddress = siteSettings.EmailAddress;
            SiteSettingsOld.Menu = siteSettings.Menu;
            await _BlogContext.SaveChangesAsync();
        }
    }
}
