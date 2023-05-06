using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapBakalare
{

	public class UserInterface
	{
		private IDataStorage dataStorage;

		internal UserInterface(IDataStorage dataStorage)
		{
			this.dataStorage = dataStorage;
		}

		public void AddRecord()
		{
				Console.WriteLine("Enter student details:");
			
				Console.Write("First Name: ");
				string firstName = Console.ReadLine();
				Console.Write("Last Name: ");
				string lastName = Console.ReadLine();
				Console.Write("Subject: ");
				string subject = Console.ReadLine();
				Console.Write("Grade: ");
				if (int.TryParse(Console.ReadLine(), out int grade))
				{
					Record record = new Record
					{
						Jmeno = firstName,
						Prijmeni = lastName,
						Predmet = subject,
						Znamka = grade
					};

					dataStorage.AddRecord(record);
					Console.WriteLine("Record added successfully.");
				}
				else
				{
					Console.WriteLine("Invalid grade. Record not added.");
				}
		}

		public void UpdateRecord()
		{
			Console.Write("Enter ID of the record to update: ");
			if (int.TryParse(Console.ReadLine(), out int id))
			{
				Record existingRecord = dataStorage.GetRecord(id);
				if (existingRecord != null)
				{
					Console.WriteLine("Enter new details:");

					Console.Write("First Name: ");
					string firstName = Console.ReadLine();
					Console.Write("Last Name: ");
					string lastName = Console.ReadLine();
					Console.Write("Subject: ");
					string subject = Console.ReadLine();
					Console.Write("Grade: ");
					if (int.TryParse(Console.ReadLine(), out int grade))
					{
						Record updatedRecord = new Record
						{
							Id = id,
							Jmeno = firstName,
							Prijmeni = lastName,
							Predmet = subject,
							Znamka = grade
						};

						dataStorage.UpdateRecord(updatedRecord);
						Console.WriteLine("Record updated successfully.");
					}
					else
					{
						Console.WriteLine("Invalid grade. Record not updated.");
					}
				}
				else
				{
					Console.WriteLine("Record not found.");
				}
			}
			else
			{
				Console.WriteLine("Invalid ID. Record not updated.");
			}
		}

		public void DeleteRecord()
		{
			Console.Write("Enter ID of the record to delete: ");
			if (int.TryParse(Console.ReadLine(), out int id))
			{
				try
				{
					dataStorage.DeleteRecord(id);
					Console.WriteLine("Record deleted successfully.");
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			else
			{
				Console.WriteLine("Invalid ID. Record not deleted.");
			}
		}

		public void ViewRecords()
		{
			Console.WriteLine("Student Records:");
			List<Record> records = dataStorage.GetRecords();
			foreach (Record record in records)
			{
				Console.WriteLine($"ID: {record.Id}, Name: {record.Jmeno} {record.Prijmeni}, Subject: {record.Predmet}, Grade:{record.Znamka}");
			}
		}

		public void SearchRecords()
		{
			Console.Write("Enter search keyword: ");
			string keyword = Console.ReadLine();

			List<Record> records = dataStorage.GetRecords(keyword);
			if (records.Count > 0)
			{
				Console.WriteLine("Matching Records:");
				foreach (Record record in records)
				{
					Console.WriteLine($"ID: {record.Id}, Name: {record.Jmeno} {record.Prijmeni}, Subject: {record.Predmet}, Grade: {record.Znamka}");
				}
			}
			else
			{
				Console.WriteLine("No matching records found.");
			}
		}

		public void Run()
		{
			bool exit = false;
			while (!exit)
			{
				Console.WriteLine();
				Console.WriteLine("Menu:");
				Console.WriteLine("1. Add Record");
				Console.WriteLine("2. Update Record");
				Console.WriteLine("3. Delete Record");
				Console.WriteLine("4. View Records");
				Console.WriteLine("5. Search Records");
				Console.WriteLine("6. Exit");
				Console.Write("Enter your choice: ");
				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						AddRecord();
						break;
					case "2":
						UpdateRecord();
						break;
					case "3":
						DeleteRecord();
						break;
					case "4":
						ViewRecords();
						break;
					case "5":
						SearchRecords();
						break;
					case "6":
						exit = true;
						break;
					default:
						Console.WriteLine("Invalid choice.");
						break;
				}
			}
		}
	}
}
