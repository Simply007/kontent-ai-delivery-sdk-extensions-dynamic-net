using Kontent.Ai.Delivery.Abstractions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public class TaxonomyTerm : ITaxonomyTerm
    {
        public string? Codename { get; set; }

        public string? Name { get; set; }
    }
}