using System.Text.Json.Serialization;

namespace VkToTgRetranslator.Models.Vk.GetHistory
{
	public class Action
	{
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("member_id")]
		public int? MemberId { get; set; }
	}

	public class Attachment
	{
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("photo")]
		public Photo Photo { get; set; }
	}

	public class ChatSettings
	{
		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("members_count")]
		public int? MembersCount { get; set; }

		[JsonPropertyName("friends_count")]
		public int? FriendsCount { get; set; }

		[JsonPropertyName("state")]
		public string State { get; set; }

		[JsonPropertyName("active_ids")]
		public List<int?> ActiveIds { get; set; }

		[JsonPropertyName("is_service")]
		public bool? IsService { get; set; }

		[JsonPropertyName("short_poll_reactions")]
		public bool? ShortPollReactions { get; set; }
	}

	public class Conversation
	{
		[JsonPropertyName("peer")]
		public Peer Peer { get; set; }

		[JsonPropertyName("last_message_id")]
		public int? LastMessageId { get; set; }

		[JsonPropertyName("in_read")]
		public int? InRead { get; set; }

		[JsonPropertyName("out_read")]
		public int? OutRead { get; set; }

		[JsonPropertyName("last_conversation_message_id")]
		public int? LastConversationMessageId { get; set; }

		[JsonPropertyName("in_read_cmid")]
		public int? InReadCmid { get; set; }

		[JsonPropertyName("out_read_cmid")]
		public int? OutReadCmid { get; set; }

		[JsonPropertyName("unread_count")]
		public int? UnreadCount { get; set; }

		[JsonPropertyName("is_marked_unread")]
		public bool? IsMarkedUnread { get; set; }

		[JsonPropertyName("important")]
		public bool? Important { get; set; }

		[JsonPropertyName("can_write")]
		public CanWrite CanWrite { get; set; }

		[JsonPropertyName("can_send_money")]
		public bool? CanSendMoney { get; set; }

		[JsonPropertyName("can_receive_money")]
		public bool? CanReceiveMoney { get; set; }

		[JsonPropertyName("chat_settings")]
		public ChatSettings ChatSettings { get; set; }

		[JsonPropertyName("is_new")]
		public bool? IsNew { get; set; }
	}

	public class FwdMessage
	{
		[JsonPropertyName("date")]
		public int Date { get; set; }

		[JsonPropertyName("from_id")]
		public int? FromId { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("attachments")]
		public List<object> Attachments { get; set; }
	}

	public class Item
	{
		[JsonPropertyName("date")]
		public int? Date { get; set; }

		[JsonPropertyName("from_id")]
		public int? FromId { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("out")]
		public int? Out { get; set; }

		[JsonPropertyName("attachments")]
		public List<Attachment> Attachments { get; set; }

		[JsonPropertyName("conversation_message_id")]
		public int ConversationMessageId { get; set; }

		[JsonPropertyName("fwd_messages")]
		public List<FwdMessage> FwdMessages { get; set; }

		[JsonPropertyName("important")]
		public bool? Important { get; set; }

		[JsonPropertyName("is_hidden")]
		public bool? IsHidden { get; set; }

		[JsonPropertyName("peer_id")]
		public int? PeerId { get; set; }

		[JsonPropertyName("random_id")]
		public int? RandomId { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("action")]
		public Action Action { get; set; }
	}

	public class OnlineInfo
	{
		[JsonPropertyName("visible")]
		public bool? Visible { get; set; }

		[JsonPropertyName("last_seen")]
		public int? LastSeen { get; set; }

		[JsonPropertyName("is_online")]
		public bool? IsOnline { get; set; }

		[JsonPropertyName("is_mobile")]
		public bool? IsMobile { get; set; }

		[JsonPropertyName("app_id")]
		public int? AppId { get; set; }
	}

	public class Photo
	{
		[JsonPropertyName("album_id")]
		public int? AlbumId { get; set; }

		[JsonPropertyName("date")]
		public int? Date { get; set; }

		[JsonPropertyName("id")]
		public int? Id { get; set; }

		[JsonPropertyName("owner_id")]
		public int? OwnerId { get; set; }

		[JsonPropertyName("access_key")]
		public string AccessKey { get; set; }

		[JsonPropertyName("sizes")]
		public List<Size> Sizes { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }
	}

	public class Profile
	{
		[JsonPropertyName("id")]
		public int? Id { get; set; }

		[JsonPropertyName("sex")]
		public int? Sex { get; set; }

		[JsonPropertyName("screen_name")]
		public string ScreenName { get; set; }

		[JsonPropertyName("photo_50")]
		public string Photo50 { get; set; }

		[JsonPropertyName("photo_100")]
		public string Photo100 { get; set; }

		[JsonPropertyName("online_info")]
		public OnlineInfo OnlineInfo { get; set; }

		[JsonPropertyName("online")]
		public int? Online { get; set; }

		[JsonPropertyName("first_name")]
		public string FirstName { get; set; }

		[JsonPropertyName("last_name")]
		public string LastName { get; set; }

		[JsonPropertyName("online_app")]
		public int? OnlineApp { get; set; }
	}

	public class Response
	{
		[JsonPropertyName("count")]
		public int? Count { get; set; }

		[JsonPropertyName("items")]
		public List<Item> Items { get; set; }

		[JsonPropertyName("profiles")]
		public List<Profile> Profiles { get; set; }

		[JsonPropertyName("conversations")]
		public List<Conversation> Conversations { get; set; }
	}

	public class Size
	{
		[JsonPropertyName("height")]
		public int? Height { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("width")]
		public int? Width { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}
