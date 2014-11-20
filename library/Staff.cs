using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Staff: ILibrary 
	{
		public static void CheckOut(string name)
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
					listOfCheckedOut.Add(materials[barcodes.IndexOf(barcode)]);

					//removes the materials from available barcodes and materials list
					materials.Remove(materials[barcodes.IndexOf(barcode)]);
					barcodes.Remove(barcode);

					foreach (string x in material)
					{
						Console.WriteLine ("     " + x);
					}

					Console.WriteLine ("\n- ITEM CHECKED OUT TO "  + name.ToUpper () +"  -  ");
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

		public static void CheckIn()
		{
		}

		public static void NewUser()
		{
		}

		public static void Restore()
		{
			StreamReader master = FIO.OpenReader ("master.txt");
			StreamWriter catalog = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"catalog.txt");

			while(!master.EndOfStream)
			{
				catalog.WriteLine (master.ReadLine());
			}

			master.Close ();
			catalog.Close ();
		}
	}
}

