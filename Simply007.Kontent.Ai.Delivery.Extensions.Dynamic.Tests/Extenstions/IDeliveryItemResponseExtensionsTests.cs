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

        [Fact]
        public async Task GetElementValue_LinkedItems_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<IEnumerable<string>>("linked_items_element");
            element.Should().BeEquivalentTo(new string[] { "text_element_item" });
        }

        [Fact]
        public async Task GetElementValue_SubPages_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<IEnumerable<string>>("subpages_element");
            element.Should().BeEquivalentTo(new string[] { "sample_item" });
        }

        [Fact]
        public async Task GetElementValue_RichText_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<string>("rich_text_element");
            element.Should().Be("\u003Cp\u003EThis is a \u003Cstrong\u003ERich text element value with a \u003C/strong\u003E\u003Ca data-item-id=\u00224ba847a7-26e9-4ed0-9260-79afa466330b\u0022 href=\u0022\u0022\u003Elink to the content item\u003C/a\u003E and link \u003Ca data-asset-id=\u0022be30fbe7-3889-4e52-be38-7878648d7276\u0022 href=\u0022https://assets-eu-01.kc-usercontent.com:443/247bf4c8-ac68-01de-3477-0fd61185a73a/b9bf257d-48ab-4a91-b8e8-1686d1440062/Grouped1.png\u0022\u003Eto image\u003C/a\u003E and \u003Ca data-asset-id=\u002264937196-f7de-40ed-b568-52629a4a424f\u0022 href=\u0022https://assets-eu-01.kc-usercontent.com:443/247bf4c8-ac68-01de-3477-0fd61185a73a/32a07e1c-b59c-4580-ab10-e85064b8dff9/D%20L%20NA.txt\u0022\u003Easset\u003C/a\u003E.\u003C/p\u003E\n\u003Cp\u003EList:\u0026nbsp;\u003C/p\u003E\n\u003Cul\u003E\n  \u003Cli\u003EFirst\u003C/li\u003E\n  \u003Cli\u003ESecond\u003C/li\u003E\n\u003C/ul\u003E\n\u003Cfigure data-asset-id=\u002211158702-ce78-4d80-bc6b-32c03d76e523\u0022 data-image-id=\u002211158702-ce78-4d80-bc6b-32c03d76e523\u0022\u003E\u003Cimg src=\u0022https://assets-eu-01.kc-usercontent.com:443/247bf4c8-ac68-01de-3477-0fd61185a73a/b955f8ca-09c5-4d43-8090-733553c31efa/Grouped2.png\u0022 data-asset-id=\u002211158702-ce78-4d80-bc6b-32c03d76e523\u0022 data-image-id=\u002211158702-ce78-4d80-bc6b-32c03d76e523\u0022 alt=\u0022Test description\u0022\u003E\u003C/figure\u003E\n\u003Ctable\u003E\u003Ctbody\u003E\n  \u003Ctr\u003E\u003Ctd\u003ECol1\u003C/td\u003E\u003Ctd\u003ECol2\u003C/td\u003E\u003Ctd\u003ECol3\u003C/td\u003E\u003C/tr\u003E\n  \u003Ctr\u003E\u003Ctd\u003ERow1\u003C/td\u003E\u003Ctd\u003ERow2\u003C/td\u003E\u003Ctd\u003ERow3\u003C/td\u003E\u003C/tr\u003E\n\u003C/tbody\u003E\u003C/table\u003E\n\u003Cp\u003E\u003Cbr\u003E\u003C/p\u003E");
        }

        [Fact]
        public async Task GetElementValue_MultipleChoice_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<IEnumerable<MultipleChoiceOption>>("multiple_choice_element");
            element.Should().BeEquivalentTo(new[]
                {
                new MultipleChoiceOption
                {
                    Name = "Second",
                    Codename = "second"
                },
                new MultipleChoiceOption
                {
                    Name = "Third",
                    Codename = "third"
                }
            });
        }

        [Fact]
        public async Task GetElementValue_TaxonomyElement_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<IEnumerable<TaxonomyTerm>>("taxonomy_element");
            element.Should().BeEquivalentTo(new[]{
                new TaxonomyTerm
                {
                    Name = "Term 1",
                    Codename = "term_1"
                },
                new TaxonomyTerm
                {
                    Name = "Sub Term 1",
                    Codename ="sub_term_1"
                },
                new TaxonomyTerm
                {
                    Name = "Term 2",
                    Codename = "term_2"
                }
            });
        }

        [Fact]
        public async Task GetElementValue_AssetElement_ReturnsCorrectValue()
        {
            var response = await _client.GetItemAsync<object>(itemCodename);

            var element = response.GetElementValue<IEnumerable<Asset>>("asset_element");
            element.Should().BeEquivalentTo(new[] {
                new Asset
                {
                    Name = "Grouped1.png",
                    Description= null,
                    Type= "image/png",
                    Size= 23736,
                    Url= "https://assets-eu-01.kc-usercontent.com:443/247bf4c8-ac68-01de-3477-0fd61185a73a/b9bf257d-48ab-4a91-b8e8-1686d1440062/Grouped1.png",
                    Width= 450,
                    Height= 450,
                    Renditions= new Dictionary<string, IAssetRendition>()
                },
                new Asset
                {
                    Name = "Grouped2.png",
                    Description= "Test description",
                    Type= "image/png",
                    Size= 25577,
                    Url= "https://assets-eu-01.kc-usercontent.com:443/247bf4c8-ac68-01de-3477-0fd61185a73a/b955f8ca-09c5-4d43-8090-733553c31efa/Grouped2.png",
                    Width= 450,
                    Height= 450,
                    Renditions= new Dictionary<string, IAssetRendition>()
                    {
                        ["default"] = new AssetRendition
                        {
                            RenditionId= "aaec3710-5929-41fe-af3d-851d9777ce00",
                            PresetId = "a6d98cd5-8b2c-4e50-99c9-15192bce2490",
                            Width= 260,
                            Height= 250,
                            Query= "w=260\u0026h=250\u0026fit=clip\u0026rect=95,100,260,250"
                        }
                    }
                }
            });
        }

    }
}