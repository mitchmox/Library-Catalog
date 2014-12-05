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
			string pass = "";
			ConsoleKeyInfo key;

			do{
				pass = "";

				Console.Write(@"
-------------------------------
  Please enter your password: 
-------------------------------
 - ");
				do
				{
					key = Console.ReadKey(true);

					// Backspace Should Not Work
					if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
					{
						pass += key.KeyChar;
						Console.Write("*");
					}
					else
					{
						if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
						{
							pass = pass.Substring(0, (pass.Length - 1));
							Console.Write("\b \b");
						}
					}

				}while (key.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

				if(pass!= staff[username][1])
				{
					Console.WriteLine(@"
------------------------
!!!Incorrect Password!!!
------------------------");

				}
			}while(pass!= staff[username][1]);

			Console.Write(@"

      Welcome {0}!", username);

			string command = "";

			do
			{
				command = UI.PromptLine (@"
--------------------------
  How may we assist you? 

       You may:
    1 - check out
    2 - return books
    3 - reset password
    4 - add user
    5 - restore library
    6 - quit
--------------------------
 - ");

				switch (command.ToLower ())
				{
					case "1":
					case "check":
					case "check out":
					case "out":
						Patron.CheckOut (UI.PromptLine("Whom would you like to check books out for?\n-"));
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
						Console.WriteLine(@"
-------------------------
PASSWORD NOT RESET");
						//Staff.ResetPassword(UI.PromptLine("Whom would you like to change the book out for?\n-"));
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
			} while(!(command.ToLower().Contains("6")));
		}

		public static void ResetPassword(string username)
		{
			Console.WriteLine("Password was not reset!");
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


			string password1 = "";
			string password2 = "";

			do 
			{

				password1 = "";
				password2 = "";

				//asks for the password a first time
				Console.Write (@"
-------------------------------
  Enter password for user: 
-------------------------------
 - ");


			
				ConsoleKeyInfo keys;

				do 
				{
					keys = Console.ReadKey (true);

					// Backspace Should Not Work
					if (keys.Key != ConsoleKey.Backspace && keys.Key != ConsoleKey.Enter) 
					{
						password1 += keys.KeyChar;
						Console.Write ("*");
					} else 
					{
						if (keys.Key == ConsoleKey.Backspace && password1.Length > 0) 
						{
							password1 = password1.Substring (0, (password1.Length - 1));
							Console.Write ("\b \b");
						}
					}

				} while (keys.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed


				//asks for the password a second time
				Console.Write (@"
-------------------------------
  Re-Enter password for new user: 
-------------------------------
 - ");

				do 
				{
					keys = Console.ReadKey (true);

					// Backspace Should Not Work
					if (keys.Key != ConsoleKey.Backspace && keys.Key != ConsoleKey.Enter) 
					{
						password2 += keys.KeyChar;
						Console.Write ("*");
					} else 
					{
						if (keys.Key == ConsoleKey.Backspace && password2.Length > 0) 
						{
							password2 = password2.Substring (0, (password2.Length - 1));
							Console.Write ("\b \b");
						}
					}

				} while (keys.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

				Console.WriteLine ("");

				if (password1 != password2) //if passwords do not match error message is shown
				{ 
					Console.WriteLine ("PASSWORDS DO NOT MATCH! PLEASE RE-ENTER!");
				}

			} while (password1 != password2);

				
			string password = password1;

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
			WriteUsers ("patrons", patrons);
			WriteUsers ("staff", staff);

		}
			
		/// <summary>
		/// Writes the users to their respective files.
		/// </summary>
		/// <param name="userGroup">User group.</param>
		/// <param name="userDict">User dict.</param>
		public static void WriteUsers (string userGroup, Dictionary<string,string[]> userDict)
		{
			StreamWriter writeUsers = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"users-" + userGroup + ".txt");

			foreach (string key in userDict.Keys)
			{	
				writeUsers.WriteLine ("{0},{1},{2}", key,  userDict[key] [0], userDict [key] [1]);
			}

			writeUsers.Close ();
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