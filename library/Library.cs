using System;

namespace library
{
	class Library
	{
		public static void Main (string[] args)
		{

			Console.WriteLine ("Welcome to the D. Moxinuzzi Library");

			string name = UI.PromptLine ("\nPlease enter your name: \n - ").ToLower();
			int count = 0;

			do
			{
				string command = UI.PromptLine ("\nHow may we assist you? \n - ");
				Console.WriteLine ();

				switch(command.ToLower())
				{
					case "check out":
						count++;
						Patron.CheckOut(name);
						break;
					case "restore":
						count++;
						Staff.Restore();
						break;
					default:
						Console.WriteLine ("Request not recognized. You can \"Check Out\" materials.");
						break;
				}
			}while(count == 0);	
		}
	}
}
