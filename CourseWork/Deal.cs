using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork
{
	public class Deal
    {
		[Column("deal_id")]
		public int Id { get; set; }

		[ForeignKey("DealTypeId")]
		public int TypeId { get; set; }
		public virtual DealType Type { get; set; }

		[ForeignKey("DealPlaceId")]
		public int PlaceId { get; set; }
		public virtual DealPlace Place { get; set; }

		[ForeignKey("CurrencyId")]
		public int CurrencyId { get; set; }
		public virtual Currency Currency { get; set; }

		public string Tiker { get; set; }
		public int Order { get; set; }
		public string Number { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
		public float TotalCost { get; set; }
		public string Trader { get; set; }
		public float Commision { get; set; }
	}
}
