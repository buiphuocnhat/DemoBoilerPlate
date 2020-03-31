using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project1.Authorization;

namespace Project1
{
    [DependsOn(
        typeof(Project1CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class Project1ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<Project1AuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(Project1ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
