using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkToTgRetranslator.Engines
{
	static public class VkTokenGetterEngine
	{
		static public string GetNewToken()
		{
			var token = "";

			Logger.Log("Getting new token for vk.com");

			try
			{
				var service = FirefoxDriverService.CreateDefaultService();
				var opts = new FirefoxOptions();
				opts.AddArgument("--headless");

				var driver = new FirefoxDriver(opts);

				driver.Url = ConfigManager.Config.VkGetTokenUrl;

				var loginElem = driver.FindElementByXPath("//input[@name='email']");
				var pswdElem = driver.FindElementByXPath("//input[@name='pass']");
				var sendBtn = driver.FindElementByXPath("//button[@id='install_allow']");
				loginElem.SendKeys(ConfigManager.Config.VkLogin);
				pswdElem.SendKeys(ConfigManager.Config.VkPassword);
				sendBtn.Click();

				var megaConfirmBtn = driver.FindElementByXPath("//button[@class='flat_button fl_r button_indent']");
				megaConfirmBtn.Click();

				token = ExtractTokenFromUrl(driver.Url);
				Logger.Log("New token received successfully.");
			}
			catch 
			{
				Logger.Log("Error occured while receiving new token :(");
				token = ""; 
			}

			return token;
		}

		static public string ExtractTokenFromUrl(string url) => url.Split("access_token=").Last().Split("&expires_in").First();
	}
}
