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

			staff ["admin"] = new string[] { "adminNmae", "1234" }; //creates entry level user so program can be run once
			patrons ["kid"] = new string[] { "kidName", "12" };

			//opens the users files and adds the contents to 
			GetUsers ("patrons", patrons);
			GetUsers ("staff", staff);

			Console.WriteLine ("The D. Moxinuzzi Library LMS");

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
		






		public static void GetUsers(string usersgroup, Dictionary<string,string[]> userDict)
		{
			if (FIO.GetLocation ("users-" + usersgroup + ".txt") != "")
			{
				StreamReader readUsers = FIO.OpenReader ("users-" + usersgroup + ".txt");

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
 