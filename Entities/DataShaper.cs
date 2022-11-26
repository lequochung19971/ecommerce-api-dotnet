using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ecommerce.Entities;

public class DataShaper<T> : IDataShaper<T>
{
    public PropertyInfo[] properties { get; set; }

    public DataShaper()
    {
        properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }
    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string[] fields)
    {
        var requiredProperties = GetRequiredProperties(fields);
        return FetchData(entities, requiredProperties);
    }

    public ExpandoObject ShapeData(T entity, string[] fields)
    {
        var requiredProperties = GetRequiredProperties(fields);
        return FetchDataForEntity(entity, requiredProperties);
    }

    private IEnumerable<PropertyInfo> GetRequiredProperties(string[] fields)
    {
        var requiredProperties = new List<PropertyInfo>();

        if (fields != null)
        {
            foreach (var field in fields)
            {
                var property = properties.FirstOrDefault(prop => prop.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                if (property == null)
                {
                    continue;
                }
                requiredProperties.Add(property);
            }
        }
        else
        {
            requiredProperties = properties.ToList();
        }

        return requiredProperties;
    }

    private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedData = new List<ExpandoObject>();
        foreach (var entity in entities)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapedData.Add(shapedObject);
        }
        return shapedData;
    }

    private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedObject = new ExpandoObject();

        foreach (var property in requiredProperties)
        {
            var objectPropertyValue = property.GetValue(entity);
            shapedObject.TryAdd(property.Name, objectPropertyValue);
        }

        return shapedObject;
    }
}