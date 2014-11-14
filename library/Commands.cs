using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Commands
	{
		public static void CheckOut(string name)
		{
			StreamReader catalog = FIO.OpenReader ("catalog.txt");
			List<string> barcodes = new List<string>();
			List<string[]> materials = new List<string[]>();
			List<string> listOfCheckedOut = new List<string> ();

			while (!catalog.EndOfStream)
			{
				string[] line = catalog.ReadLine ().Split (',');
				barcodes.Add (line [0]);
				materials.Add (line);
			}

			string barcode = "";

			do
			{
				barcode = UI.PromptLine (@"Enter the barcode for the material that you'd like to check out. 
When you are finsihed checking out materials, enter 'Q' for the barcode.
- ");

				if (barcodes.Contains (barcode))
				{
					foreach (string r in materials [barcodes.IndexOf (barcode)])
					{
						Console.WriteLine ("     " + r);
						listOfCheckedOut.Add (r);
					}
					listOfCheckedOut.Add("");

					Console.WriteLine ("- ITEM CHECKED OUT TO " + name.ToUpper () + " - \n");

					//choice = UI.PromptLine ("To check out more materials press Enter, otherwise enter 'Q' to Quit: ");
				}
				else
				{
					Console.WriteLine ("Invalid barcode!");
					//choice = UI.PromptLine ("To continue press enter, otherwise enter 'Q' to Quit: ");
				}
			}while(barcode.ToUpper() != "Q");

			catalog.Close ();

			Console.WriteLine ();
			Console.WriteLine (" ALL ITEMS CHECKED OUT TO " + name.ToUpper () + " ARE: \n");

			foreach (string r in listOfCheckedOut)
			{
				Console.WriteLine ("     " + r);
			}

			Console.WriteLine ();
		}

		public static void Return()
		{
			Console.WriteLine ("Returned!");
		}
	}
}