using System.Text.Json.Serialization;

namespace Ecommerce.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductTypes
{
    NORMAL = 1,
    FORMAL = 2,
}