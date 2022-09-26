using System.Text.RegularExpressions;

class Item
{
	public Item(string category, string name, int price)
	{
		Category = category;
		Name = name;
		Price = price;
	}

	public string Category { get; private set; }
	public string Name { get; private set; }
	public int Price { get; private set; }
}
	
class Program
{
	static void Main(string[] args)
	{
		List<Item> list = new();
		StartupScreen(list);
	}

	private static void StartupScreen(List<Item> list)
	{
		Console.Clear();
		Console.WriteLine("\nWelcome to Productlist: Home");
		Console.WriteLine("----------------------------\n");
		while (true)
		{
			Console.WriteLine("Please Enter 'Category', 'Product Name', and 'Price' to add Item. Separate with space.");
			Console.WriteLine("Enter 'q' to Quit. Enter 'c' to clear the screen.");
			if(list.Count != 0) Console.WriteLine("You have one or more items in you list, Enter 'list' to show all items.");
			String input = Console.ReadLine().Trim();
			if (list.Count != 0 && input.ToLower() == "list")
			{
				ShowList(list);
				break;
			}
			if (input.ToLower() == "q") break;
			if (input.ToLower() == "c")
			{
				StartupScreen(list);
				break;
			}
			if (input == "" || input == null) 
			{ 
				Console.Write("Error: "); 
				ErrorMessage("Input can't be empty!\n");
				continue;
			}
			input = Regex.Replace(input,@"\s+"," ");
			string[] split = input.Split(' ');
			if (split.Length != 3)
			{
				Console.Write("Error: ");
				ErrorMessage("Input contains too " + (split.Length>3?"many":"few") + " parameters!\n");
				continue;
			}
			bool e = false;
			for (int i=0;i<3;i++)
			{
				split[i] = split[i].Trim();
				if (split[i] == "" || split[i] == null)
				{
					e = true;
				}
			}
			if (e)
			{
				Console.Write("Error: ");
				ErrorMessage("One or more parameters were empty!\n");
				continue;
			}
			if (!split[2].All(Char.IsNumber))
			{
				Console.Write("Error: ");
				ErrorMessage("Price must be a number!\n");
				continue;
			}
			int price = Convert.ToInt32(split[2]);
			list.Add(new Item(split[0], split[1], price));
		}
		Console.Clear();
	}

	private static void ShowList(List<Item> list)
	{
		List<Item> sortedList = list.OrderBy(item => item.Price).ToList();
		Console.Clear();
		Console.WriteLine("\nWelcome to Productlist: List");
		Console.WriteLine("----------------------------\n");
		Console.WriteLine("Enter 'q' to Quit, 'c' to clear the screen, or 'b' to go back to the start screen.");
		Console.WriteLine("To search for something specific, type 'search' and the name or category.");
		Console.WriteLine("Adding the '-c' or '-n' flags after search will make it only list matching category/product names.");
		Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)));
		Console.WriteLine("| Category".PadRight(15) + "| Name".PadRight(15) + "| Price".PadRight(15) + "|");
		Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)));
		foreach (Item item in sortedList)
		{
			Console.WriteLine("| " + item.Category.PadRight(13) + "| " + item.Name.PadRight(13) + "| " + item.Price.ToString().PadRight(13) + "|");
		}
		Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)));
		Console.WriteLine("| Sum: "+ sortedList.Sum(item => item.Price).ToString().PadRight(38)+"|");
		Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46))+"\n");
		while (true)
		{
			String input = Console.ReadLine().Trim();
			if (input.ToLower() == "b")
			{
				StartupScreen(list);
				break;
			}
			if (input.ToLower() == "q") break;
			if (input.ToLower() == "c")
			{
				ShowList(list);
				break;
			}
			if (input == "" || input == null)
			{
				Console.Write("Error: ");
				ErrorMessage("Input can't be empty!\n");
				continue;
			}
			input = Regex.Replace(input, @"\s+", " ");
			string[] split = input.Split(' ');
			if (split[0].ToLower() == "search")
			{
				if (split.Length == 1)
				{
					Console.Write("Error: ");
					ErrorMessage("Search requires one search parameter!\n");
					continue;
				}
				int search;
				switch (split[1].ToLower())
				{
					case "-c":
						search = 1;
						break;
					case "-n":
						search = 2;
						break;
					default: 
						search = 0;
						break;
				}
				if (split.Length == 2 && search!=0)
				{
					Console.Write("Error: ");
					ErrorMessage("Search requires one search parameter!\n");
					continue;
				}
				if (split.Length == 3 && search == 0)
				{
					Console.Write("Error: ");
					ErrorMessage("Unknown flag '" + split[1] + "'!\n");
					continue;
				}
				Console.Clear();
				Console.WriteLine("\nWelcome to Productlist: List");
				Console.WriteLine("----------------------------\n");
				Console.WriteLine("Enter 'q' to Quit, 'c' to clear the screen, or 'b' to go back to the start screen.");
				Console.WriteLine("To search for something specific, type 'search' and the name or category.");
				Console.WriteLine("Adding the '-c' or '-n' flags after search will make it only list matching category/product names.");
				Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)));
				Console.WriteLine("| Category".PadRight(15) + "| Name".PadRight(15) + "| Price".PadRight(15) + "|");
				Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)));
				foreach (Item item in sortedList)
				{
					if (search == 0 && (item.Category.Contains(split[1]) || item.Name.Contains(split[1])))
					{
						HighlightMessage("| " + item.Category.PadRight(13) + "| " + item.Name.PadRight(13) + "| " + item.Price.ToString().PadRight(13) + "|");
					}
					else if((search == 1 && item.Category.Contains(split[2])) || (search == 2 && item.Name.Contains(split[2])))
					{
						HighlightMessage("| " + item.Category.PadRight(13) + "| " + item.Name.PadRight(13) + "| " + item.Price.ToString().PadRight(13) + "|");
					}
					else
					{
						Console.WriteLine("| " + item.Category.PadRight(13) + "| " + item.Name.PadRight(13) + "| " + item.Price.ToString().PadRight(13) + "|");
					}
				}
				Console.BackgroundColor = ConsoleColor.Black;
				Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)));
				Console.WriteLine("| Sum: " + sortedList.Sum(item => item.Price).ToString().PadRight(38) + "|");
				Console.WriteLine(string.Concat(Enumerable.Repeat('-', 46)) + "\n");
				continue;
			}
			Console.Write("Error: ");
			ErrorMessage("Unknown command!\n");
		}
	}

	private static void ErrorMessage(string msg)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(msg);
		Console.ForegroundColor = ConsoleColor.Gray;
	}

	private static void HighlightMessage(string msg)
	{
		Console.BackgroundColor = ConsoleColor.Yellow;
		Console.ForegroundColor = ConsoleColor.Black;
		Console.WriteLine(msg);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.BackgroundColor = ConsoleColor.Black;
	}
}
