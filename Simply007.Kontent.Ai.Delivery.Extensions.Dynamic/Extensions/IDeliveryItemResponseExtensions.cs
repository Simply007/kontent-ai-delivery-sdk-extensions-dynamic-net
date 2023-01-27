using System.Text.Json;
using Kontent.Ai.Delivery.Abstractions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public static class IDeliveryItemResponseExtensions
    {
        public static T GetElementValue<T>(this IDeliveryItemResponse<object> response, string elementCodename)
        {
            // response.ApiResponse.Content
            var content = JsonDocument.Parse(response.ApiResponse.Content);

            var elements = content.RootElement.GetProperty("item").GetProperty("elements");

            var element = elements.GetProperty(elementCodename);

            var elementValue = element.GetProperty("value");
            var result =  JsonSerializer.Deserialize<T>(elementValue);
            if(result == null) {
                throw new KeyNotFoundException();
            }
            return result;
        }
    }
}