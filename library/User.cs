using System;
using System.Collections.Generic;
using System.IO;

namespace library
{
	public class User
	{
		private string username;
		private string password;
		private string name;
		private string access;

		public User (string username, string name, string password, string access) 
		{
			this.username = username;
			this.name = name;
			this.password = password;
			this.access = access;
		}//end User constructor

		public void Add(ref Dictionary<string,string[]> staff, ref Dictionary<string,string[]> patrons)
		{
			if (access == "p") 
			{
				patrons.Add (username, new string [] { name, password });
				Console.WriteLine ("USER CREATED!");
			}
			if (access == "s") 
			{
				staff.Add (username, new string [] { name, password });
				Console.WriteLine ("USER CREATED!");
			}
		}

		public void Write(string userGroup, Dictionary<string,string[]> userDict)
		{

			StreamWriter writeUsers = FIO.OpenWriter (FIO.GetLocation("catalog.txt"),"users-" + userGroup + ".txt");

			foreach (string key in userDict.Keys)
			{	
				writeUsers.WriteLine ("{0},{1},{2}", key,  userDict[key] [0], userDict [key] [1]);
			}

			writeUsers.Close ();
		}


	}
}

