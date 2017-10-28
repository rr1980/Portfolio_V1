using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RR.Common_V1
{
    public enum ViewModelPropertyType
    {
        String
    }

    public enum ViewModelValidationType
    {
        NotNull
    }

    public interface IAttributeService<IViewModelAttribute>
    {
        List<IViewModelAttribute> GetAllByType(Type type);
        List<IViewModelAttribute> GetAllByType<TSource>();
        List<IViewModelAttribute> GetAllByObj<TSource>(TSource obj);
        IViewModelAttribute GetByProperty<TSource>(Expression<Func<TSource, object>> property);
        IViewModelAttribute GetByName<TSource>(string propertyName);
    }
}
