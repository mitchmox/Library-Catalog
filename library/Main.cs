using System;

namespace library
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Welcome to the D. Moxinuzzi Library");
			int r = 0;

			do
			{
				string command = UI.PromptLine ("\nHow may we assist you? \n -").ToLower();
				Console.WriteLine ();


				if (command.Contains ("out"))
				{	
					Commands.CheckOut ();
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
