using System;
using System.Text.Json;
using VkToTgRetranslator.Models.Vk;

namespace VkToTgRetranslator.Engines
{
    class VkEngine
	{
		public static string ApiVersion = "5.81";
		private static string _token;

		static private HttpClient _getHttpClient()
		{
			var client = new HttpClient();

			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

			return client;
		}

		static private string _getMainUrl(string methodName) => $"https://api.vk.com/method/{methodName}?v={ApiVersion}";

		static private string _getFieldsString(IEnumerable<string> fields = null)
		{
			var result = "";

			if (fields != null)
			{
				if (fields.Any())
				{
					result += $"&fields=";
					foreach (var item in fields)
						result += $"{item},";
				}
			}
				
			return result;
		}

		static private T _sendGet<T, T2>(string url) where T : IVkResponse<T2>, new()
		{
			T result = new T();

			using (var client = _getHttpClient())
			{
				try
				{
					var response = client.GetAsync(url).GetAwaiter().GetResult();
					var responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();


					if (response.IsSuccessStatusCode) 
					{
						result = JsonSerializer.Deserialize<T>(responseText);

						if (result == null)
							throw new Exception("Unable to convert response to json.");
					}
					else
					{
						result.IsOk = false;
						result.Exception = new Exception(responseText);
					}

				}
				catch (Exception ex)
				{
					result.Exception = ex;
					result.IsOk = false;
				}
			}

			return result;
		}


		//https://vk.com/dev/messages.getConversations
		public GetConversationsResponse GetConversations(
			int offset = 0, int count = 20, string filter = "all", 
			int extended = 1, int? start_message_id = null, IEnumerable<string> fields = null, 
			int? group_id = null)
		{

			var url = $"{_getMainUrl("messages.getConversations")}&filter={filter}&count={count}" +
				$"&offset={offset}&extended={extended}{_getFieldsString(fields)}";

			if (start_message_id != null)
				url += $"&start_message_id={start_message_id}";

			if (group_id != null)
				url += $"&group_id={group_id}";

			return _sendGet<GetConversationsResponse, Models.Vk.GetConversations.Response>(url);
		}

		//https://vk.com/dev/messages.getHistory
		public GetHistoryResponse GetHistory(
			int peer_id, int? offset = null, int count = 20, string? user_id = null,
			int? start_message_id = null, int rev = 0, int extended = 1, 
			IEnumerable<string> fields = null, int? group_id = null)
		{

			var url = $"{_getMainUrl("messages.getHistory")}&peer_id={peer_id}&count={count}&rev={rev}" +
				$"&extended={extended}{_getFieldsString(fields)}";

			if (start_message_id != null)
				url += $"&start_message_id={start_message_id}";

			if (group_id != null)
				url += $"&group_id={group_id}";

			if (offset != null)
				url += $"&group_id={offset}";

			return _sendGet<GetHistoryResponse, Models.Vk.GetHistory.Response>(url);
		}

		public bool SetToken(string newToken)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newToken);

					var url = _getMainUrl("account.getInfo");

					var response = client.GetAsync(url).GetAwaiter().GetResult();
					var responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

					if ((responseText.Contains("error_code") && responseText.Contains("error_msg")) || !response.IsSuccessStatusCode)
						return false;

				}

				_token = newToken;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}

}
