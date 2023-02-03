# Kontent.ai Delivery Dynamic Extensions

[![NuGet](https://img.shields.io/nuget/v/Simply007.Kontent.Ai.Delivery.Extensions.Dynamic.svg)](https://www.nuget.org/packages/Simply007.Kontent.Ai.Delivery.Extensions.Dynamic)

Extensions allowing to work with non-typed response from Kontent.ai Delivery API SDK for .NET.

## Get started

Supporting .NET 6.0

```sh
dotnet add Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
```

## Usage

### `IDeliveryItemResponse.GetElementValue` usage

```csharp

var response = await client.GetItemAsync<object>("text_element_item");

var textElementValue = response.GetElementValue<string>("text_element");
var numberElementValue = response.GetElementValue<decimal>("number_element");
var customElementValue = response.GetElementValue<string>("custom_element");
var urlSlugElementValue = response.GetElementValue<string>("richText_element");
var linkedItemsValue = response.GetElementValue<IEnumerable<string>>("linked_items_element");
var subPagesValue = response.GetElementValue<IEnumerable<string>>("sub_pages_element");
var richTextElementHtmlValue = response.GetElementValue<string>("rich_text_element");

var element = response.GetElementValue<IEnumerable<MultipleChoiceOption>>("multiple_choice_element");
var element = response.GetElementValue<IEnumerable<TaxonomyTerm>>("taxonomy_element");
var element = response.GetElementValue<IEnumerable<Asset>>("asset_element");
```

> For actual usage, feel free to check out the [`IDeliveryItemResponseExtensionsTests` tests](./Simply007.Kontent.Ai.Delivery.Extensions.Dynamic.Tests/Extenstions/IDeliveryItemResponseExtensionsTests.cs).

## Limitation

⚠ Works only for simple values. Mapping of the linked items and rich text entities it not supported yet.
