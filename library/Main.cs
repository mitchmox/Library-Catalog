using System;
using System.Collections.Generic;
using System.IO;

namespace library
{
	class MainClass
	{
		public static void Main ()
		{
			Dictionary<string,string[]> patrons = new Dictionary <string,string[]> ();

			Dictionary<string,string[]> staff = new Dictionary <string,string[]> ();

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
					Patron.Validate (patrons, username);
				}
				else if (staff.ContainsKey (username))
				{
					Staff.Validate (ref staff, ref patrons, username);
				}
				else
				{
					Console.WriteLine (@"
---------------------------------------------
 !!! Username not found. Please try again !!! 
---------------------------------------------");
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
 