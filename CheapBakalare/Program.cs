using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheapBakalare
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string filePath = "students.csv";
			CsvDataStorage dataStorage = new CsvDataStorage(filePath);
			UserInterface ui = new UserInterface(dataStorage);
			ui.Run();
		}
	}
}