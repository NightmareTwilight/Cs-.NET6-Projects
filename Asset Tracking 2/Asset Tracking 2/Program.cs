using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

class Program //Prevents global variables
{

	static AssetContext context = new();//Initialize Database connection.
	static List<Asset> assets = new();
	static List<Office> offices = new();
	static void Main()
	{
		//get DB data
		assets = getData();
		offices = context.Offices.ToList();

		Screen();
	}

	private static void Screen()
	{
		bool con = true; //Required since using break in a switch doesn't end the loop.
		Console.WriteLine("\nWelcome to AssetTracker!" +
				"\nTo add new Asset, type 'Add' followed by 'Office', 'Asset Type', 'Brand', 'Model', 'Price (in USD)', and 'Purchase Date'." +
				"\nTo change an Asset, type 'Update' followed by 'Office', 'Asset Type', 'Brand', 'Model', and 'Purchase Date'." +
				"\nTo remove an Asset, type 'Remove' followed by 'Office', 'Asset Type', 'Brand', 'Model', and 'Purchase Date'." +
				"\nTo view all Assets, type 'list'." +
				"\nTo Quit, type 'quit'.");

		while (con)
		{
			string input = Console.ReadLine().Trim();
			if (input.Length == 0) //Ignore empty
			{
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				continue;
			}
			var split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

			switch (split[0].ToLower())
			{
				case "clear": //Restarts Screen
					con = false;
					Console.Clear();
					Screen();
					break;
				case "add":
					if (split.Length != 7) //Make sure the amount of arguments match
					{
						Console.WriteLine("Too " + (split.Length > 7 ? "many" : "few") + " arguments.");
						break;
					}
					Office office = context.Offices.FirstOrDefault(x => x.Name.ToLower() == split[1].ToLower(), null);
					if (office == null)
					{
						Console.WriteLine("Unknown office!");
						break;
					}
					double price;
					if (!double.TryParse(split[5], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price)) //Parses doubles using both ',' and '.'
					{
						Console.WriteLine("Price most be a number!");
						break;
					}
					DateOnly date;
					if (split[6].ToLower() == "today")
					{
						date = DateOnly.FromDateTime(DateTime.Today);
					}
					else if (!DateOnly.TryParse(split[6], out date))
					{
						Console.WriteLine("Date is invalid!");
					}
					switch (split[2].ToLower())
					{
						case "pc":
						case "computer":
							context.PCs.Add(new PC(office, date, price, split[3], split[4]));
							break;
						case "laptop":
							context.Laptops.Add(new Laptop(office, date, price, split[3], split[4]));
							break;
						case "phone":
						case "mobile":
						case "smartphone":
							context.Phones.Add(new MobilePhone(office, date, price, split[3], split[4]));
							break;
						default:
							Console.WriteLine("Invalid Type!");
							break;
						
					}
					context.SaveChanges();
					Console.WriteLine("Successfully added Asset!");
					break;
				case "update":
					if(split.Length != 6) //update, office, type, brand, model, purchaseDate
					{
						Console.WriteLine("Too " + (split.Length > 6 ? "many" : "few") + " arguments.");
						break;
					}
					office = context.Offices.FirstOrDefault(x => x.Name.ToLower() == split[1].ToLower(), null);
					if (office == null)
					{
						Console.WriteLine("Unknown office!");
						break;
					}
					String type;
					switch (split[2].ToLower())
					{
						case "pc":
						case "computer":
							type = "PC";
							break;
						case "laptop":
							type = "Laptop";
							break;
						case "phone":
						case "mobile":
						case "smartphone":
							type = "MobilePhone";
							break;
						default:
							type = "";
							break;
					}
					DateOnly d;
					if (!DateOnly.TryParse(split[5], out d))
					{
						Console.WriteLine("Date is invalid!");
					}
					assets = getData();
					Asset? asset = assets.FirstOrDefault(x => x.Office == office && x.GetType().ToString() == type &&
						x.Brand.ToLower() == split[3].ToLower() && x.Model.ToLower() == split[4].ToLower() && x.PurchaseDate == d, null);
					if(asset == null)
					{
						Console.WriteLine("No matching asset found.");
						break;
					}
					Console.WriteLine("Asset found! Please enter new values: ");
					bool c = true;
					while (c)
					{
						input = Console.ReadLine();
						string[] split2 = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
						if (split2.Length == 0)
						{
							continue;
						}
						if (split2[0].ToLower() == "cancel" && split2.Length == 1) //Ensure that user can back out of editing an asset.
						{
							Console.WriteLine("Cancelling edit.");
							break;
						}
						if(split.Length != 6)
						{
							Console.WriteLine("Too " + (split.Length > 6 ? "many" : "few") + " arguments.");
							continue;
						}
						office = context.Offices.FirstOrDefault(x => x.Name.ToLower() == split[1].ToLower(), null);
						if (office == null)
						{
							Console.WriteLine("Unknown office!");
							break;
						}
						double p;
						if (!double.TryParse(split[4], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out p)) //Parses doubles using both ',' and '.'
						{
							Console.WriteLine("Price most be a number!");
							break;
						}
						d = new();
						if (split[5].ToLower() == "today")
						{
							d = DateOnly.FromDateTime(DateTime.Today);
						}
						else if (!DateOnly.TryParse(split[5], out d))
						{
							Console.WriteLine("Date is invalid!");
						}
						type = "";
						switch (split[1].ToLower())
						{
							case "pc":
							case "computer":
								context.PCs.Add(new PC(office, d, p, split[2], split[3]));
								c = false;
								break;
							case "laptop":
								context.Laptops.Add(new Laptop(office, d, p, split[2], split[3]));
								c = false;
								break;
							case "phone":
							case "mobile":
							case "smartphone":
								context.Phones.Add(new MobilePhone(office, d, p, split[2], split[3]));
								c = false;
								break;
							default:
								Console.WriteLine("Invalid Type!");
								break;
						}
						if(c==false)
						{
							switch (asset.GetType().ToString())
							{
								case "PC":
									context.PCs.Remove((PC)asset);
									break;
								case "Laptop":
									context.Laptops.Remove((Laptop)asset);
									break;
								case "MobilePhone":
									context.Phones.Remove((MobilePhone)asset);
									break;
								default:
									Console.WriteLine("Invalid Type Error!");
									break;
							}
							context.SaveChanges();
						}
					}
					break;
				case "remove":
					if(split.Length != 6) //remove, office, type, brand, model, purchaseDate
					{
						Console.WriteLine("Too " + (split.Length > 6 ? "many" : "few") + " arguments.");
						break;
					}
					office = context.Offices.FirstOrDefault(x => x.Name.ToLower() == split[1].ToLower(), null);
					if (office == null)
					{
						Console.WriteLine("Unknown office!");
						break;
					}
					type = "";
					switch (split[2].ToLower())
					{
						case "pc":
						case "computer":
							type = "PC";
							break;
						case "laptop":
							type = "Laptop";
							break;
						case "phone":
						case "mobile":
						case "smartphone":
							type = "MobilePhone";
							break;
						default:
							type = "";
							break;
					}
					d = new();
					if (!DateOnly.TryParse(split[5], out d))
					{
						Console.WriteLine("Date is invalid!");
					}
					assets = getData();
					asset = assets.FirstOrDefault(x => x.Office == office && x.GetType().ToString() == type &&
						x.Brand.ToLower() == split[3].ToLower() && x.Model.ToLower() == split[4].ToLower() && x.PurchaseDate == d, null);
					if(asset == null)
					{
						Console.WriteLine("No matching asset found.");
						break;
					}
					Console.WriteLine("Asset found! Removing...");
					switch (asset.GetType().ToString())
					{
						case "PC":
							context.PCs.Remove((PC)asset);
							break;
						case "Laptop":
							context.Laptops.Remove((Laptop)asset);
							break;
						case "MobilePhone":
							context.Phones.Remove((MobilePhone)asset);
							break;
						default:
							Console.WriteLine("Invalid Type Error!");
							break;
					}
					context.SaveChanges();
					break;
				case "list":
					assets = getData();
					if (assets.Count() == 0)
					{
						Console.WriteLine("No list to show.");
						break;
					}
					List<Asset> sorted = assets.OrderBy(asset => asset.Office.Name).ThenBy(asset => asset.PurchaseDate).ToList();
					Console.WriteLine();
					Console.WriteLine("| Office".PadRight(15) + "| Type".PadRight(15) + "| Brand".PadRight(15) + "| Model".PadRight(15) +
						"| Purchase Date".PadRight(15) + "| Price (USD)".PadRight(15) + "| Currency".PadRight(15) + "| Price (Local)".PadRight(15) + "|");
					Console.WriteLine(string.Concat(Enumerable.Repeat('-', 15 * 8 + 1)));
					foreach (Asset a in sorted)
					{
						if (DateOnly.FromDateTime(DateTime.Today) > a.PurchaseDate.AddYears(2).AddMonths(9))
						{
							LessThan3Months("| " + a.Office.Name.PadRight(13) + "| " + a.GetType().ToString().PadRight(13) + "| " + a.Brand.PadRight(13) +
								"| " + a.Model.PadRight(13) + "| " + a.PurchaseDate.ToString().PadRight(13) + "| " + a.Price.ToString().PadRight(13) +
								"| " + a.Office.Currency.PadRight(13) + "| " + (Math.Floor(100 * a.Price * a.Office.Conversion) / 100).ToString().PadRight(13) + "|");
							Console.BackgroundColor = ConsoleColor.Black;
						}
						else if (DateOnly.FromDateTime(DateTime.Today) > a.PurchaseDate.AddYears(2).AddMonths(6))
						{
							LessThan6Months("| " + a.Office.Name.PadRight(13) + "| " + a.GetType().ToString().PadRight(13) + "| " + a.Brand.PadRight(13) +
								"| " + a.Model.PadRight(13) + "| " + a.PurchaseDate.ToString().PadRight(13) + "| " + a.Price.ToString().PadRight(13) +
								"| " + a.Office.Currency.PadRight(13) + "| " + (Math.Floor(100 * a.Price * a.Office.Conversion) / 100).ToString().PadRight(13) + "|");
							Console.BackgroundColor = ConsoleColor.Black;
						}
						else
						{
							Console.WriteLine("| " + a.Office.Name.PadRight(13) + "| " + a.GetType().ToString().PadRight(13) + "| " + a.Brand.PadRight(13) +
								"| " + a.Model.PadRight(13) + "| " + a.PurchaseDate.ToString().PadRight(13) + "| " + a.Price.ToString().PadRight(13) +
								"| " + a.Office.Currency.PadRight(13) + "| " + (Math.Floor(100 * a.Price * a.Office.Conversion) / 100).ToString().PadRight(13) + "|");
						}
					}
					Console.BackgroundColor = ConsoleColor.Black;
					Console.WriteLine(string.Concat(Enumerable.Repeat('-', 15 * 8 + 1)));
					Console.WriteLine();
					break;
				case "quit":
					con = false;
					break;
				default:
					Console.WriteLine("Unknown Command!");
					break;
			}
		}
	}
	private static void LessThan3Months(string msg = "")
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.ForegroundColor = ConsoleColor.Black;
		Console.WriteLine(msg);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.BackgroundColor = ConsoleColor.Black;
	}
	private static void LessThan6Months(string msg = "")
	{
		Console.BackgroundColor = ConsoleColor.Yellow;
		Console.ForegroundColor = ConsoleColor.Black;
		Console.WriteLine(msg);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.BackgroundColor = ConsoleColor.Black;
	}

	static List<Asset> getData()
	{
		List<Asset> assets = new();

		if(context.Database.EnsureCreated())
		{
			List<Office> offices = new(); //Initialize offices
			offices.Add(new Office("Sweden", "SEK", 10.67d));
			offices.Add(new Office("Germany", "EUR", 1.0d));
			offices.Add(new Office("Poland", "PLN", 4.74d));
			context.Offices.AddRange(offices);
			context.SaveChanges();
		}
		else
		{
			assets.AddRange(context.PCs.Include(c => c.Office).ToList());
			assets.AddRange(context.Laptops.Include(c => c.Office).ToList());
			assets.AddRange(context.Phones.Include(c => c.Office).ToList());
		}
		return assets;
	}
}

class Office
{
	public string Name { get; private set; }
	public string Currency { get; private set; }
	public double Conversion { get; private set; }
	public int Id { get; set; } //For Database
	/**
	 * <summary>Object containing information about an office</summary>
	 * <param name="name">Name of the office</param>
	 * <param name="currency">Shorthand name of local currency</param>
	 * <param name="conversion">Conversion rate between local currency and USD</param>
	 */
	public Office(string name, string currency, double conversion)
	{
		Name = name;
		Currency = currency;
		Conversion = conversion;
	}
}
abstract class Asset
{
	public Office Office { get; set; }
	public DateOnly PurchaseDate { get; set; }
	public double Price { get; set; }
	public string Brand { get; set; }
	public string Model { get; set; }
}
class PC : Asset
{
	public int Id { get; set; } //For Database
	public PC() { }
	/**
	 * <summary>Object containing information about an asset</summary>
	 * <param name="office">The office the asset belongs to</param>
	 * <param name="purchaseDate">Date of purchase 'YYYY/MM/DD', alternatively 'today' for today's date</param>
	 * <param name="price">The purchase cost of the asset in USD</param>
	 * <param name="brand">The Brand of the asset</param>
	 * <param name="model">model of the asset</param>
	 */
	public PC(Office office, DateOnly purchaseDate, double price, string brand, string model)
	{
		Office = office;
		PurchaseDate = purchaseDate;
		Price = price;
		Brand = brand;
		Model = model;
	}
}
class Laptop : Asset
{
	public int Id { get; set; } //For Database
	public Laptop() { }
	/**
	* <summary>Object containing information about an asset</summary>
	* <param name="office">The office the asset belongs to</param>
	* <param name="purchaseDate">Date of purchase 'YYYY/MM/DD', alternatively 'today' for today's date</param>
	* <param name="price">The purchase cost of the asset in USD</param>
	* <param name="brand">The Brand of the asset</param>
	* <param name="model">model of the asset</param>
	*/
	public Laptop(Office office, DateOnly purchaseDate, double price, string brand, string model)
	{
		Office = office;
		PurchaseDate = purchaseDate;
		Price = price;
		Brand = brand;
		Model = model;
	}
}
class MobilePhone : Asset
{
	public int Id { get; set; } //For Database
	public MobilePhone() { }
	/**
	 * <summary>Object containing information about an asset</summary>
	 * <param name="office">The office the asset belongs to</param>
	 * <param name="purchaseDate">Date of purchase 'YYYY/MM/DD'</param>
	 * <param name="price">The purchase cost of the asset in USD</param>
	 * <param name="brand">The Brand of the asset</param>
	 * <param name="model">model of the asset</param>
	 */
	public MobilePhone(Office office, DateOnly purchaseDate, double price, string brand, string model)
	{
		Office = office;
		PurchaseDate = purchaseDate;
		Price = price;
		Brand = brand;
		Model = model;
	}
}
class AssetContext : DbContext
{
	public DbSet<Office> Offices { get; set; }
	public DbSet<PC> PCs { get; set; }
	public DbSet<Laptop> Laptops { get; set; }
	public DbSet<MobilePhone> Phones { get; set; }

	string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = AssetTracking; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// We tell the app to use the connectionstring.
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
	protected override void ConfigureConventions(ModelConfigurationBuilder builder)
	{
		builder.Properties<DateOnly>()
			.HaveConversion<DateOnlyConverter>()
			.HaveColumnType("date");
	}
}


/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
	/// <summary>
	/// Creates a new instance of this converter.
	/// </summary>
	public DateOnlyConverter() : base(
			d => d.ToDateTime(TimeOnly.MinValue),
			d => DateOnly.FromDateTime(d))
	{ }
}