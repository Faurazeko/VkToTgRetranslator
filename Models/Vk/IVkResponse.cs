using System.Text.Json.Serialization;

namespace VkToTgRetranslator.Models.Vk
{
    internal interface IVkResponse<T>
    {
        [JsonPropertyName("response")]
        public T? Response { get; set; }

        [JsonIgnore]
        public bool IsOk { get; set; }

        [JsonIgnore]
        public Exception? Exception { get; set; }
    }
}
