using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapBakalare
{
	internal class CsvDataStorage : IDataStorage
	{
		private string filePath;
		private List<Record> records;

		public CsvDataStorage(string filePath)
		{
			this.filePath = filePath;
			records = LoadRecords();
		}

		public void AddRecord(Record record)
		{
			int nextId = records.Count > 0 ? records.Max(r => r.Id) + 1 : 1;
			record.Id = nextId;
			records.Add(record);
			SaveRecords();
		}

		public void UpdateRecord(Record record)
		{
			int index = records.FindIndex(r => r.Id == record.Id);
			if (index >= 0)
			{
				records[index] = record;
				SaveRecords();
			}
			else
			{
				throw new ArgumentException("Record not found.");
			}
		}

		public void DeleteRecord(int id)
		{
			int index = records.FindIndex(r => r.Id == id);
			if (index >= 0)
			{
				records.RemoveAt(index);
				SaveRecords();
			}
			else
			{
				throw new ArgumentException("Record not found.");
			}
		}

		public Record GetRecord(int id)
		{
			return records.Find(r => r.Id == id);
		}

		public List<Record> GetRecords()
		{
			return records;
		}

		public List<Record> GetRecords(string filter)
		{
			return records.FindAll(r => r.Jmeno.Contains(filter) || r.Prijmeni.Contains(filter) || r.Predmet.Contains(filter));
		}

		public void SaveRecords()
		{
			using (StreamWriter writer = new StreamWriter(filePath, false))
			{
				foreach (Record record in records)
				{
					string line = $"{record.Id},{record.Jmeno},{record.Prijmeni},{record.Predmet},{record.Znamka}";
					writer.WriteLine(line);
				}
			}
		}

		private List<Record> LoadRecords()
		{
			List<Record> loadedRecords = new List<Record>();
			if (File.Exists(filePath))
			{
				using (StreamReader reader = new StreamReader(filePath))
				{
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						string[] parts = line.Split(',');
						if (parts.Length == 5)
						{
							if (int.TryParse(parts[0], out int id) && int.TryParse(parts[4], out int grade))
							{
								Record record = new Record
								{
									Id = id,
									Jmeno = parts[1],
									Prijmeni = parts[2],
									Predmet = parts[3],
									Znamka = grade
								};
								loadedRecords.Add(record);
							}
							else
							{
								Console.WriteLine("Invalid record format. Skipping the record.");
							}
						}
					}
				}
			}
			return loadedRecords;
		}
	}
}

