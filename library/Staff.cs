using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Staff: ILibrary 
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

			Console.Write(@"

      Welcome {0}!", username);

			string command = "";

			do
			{
				command = UI.PromptLine (@"
--------------------------
  How may we assist you? 

       You may:
    1 - check out materials
    2 - check in materials
    3 - reset password
    4 - add user
    5 - restore library
    6 - quit
--------------------------
 - ");
				Console.WriteLine();
				switch (command.ToLower ())
				{
					case "1":
					case "check":
					case "check out":
					case "out":
						string checkoutUser = UI.PromptLine("Whom would you like to check books out for?\n-");

						while (!(patrons.ContainsKey (checkoutUser) || staff.ContainsKey (checkoutUser))) 
						{
							Console.WriteLine (@"
---------------------------------------------
!!! Username not found. Please try again !!! 
---------------------------------------------");

							checkoutUser = UI.PromptLine("Whom would you like to check books out for?\n-");
						}

						Patron.CheckOut(checkoutUser);
						break;
					case "2":
					case "return":
					case "return books":
						Console.WriteLine(@"
------------------------
!!!BOOKS NOT RETURNED!!!
------------------------");
						//Staff.Return (username);
						break;
					case "3":
					case "reset password":
					case "reset":
						string resetUser = UI.PromptLine("For whom would you like to change the password?\n-");

						while (!(patrons.ContainsKey (resetUser) || staff.ContainsKey (resetUser))) 
						{
							Console.WriteLine (@"
---------------------------------------------
!!! Username not found. Please try again !!! 
---------------------------------------------");

							resetUser = UI.PromptLine("For whom would you like to change the password?\n-");
						}
						Account.ResetPassword(resetUser, ref staff, ref patrons);
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
					case "q":
					case "quit":
						break;
					default:
						Console.WriteLine (@"
-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
						break;
				}
			} while(!(command.ToLower().Contains("6") || command.ToLower().Contains("q") || (command.ToLower().Contains("quit"))));
		}

		/// <summary>
		/// Checks out a material to the username sent to the function. (i.e. the user whom the STAFF user sent in line 87)
		/// </summary>
		/// <param name="name">Name.</param>
		public static void CheckOut(string username)
		{
			StreamReader catalog = FIO.OpenReader ("catalog.txt");

			List<string> barcodes = new List<string>();
			List<string> materials = new List<string>();

			List<string> listOfCheckedOut = new List<string> ();

			while (!catalog.EndOfStream)
			{
				string line = catalog.ReadLine ();
				barcodes.Add(line.Substring(0,6));
				materials.Add(line);
			}

			string barcode = ""; 

			do 
			{
				barcode = UI.PromptLine (@"
--------------------------------------------------------------------------------
        Enter the barcode for the material that you'd like to check out. 
When you are finished checking out materials, enter 'Q' for the barcode to quit.
--------------------------------------------------------------------------------

- ");									
				if (barcodes.Contains (barcode))//checks if barcode is in the array of barcodes
				{
					Console.WriteLine();
					string[] material = materials[barcodes.IndexOf(barcode)].Split(',');
					listOfCheckedOut.Add(materials[barcodes.IndexOf(barcode)]);

					//removes the materials from available barcodes and materials list
					materials.Remove(materials[barcodes.IndexOf(barcode)]);
					barcodes.Remove(barcode);

					foreach (string x in material)
					{
						Console.WriteLine ("     " + x);
					}

					Console.WriteLine ("\n- ITEM CHECKED OUT TO "  + username.ToUpper () +"  -  ");
				}
				else
				{

					if (barcode.ToLower() != "q") //makes it so if 'Q' is typed, invalid barcode isn't printed
					{
						Console.WriteLine ();
						Console.WriteLine (@"
-----------------------------------------------------------------------
!!! Invalid barcode. Material is either checked out or nonexistent. !!!
-----------------------------------------------------------------------");
						Console.WriteLine ();
					}
				}
			}while(barcode.ToUpper() != "Q");

			catalog.Close ();

			StreamWriter outMaterials = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "checkedOut.txt");
			StreamWriter updateCatalog = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "catalog.txt");

			Console.WriteLine ();
			Console.WriteLine (" ALL ITEMS CHECKED OUT TO " + username.ToUpper () + " ARE: \n");

			foreach (string x in listOfCheckedOut)
			{
				outMaterials.WriteLine (x);

				string[] material = x.Split (',');

				foreach (string y in material)
				{
					Console.WriteLine ("     " + y);
				}

				Console.WriteLine ();
			}

			foreach (string x in materials)
			{
				updateCatalog.WriteLine (x);
			}

			updateCatalog.Close ();
			outMaterials.Close ();
		}


		public static void CheckIn()
		{
			Console.WriteLine("Material was not checked in!");
		}

		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="staff">Staff.</param>
		/// <param name="patrons">Patrons.</param>
		public static void NewUser(ref Dictionary<string, string[]> staff, ref Dictionary<string, string[]> patrons)
		{
			string username = "";

			username = UI.PromptLine ("Enter username for new user: ");

			//checks if username is already taken by staff or patrons
			while (staff.ContainsKey (username) || patrons.ContainsKey (username)) 
			{
				Console.WriteLine ("USERNAME IS ALREADY TAKEN");
				username = UI.PromptLine ("Enter username for new user: ");
			}
				
			string name = UI.PromptLine ("Enter name for new user: ");
		
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

			Console.WriteLine ("Library Restored");
		}
	}
}