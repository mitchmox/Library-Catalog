using System;
using System.IO;
using System.Collections.Generic;

namespace library//master//eva
{
	public class Commands
	{
		public static void CheckOut(string name)
		{
			StreamReader catalog = FIO.OpenReader ("catalog.txt");

			List<string> barcodes = new List<string>();
			List<string> materials = new List<string>();

			List<string> listOfCheckedOut = new List<string> ();
			List<string> listOfCheckedIn = new List<string> ();

			while (!catalog.EndOfStream)
			{
				string line = catalog.ReadLine ();
				barcodes.Add(line.Substring(0,6));
				materials.Add(line);
			}

			for (int i = 0; i < materials.Count; i++)
			{
				foreach (string r in materials [i])	//at the index of the entered barcode, write out all other lines of text at that index
				{
					listOfCheckedIn.Add (r);
				}
			}//end for loop

			string barcode = "";

			do  //while user doesnt enter Q, add  entered barcode to list of Checked Out books
			{
				barcode = UI.PromptLine (@"Enter the barcode for the material that you'd like to check out.   
When you are finsihed checking out materials, enter 'Q' for the barcode.
- ");									

				if (barcodes.Contains (barcode))
				{
					foreach (string r in materials [barcodes.IndexOf (barcode)])	//at the index of the entered barcode, write out all other lines of text at that index
					{
						Console.WriteLine ("     " + r);							//prints out item data
						outCatalog.WriteLine(r);
						while (master.ReadLine()==r)
						{
							string line = master.ReadLine();
							inCatalog.WriteLine(line);
						}
					}

					listOfCheckedOut.Add("");

					Console.WriteLine ("- ITEM CHECKED OUT TO " + name.ToUpper () + " - \n");

				}
				else
				{
					if (barcode.ToLower() != "q"){ //makes it so if 'Q' is typed, invalid barcode isn't printed
						Console.WriteLine ("Invalid barcode!");
						Console.WriteLine ();}
				}
			}while(barcode.ToUpper() != "Q");

			inCatalog.Close ();
			master.Close ();
			outCatalog.Close ();


		//	Console.WriteLine ();
			Console.WriteLine (" ALL ITEMS CHECKED OUT TO " + name.ToUpper () + " ARE: \n");

			//StreamWriter CheckedOut = File.AppendText(FIO.GetPath("CheckedOut.txt"));

			foreach (string r in listOfCheckedOut)
			{
				Console.WriteLine ("     " + r);
			}
				
			//need to find a way to NOT have CheckedOut.txt not all be on one line
			//some kind of split method for an array

		}


		public static void CheckIn(string name)
		{
			Console.WriteLine ("Returned!");
		}


		}

	}