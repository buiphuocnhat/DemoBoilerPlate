using System.Threading.Tasks;
using Project1.Configuration.Dto;

namespace Project1.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
