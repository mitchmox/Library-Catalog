using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Staff 
	{
		/// <summary>
		/// Validates the specified username and password for STAFF and then displays a menu of actions allowed to a STAFF user.
		/// </summary>
		/// <param name="staff">Staff.</param>
		/// <param name="patrons">Patrons.</param>
		/// <param name="username">Username.</param>
		public static void Validate (ref Dictionary<string,string[]> staff, ref Dictionary<string,string[]> patrons,  string username)
		{
			Account.GetPassword (staff [username] [1]);

			string welcome = "Welcome " + username + "!";

			Console.WriteLine ();
			Console.WriteLine ();

			for (int i = 0; i < (30 - welcome.Length) / 2; i++)
			{
				Console.Write (" ");
			}

			Console.Write(welcome);

			string command = "";

			do
			{
				command = UI.PromptLine (@"
------------------------------
    How may we assist you?     

    1 - Check out materials
    2 - Check in materials
    3 - Reset user password
    4 - Add user
    5 - Restore library
    6 - See all out materials
    7 - Quit (Q)
------------------------------
 - ");
				Console.WriteLine();
				switch (command.ToLower ())
				{
					case "1":
					case "check":
					case "check out":
					case "out":
						Patron.CheckOut(Account.GetUsername(@"-----------------------------------------------
  Whom would you like to check books out for?
-----------------------------------------------", "Username not found. Please try again.", ref staff, ref patrons));
						break;
					case "2":
					case "return":
					case "return books":
						Staff.CheckIn();
						break;
					case "3":
					case "reset password":
					case "reset":
						Account.ResetPassword(Account.GetUsername(@"--------------------------------------------------------
  For which user would you like to reset the password?
--------------------------------------------------------", "Username not found. Please try again.", ref staff, ref patrons), ref staff, ref patrons);
						break;
					case "4":
					case "add user":
					case "user":
						Staff.NewUser (ref staff, ref patrons);
						break;
					case "5":
					case "restore":
					case "restore library":
						Staff.Restore ();
						break;
					case "6":
					case "see all out materials":
					case "see out":
						Staff.ShowOutMaterials();
						break;
					case "7":
					case "q":
					case "quit":
						break;
					default:
						Console.WriteLine (@"-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
						break;
				}
			} while(!(command.ToLower().Contains("7") || command.ToLower().Contains("q") || (command.ToLower().Contains("quit"))));
		}

		/// <summary>
		/// Checks in a material. REMOVES from checkedOut.txt and ADDS back to catalog.txt
		/// </summary>
		public static void CheckIn()
		{
				StreamReader checkedOut = FIO.OpenReader ("checkedOut.txt");
				List<string> barcodes = new List<string>();
				List<string> listOfCheckedOut = new List<string> ();
				List<string> listOfCheckedIn = new List<string> ();


			while (!checkedOut.EndOfStream)
				{
					string line = checkedOut.ReadLine ();
					barcodes.Add (line.Substring (0, 6));
					listOfCheckedOut.Add(line);
				}

				string barcode = "";//barcode is a string becasue we would like 'Q' to be a valid entry for a barcode.

				do 
				{
					barcode = UI.PromptLine (@"
---------------------------------------------------------------------------------
          Enter the barcode for the material that you'd like to check in. 
 When you are finished checking in materials, enter 'Q' for the barcode to quit.
---------------------------------------------------------------------------------

- ");									
					if (barcodes.Contains (barcode))//checks if barcode is in the array of barcodes
					{
						Console.WriteLine();
						string[] material = listOfCheckedOut[barcodes.IndexOf(barcode)].Split(',');

						
						//removes the materials from available barcodes and materials list
						listOfCheckedIn.Add(listOfCheckedOut[barcodes.IndexOf(barcode)].Remove(listOfCheckedOut[barcodes.IndexOf(barcode)].LastIndexOf(",")));
						
						listOfCheckedOut.Remove(listOfCheckedOut[barcodes.IndexOf(barcode)]);
						barcodes.Remove(barcode);

						foreach (string x in material)
						{
							Console.WriteLine ("     " + x);
						}

					Console.WriteLine ("\n- ITEM CHECKED IN");
					}
					else
					{

						if (barcode.ToLower() != "q")
						{
							Console.WriteLine ();
							Console.WriteLine (@"
----------------------------------------------------------------------
!!! Invalid barcode. Material is either checked in or nonexistent. !!!
----------------------------------------------------------------------");
							Console.WriteLine ();
						}
					}
				}while(barcode.ToUpper() != "Q");

				checkedOut.Close ();

				StreamWriter outMaterials = FIO.OpenWriter(FIO.GetLocation ("catalog.txt"), "checkedOut.txt");
				StreamWriter updateCatalog = FIO.AppendText (FIO.GetLocation ("catalog.txt"), "catalog.txt");

				Console.WriteLine ();

				foreach (string x in listOfCheckedOut)
				{
					outMaterials.WriteLine (x);
				}

				foreach (string x in listOfCheckedIn)
				{
					updateCatalog.WriteLine (x);
				}

				updateCatalog.Close ();
				outMaterials.Close ();
			}
			
		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="staff">Staff.</param>
		/// <param name="patrons">Patrons.</param>
		public static void NewUser(ref Dictionary<string, string[]> staff, ref Dictionary<string, string[]> patrons)
		{
			string username = "";

			username = UI.PromptLine (@"
-------------------------------
  Enter username for new user 
-------------------------------
- ").ToLower();

			//checks if username is already taken by staff or patrons
			while (staff.ContainsKey (username) || patrons.ContainsKey (username)) 
			{
				Console.WriteLine ("  !!! USERNAME IS ALREADY TAKEN !!!");
				username = UI.PromptLine ("Enter username for new user: ").ToLower();
			}
				
			string name = UI.PromptLine (@"
---------------------------
  Enter name for new user  
---------------------------
- ");
			string password = Account.PasswordMatch ();

			string access = "";

			access = UI.PromptLine ("Select access level for new user. (\'p\' for patron, \'s\' for staff): ").ToLower ();

			while (!(access == "p" || access == "s"))   //if access level given is an invalid input (i.e. p or s)
			{
				Console.WriteLine ("User can not be created. Invalid access level.");
				access = UI.PromptLine ("Select access level for new user. (\'p\' for patron, \'s\' for staff): ").ToLower ();
			}

			Console.WriteLine ();

			//adds user info to the proper list based on the access key given
			if (access == "p") 
			{
				patrons.Add (username, new string [] { name, password });
				Console.WriteLine ("USER CREATED!");
			}
			if (access == "s") 
			{
				staff.Add (username, new string [] { name, password });
				Console.WriteLine ("USER CREATED!");
			}

			//sends dictionaries of users to WriteUsers
			Account.WriteUsers ("patrons", patrons);
			Account.WriteUsers ("staff", staff);
		}
	
		public static void ShowOutMaterials()
		{
			StreamReader checkedOut = FIO.OpenReader (FIO.GetLocation("catalog.txt"),"checkedOut.txt");

			Console.WriteLine (@"
----------------------------------
  All checked out materials are:
----------------------------------");

			while(!checkedOut.EndOfStream)
			{
				string [] materials = checkedOut.ReadLine().Split(',');

				foreach (string x in materials)
				{
					Console.WriteLine ("     " + x);
				}

				Console.WriteLine ();
			}

			checkedOut.Close ();

			Console.WriteLine ("Press ENTER to return to the menu...");

			Console.ReadLine ();
		}

		/// <summary>
		/// Restores the library catalog. (In a sense, checks all books back into the library. Primarily used for testing purposes.)
		/// </summary>
		public static void Restore()
		{
			StreamReader master = FIO.OpenReader ("master.txt");
			StreamWriter catalog = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"catalog.txt");
			StreamWriter checkedOut = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"checkedOut.txt");

			while(!master.EndOfStream)
			{
				catalog.WriteLine (master.ReadLine());
			}

			checkedOut.WriteLine("");

			master.Close ();
			catalog.Close ();
			checkedOut.Close ();

			Console.WriteLine (@"-------------------
 Library Restored! 
-------------------");

			Console.WriteLine ();

			Console.WriteLine ("Press ENTER to return to the menu...");

			Console.ReadLine ();
		}
	}
}