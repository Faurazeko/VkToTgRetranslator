using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkToTgRetranslator.Models
{
	public class DbConversation
	{
		public int Id { get; set; }
		public int ChatId { get; set; }
		public int LastReadConvMsgId { get; set; }
		public int LastReadMsgId { get; set; }
	}
}
