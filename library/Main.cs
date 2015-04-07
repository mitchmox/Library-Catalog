using System;
using System.Collections.Generic;
using System.IO;

namespace library
{
	class MainClass:Library //takes an instance of Library and therefore has the two user dictionaries
	{
		public static void Main ()
		{
			staff ["admin"] = new string[] { "adminNmae", "1234" }; //creates entry level user so program can be run once to add more users

			GetUsers ("patrons", patrons);
			GetUsers ("staff", staff);

			Console.WriteLine ("The D. Moxinuzzi Library LMS"); //welcome message

			string username = "";

			do
			{
				username = UI.PromptLine (@"
-------------------------------
  Please enter your username: 
-------------------------------
 - ").ToLower ();

				if (patrons.ContainsKey (username))
				{
					Patron.Validate (ref staff,ref patrons, username);
				}
				else if (staff.ContainsKey (username))
				{
					Staff.Validate (ref staff, ref patrons, username);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine (@"
---------------------------------------------
 !!! Username not found. Please try again !!! 
---------------------------------------------");
					Console.ForegroundColor = ConsoleColor.Black;
				}
	
			} while(!(patrons.ContainsKey (username) || staff.ContainsKey (username)));
		}

		/// <summary>
		/// Reads users in from their respective user files.
		/// </summary>
		/// <param name="userGroup">User group.</param>
		/// <param name="userDict">User dict.</param>
		public static void GetUsers(string userGroup, Dictionary<string,string[]> userDict)												
		{
			if (FIO.GetLocation ("users-" + userGroup + ".txt") != "")
			{
				StreamReader readUsers = FIO.OpenReader ("users-" + userGroup + ".txt");

				while (!readUsers.EndOfStream)
				{
					string[] userArray = readUsers.ReadLine ().Split (',');

					userDict [userArray [0]] = new string[] { userArray [1], userArray [2]};
				}

				readUsers.Close ();
			}

		}
	}
}
 