using System;

namespace library
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Welcome to the D. Moxinuzzi Library");
			string command = UI.PromptLine ("How may we assist you? \n").ToLower();
			Console.WriteLine ();

			if (command.Contains ("out"))
				Commands.CheckOut ();
			else if (command.Contains ("return") || command.Contains("in"))
				Commands.Return ();
			else
				Console.WriteLine ("Request not recognized. You can \"Check Out\" materials OR \"Return\" materials.");
		}
	}
}
