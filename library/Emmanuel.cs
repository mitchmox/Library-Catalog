using System;
using System.IO;
using System.Collections.Generic;

namespace library
{
	public class Emmanuel
	{
		static Random rand = new Random();

		public static void Ask()
		{
			StreamReader reader = FIO.OpenReader("emmanuel-welcome.txt");
			// special data is in the first two paragraphs
			string welcome = ReadParagraph(reader);
			string goodbye = ReadParagraph(reader);
			List<string> guessList = GetParagraphs(reader); //  list of random responses
			reader.Close();

			reader = FIO.OpenReader("emmanuel-responses.txt");
			Dictionary<string, string> responses = GetDictionary(reader);
			reader.Close();

			Console.Write(welcome);
			string prompt = "\n-- ";
			Console.WriteLine("Enter 'bye' to put me out of my misery.");
			string fromUser;
			do {
				fromUser = UI.PromptLine(prompt).ToLower().Trim();
				if (fromUser != "bye") {
					string answer = Response(fromUser, guessList, responses);
					Console.Write("\n" + answer);
				}
			} while (fromUser != "bye");
			Console.Write("\n"+ goodbye);
		}

		public static string Response(string fromUser, List<string> guessList,
			Dictionary<string, string> responses)
		{
			char[] sep = "\t !@#$%^&*()_+{}|[]\\:\";<>?,./".ToCharArray();
			string[] words = fromUser.ToLower().Split(sep);
			foreach (string word in words) {
				if (responses.ContainsKey(word)){
					return responses[word];
				}
			}
			return guessList[rand.Next(guessList.Count)];
		}

		public static string ReadParagraph(StreamReader reader)
		{ 
			string line = reader.ReadLine ();
			string paragraph = line+"\n";

			while(!string.IsNullOrWhiteSpace(line))
			{
				line = reader.ReadLine();
				paragraph += line+ "\n";
			}

			return paragraph;
		}

		public static List<string> GetParagraphs(StreamReader reader)
		{
			List<string> all = new List<string>();

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine ();
				string paragraph = line+"\n";

				while (!string.IsNullOrWhiteSpace (line))
				{
					line = reader.ReadLine ();
					paragraph += line + "\n";
				}

				all.Add (paragraph);
			}

			return all;
		}

		public static Dictionary<string, string> GetDictionary(StreamReader reader)
		{
			Dictionary<string, string> d = new Dictionary<string, string>();

			while (!reader.EndOfStream)
			{
				string key = reader.ReadLine ();
				string line = reader.ReadLine ();
				string value = "";

				while (!string.IsNullOrWhiteSpace (line))
				{
					value += line + "\n";
					line = reader.ReadLine ();
				}
				d.Add (key, value);
			}

			return d;
		}
	}
}