using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Cloud.BookList.Authorization;
using Cloud.BookList.CloudBookLists.BookLists.Mapper;
using Cloud.BookList.CloudBookLists.Books.Authorization;
using Cloud.BookList.CloudBookLists.Books.Mapper;
using Cloud.BookList.CloudBookLists.BookTags.Authorization;
using Cloud.BookList.CloudBookLists.BookTags.Mapper;

namespace Cloud.BookList
{
    [DependsOn(
        typeof(BookListCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BookListApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BookListAuthorizationProvider>();


            #region ���鵥Ȩ��

            // �鼮��ǩȨ��
            Configuration.Authorization.Providers.Add<BookTagAuthorizationProvider>();

            // �鼮Ȩ��
            Configuration.Authorization.Providers.Add<BookAuthorizationProvider>();

            // �鵥Ȩ��
          Configuration.Authorization.Providers.Add<CloudBookLists.BookLists.Authorization.BookListAuthorizationProvider>();

            #endregion


            // �Զ�������ӳ��
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {

                #region ���鵥Mapper

                // �鼮��ǩ
                BookTagMapper.CreateMappings(configuration);

                // �鼮
                BookMapper.CreateMappings(configuration);

                // �鵥
                BookListMapper.CreateMappings(configuration);

                #endregion

            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BookListApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
