using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Account
	{
		public static string PasswordMatch()
		{
			string password1 = "";
			string password2 = "";

			do 
			{

				password1 = "";
				password2 = "";

				//asks for the password a first time
				Console.Write (@"
-------------------------------
  Enter password for user: 
-------------------------------
 - ");
				ConsoleKeyInfo keys;

				do 
				{
					keys = Console.ReadKey (true);

					// Backspace Should Not Work
					if (keys.Key != ConsoleKey.Backspace && keys.Key != ConsoleKey.Enter) 
					{
						password1 += keys.KeyChar;
						Console.Write ("*");
					} else 
					{
						if (keys.Key == ConsoleKey.Backspace && password1.Length > 0) 
						{
							password1 = password1.Substring (0, (password1.Length - 1));
							Console.Write ("\b \b");
						}
					}

				} while (keys.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed


				//asks for the password a second time
				Console.Write (@"
----------------------------------
  Re-Enter password for user: 
----------------------------------
 - ");

				do 
				{
					keys = Console.ReadKey (true);

					// Backspace Should Not Work
					if (keys.Key != ConsoleKey.Backspace && keys.Key != ConsoleKey.Enter) 
					{
						password2 += keys.KeyChar;
						Console.Write ("*");
					} else 
					{
						if (keys.Key == ConsoleKey.Backspace && password2.Length > 0) 
						{
							password2 = password2.Substring (0, (password2.Length - 1));
							Console.Write ("\b \b");
						}
					}

				} while (keys.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

				Console.WriteLine ("");

				if (password1 != password2) //if passwords do not match error message is shown
				{ 
					Console.WriteLine ("PASSWORDS DO NOT MATCH! PLEASE RE-ENTER!");
				}

			} while (password1 != password2);

			return password1;
		}

		public static void ResetPassword(string username, ref Dictionary<string,string[]> staff, ref Dictionary<string,string[]> patrons)
		{
			if (staff.ContainsKey(username))
			{
				string pass = "";
				ConsoleKeyInfo key;

				do
				{
					pass = "";

					Console.Write(@"
-------------------------------------------
  Please enter CURRENT password for user:  
-------------------------------------------
 - ");
					do
					{
						key = Console.ReadKey(true);

						// Backspace Should Not Work
						if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
						{
							pass += key.KeyChar;
							Console.Write("*");
						}
						else
						{
							if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
							{
								pass = pass.Substring(0, (pass.Length - 1));
								Console.Write("\b \b");
							}
						}

					}while (key.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

					if(pass!= staff [username] [1])
					{
						Console.WriteLine(@"
----------------------------
  !!!Incorrect Password!!!
----------------------------");

					}

				}while(pass!= staff [username] [1]);

				staff [username] [1] = ResetPasswordMatch ();

				WriteUsers ("staff", staff);
			}
			else if (patrons.ContainsKey(username))
			{
				string pass = "";
				ConsoleKeyInfo key;

				do
				{
					pass = "";

					Console.Write(@"
-------------------------------------------
  Please enter CURRENT password for user:  
-------------------------------------------
 - ");
					do
					{
						key = Console.ReadKey(true);

						// Backspace Should Not Work
						if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
						{
							pass += key.KeyChar;
							Console.Write("*");
						}
						else
						{
							if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
							{
								pass = pass.Substring(0, (pass.Length - 1));
								Console.Write("\b \b");
							}
						}

					}while (key.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

					if(pass!= patrons [username] [1])
					{
						Console.WriteLine(@"
----------------------------
  !!!Incorrect Password!!!
----------------------------");

					}
				}while(pass!= patrons [username] [1]);

				patrons [username] [1] = ResetPasswordMatch ();

				WriteUsers ("patrons", patrons);
			}


			Console.WriteLine("\nPassword for " + username + " has been changed");
		}

		public static string ResetPasswordMatch()
		{
			string password1 = "";
			string password2 = "";

			do 
			{

				password1 = "";
				password2 = "";

				//asks for the password a first time
				Console.Write (@"
-------------------------------
  Enter NEW password for user: 
-------------------------------
 - ");
				ConsoleKeyInfo keys;

				do 
				{
					keys = Console.ReadKey (true);

					// Backspace Should Not Work
					if (keys.Key != ConsoleKey.Backspace && keys.Key != ConsoleKey.Enter) 
					{
						password1 += keys.KeyChar;
						Console.Write ("*");
					} else 
					{
						if (keys.Key == ConsoleKey.Backspace && password1.Length > 0) 
						{
							password1 = password1.Substring (0, (password1.Length - 1));
							Console.Write ("\b \b");
						}
					}

				} while (keys.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed


				//asks for the password a second time
				Console.Write (@"
----------------------------------
  Re-enter NEW password for user: 
----------------------------------
 - ");

				do 
				{
					keys = Console.ReadKey (true);

					// Backspace Should Not Work
					if (keys.Key != ConsoleKey.Backspace && keys.Key != ConsoleKey.Enter) 
					{
						password2 += keys.KeyChar;
						Console.Write ("*");
					} else 
					{
						if (keys.Key == ConsoleKey.Backspace && password2.Length > 0) 
						{
							password2 = password2.Substring (0, (password2.Length - 1));
							Console.Write ("\b \b");
						}
					}

				} while (keys.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

				Console.WriteLine ("");

				if (password1 != password2) //if passwords do not match error message is shown
				{ 
					Console.WriteLine ("PASSWORDS DO NOT MATCH! PLEASE RE-ENTER!");
				}

			} while (password1 != password2);

			return password1;
		}

		public static void GetPassword(string userPassword)
		{
			string pass = "";
			ConsoleKeyInfo key;

			do
			{
				pass = "";

				Console.Write(@"
-------------------------------
  Please enter your password: 
-------------------------------
 - ");
				do
				{
					key = Console.ReadKey(true);

					// Backspace Should Not Work
					if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
					{
						pass += key.KeyChar;
						Console.Write("*");
					}
					else
					{
						if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
						{
							pass = pass.Substring(0, (pass.Length - 1));
							Console.Write("\b \b");
						}
					}

				}while (key.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed

				if(pass!= userPassword)
				{
					Console.WriteLine(@"
------------------------
!!!Incorrect Password!!!
------------------------");

				}

			}while(pass!= userPassword);
		}

		/// <summary>
		/// Writes the users to their respective files.
		/// </summary>
		/// <param name="userGroup">User group.</param>
		/// <param name="userDict">User dict.</param>
		public static void WriteUsers (string userGroup, Dictionary<string,string[]> userDict)
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

