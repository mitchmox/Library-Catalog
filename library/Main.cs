using System;

namespace library
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Welcome to the D. Moxinuzzi Library");
			int r = 0;

			string name = UI.PromptLine ("\nPlease enter your name: \n - ").ToLower();
			int id = UI.PromptInt("\nPlease enter your ID Number\n - ");

			//anyone have a better way of doing this?
			do
			{
				string command = UI.PromptLine ("\nHow may we assist you? \n - ").ToLower();
				Console.WriteLine ();


				if (command.Contains ("out"))
				{	
					Commands.CheckOut (name);
					r++;
				}
				else if (command.Contains ("return") || command.Contains ("in"))
				{
					Commands.Return ();
					r++;
				}
				else
				{
					Console.WriteLine ("Request not recognized. You can \"Check Out\" materials OR \"Return\" materials.");
				}
			} while(r == 0);
		}
	}
}
