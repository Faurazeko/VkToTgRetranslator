using System.Text.Json.Serialization;

namespace VkToTgRetranslator.Models.Vk
{
	public class CanWrite
	{
		[JsonPropertyName("allowed")]
		public bool? Allowed { get; set; }
	}
}
