using System.Collections.Generic;
using System.Globalization;


class Program //Prevents global variables
{
	static List<Office> offices = new(); //Initialize a list of valid offices
	static List<Asset> assets = new(); //Initialize a list of assets
	static void Main()
	{
		//Initialize offices
		offices.Add(new Office("Sweden","SEK", 10.67d));
		offices.Add(new Office("Germany", "EUR", 1.0d));
		offices.Add(new Office("Poland", "PLN", 4.74d));

		Screen();
	}

	private static void Screen()
	{
		bool con = true; //Required since using break in a switch doesn't end the loop.
		Console.WriteLine("\nWelcome to AssetTracker!" +
				"\nTo add new Asset, type 'Add' followed by 'Office', 'Asset Type', 'Brand', 'Model', 'Price (in USD)', and 'Purchase Date'." +
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
					con=false;
					Console.Clear();
					Screen();
					break;
				case "add":
					if (split.Length != 7) //Make sure the amount of arguments match
					{
						Console.WriteLine("Too " + (split.Length>7?"many":"few") + " arguments.");
						break;
					}
					Office office = offices.FirstOrDefault(x => x.Name.ToLower() == split[1].ToLower(), null);
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
					else if(!DateOnly.TryParse(split[6], out date))
					{
						Console.WriteLine("Date is invalid!");
					}
					switch (split[2].ToLower())
					{
						case "pc":
						case "computer":
							assets.Add(new PC(office, date, price, split[3], split[4]));
							break;
						case "laptop":
							assets.Add(new Laptop(office, date, price, split[3], split[4]));
							break;
						case "phone":
						case "mobile":
						case "smartphone":
							assets.Add(new MobilePhone(office, date, price, split[3], split[4]));
							break;
						default:
							Console.WriteLine("Invalid Type!");
							break;
					}
					Console.WriteLine("Successfully added Asset!");
					break;
				case "list":
					if (assets.Count() == 0)
					{
						Console.WriteLine("No list to show.");
						break;
					}
					List<Asset> sorted = assets.OrderBy(asset => asset.Office.Name).ThenBy(asset => asset.PurchaseDate).ToList();
					Console.WriteLine();
					Console.WriteLine("| Office".PadRight(15) + "| Type".PadRight(15) + "| Brand".PadRight(15) + "| Model".PadRight(15) + 
						"| Purchase Date".PadRight(15) + "| Price (USD)".PadRight(15) + "| Currency".PadRight(15) + "| Price (Local)".PadRight(15) + "|");
					Console.WriteLine(string.Concat(Enumerable.Repeat('-', 15*8+1)));
					foreach (Asset asset in sorted)
					{
						if (DateOnly.FromDateTime(DateTime.Today) > asset.PurchaseDate.AddYears(2).AddMonths(9))
						{
							LessThan3Months("| " + asset.Office.Name.PadRight(13) + "| " + asset.GetType().ToString().PadRight(13) + "| " + asset.Brand.PadRight(13) + 
								"| " + asset.Model.PadRight(13) + "| " + asset.PurchaseDate.ToString().PadRight(13) + "| " + asset.Price.ToString().PadRight(13) + 
								"| " + asset.Office.Currency.PadRight(13) + "| " + (Math.Floor(100*asset.Price*asset.Office.Conversion)/100).ToString().PadRight(13) + "|");
							Console.BackgroundColor = ConsoleColor.Black;
						}
						else if (DateOnly.FromDateTime(DateTime.Today) > asset.PurchaseDate.AddYears(2).AddMonths(6))
						{
							LessThan6Months("| " + asset.Office.Name.PadRight(13) + "| " + asset.GetType().ToString().PadRight(13) + "| " + asset.Brand.PadRight(13) +
								"| " + asset.Model.PadRight(13) + "| " + asset.PurchaseDate.ToString().PadRight(13) + "| " + asset.Price.ToString().PadRight(13) +
								"| " + asset.Office.Currency.PadRight(13) + "| " + (Math.Floor(100 * asset.Price * asset.Office.Conversion) / 100).ToString().PadRight(13) + "|");
							Console.BackgroundColor = ConsoleColor.Black;
						}
						else
						{
							Console.WriteLine("| " + asset.Office.Name.PadRight(13) + "| " + asset.GetType().ToString().PadRight(13) + "| " + asset.Brand.PadRight(13) +
								"| " + asset.Model.PadRight(13) + "| " + asset.PurchaseDate.ToString().PadRight(13) + "| " + asset.Price.ToString().PadRight(13) +
								"| " + asset.Office.Currency.PadRight(13) + "| " + (Math.Floor(100 * asset.Price * asset.Office.Conversion) / 100).ToString().PadRight(13) + "|");
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
	private static void LessThan3Months(string msg="")
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.ForegroundColor = ConsoleColor.Black;
		Console.WriteLine(msg);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.BackgroundColor = ConsoleColor.Black;
	}
	private static void LessThan6Months(string msg="")
	{
		Console.BackgroundColor = ConsoleColor.Yellow;
		Console.ForegroundColor = ConsoleColor.Black;
		Console.WriteLine(msg);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.BackgroundColor = ConsoleColor.Black;
	}
}

class Office
{
	public string Name { get; private set; }
	public string Currency { get; private set; }
	public double Conversion { get; private set; }

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
	public Office Office { get; private set; }
	public DateOnly PurchaseDate { get; protected set; }
	public double Price { get; protected set; }
	public string Brand { get; protected set; }
	public string Model { get; protected set; }

	/**
	 * <summary>Object containing information about an asset</summary>
	 * <param name="office">The office the asset belongs to</param>
	 * <param name="purchaseDate">Date of purchase 'YYYY/MM/DD', alternatively 'today' for today's date</param>
	 * <param name="price">The purchase cost of the asset in USD</param>
	 * <param name="brand">The Brand of the asset</param>
	 * <param name="model">model of the asset</param>
	 */
	public Asset(Office office, DateOnly purchaseDate, double price, string brand, string model)
	{
		Office = office;
		PurchaseDate = purchaseDate;
		Price = price;
		Brand = brand;
		Model = model;
	}
}

class PC : Asset
{
	/**
	 * <summary>Object containing information about an asset</summary>
	 * <param name="office">The office the asset belongs to</param>
	 * <param name="purchaseDate">Date of purchase 'YYYY/MM/DD', alternatively 'today' for today's date</param>
	 * <param name="price">The purchase cost of the asset in USD</param>
	 * <param name="brand">The Brand of the asset</param>
	 * <param name="model">model of the asset</param>
	 */
	public PC(Office office, DateOnly purchaseDate, double price, string brand, string model) : 
		base(office, purchaseDate, price, brand, model)
	{

	}
}
class Laptop : Asset
{
	/**
	 * <summary>Object containing information about an asset</summary>
	 * <param name="office">The office the asset belongs to</param>
	 * <param name="purchaseDate">Date of purchase 'YYYY/MM/DD', alternatively 'today' for today's date</param>
	 * <param name="price">The purchase cost of the asset in USD</param>
	 * <param name="brand">The Brand of the asset</param>
	 * <param name="model">model of the asset</param>
	 */
	public Laptop(Office office, DateOnly purchaseDate, double price, string brand, string model) : 
		base(office, purchaseDate, price, brand, model)
	{

	}
}
class MobilePhone : Asset
{
	/**
	 * <summary>Object containing information about an asset</summary>
	 * <param name="office">The office the asset belongs to</param>
	 * <param name="purchaseDate">Date of purchase 'YYYY/MM/DD'</param>
	 * <param name="price">The purchase cost of the asset in USD</param>
	 * <param name="brand">The Brand of the asset</param>
	 * <param name="model">model of the asset</param>
	 */
	public MobilePhone(Office office, DateOnly purchaseDate, double price, string brand, string model) : 
		base(office, purchaseDate, price, brand, model)
	{
		
	}
}
