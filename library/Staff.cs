using System;
using System.IO;
using System.Collections.Generic;

namespace library//mitchell
{
	public class Staff: ILibrary 
	{
//		public static void CheckOut(string name, string userPassword, string password)
//		{
//			Patron.CheckOut (name, userPassword,password);
//		}
		public static void CheckIn(string name, string staffPassword, string password)
		{
			password = UI.PromptLine (@"
Please enter your password: 
- ");
			if (staffPassword == password)
			{
				StreamReader catalog = FIO.OpenReader ("catalog.txt");

				List<string> barcodes = new List<string> ();
				List<string> materials = new List<string> ();

				List<string> listOfCheckedOut = new List<string> ();

				while (!catalog.EndOfStream)
				{
					string line = catalog.ReadLine ();
					barcodes.Add (line.Substring (0, 6));
					materials.Add (line);
				}

				string barcode = "";//barcode is a string becasue we would like 'Q' to be a valid entry for a barcode.

				do
				{
					barcode = UI.PromptLine (@"
--------------------------------------------------------------------------------
	    Enter the barcode for the material that you'd like to check in.
When you are finished checking in materials, enter 'Q' for the barcode to quit.
--------------------------------------------------------------------------------

	- ");									
					if (barcodes.Contains(barcode))//checks if barcode is in the array of barcodes
					{
						Console.WriteLine ();
						string[] material = materials [barcodes.IndexOf (barcode)].Split (',');
						listOfCheckedOut.Add (materials [barcodes.IndexOf (barcode)]);

						//removes the materials from available barcodes and materials list
						materials.Remove (materials [barcodes.IndexOf (barcode)]);
						barcodes.Remove (barcode);

						foreach (string x in material)
						{
							Console.WriteLine ("     " + x);
						}

						Console.WriteLine ("\n- ITEM CHECKED OUT TO " + name.ToUpper () + "  -  ");
					}
					else
					{
						if (barcode.ToLower () != "q") //makes it so if 'Q' is typed, invalid barcode isn't printed
						{
							Console.WriteLine ();
							Console.WriteLine (@"
-----------------------------------------------------------------------
!!! Invalid barcode. Material is either checked out or nonexistent. !!!
-----------------------------------------------------------------------");
							Console.WriteLine ();
						}
					}
				} while(barcode.ToUpper () != "Q");

				catalog.Close ();

				StreamWriter outMaterials = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "checkedOut.txt");
				StreamWriter updateCatalog = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "catalog.txt");

				Console.WriteLine ();
				Console.WriteLine (" ALL ITEMS CHECKED OUT TO " + name.ToUpper () + " ARE: \n");

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
			}//end of Check In if
			else
			{
				Console.WriteLine ("Wrong password!");
			}
		}//end of Check In


		public static void NewUser(string userPassword, string name, Dictionary <string, string> users, string password, string staffPassword, Dictionary <string, string> staff)
		{
			password = UI.PromptLine (@"
Please enter your staff password: 
- ");

			if (staff.ContainsValue (password))
			{

				string staffOrUser = UI.PromptLine ("Is the user you want to add a staff or user?  \'s\' for staff, \'u\' for user: ");
				staffOrUser = staffOrUser.ToLower ();

				if (staffOrUser == "u")
				{
					name = UI.PromptLine ("What is your first name?");
					userPassword = UI.PromptLine ("Set your password: ");
				
					name = name.ToUpper ();

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
	    ~ check in

	 - ");
						Console.WriteLine ();

						switch (command.ToLower ())
						{
							case "check out":
								count++;
								Patron.CheckOut (name, userPassword, password);
								break;
							case "out":
								count++;
								Patron.CheckOut (name, userPassword, password);
								break;
							case "restore":
								count++;
								Staff.Restore ();
								break;
							default:
								Console.WriteLine (@"
-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
								break;
						}
					} while(count == 0);
				}//end of inner if
				else
				{
					name = UI.PromptLine ("What is your first name?");
					staffPassword = UI.PromptLine ("Set your password: ");

					name = name.ToUpper ();

					staff.Add (name, userPassword);

					Console.WriteLine (@"
------------------------------------------------------
	   Thank you {0}, your password is set to {1}
	        What would you like to do now?
------------------------------------------------------", name, staffPassword);

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
	    ~ check in

	 - ");
						Console.WriteLine ();

						switch (command.ToLower ())
						{
							case "check out":
								count++;
								Patron.CheckOut (name, userPassword, password);
								break;
							case "out":
								count++;
								Patron.CheckOut (name, userPassword, password);
								break;
							case "restore":
								count++;
								Staff.Restore ();
								break;
							case "check in":
								count++;
								Staff.CheckIn (name, userPassword, password);
								break;
							default:
								Console.WriteLine (@"
-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
								break;
						}
					} while(count == 0);
				}//end of inner else
			}//end of outer if
		else
		{
					Console.WriteLine(@"
----------------------------------------------------------
You are not a staff member!  Please exit out and try again
----------------------------------------------------------");
		}//end of outer else
}//end of New User
		
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