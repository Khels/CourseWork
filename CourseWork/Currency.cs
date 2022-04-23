using System;
using System.ComponentModel.DataAnnotations;

namespace CourseWork
{
	public class Currency
    {
		public int Id { get; set; }
		[MaxLength(50)]
		public string CurrencyFull { get; set; }
		[MaxLength(3)]
		public string CurrencyShort { get; set; }
	}
}
