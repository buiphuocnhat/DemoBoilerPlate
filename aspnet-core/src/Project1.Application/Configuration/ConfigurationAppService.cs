using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Project1.Configuration.Dto;

namespace Project1.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : Project1AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
