# Kontent.ai Delivery Dynamic Extensions

 [![NuGet](https://img.shields.io/nuget/v/Simply007.Kontent.Ai.Delivery.Extensions.Dynamic.svg)](https://www.nuget.org/packages/Simply007.Kontent.Ai.Delivery.Extensions.Dynamic)

Extensions allowing to work with non-typed response from Kontent.ai Delivery API SDK for .NET.

## Get started

Supporting .NET 6.0

```sh
dotnet add Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
```

## Usage

### `GetElementValue` usage

```csharp

var response = await client.GetItemAsync<object>("text_element_item");

var textElementValue = response.GetElementValue<string>("text_element");
var numberElementValue = response.GetElementValue<decimal>("number_element");
var customElementValue = response.GetElementValue<string>("text_element");
var urlSlugElementValue = response.GetElementValue<string>("richText_element");

// TBD:
// * Asset
// * Multiple Choice
// * Taxonomy

var linkedItemsValue = response.GetElementValue<IEnumerable<string>>("linked_items_element");
var subPagesValue = response.GetElementValue<IEnumerable<string>>("sub_pages_element");
var richTextElementHtmlValue = response.GetElementValue<string>("rich_text_element");

```

## Limitation

⚠ Works only for subset of elements.
