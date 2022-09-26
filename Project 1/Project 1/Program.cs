using System.Text;
using System.Globalization;
using System.Text.Json;

class Account
{
	public double balance()
	{
		double balance = 0;
		foreach (Item item in items.Where(item => item.Date <= DateOnly.FromDateTime(DateTime.Today)))
		{
			balance += item.Amount * (item.isIncome ? 1 : -1);
		}
		return balance;
	}
	public List<Item> items;
	public Account() 
	{
		items = new();
	}
	public Account(List<Item> items)
	{
		this.items = items;
	}

}
public class Item
{
	public string Title { get; private set; }
	public double Amount { get; private set; }
	public DateOnly Date { get; private set; }
	public bool isIncome { get; private set; }
	public Item(string title, double amount, DateOnly date, bool isIncome)
	{
		Title = title;
		Amount = amount;
		Date = date;
		this.isIncome = isIncome;
	}
}
class Program
{
	static async Task Main(string[] args)
	{

		Account account = new(loadFromFile());

		StartupScreen(account);
	}

	private static void StartupScreen(Account account)
	{
		bool con = true;
		Console.Clear();
		Console.WriteLine("Welcome to TrackMoney" +
			$"\nYou have {account.balance} kr on your account." +
			"\nPick an option:" +
			"\n(1) Show Items (All/Expense(s)/Income(s)" +
			"\n(2) Add new Expense/Income" +
			"\n(3) Edit Item (edit, remove)" +
			"\n(4) Save and Quit");
		while (con)
		{
			String input = Console.ReadLine().ToLower().Trim();
			switch (input)
			{
				case "1":
					con = false;
					ShowItemsScreen(account);
					break;
				case "2":
					con = false;
					AddItemsScreen(account);
					break;
				case "3":
					con = false;
					EditItemsScreen(account);
					break;
				case "4":
					con = false;
					SaveAndQuit(account);
					break;
				default:
					Console.WriteLine("Please enter a valid value:");
					break;
			}
		}
	}

	private static void ShowItemsScreen(Account account, int ShowType = 0)
	{
		var commands = new List<string>
		{
			"back - go back to the main menu.",
			"all - show all items.",
			"incomes - show only incomes.",
			"expenses - show only expenses."
		};
		Console.Clear();
		Console.WriteLine("Showing All " + (ShowType == 0 ? "Items." : ShowType == 1 ? "Incomes." : "Expenses."));
		bool con = true;
		Console.WriteLine("Type 'back' to go back to start screen.");
		List<Item> sorted = account.items.OrderBy(item => item.Date).ToList();
		List<int> uniqueYears = sorted.Select(item => item.Date.Year).Distinct().ToList();
		foreach (int year in uniqueYears)
		{
			Console.WriteLine(year);
			List<int> uniqueMonths = sorted.Where(item => item.Date.Year == year).Select(item => item.Date.Month).Distinct().ToList();
			foreach (int month in uniqueMonths)
			{
				Console.WriteLine(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month).PadLeft(5));
				foreach (Item item in sorted.Where(item => item.Date.Year == year && item.Date.Month == month))
				{
					if (ShowType==0) Console.WriteLine(item.Date.Day.ToString().PadLeft(10).PadRight(15)+item.Title.PadRight(15)+(item.Amount*(item.isIncome?1:-1)).ToString());
					if (ShowType == 1 && item.isIncome) Console.WriteLine(item.Date.Day.ToString().PadLeft(10).PadRight(15) + item.Title.PadRight(15) + (item.Amount).ToString());
					if (ShowType == 2 && !item.isIncome) Console.WriteLine(item.Date.Day.ToString().PadLeft(10).PadRight(15) + item.Title.PadRight(15) + (item.Amount*-1).ToString());
				}
			}
		}
		while (con)
		{
			string input = Console.ReadLine().Trim();
			string[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 0)
			{
				Console.WriteLine("Unknown command!");
				continue;
			}
			switch (split[0].ToLower())
			{
				case "help":
					foreach (string command in commands) Console.WriteLine(command);
					break;
				case "back":
					con = false;
					StartupScreen(account);
					break;
				case "all":
					con = false;
					ShowItemsScreen(account, 0);
					break;
				case "incomes":
					con = false;
					ShowItemsScreen(account, 1);
					break;
				case "expenses":
					con = false;
					ShowItemsScreen(account, 2);
					break;
				default:
					Console.WriteLine("Unknown command!");
					break;
			}
		}

	}
	private static void AddItemsScreen(Account account)
	{
		var commands = new List<string>
		{
			"back - go back to the main menu.",
			"new income {title} {amount} {date} - add new income.",
			"new expense {title} {amount} {date} - add new expense."
		};
		Console.Clear();
		Console.WriteLine("Add new Items:");
		bool con = true;
		Console.WriteLine("Type 'back' to go back to start screen.");
		while (con)
		{
			string input = Console.ReadLine().Trim();
			string[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 0)
			{
				Console.WriteLine("Unknown command!");
				continue;
			}
			switch (split[0])
			{
				case "help":
					foreach (string command in commands) Console.WriteLine(command);
					break;
				case "back":
					con = false;
					StartupScreen(account);
					break;
				case "new":
					if (split.Length != 5)
					{
						Console.WriteLine("Too " + (split.Length>5?"many":"few") + " arguments! Must have 4 arguments.");
						break;
					}
					var type = split[1].ToLower() == "income" ? 0 : split[1].ToLower() == "expense" ? 1 : 2;
					if (type == 2)
					{
						Console.WriteLine("First argument must either be 'income' or 'expense'!");
						break;
					}
					double amount;
					if (!double.TryParse(split[3],NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out amount))
					{
						Console.WriteLine("Amount must be a vallid number!");
						break;
					}
					if (amount != Math.Floor(100 * amount) / 100)
					{
						Console.WriteLine("Too many decimals!");
						break;
					}
					DateOnly date;
					if (split[4].ToLower() == "today")
					{
						date = DateOnly.FromDateTime(DateTime.Today);
					}
					else if (!DateOnly.TryParse(split[4], out date))
					{
						Console.WriteLine("Date is invalid!");
						break;
					}
					account.items.Add(new Item(split[2], Math.Abs(amount), date, type==0));
					break;
				default:
					Console.WriteLine("Unknown command!");
					break;
			}
		}
	}
	private static void EditItemsScreen(Account account)
	{
		var commands = new List<string>
		{
			"back - go back to the main menu.",
			"edit {income/expense} {title} {date} - edits matching item. Afterwards asks for '{income/expense} {title} {amount} {date}', 'keep' keeps the original value for that attribute.",
			"remove {income/expense} {title} {date} - removes the matching item."
		};
		Console.Clear();
		Console.WriteLine("Edit Items:");
		bool con = true;
		Console.WriteLine("Type 'back' to go back to start screen.");
		while (con)
		{
			string input = Console.ReadLine().Trim();
			string[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 0)
			{
				Console.WriteLine("Unknown command!");
				continue;
			}
			switch (split[0].ToLower())
			{
				case "help":
					foreach (string command in commands) Console.WriteLine(command);
					break;
				case "back":
					if (split.Length > 1)
					{
						Console.WriteLine("Command can't take arguments!");
						break;
					}
					con = false;
					StartupScreen(account);
					break;
				case "remove":
					if (split.Length != 4)
					{
						Console.WriteLine("Too " + (split.Length > 4 ? "many" : "few") + " arguments! Must have 4 arguments.");
						break;
					}
					var type = split[1].ToLower() == "income" ? 0 : split[1].ToLower() == "expense" ? 1 : 2;
					if (type == 2)
					{
						Console.WriteLine("First argument must either be 'income' or 'expense'!");
						break;
					}
					DateOnly date;
					if (!DateOnly.TryParse(split[3], out date))
					{
						Console.WriteLine("Date is invalid!");
						break;
					}
					var item = account.items.FirstOrDefault(item => item.isIncome == (type == 0 ? true : false) && item.Title.ToLower() == split[2].ToLower() && item.Date == date, null);
					if (item == null)
					{
						Console.WriteLine("No such item!");
						break;
					}
					account.items.Remove(item);
					Console.WriteLine("Item removed!");
					break;
				case "edit":
					if (split.Length != 4)
					{
						Console.WriteLine("Too " + (split.Length > 4 ? "many" : "few") + " arguments! Must have 4 arguments.");
						break;
					}
					var type2 = split[1].ToLower() == "income" ? 0 : split[1].ToLower() == "expense" ? 1 : 2;
					if (type2 == 2)
					{
						Console.WriteLine("First argument must either be 'income' or 'expense'!");
						break;
					}
					DateOnly date2;
					if (!DateOnly.TryParse(split[3], out date2))
					{
						Console.WriteLine("Date is invalid!");
						break;
					}
					var item2 = account.items.FirstOrDefault(item => item.isIncome == (type2 == 0 ? true : false) && item.Title.ToLower() == split[2].ToLower() && item.Date == date2, null);
					if (item2 == null)
					{
						Console.WriteLine("No such item!");
						break;
					}
					Console.WriteLine("Item found! Please enter new values: ");
					bool cancel = false;
					while (true)
					{
						input = Console.ReadLine();
						string[] split2 = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
						if (split2.Length == 0)
						{
							Console.WriteLine("Unknown command!");
							continue;
						}
						if (split2[0].ToLower() == "help" && split2.Length == 1)
						{
							Console.WriteLine("{income/expense} {new title} {new amount} {new date} - use 'keep' for each variable, to keep the old value." +
								"\ncancel - cancels editing.");
							continue;
						}
						if (split2[0].ToLower() == "cancel" && split2.Length == 1)
						{
							Console.WriteLine("Cancelling edit.");
							cancel = true;
							break;
						}
						if (split2.Length != 4)
						{
							Console.WriteLine("Too " + (split2.Length > 5 ? "many" : "few") + " arguments! Must have 4 arguments.");
							continue;
						}
						var type3 = split2[0].ToLower() == "income" ? 0 : split2[0].ToLower() == "expense" ? 1 : split2[0].ToLower() == "keep" ? -1 : 2;
						if (type3 == 2)
						{
							Console.WriteLine("First argument must either be 'income' or 'expense'!");
							continue;
						}
						double amount;
						if (split2[2].ToLower() == "keep") amount = item2.Amount;
						else if (!double.TryParse(split2[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out amount))
						{
							Console.WriteLine("Amount must be a vallid number!");
							continue;
						}
						if (amount != Math.Floor(100 * amount) / 100)
						{
							Console.WriteLine("Too many decimals!");
							continue;
						}
						DateOnly date3;
						if (split2[4].ToLower() == "today") date3 = DateOnly.FromDateTime(DateTime.Today);
						else if (split2[4].ToLower() == "keep") date3 = item2.Date;
						else if (!DateOnly.TryParse(split2[4], out date3))
						{
							Console.WriteLine("Date is invalid!");
							continue;
						}
						account.items.Add(new Item(split2[1]=="keep"?item2.Title:split2[1], Math.Abs(amount), date3, type3==-1?item2.isIncome:type3 == 0));
						break;
					}
					if(!cancel) account.items.Remove(item2);
					break;
				default:
					Console.WriteLine("Unknown command!");
					break;
			}
		}
	}
	private static void SaveAndQuit(Account account)
	{
		Console.Clear();
		Console.WriteLine("Saving and quiting...");
		var options = new JsonSerializerOptions()
		{
			WriteIndented = true
		};
		var jsonString = JsonSerializer.Serialize(account.items, options);
		File.WriteAllText("account.json", jsonString);
	}
	private static List<Item> loadFromFile()
	{
		string fileName = "account.json";
		List<Item> items = new();
		if (File.Exists(fileName))
		{
			string jsonString = File.ReadAllText(fileName);
			items = JsonSerializer.Deserialize<List<Item>>(jsonString);
		}
		return items;
	}
}