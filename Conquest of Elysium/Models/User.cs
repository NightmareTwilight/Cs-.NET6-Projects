using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conquest_of_Elysium.Models
{
	[NotMapped]
	public class User : IdentityUser
	{
		public List<Sheet>? Sheets { get; set; }
	}
}
