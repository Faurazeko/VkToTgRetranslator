using System.Text.Json.Serialization;

namespace VkToTgRetranslator.Models.Vk
{
	public class Peer
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("local_id")]
		public int LocalId { get; set; }
	}
}
