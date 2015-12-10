using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ZC_IT_TimeTracking.DataAccess.Library.Entities;

namespace ZC_IT_TimeTracking.DataAccess.Library.MapperUtility
{

    /// <summary>
    /// Custom converter used to handle mapping conflicts between Sprocs and entities
    /// Ref: http://www.dominicpettifer.co.uk/Blog/45/automapper---a-custom-type-converter-that-exposes-a-destination-value
    /// TODO: Fill gaps in this and enhance the performance
    /// <typeparam name="IDataReader"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class CustomConverter<IDataReader, T> : ITypeConverter<System.Data.IDataReader, T> 
    { 

        private ResolutionContext _Context = null;
        private Dictionary<string, string> _CustomProperties { get; set; }

        public T Convert(ResolutionContext context) 
        { 
            _Context = context; // Save the context for future usage
 
            if (_Context.SourceValue != null &&
                !(_Context.SourceValue is System.Data.IDataReader)) 
            { 
                string message = "Value supplied is of type {0} but expected {1}.\n" + 
                                 "Change the type converter source type, or redirect " + 
                                 "the source value supplied to the value resolver using FromMember."; 
 
                throw new AutoMapperMappingException(_Context, string.Format(
                    message, typeof(System.Data.IDataReader), _Context.SourceValue.GetType())); 
            }

            _CustomProperties = new Dictionary<string, string>();

            foreach (PropertyInfo propInfo in context.DestinationType.GetProperties())
            {
                //Loop through properties for any mismatched types
                if (propInfo.GetCustomAttributes(typeof(DBColumn),true).ToList().Any())
                {
                    DBColumn propertyValue = (DBColumn)propInfo.GetCustomAttributes(typeof(DBColumn),true).First();
                    _CustomProperties.Add(propertyValue.ColumnName, propInfo.Name);
                }
            }
           
            return ConvertCore((System.Data.IDataReader)context.SourceValue); 
        }  

        protected T ExistingDestination()
        { 
            if (_Context == null) 
            { 
                string message = "ResolutionContext is not yet set. " + 
                                    "Only call this property inside the 'ConvertCore' method."; 
                                      
                throw new InvalidOperationException(message); 
            } 
 
            if (_Context.DestinationValue != null && 
                !(_Context.DestinationValue is T)) 
            { 
                string message = "Destination Value is of type {0} but expected {1}."; 
 
                throw new AutoMapperMappingException(_Context, string.Format( 
                    message, typeof(T), _Context.DestinationValue.GetType())); 
            } 
 
            return (T)_Context.DestinationValue; 
        }

        protected T ConvertCore(System.Data.IDataReader source)
        {
            T obj = ExistingDestination();
            if (obj != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in _CustomProperties)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(keyValuePair.Key, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(obj, System.Convert.ChangeType(source[keyValuePair.Value], prop.PropertyType),null);
                    }
                }
            }
            return obj;
        }
    }
}
