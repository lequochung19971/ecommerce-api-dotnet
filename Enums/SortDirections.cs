using System.ComponentModel;
using System.Text.Json.Serialization;
using Castle.Components.DictionaryAdapter;

namespace Ecommerce.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirections
{
    ASC,
    DESC
}
