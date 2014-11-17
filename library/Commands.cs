﻿using System;
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

			Console.WriteLine (listOfCheckedIn);

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
						listOfCheckedIn.Remove(r);
					}
					listOfCheckedOut.Add("");

					Console.WriteLine ("- ITEM CHECKED OUT TO " + name.ToUpper () + " - \n");

				}
				else
				{
					Console.WriteLine ("Invalid barcode!");//this prints even if you enter q
				}
			}while(barcode.ToUpper() != "Q");

			catalog.Close ();

			Console.WriteLine ();
			Console.WriteLine (" ALL ITEMS CHECKED OUT TO " + name.ToUpper () + " ARE: \n");

			foreach (string r in listOfCheckedOut)
			{
				Console.WriteLine ("     " + r);
			}
		}

		public static void Return(string name)
		{
				StreamReader catalog = FIO.OpenReader ("catalog.txt");
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

				do
				{
					barcode = UI.PromptLine (@"Enter the barcode for the material that you'd like to check in. 
When you are finsihed checking out materials, enter 'Q' for the barcode.
- ");
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
						Console.WriteLine ("Invalid barcode!");//this prints even if you enter q
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