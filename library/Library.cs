﻿using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	class Library
	{
		public static void Main (string[] args)
		{

			Console.WriteLine ("Welcome to the D. Moxinuzzi Library");

			string userPassword = "1234";
			string password = "";
			Dictionary<string, string> users = new Dictionary<string, string> ();

			users.Add ("", "");

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
    ~ become a new user
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
					case "new user":
						count++;
						Staff.NewUser(userPassword, name, users, password);
						break;
					default:
						Console.WriteLine (@"
-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
						break;
				}
			}while(count == 0);
			Console.ReadLine ();
		}
	}
}
 