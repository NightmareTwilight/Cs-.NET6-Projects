namespace Conquest_of_Elysium.Models
{
    public class SheetObject
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string? Desc { get; set; }
		public string? LongDesc { get; set; }
	}
}
