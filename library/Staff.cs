using System;
using System.IO;
using System.Collections.Generic;

namespace library//mitchell
{
	public class Staff: ILibrary 
	{
		public static void CheckOut(string name)
		{
		}
		public static void CheckIn()
		{
		}
		public static void NewUser(string userPassword, string name, Dictionary <string, string> users, string password)
		{
			name = UI.PromptLine ("What is your first name?");
			userPassword = UI.PromptLine ("Set your password: ");

			name = name.ToUpper();

			users.Add (name, userPassword);

			Console.WriteLine (@"
------------------------------------------------------
     Thank you {0}, your password is set to {1}
          What would you like to do now?
------------------------------------------------------", name, userPassword);

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
   
   For staff:
	~ restore

 - ");
				Console.WriteLine ();

				switch(command.ToLower())
				{
					case "check out":
						count++;
						Patron.CheckOut(name, userPassword,password);
						break;
					case "out":
						count++;
						Patron.CheckOut(name, userPassword,password);
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
		public static void Restore()
		{
			StreamReader master = FIO.OpenReader ("master.txt");
			StreamWriter catalog = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "catalog.txt");
			StreamWriter checkedOut = FIO.OpenWriter (FIO.GetLocation("checkedOut.txt"), "checkedOut.txt");

			while (!master.EndOfStream)
			{
				catalog.WriteLine (master.ReadLine ());
			}

			while (!master.EndOfStream)
			{
				checkedOut.WriteLine ("");
			}


			checkedOut.Close ();
			master.Close ();
			catalog.Close ();
		}
	}
}

