using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapBakalare
{
	internal interface IDataStorage
	{
		void AddRecord(Record record);
		void UpdateRecord(Record record);
		void DeleteRecord(int id);
		Record GetRecord(int id);
		List<Record> GetRecords();
		List<Record> GetRecords(string filter);
		void SaveRecords();
	}
}
