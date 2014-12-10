using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Account
	{
		/// <summary>
		/// Ensures that a specific username exists and then returns that username
		/// </summary>
		/// <returns>The username.</returns>
		/// <param name="prompt">Prompt.</param>
		/// <param name="error">Error.</param>
		/// <param name="staff">Staff.</param>
		/// <param name="patrons">Patrons.</param>
		public static string GetUsername(string prompt, string error, ref Dictionary<string,string[]> staff, ref Dictionary<string,string[]> patrons)
		{
			string username = UI.PromptLine(prompt + "\n-").ToLower();

			while (!(patrons.ContainsKey (username) || staff.ContainsKey (username))) 
			{
				for (int i = 0; i < error.Length + 10; i++)
				{
					Console.Write("-");
				}

				Console.WriteLine();

				Console.WriteLine(" !!! " + error + " !!! ");

				for (int i = 0; i < error.Length + 10; i++)
				{
					Console.Write("-");
				}

				Console.WriteLine();

				username = UI.PromptLine(prompt + "\n-").ToLower();
			}

			return username;
		}

		/// <summary>
		/// Makes sure that two passwords match
		/// </summary>
		/// <returns>The match.</returns>
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

		/// <summary>
		/// Resets the password for a specific username
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="staff">Staff.</param>
		/// <param name="patrons">Patrons.</param>
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

				User newStaff = new User (username,staff[username][0], staff [username] [1],"s");

				newStaff.Write ("staff", staff);
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

				User newPatron = new User (username,patrons[username][0],patrons [username] [1] ,"p");

				newPatron.Write ("patrons", patrons);
			}


			Console.WriteLine("\nPassword for " + username + " has been changed!");
		}

		/// <summary>
		/// Makes sure that passwords match for a password reset.
		/// </summary>
		/// <returns>The password match.</returns>
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

		/// <summary>
		/// Gets the password from a user and checks to see if it is the correct password
		/// </summary>
		/// <param name="userPassword">User password.</param>
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
	}
}

