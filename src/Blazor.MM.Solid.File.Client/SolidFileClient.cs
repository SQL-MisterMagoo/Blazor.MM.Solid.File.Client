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
			set { SessionCredentials.name = value; NameChanged?.Invoke(value); }
		}

		public static async Task<string> FetchName()
		{
			var result = await JSRuntime.Current.InvokeAsync<string>("SolidFileClientDotNet.fetchFoaf", new string[] { SessionCredentials.webId, "name" });
			return result;
		}
	}

#pragma warning disable IDE1006 // Naming Styles
	public class SessionCredentials
	{
		public string credentialType { get; set; }
		public string issuer { get; set; }
		public Authorization authorization { get; set; }
		public string sessionKey { get; set; }
		public Idclaims idClaims { get; set; }
		public string webId { get; set; }
		public string idp { get; set; }
		public string name { get; set; }
	}

	public class Authorization
	{
		public string client_id { get; set; }
		public string access_token { get; set; }
		public string id_token { get; set; }
	}

	public class Idclaims
	{
		public string iss { get; set; }
		public string sub { get; set; }
		public string aud { get; set; }
		public int exp { get; set; }
		public int iat { get; set; }
		public string jti { get; set; }
		public string nonce { get; set; }
		public string azp { get; set; }
		public Cnf cnf { get; set; }
		public string at_hash { get; set; }
	}

	public class Cnf
	{
		public Jwk jwk { get; set; }
	}

	public class Jwk
	{
		public string alg { get; set; }
		public string e { get; set; }
		public bool ext { get; set; }
		public string[] key_ops { get; set; }
		public string kty { get; set; }
		public string n { get; set; }
	}
#pragma warning restore IDE1006 // Naming Styles
}
