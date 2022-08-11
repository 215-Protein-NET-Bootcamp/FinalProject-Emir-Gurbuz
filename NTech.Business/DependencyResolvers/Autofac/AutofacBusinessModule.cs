﻿using Autofac;
using Core.Utilities.Security.JWT;
using NTech.Business.Abstract;
using NTech.Business.Concrete;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.Concrete.EntityFramework;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.DataAccess.UnitOfWork.Concrete;
using Autofac.Extras.DynamicProxy;
using Core.Utilities.Interceptor;
using Core.Utilities.Mail;

namespace NTech.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region DataAccess
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();
            builder.RegisterType<EfImageDal>().As<IImageDal>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<EfUsingStatusDal>().As<IUsingStatusDal>().SingleInstance();
            #endregion

            #region Business
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<UsingStatusManager>().As<IUsingStatusService>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            #endregion

            #region UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #endregion

            #region Jwt
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
            #endregion

            #region EmailService
            builder.RegisterType<SmtpEmailSender>().As<IEmailSender>().SingleInstance();
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
