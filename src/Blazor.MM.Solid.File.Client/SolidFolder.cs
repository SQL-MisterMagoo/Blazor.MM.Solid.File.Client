using System;

namespace Blazor.MM.Solid.File.Client
{
#pragma warning disable IDE1006 // Naming Styles
	public class SolidFolder
	{
		public string type { get; set; }
		public string name { get; set; }
		public string url { get; set; }
		public DateTime modified { get; set; }
		public int size { get; set; }
		public string mtime { get; set; }
		public string parent { get; set; }
		public string content { get; set; }
		public Folder[] folders { get; set; }
		public File[] files { get; set; }
	}

	public class Folder
	{
		public string type { get; set; }
		public DateTime modified { get; set; }
		public int size { get; set; }
		public string mtime { get; set; }
		public string label { get; set; }
		public string name { get; set; }
		public string url { get; set; }
	}

	public class File
	{
		public string type { get; set; }
		public DateTime modified { get; set; }
		public int size { get; set; }
		public string mtime { get; set; }
		public string label { get; set; }
		public string url { get; set; }
		public string name { get; set; }
	}
#pragma warning restore IDE1006 // Naming Styles

}
