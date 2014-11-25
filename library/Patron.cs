using System;
using System.IO;
using System.Collections.Generic;

namespace library//mitchell
{
	public class Patron: ILibrary 
	{
		public static void CheckOut(string name, string userPassword, string password)
		{
			password= UI.PromptLine (@"
Please enter your password: 
- ");
			if (userPassword == password)
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
					barcode = UI.PromptLine(@"
--------------------------------------------------------------------------------
	    Enter the barcode for the material that you'd like to check out.
When you are finished checking out materials, enter 'Q' for the barcode to quit.
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
----------------------------------------------------------------------
!!! Invalid barcode. Material is either checked out or nonexistent !!!
----------------------------------------------------------------------");
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
			}
			else
			{
				Console.WriteLine ("Wrong password!");
			}
		}

		public static void Return(string name, string userPassword, string password, string staffPassword)
		{
			password = UI.PromptLine (@"
Please enter your password: 
- ");
			if (userPassword == password)
			{

				StreamReader catalog = FIO.OpenReader ("catalog.txt");

				List<string> barcodes = new List<string> ();
				List<string> materials = new List<string> ();

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
	    Enter the barcode for the material that you'd like to return.
When you are finished checking out materials, enter 'Q' for the barcode to quit.
--------------------------------------------------------------------------------

	- ");									
				//need to fix
				Console.WriteLine ("\n- ITEM RETURNED FOR " + name.ToUpper () + "  -  ");
				} while(barcode.ToUpper () != "Q");

				catalog.Close ();

				StreamWriter outMaterials = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "checkedOut.txt");
				StreamWriter updateCatalog = FIO.OpenWriter (FIO.GetLocation ("catalog.txt"), "catalog.txt");

				Console.WriteLine ();
				Console.WriteLine (" ALL RETURNED FOR " + name.ToUpper () + " ARE: \n");

				updateCatalog.Close ();
				outMaterials.Close ();
			}
			else
			{
				Console.WriteLine ("Wrong password!");
			}
		}
		public static void Reset(string userPassword, string name, Dictionary <string, string> users, string password, string staffPassword, Dictionary <string, string> staff)
		{
			password = UI.PromptLine (@"
Please enter your password: 
- ");

			int count = 0;
			if (userPassword == password)
			{
				do
				{
					string newUserPassword = UI.PromptLine ("Please enter the new password: ");
					if (newUserPassword != userPassword)
					{
						string newUserPassword2 = UI.PromptLine ("Please enter the password again: ");
						if (newUserPassword==newUserPassword2)
						{
							userPassword=newUserPassword2;
							Console.WriteLine("Password has been reset to: "+userPassword);
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
    ~ ask Emanuel for assistance
   
   For staff:
    ~ restore
    ~ check in

 - ");


								Console.WriteLine ();

								switch(command.ToLower())
								{
									case "check out":
										count++;
										Patron.CheckOut(name, userPassword,password);
										break;
										//reset psswrd
										//assistance
									case "out":
										count++;
										Patron.CheckOut(name, userPassword,password);
										break;
									case"return":
										count++;
										Patron.Return(name, userPassword, password, staffPassword);
										break;
									case "restore":
										count++;
										Staff.Restore(staff);
										break;
									case "new user":
										count++;
										Staff.NewUser (userPassword, name, users, password, staffPassword, staff);
										break;
									case "check in":
										count++;
										Staff.CheckIn(name, staffPassword, password);
										break;
									default:
										Console.WriteLine (@"
-------------------------------------------------
 !! Request not recognized. Please see menu !!! 
-------------------------------------------------");
										break;
								}

							}while(count == 0);
							count=1;
						}
						else
						{
							Console.WriteLine("The passwords you have entered do not match!");
							count=0;
						}
					}
					else
					{
						Console.WriteLine ("Please enter a different password than the one you have now!");
						count = 0;
					}
				}while (count==0);
			}
		}

	}
}