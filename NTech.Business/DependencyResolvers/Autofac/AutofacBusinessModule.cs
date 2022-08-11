using Autofac;
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
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();
            builder.RegisterType<EfImageDal>().As<IImageDal>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<EfUsingStatusDal>().As<IUsingStatusDal>().SingleInstance();
            #endregion

            #region Business
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            #endregion

            #region UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
