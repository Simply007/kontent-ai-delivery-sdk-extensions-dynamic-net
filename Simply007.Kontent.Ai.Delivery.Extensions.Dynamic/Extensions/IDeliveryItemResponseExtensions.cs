using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontent.Ai.Delivery.Abstractions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public class TypeMappingConverter<TType, TImplementation> : JsonConverter<TType>
  where TImplementation : TType
    {
        [return: MaybeNull]
        public override TType Read(
          ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            JsonSerializer.Deserialize<TImplementation>(ref reader, options);

        public override void Write(
          Utf8JsonWriter writer, TType value, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, (TImplementation)value!, options);
    }


    public static class IDeliveryItemResponseExtensions
    {
        public static T GetElementValue<T>(this IDeliveryItemResponse<object> response, string elementCodename)
        {
            // response.ApiResponse.Content
            var content = JsonDocument.Parse(response.ApiResponse.Content);

            var elements = content.RootElement.GetProperty("item").GetProperty("elements");

            var element = elements.GetProperty(elementCodename);

            var elementValue = element.GetProperty("value");
            var result = JsonSerializer.Deserialize<T>(elementValue, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new TypeMappingConverter<IAssetRendition, AssetRendition>()
                }
            });
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return result;
        }
    }
}