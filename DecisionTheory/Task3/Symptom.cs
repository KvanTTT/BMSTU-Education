using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
	public class Symptom
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Question
		{
			get;
			set;
		}

		public Symptom()
		{
		}

		public Symptom(int id, string name, string question)
		{
			Id = id;
			Name = name;
			Question = question;
		}

		public override string ToString()
		{
			return String.Format("Id={0}; Name={1}; Question={2}", Id, Name, Question);
		}
	}
}
