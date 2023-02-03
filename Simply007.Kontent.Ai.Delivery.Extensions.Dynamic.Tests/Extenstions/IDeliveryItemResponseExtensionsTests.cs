using RichardSzalay.MockHttp;
using Kontent.Ai.Delivery;
using Kontent.Ai.Delivery.Builders.DeliveryClient;
using FluentAssertions;
using Kontent.Ai.Delivery.Abstractions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public class IDeliveryItemResponseExtensionsTests
    {
        private const string itemCodename = "content_item";
        private const string itemJsonCodename = itemCodename + ".json";
        private readonly string _baseUrl;
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly Guid _guid;
        private readonly string _projectId;
        private readonly IDeliveryClient _client;

        private HttpClient httpClient => _mockHttp.ToHttpClient();


        public IDeliveryItemResponseExtensionsTests()
        {
            _guid = Guid.NewGuid();
            _projectId = _guid.ToString();
            _baseUrl = $"https://deliver.kontent.ai/{_projectId}";
            _mockHttp = new MockHttpMessageHandler();
            string url = $"{_baseUrl}/items/";

            _mockHttp
                .When($"{url}{itemCodename}")
                .Respond("application/json", File.ReadAllText(Path.Combine(Environment.CurrentDirectory, $"Data{Path.DirectorySeparatorChar}{itemJsonCodename}")));

            _client = DeliveryClientBuilder.WithProjectId(_projectId)
                .WithDeliveryHttpClient(new DeliveryHttpClient(httpClient))
                .Build();
        }

        [Fact]
        public async Task GetElementValue_Text_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<string>("text_element");
            element.Should().Be("Ut tincidunt tincidunt erat");
        }

        [Fact]
        public async Task GetElementValue_Number_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<decimal>("number_element");
            element.Should().Be(42);
        }

                [Fact]
        public async Task GetElementValue_CustomElement_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<string>("custom_element");
            element.Should().Be("Test value");
        }
    }
}