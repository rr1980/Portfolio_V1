using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RR.AttributeService_V1
{
    public class AttributeService : IAttributeService<ViewModelAttribute>
    {
        private readonly ILogger<AttributeService> _logger;

        public AttributeService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AttributeService>();
            _logger.LogInformation("AttributeService created!");
        }

        public List<ViewModelAttribute> GetAllByType(Type type)
        {
            try
            {
                _logger.LogDebug("Execute GetAllByType(Type type)");
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }
                _logger.LogTrace("Call AttributeHelper.GetProperties");
                var _properties = AttributeHelper.GetProperties(_logger, type);

                _logger.LogTrace("Call AttributeHelper.GetAttributes", _properties);
                var result = AttributeHelper.GetAttributes(_logger, _properties);

                return result;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException("Ein Fehler ist in AttributeService.GetAllByType aufgetreten!", ex, type);
            }
        }

        public List<ViewModelAttribute> GetAllByType<TSource>()
        {
            try
            {
                _logger.LogDebug("Execute GetAllByType<TSource>()");

                _logger.LogTrace("Call AttributeHelper.GetProperties");
                var _properties = AttributeHelper.GetProperties(_logger, typeof(TSource));

                _logger.LogTrace("Call AttributeHelper.GetAttributes");
                var result = AttributeHelper.GetAttributes(_logger, _properties);

                return result;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException("Ein Fehler ist in AttributeService.GetAllByType aufgetreten!", ex, typeof(TSource));
            }
        }

        public List<ViewModelAttribute> GetAllByObj<TSource>(TSource obj)
        {
            try
            {
                _logger.LogDebug("Execute GetAllByObj<TSource>(TSource obj)");
                if (obj == null)
                {
                    throw new ArgumentNullException("obj");
                }

                _logger.LogTrace("Call AttributeHelper.GetProperties");
                var _properties = AttributeHelper.GetProperties(_logger, obj.GetType());

                _logger.LogTrace("Call AttributeHelper.GetAttributes");
                var result = AttributeHelper.GetAttributes(_logger, _properties);

                return result;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException<TSource>("Ein Fehler ist in AttributeService.GetAllByObj aufgetreten!", ex, obj);
            }
        }

        public ViewModelAttribute GetByName<TSource>(string propertyName)
        {
            try
            {
                _logger.LogDebug("Execute GetByName<TSource>(string propertyName)");

                if (string.IsNullOrEmpty(propertyName))
                {
                    throw new ArgumentNullException("propertyName");
                }

                _logger.LogTrace("Call AttributeHelper.GetProperties");
                var _property = AttributeHelper.GetProperties(_logger, typeof(TSource)).FirstOrDefault(p => p.Name == propertyName);

                if (_property == null)
                {
                    throw new NullReferenceException("No Property '" + propertyName + "' found in " + typeof(TSource).Name);
                }

                _logger.LogTrace("Call AttributeHelper.GetAttributes");
                var result = AttributeHelper.GetAttribute(_logger, _property);

                return result;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException("Ein Fehler ist in AttributeService.GetByName aufgetreten!", ex, typeof(TSource), propertyName);
            }
        }

        public ViewModelAttribute GetByProperty<TSource>(Expression<Func<TSource, object>> property)
        {
            try
            {
                _logger.LogDebug("Execute GetByProperty<TSource>(Expression<Func<TSource, object>> property)");

                if (property == null)
                {
                    throw new ArgumentNullException("property");
                }

                var _property = (PropertyInfo)((MemberExpression)property.Body).Member;

                if (_property == null)
                {
                    throw new NullReferenceException("No Property '" + property + "' found in " + typeof(TSource).Name);
                }

                _logger.LogTrace("Call AttributeHelper.GetAttributes results");
                var result = AttributeHelper.GetAttribute(_logger, _property);

                return result;
            }
            catch (Exception ex)
            {

                throw new AttributeServiceException<TSource>("Ein Fehler ist in AttributeService.GetByProperty aufgetreten!", ex, property);
            }
        }
    }
}
