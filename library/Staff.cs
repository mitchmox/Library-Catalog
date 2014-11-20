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

