using System.Text.Json.Serialization;
using VkToTgRetranslator.Models.Vk.GetConversations;

namespace VkToTgRetranslator.Models.Vk
{
	public class GetConversationsResponse : IVkResponse<Response>
	{
		[JsonPropertyName("response")]
		public Response? Response { get; set; } = null;

		[JsonIgnore]
		public bool IsOk { get; set; } = true;

		[JsonIgnore]
		public Exception? Exception { get; set; } = null;
	}
}
