﻿using Core.DataAccess;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Abstract
{
    public interface IImageDal : IAsyncRepository<Image>
    {
    }
}
