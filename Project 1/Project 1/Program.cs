using AutoCompleteUtils;
using System.Text;
using System.Globalization;

class Account
{
	public double balance { get; set; }
	public List<Item> items = new();
	public Account(double balance=0)
	{
		this.balance = balance;
	}
	public Account(double balance, List<Item> items)
	{
		this.balance= balance;
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
		Account account = new();

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

	private static void ShowItemsScreen(Account account)
	{
		var commands = new List<string>
		{
			"back"
		};
		Console.Clear();
		Console.WriteLine("Showing Items:");
		bool con = true;
		Console.WriteLine("Type 'back' to go back to start screen.");
		while (con)
		{
			var builder = new StringBuilder();
			var line = new StringBuilder();
			string autoCompletedLine = null;
			var result = Console.ReadKey(true);
			while (result.Key != ConsoleKey.Enter)
			{
				if (result.Key == ConsoleKey.Tab)
				{
					autoCompletedLine = AutoComplete.GetComplimentaryAutoComplete(builder.ToString(), commands);
					line.Clear();
					line.Append(autoCompletedLine);
					ClearCurrentLine();
					Console.Write(autoCompletedLine);
				}
				else
				{
					HandleKeyInput(builder, result, line);
				}
				result = Console.ReadKey(intercept: true);
			}
			Console.WriteLine();
			if (autoCompletedLine != null)
			{
				builder.Clear();
				builder.Append(autoCompletedLine);
			}
			string[] split = builder.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 0)
			{
				Console.WriteLine("Unknown command!");
				continue;
			}
			switch (split[0])
			{
				case "back":
					con = false;
					StartupScreen(account);
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
			"back",
			"new income",
			"new expense"
		};
		Console.Clear();
		Console.WriteLine("Add new Items:");
		bool con = true;
		Console.WriteLine("Type 'back' to go back to start screen.");
		while (con)
		{
			var builder = new StringBuilder();
			var line = new StringBuilder();
			string autoCompletedLine = null;
			var result = Console.ReadKey(true);
			while (result.Key != ConsoleKey.Enter)
			{
				if (result.Key == ConsoleKey.Tab)
				{
					autoCompletedLine = AutoComplete.GetComplimentaryAutoComplete(builder.ToString(), commands);
					line.Clear();
					line.Append(autoCompletedLine);
					ClearCurrentLine();
					Console.Write(autoCompletedLine);
				}
				else
				{
					HandleKeyInput(builder, result, line);
				}
				result = Console.ReadKey(intercept: true);
			}
			Console.WriteLine();
			if (autoCompletedLine != null)
			{
				builder.Clear();
				builder.Append(autoCompletedLine);
			}
			string[] split = builder.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 0)
			{
				Console.WriteLine("Unknown command!");
				continue;
			}
			switch (split[0])
			{
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
					}
					account.items.Add(new Item(split[2], amount, date, type==0));
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
			"back",
			"edit",
			"remove"
		};
		Console.Clear();
		Console.WriteLine("Edit Items:");
		bool con = true;
		Console.WriteLine("Type 'back' to go back to start screen.");
		while (con)
		{
			var builder = new StringBuilder();
			var line = new StringBuilder();
			string autoCompletedLine = null;
			var result = Console.ReadKey(true);
			while (result.Key != ConsoleKey.Enter)
			{
				if (result.Key == ConsoleKey.Tab)
				{
					autoCompletedLine = AutoComplete.GetComplimentaryAutoComplete(builder.ToString(), commands);
					line.Clear();
					line.Append(autoCompletedLine);
					ClearCurrentLine();
					Console.Write(autoCompletedLine);
				}
				else
				{
					HandleKeyInput(builder, result, line);
				}
				result = Console.ReadKey(intercept: true);
			}
			Console.WriteLine();
			if (autoCompletedLine != null)
			{
				builder.Clear();
				builder.Append(autoCompletedLine);
			}
			string[] split = builder.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 0)
			{
				Console.WriteLine("Unknown command!");
				continue;
			}
			switch (split[0].ToLower())
			{
				case "back":
					if (split.Length > 1)
					{
						Console.WriteLine("Command can't take arguments!");
						break;
					}
					con = false;
					StartupScreen(account);
					break;
				case "edit":

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
	}
	/**<summary>
	 * Clears a row, leaving 'pos' amount of characters.
	 * </summary>
	 * <param name="pos">How many characters to leave.</param>
	 */
	private static void ClearCurrentLine(int pos=0)
	{
		var currentLine = Console.CursorTop;
		Console.SetCursorPosition(pos, Console.CursorTop);
		Console.Write(new string(' ', Console.WindowWidth));
		Console.SetCursorPosition(pos, currentLine);
	}
	/**<summary>
	 * Handles writing characters to prevent issues.
	 * </summary>
	 * <param name="builder">The current input.</param>
	 * <param name="input">Key pressed</param>
	 */
	private static void HandleKeyInput(StringBuilder builder, ConsoleKeyInfo input, StringBuilder line)
	{
		var currentInput = builder.ToString();
		if (input.Key == ConsoleKey.Backspace && currentInput.Length > 0)
		{
			try
			{
				builder.Remove(Console.CursorLeft-1,1);
				line.Remove(Console.CursorLeft - 1, 1);
				ClearCurrentLine();
	
				currentInput = line.ToString();
				Console.Write(currentInput);
			}
			catch(Exception e) { 
				Console.WriteLine("\n" +e);
				Console.WriteLine("length="+ currentInput.Length);
				Console.WriteLine("CurrentInput=" + currentInput);
				Console.WriteLine("builder=" + builder);
				Console.WriteLine("line=" + line);
			}
			return;
		}
		if (input.Key == ConsoleKey.Delete && line.Length>Console.CursorLeft)
		{
			try
			{
				line.Remove(Console.CursorLeft, 1);
				ClearCurrentLine();

				currentInput = line.ToString();
				Console.Write(currentInput);
				
			}
			catch (Exception e)
			{
				Console.WriteLine("\n" + e);
			}
			return;
		}
		if (input.Key != ConsoleKey.Backspace)
		{
			if (currentInput.Length > 0 && input.Key == ConsoleKey.LeftArrow)
			{
				try
				{
					Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
					currentInput = currentInput.Remove(currentInput.Length - 1);
					builder.Clear();
					builder.Append(currentInput);
				}
				catch
				{
				}
				
			}
			else if (input.Key == ConsoleKey.RightArrow && line.Length>builder.Length)
			{
				Console.SetCursorPosition(Console.CursorLeft+1, Console.CursorTop);
				builder.Append(line.ToString()[builder.Length]);
			}
			else if (!(input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.DownArrow))
			{
				var key = input.KeyChar;
				builder.Append(key);
				line.Insert(Console.CursorLeft, key);
				Console.Write(key);
			}
		}
	}
}