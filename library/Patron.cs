using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Patron: ILibrary 
	{



		public static void Validate (Dictionary<string,string[]> patrons, string username)
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
					// Stops Receving Keys Once Enter is Pressed
				}while (key.Key != ConsoleKey.Enter);

			}while(pass!= patrons[username][1]);

			Console.WriteLine ("\nWelcome {0}!", username);

			string command = "";

			do
			{
				command = UI.PromptLine (@"
--------------------------
  How may we assist you? 
--------------------------
   You may:
    1 - check out
    2 - reset password
    3 - ask Emanuel for assistance
    4 - quit
 - ");

				switch (command.ToLower ())
				{
					case "1":
						Patron.CheckOut (username);
						break;
					case "2":
						//Patron.ResetPassword (username);
						break;
					case "4":
						break;
					default:
						Console.WriteLine (@"
-------------------------------------------------
 !!! Request not recognized. Please see menu !!! 
-------------------------------------------------");
						break;
				}
			} while(command.ToLower () != "4");	
		}















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

			string barcode = "";//barcode is a string becasue we would like 'Q' to be a valid entry for a barcode.

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
					listOfCheckedOut.Add(materials[barcodes.IndexOf(barcode)] + "," + username);

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
	
			StreamWriter outMaterials = FIO.AppendText(FIO.GetLocation ("catalog.txt"), "checkedOut.txt");
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
	} 
}