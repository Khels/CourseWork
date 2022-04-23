using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork
{
    public class DealPlace
    {
        public int Id { get; set; }
        public string DealPlaceFull { get; set; }
        public string DealPlaceShort { get; set; }
        public virtual List<Deal> Deals { get; set; } = new List<Deal>();
    }
}
