using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Commands
	{

		public static void CheckOut(string name)
		{


			StreamReader catalog = FIO.OpenReader ("INcatalog.txt");
			List<string> barcodes = new List<string>();
			List<string[]> materials = new List<string[]>();
			List<string> listOfCheckedOut = new List<string> ();
			List<string> listOfCheckedIn = new List<string> ();
			string type = "";//type can be movie, book, album, etc...


			while (!catalog.EndOfStream)  //creates an array of all elements of each line in "catalog"  
										  //Ex: 230001,Harry Potter stores as line[0]=2300001, line[1]=Harry Potter
			{
				string[] line = catalog.ReadLine ().Split (',');
				barcodes.Add (line [0]);
				materials.Add (line);
			}

			//add all materials to listofcheckedin
<<<<<<< HEAD
=======
			//foreach (string r in barcodes)
			//{
				//listOfCheckedIn.Add (r);
			//}

			//Console.WriteLine (listOfCheckedIn);
>>>>>>> origin/master

			string barcode = "";

			do  //while user doesnt enter Q, add  entered barcode to list of Checked Out books
			{
				barcode = UI.PromptLine (@"Enter the barcode for the material that you'd like to check out.   
When you are finsihed checking out materials, enter 'Q' for the barcode.
- ");																				//user sets barcode
				if (barcode.Contains("23"))
				{
					type = "book";
				}
				if (barcode.Contains("45"))
				{
					type = "album";
				}
				if (barcode.Contains("67"))
				{
					type = "movie";
				}
				Console.WriteLine("The item you are trying to check out is a {0}", type);


				if (barcodes.Contains (barcode))
				{
					foreach (string r in materials [barcodes.IndexOf (barcode)])	//at the index of the entered barcode, write out all other lines of text at that index
					{
						Console.WriteLine ("     " + r);							//prints out item data
						listOfCheckedOut.Add (r);
						listOfCheckedIn.Remove (r);
					}

					listOfCheckedOut.Add("");

					Console.WriteLine ("- ITEM CHECKED OUT TO " + name.ToUpper () + " - \n");

				}
				else
				{
<<<<<<< HEAD
					Console.WriteLine ("Invalid barcode!");							//this prints even if you enter q
=======
					if (barcode.ToLower() != "q") //makes it so if 'Q' is typed, invalid barcode isn't printed
						Console.WriteLine ("Invalid barcode!");
						Console.WriteLine ();
>>>>>>> origin/master
				}
			}while(barcode.ToUpper() != "Q");

			catalog.Close ();

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

		public static void Return(string name)
		{
				StreamReader catalog = FIO.OpenReader ("CheckedOut.txt");
				List<string> barcodes = new List<string>();
				List<string[]> materials = new List<string[]>();
				List<string> listOfCheckedOut = new List<string> ();
				List<string> listOfCheckedIn = new List<string> ();


				while (!catalog.EndOfStream)
				{
					string[] line = catalog.ReadLine ().Split (',');
					barcodes.Add (line [0]);
					materials.Add (line);
				}

				//add all materials to listofcheckedin
				//foreach (string r in barcodes)
				//{
				//listOfCheckedIn.Add (r);
				//}

				string barcode = "";

				do //while user doesnt enter Q, add  entered barcode to list of Checked Out books
				{
					barcode = UI.PromptLine (@"Enter the barcode for the material that you'd like to return. 
When you are finsihed checking out materials, enter 'Q' for the barcode.
- ");

				if (barcode.Contains("23"))
				{
					type = "book";
				}
				if (barcode.Contains("45"))
				{
					type = "album";
				}
				if (barcode.Contains("67"))
				{
					type = "movie";
				}
				Console.WriteLine("The item you are trying to check in is a {0}", type);



					if (barcodes.Contains (barcode))
					{
						foreach (string r in materials [barcodes.IndexOf (barcode)])
						{
							Console.WriteLine ("     " + r);
							listOfCheckedIn.Add (r);
							listOfCheckedOut.Remove(r);
						}
						listOfCheckedIn.Add("");

						Console.WriteLine ("- ITEM CHECKED IN FOR " + name.ToUpper () + " - \n");

					}
					else
					{
						if (barcode.ToLower() != "q") //makes it so if 'Q' is typed, invalid barcode isn't printed
							Console.WriteLine ("Material not found!");
							Console.WriteLine ();
					}
				}while(barcode.ToUpper() != "Q");

				catalog.Close ();

				Console.WriteLine ();
				Console.WriteLine (" ALL ITEMS CHECKED IN UNDER " + name.ToUpper () + " ARE: \n");



				foreach (string r in listOfCheckedIn)
				{
					Console.WriteLine ("     " + r);
				}
			}
		}

	}