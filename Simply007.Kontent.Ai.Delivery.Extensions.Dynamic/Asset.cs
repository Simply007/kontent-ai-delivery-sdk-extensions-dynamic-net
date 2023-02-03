using System.Text.Json.Serialization;
using Kontent.Ai.Delivery.Abstractions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public class AssetRendition : IAssetRendition
    {
        [JsonPropertyName("rendition_id")]
        public string? RenditionId {get; set;}

        [JsonPropertyName("preset_id")]
        public string? PresetId {get; set;}

        public int Width {get; set;}

        public int Height {get; set;}

        public string? Query {get; set;}
    }

    public class Asset : IAsset
    {
        public string? Name { get; set; }

        public int Size { get; set; }

        public string? Type { get; set; }

        public Dictionary<string, IAssetRendition>? Renditions { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}