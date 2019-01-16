using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.MM.Solid.File.Client
{
	public class SolidFileClient
	{
		public static SessionCredentials SessionCredentials;
		public static event Action<bool> LoginStateChanged;
		public static event Action<string> NameChanged;
		public static bool LoggedIn;

		public static Task<SolidFolder> ReadFolder(string relativePath)
		{
			if (SessionCredentials is null)
			{
				return null;
			}
			Uri uri = new Uri(SessionCredentials.webId);
			string folderPath = string.Join("/", uri.GetLeftPart(UriPartial.Authority), relativePath);
			Console.WriteLine($"readFolder('{folderPath}')");
			return JSRuntime.Current.InvokeAsync<SolidFolder>("SolidFileClient.readFolder", new[] { folderPath });
		}

		public static Task<string> ReadFile(string path)
		{
			return JSRuntime.Current.InvokeAsync<string>("SolidFileClient.readFile", new[] { $"{path}" });
		}

		public static async Task Login(string WebID)
		{
			string result;
			if (!string.IsNullOrWhiteSpace(WebID))
			{
				result = await JSRuntime.Current.InvokeAsync<string>("SolidFileClient.login", new[] { WebID });
			}
			else
			{
				result = await JSRuntime.Current.InvokeAsync<string>("SolidFileClient.popupLogin", new object[] { });
			}
			LoggedIn = await IsLoggedIn();
			LoginStateChanged?.Invoke(LoggedIn);
		}

		public static async Task Logout()
		{
			var result = await JSRuntime.Current.InvokeAsync<string>("SolidFileClient.logout", null);
			SessionCredentials = default;
			LoggedIn = false;
			LoginStateChanged?.Invoke(false);
		}

		public static async Task<bool> IsLoggedIn()
		{
			try
			{
				var result = await JSRuntime.Current.InvokeAsync<object>("SolidFileClient.checkSession", new object[] { });
				if (result is null)
				{
					//logged out
					SessionCredentials = default;
					return false;
				}
				SessionCredentials = Json.Deserialize<SessionCredentials>(result.ToString());
				UserName = await FetchName();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			return false;
		}
		private static string UserName
		{
			set {
				if (SessionCredentials is null)
				{
					return;
				}
				SessionCredentials.name = value;
				NameChanged?.Invoke(value);
			}
		}

		public static async Task<string> FetchName()
		{
			var result = await JSRuntime.Current.InvokeAsync<string>("SolidFileClientDotNet.fetchFoaf", new string[] { SessionCredentials.webId, "name" });
			return result;
		}
	}

}
