class Account
{
	public int balance { get; set; }
	public Account()
	{
		balance = 0;
	}
}
class Program
{
	static void Main(string[] args)
	{
		Account account = new();

		StartupScreen(account);
	}

	private static void StartupScreen(Account account)
	{
		bool con = true;
		Console.Clear();
		Console.WriteLine("Welcome to TrackMoney");
		Console.WriteLine($"You have {account.balance} kr on your account.");
		Console.WriteLine("Pick an option:");
		Console.WriteLine("(1) Show Items (All/Expense(s)/Income(s)");
		Console.WriteLine("(2) Add new Expense/Income");
		Console.WriteLine("(3) Edit Item (edit, remove)");
		Console.WriteLine("(4) Save and Quit");
		while (con)
		{
			String input = Console.ReadLine().ToLower().Trim();
			switch (input)
			{
				case "1":
					ShowItemsScreen(account);
					con = false;
					break;
				case "2":
					AddItemsScreen(account);
					con = false;
					break;
				case "3":
					EditItemsScreen(account);
					con = false;
					break;
				case "4":
					SaveAndQuit(account);
					con = false;
					break;
				default:
					Console.WriteLine("Please enter a valid value:");
					break;
			}
		}
	}

	private static void ShowItemsScreen(Account account)
	{
		Console.Clear();
		Console.WriteLine("Showing Items:");

		Console.WriteLine("Type 'back' to go back to start screen.");
		while (true)
		{
			string input = Console.ReadLine().ToLower().Trim();
			if (input == "back")
			{
				StartupScreen(account);
				break;
			}
		}

	}
	private static void AddItemsScreen(Account account)
	{
		Console.Clear();
		Console.WriteLine("Add new Items:");

		Console.WriteLine("Type 'back' to go back to start screen.");
		while (true)
		{
			string input = Console.ReadLine().ToLower().Trim();
			if (input == "back")
			{
				StartupScreen(account);
				break;
			}
		}
	}
	private static void EditItemsScreen(Account account)
	{
		Console.Clear();
		Console.WriteLine("Edit Items:");

		Console.WriteLine("Type 'back' to go back to start screen.");
		while (true)
		{
			string input = Console.ReadLine().ToLower().Trim();
			if (input == "back")
			{
				StartupScreen(account);
				break;
			}
		}
	}
	private static void SaveAndQuit(Account account)
	{
		Console.Clear();
		Console.WriteLine("Saving and quiting...");
	}
}