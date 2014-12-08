using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Patron: ILibrary 
	{
		/// <summary>
		/// Validates the specified username and password for PATRON and then displays a menu of actions allowed to a PATRON user.
		/// </summary>
		/// <param name="patrons">Patrons.</param>
		/// <param name="username">Username.</param>
		public static void Validate (ref Dictionary<string,string[]> staff, ref Dictionary<string,string[]> patrons, string username)
		{
			Account.GetPassword(patrons[username][1]);

			Console.Write(@"

      Welcome {0}!", username);


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
    3 - ask Emmanuel for assistance
    4 - quit
 - ");
				Console.WriteLine();
				switch (command.ToLower ())
				{
					case "1":
					case "check out":
					case "check":
					case "out":
						Patron.CheckOut (username);
						break;
					case "2":
					case "reset":
					case "reset password":
						Account.ResetPassword (username, ref staff, ref patrons);
						break;
					case "3":
					case "ask Emmanuel for assistance":
				    case "ask Emmanuel":
					case "ask":
					case "assistance":
						Console.WriteLine("Emanuel isn't here right now. Please try again later.");
						break;
					case "4":
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
			} while(!(command.ToLower().Contains("4") || command.ToLower().Contains("q") || (command.ToLower().Contains("quit"))));	
		}

		/// <summary>
		/// Checks out a material to the username sent to the function. (i.e. the patron who is currently logged in)
		/// </summary>
		/// <param name="username">Username.</param>
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

					if (barcode.ToLower() != "q")
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