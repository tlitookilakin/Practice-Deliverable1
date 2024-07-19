namespace Deliverable1
{
	public class InventoryData(int stock, int restock)
	{
		public int Stock = stock;
		public int Restock = restock;
	}

	internal class Program
	{
		private static Dictionary<string, InventoryData> Inventory = new()
		{
			{"Soda", new(100, 40)}, {"Chips", new(40, 20)}, {"Candy", new(60, 40)}
		};

		static void Main(string[] args)
		{
			Console.WriteLine("- Inventory Management Tool -");
			Console.WriteLine();

			List<(string, int)> restockRequired = new();

			foreach (var (Name, Data) in Inventory)
				if (UpdateStock(ref Data.Stock, Data.Restock, Name))
					restockRequired.Add((Name, Data.Restock - Data.Stock + 1));

			if (restockRequired.Count is 0)
			{
				Console.WriteLine("Thank you for updating the inventory. There are currently no items that need restocking.");
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine($"Thank you for updating the inventory. There are {restockRequired.Count} items that require restocking:");
				foreach (var (Name, Minimum) in restockRequired)
					Console.WriteLine($"\t{Name}:\t{Minimum} units minimum.");

				Console.WriteLine();
				Console.WriteLine("Please review this list and reorder as needed.");
			}

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}

		static bool UpdateStock(ref int stock, int restock, string name)
		{
			Console.WriteLine($"How many units of {name} have been sold today? ({stock} in stock.)");
			var sold = ReadNumber(stock);
			stock -= sold;

			Console.WriteLine($"There are {stock} units of {name} left.");
			Console.WriteLine();
			return stock <= restock;
		}

		static int ReadNumber(int max)
		{
			while (true)
			{
				var line = Console.ReadLine();
				if (int.TryParse(line, out var number))
				{
					if (number <= max)
						return number;

					Console.WriteLine($"Sell count {number} is too high! Inventory will not be adjusted.");
					return 0;
				}
				Console.WriteLine($"Input '{line}' is not a valid number! Please re-enter.");
			}
		}
	}
}
