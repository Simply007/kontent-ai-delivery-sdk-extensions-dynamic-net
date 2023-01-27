using RichardSzalay.MockHttp;
using Kontent.Ai.Delivery;
using Kontent.Ai.Delivery.Builders.DeliveryClient;
using FluentAssertions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public class IDeliveryItemResponseExtensionsTests
    {
        private readonly string _baseUrl;
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly Guid _guid;
        private readonly string _projectId;

        private HttpClient httpClient => _mockHttp.ToHttpClient();


        public IDeliveryItemResponseExtensionsTests()
        {
            _guid = Guid.NewGuid();
            _projectId = _guid.ToString();
            _baseUrl = $"https://deliver.kontent.ai/{_projectId}";
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task GetElementValue_Text_ReturnsCorrectValue()
        {
            string url = $"{_baseUrl}/items/";

            _mockHttp
                .When($"{url}text_element_item")
                .Respond("application/json", await File.ReadAllTextAsync(Path.Combine(Environment.CurrentDirectory, $"Data{Path.DirectorySeparatorChar}text_element_item.json")));

            var client = DeliveryClientBuilder.WithProjectId(_projectId)
                .WithDeliveryHttpClient(new DeliveryHttpClient(httpClient))
                .Build();

            var response = await client.GetItemAsync<object>("text_element_item");

            var element = response.GetElementValue<string>("text_element");
            element.Should().Be("Donec elit libero sodales nec.");
        }
    }
}