using Autofac;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.Concrete.EntityFramework;

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

            #endregion

        }
    }
}
