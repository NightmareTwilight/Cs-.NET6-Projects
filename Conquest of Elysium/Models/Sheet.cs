using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conquest_of_Elysium.Models
{
	public class Sheet
	{
		public Guid Id { get; set; }
		[StringLength(256)]
		public string Name { get; set; }
		public int Health {get; set; }
		[Display(Name = "Max Health")]
		public int MaxHealth { get; set; }
		public virtual User? User { get; set; }
		public List<SheetObject>? sheetObjects { get; set; }
		[StringLength(64)]
		public string? Class { get; set; }
		[StringLength(64)]
		public string? Race { get; set; }
		[StringLength(64)]
		[Display(Name = "Head (1)")]
		public string? Head { get; set; }
		[StringLength(64)]
		[Display(Name = "Torso (2-4)")]
		public string? Chest { get; set; }
		[StringLength(64)]
		[Display(Name = "L. Arm (5-6)")]
		public string? LArm { get; set; }
		[StringLength(64)]
		[Display(Name = "R. Arm (7-8)")]
		public string? RArm { get; set; }
		[StringLength(64)]
		[Display(Name = "L. Leg (9)")]
		public string? LLeg { get; set; }
		[StringLength(64)]
		[Display(Name = "R. Leg (10)")]
		public string? RLeg { get; set; }
		public int? Level { get; set; }
		[StringLength(8)]
		[Display(Name = "Weapon Skill")]
		public string? WeS { get; set; }
		[Display(Name = "Range Skill")]
		[StringLength(8)]
		public string? RaS { get; set; }
		[Display(Name = "Magic Insight")]
		[StringLength(8)]
		public string? MaI { get; set; }
		[StringLength(8)]
		[Display(Name = "Divine Insight")]
		public string? DiI { get; set; }
		[StringLength(8)]
		[Display(Name = "Intelligence")]
		public string? INT { get; set; }
		[StringLength(8)]
		[Display(Name = "Physical Resistance")]
		public string? PyR { get; set; }
		[StringLength(8)]
		[Display(Name = "Magic Resistance")]
		public string? MaR { get; set; }
		[StringLength(8)]
		[Display(Name = "Mental Resistance")]
		public string? MeR { get; set; }
		[StringLength(8)]
		[Display(Name = "Strength")]
		public string? STR { get; set; }
		[StringLength(8)]
		[Display(Name = "Tinkering")]
		public string? TNK { get; set; }
		[StringLength(8)]
		[Display(Name = "Speach Craft")]
		public string? SpC { get; set; }
		[StringLength(8)]
		[Display(Name = "Dexterity")]
		public string? DEX { get; set; }
		[StringLength(8)]
		[Display(Name = "Awareness")]
		public string? AWE { get; set; }
		[Display(Name = "Divine Attention Points")]
		public string? Divine { get; set; }
		[Display(Name = "Insanity Points")]
		public int? Insanity { get; set; }
		public string? Items { get; set; }
		[Display(Name = "Passive Abilities")]
		public string? Passive { get; set; }
		[Display(Name = "Active Abilities")]
		public string? Active { get; set; }
		[Display(Name = "Blessings")]
		public string? Bless { get; set; }
		[Display(Name = "Insanity Effects")]
		public string? Effects { get; set; }
	}
}
