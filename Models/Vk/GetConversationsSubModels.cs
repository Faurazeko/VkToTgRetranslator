using System.Text.Json.Serialization;

namespace VkToTgRetranslator.Models.Vk.GetConversations
{
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
        public int UnreadCount { get; set; }

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

    public class Item
    {
        [JsonPropertyName("conversation")]
        public Conversation Conversation { get; set; }

        [JsonPropertyName("last_message")]
        public LastMessage LastMessage { get; set; }
    }

    public class LastMessage
    {
        [JsonPropertyName("date")]
        public int? Date { get; set; }

        [JsonPropertyName("from_id")]
        public int? FromId { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("out")]
        public int? Out { get; set; }

        [JsonPropertyName("attachments")]
        public List<object> Attachments { get; set; }

        [JsonPropertyName("conversation_message_id")]
        public int? ConversationMessageId { get; set; }

        [JsonPropertyName("fwd_messages")]
        public List<object> FwdMessages { get; set; }

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
    }

    public class Response
    {
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("unread_count")]
        public int UnreadCount { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
}
