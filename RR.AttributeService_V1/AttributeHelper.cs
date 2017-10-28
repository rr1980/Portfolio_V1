using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RR.AttributeService_V1
{
    internal static class AttributeHelper
    {
        internal static IEnumerable<PropertyInfo> GetProperties(ILogger<AttributeService> logger, Type type)
        {
            try
            {
                logger.LogTrace("Execute AttributeHelper.GetProperties");
                return type.GetProperties().Where(p => p.GetCustomAttributes(typeof(ViewModelAttribute), false).Any());
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException("Ein Fehler ist in AttributeHelper.GetProperties aufgetreten!", ex, type);
            }
        }

        internal static List<ViewModelAttribute> GetAttributes(ILogger<AttributeService> logger, IEnumerable<PropertyInfo> properties)
        {
            try
            {
                logger.LogTrace("Execute AttributeHelper.GetAttributes");
                List<ViewModelAttribute> results = new List<ViewModelAttribute>();

                foreach (var p in properties)
                {
                    logger.LogTrace("Call AttributeHelper.GetAttribute");
                    results.Add(GetAttribute(logger, p));
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException("Ein Fehler ist in AttributeHelper.GetAttributes aufgetreten!", ex, properties);
            }
        }

        internal static ViewModelAttribute GetAttribute(ILogger<AttributeService> logger, PropertyInfo property)
        {
            try
            {
                logger.LogTrace("Execute AttributeHelper.GetAttribute");
                var _attr = (ViewModelAttribute)property.GetCustomAttributes(typeof(ViewModelAttribute), false).FirstOrDefault();
                if (_attr == null)
                {
                    throw new NullReferenceException("No ViewModelAttribute found in " + property.DeclaringType.Name + " for '" + property.Name + "'");
                }

                _attr.PropertyInfo = property;
                _attr.PropertyName = property.Name;

                if (string.IsNullOrEmpty(_attr.Label))
                {
                    _attr.Label = property.Name;
                }

                return _attr;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException("Ein Fehler ist in AttributeHelper.GetAttribute aufgetreten!", ex, property);
            }
        }
    }
}
