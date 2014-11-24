using System;
using System.IO;
using System.Collections.Generic;

namespace library//mitchell
{
	public class Staff: ILibrary 
	{
		public static void CheckOut(string name)
		{
		}
		public static void CheckIn()
		{
		}
		public static void NewUser()
		{
			string name = UI.PromptLine ("What is your first name?");

		}
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

