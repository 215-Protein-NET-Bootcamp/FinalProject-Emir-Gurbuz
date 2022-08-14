using Autofac;
using Autofac.Extras.DynamicProxy;
using Core.Utilities.Interceptor;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.JWT;
using Core.Utilities.URI;
using NTech.Business.Abstract;
using NTech.Business.Concrete;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.Concrete.EntityFramework;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.DataAccess.UnitOfWork.Concrete;

namespace NTech.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region DataAccess
            builder.RegisterType<EfBrandDal>().As<IBrandDal>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<EfColorDal>().As<IColorDal>();
            builder.RegisterType<EfImageDal>().As<IImageDal>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();
            builder.RegisterType<EfUsingStatusDal>().As<IUsingStatusDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            #endregion

            #region Business
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<ColorManager>().As<IColorService>();
            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<UsingStatusManager>().As<IUsingStatusService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            #endregion

            #region UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            #endregion

            #region Jwt
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
            #endregion

            #region EmailService
            builder.RegisterType<SmtpEmailSender>().As<IEmailSender>().SingleInstance();
            #endregion

            #region MessageBroker
            builder.RegisterType<MqConsumerHelper>().As<IMessageConsumer>().SingleInstance();
            builder.RegisterType<MqQueueHelper>().As<IMessageBrokerHelper>().SingleInstance();
            #endregion

            #region UriService
            builder.RegisterType<UriManager>().As<IUriService>().SingleInstance();
            #endregion

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                });
        }
    }
}
