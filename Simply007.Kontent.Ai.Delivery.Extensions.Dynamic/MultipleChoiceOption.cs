using Kontent.Ai.Delivery.Abstractions;

namespace Simply007.Kontent.Ai.Delivery.Extensions.Dynamic
{
    public class MultipleChoiceOption : IMultipleChoiceOption
    {
        public string? Codename { get; set; }

        public string? Name { get; set; }
    }
}