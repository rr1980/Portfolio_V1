using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RR.AttributeService_V1
{
    public class AttributeServiceException<TSource> : Exception
    {
        public TSource Obj { get; private set; }
        public Type Type { get; private set; }
        public Expression<Func<TSource, object>> Property { get; private set; }

        public AttributeServiceException(string message, Exception ex, TSource obj) : base(message + (obj != null ? " ('" + obj.GetType().Name + "')" : ""), ex)
        {
            Obj = obj;
            Type = typeof(TSource);
        }

        public AttributeServiceException(string message, Exception ex, Expression<Func<TSource, object>> property) : base(message + (property != null ? " ('" + property.ToString() + "')" : ""), ex)
        {
            Property = property;
            Type = typeof(TSource);
        }
    }

    public class AttributeServiceException : Exception
    {
        public PropertyInfo Property { get; private set; }
        public IEnumerable<PropertyInfo> Properties { get; private set; }
        public Type Type { get; private set; }
        public string PropertyName { get; private set; }

        public AttributeServiceException(string message, Exception ex) : base(message, ex)
        {

        }

        public AttributeServiceException(string message, Exception ex, PropertyInfo property) : base(message + (property != null ? " ('" + property.Name + "')" : ""), ex)
        {
            Property = property;
            PropertyName = property.Name;
            Type = property.PropertyType;
        }

        public AttributeServiceException(string message, Exception ex, IEnumerable<PropertyInfo> properties) : base(message, ex)
        {
            Properties = properties;
        }

        public AttributeServiceException(string message, Exception ex, Type type) : base(message + (type != null ? " ('" + type.Name + "')" : ""), ex)
        {
            Type = type;
        }

        public AttributeServiceException(string message, Exception ex, Type type, string propertyName) : base(message + (String.IsNullOrEmpty(propertyName) == true ? "" : " ('" + propertyName + "')"), ex)
        {
            Type = type;
            PropertyName = propertyName;
        }
    }
}
