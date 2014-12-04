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

			staff ["admin"] = new string[] {"adminNmae", "1234"}; //creates entry level user so program can be run once
			patrons ["kid"] = new string[] {"kidName", "12"};

			//opens the users files and adds the contents to 
			GetUsers ("patron", patrons);
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
-------------------------------------------------
 !!! Username not found. Please try again !!! 
-------------------------------------------------");
				}
	
			} while(!(patrons.ContainsKey(username) || staff.ContainsKey (username)));

			//i want to break this into a function but rn it work like this so it is ok

			StreamWriter staffUsers = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"staffUsers.txt");

			foreach (string key in staff.Keys)
			{	
				staffUsers.WriteLine ("{0},{1},{2}", key, staff [key] [0], staff [key] [1]);
			}
		
			staffUsers.Close ();

			StreamWriter patronUsers = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"patronUsers.txt");

			foreach (string key in patrons.Keys)
			{	
				patronUsers.WriteLine ("{0},{1},{2}", key, patrons [key] [0], patrons [key] [1]);
			}

			patronUsers.Close ();
		}



		public static void GetUsers(string usersgroup, Dictionary<string,string[]> userDict)
		{
			if (FIO.GetLocation (usersgroup + "Users.txt") != "")
			{
				StreamReader readUsers = FIO.OpenReader (usersgroup + "Users.txt");

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
 