using System;

namespace library
{
	class Library
	{
		public static void Main (string[] args)
		{

			Console.WriteLine ("Welcome to the D. Moxinuzzi Library");

			string name = UI.PromptLine (@"
---------------------------
  Please enter your name: 
---------------------------
 - ").ToLower();
			int count = 0;

			do
			{
				string command = UI.PromptLine (@"
--------------------------
  How may we assist you? 
--------------------------
   You may:
    ~ check out
    ~ return
    ~ reset password
    ~ ask Emanuel for assistance

 - ");

				Console.WriteLine ();

				switch(command.ToLower())
				{
					case "check out":
						count++;
						Patron.CheckOut(name);
						break;
					case "out":
						count++;
						Patron.CheckOut(name);
						break;
					case "restore":
						count++;
						Staff.Restore();
						break;
					default:
						Console.WriteLine (@"
-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
						break;
				}
			}while(count == 0);	
		}
	}
}
 