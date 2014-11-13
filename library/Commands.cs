using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Commands
	{
		public static void CheckOut()
		{
			var catalog = FIO.OpenReader ("catalog.txt");
			var barcodes = new List<string>();
			var materials = new List<string[]>();
			var listOfCheckedOut = new List<string> ();

			while (!catalog.EndOfStream)
			{
				string[] line = catalog.ReadLine ().Split (',');
				barcodes.Add(line[0]);
				materials.Add (line);
			}


			int barcode = UI.PromptInt ("Enter the barcode for the material that you'd like to check out: ");
			//string material;

			do
			{
				if (barcodes.Contains (barcode.ToString ()))
				{
					foreach (string r in materials [barcodes.IndexOf (barcode.ToString())])
					{
						Console.WriteLine ("     " + r);
						listOfCheckedOut.Add (r);
					}

					Console.WriteLine ("- ITEM CHECKED OUT IN YOUR NAME - \n");

					string choice = UI.PromptLine ("To check out more materials, enter \'C\', otherwise enter \'Q\' to Quit: ");

					if (choice.ToLower () == "c")
					{
						CheckOut ();
					}
				}
				else
				{
					Console.WriteLine ("Invalid barcode! Please enter againFAKE.");
					barcode = UI.PromptInt ("Enter the barcode for the material that you'd like to check out: ");
				}
			} while(!barcodes.Contains (barcode.ToString ()));
			
			catalog.Close ();

			foreach (string r in listOfCheckedOut)
			{
				Console.WriteLine (r);
			}

			Console.WriteLine ();
		}

		public static void Return()
		{
			Console.WriteLine ("Returned!");
		}
	}
}

