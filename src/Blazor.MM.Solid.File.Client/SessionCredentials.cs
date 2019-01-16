namespace Blazor.MM.Solid.File.Client
{
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
