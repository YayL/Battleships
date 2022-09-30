using System;

namespace Battleships {

	public static class Program {

		public const int WIDTH = 10;
		public const int HEIGHT = 10;
		public const int SIZE = 2;

		public static void Main() {
			Menu menu = new Menu();
			menu.set_title("Welcome to Battleships");
			menu.add_entry("Play");
			menu.add_entry("Keybindings");
			menu.add_entry("Exit");
			menu.set_title_offset(2);
			int selected = menu.start();
			switch (selected) {
				case 0:
					new Game();
					break;
				case 1:
					Console.WriteLine("Not implemented yet");
					break;
				case 2:
					break;
				default:
					Console.WriteLine("ERROR: Something wrong with menu selection!");
					break;
			}
		}

		private static void menu(int selected_line) {
			while (true) {
				Console.Clear();
				print_center("Welcome to Battleships\n\n");
				print_center("Play");
			}

		}

		public static void print_center(string out_text) {

			Console.Write(new String(' ', (Console.WindowWidth - out_text.Length) / 2));
			Console.WriteLine(out_text);
		}
 
		public static string input(string outText) {
			Console.Write(outText);
			return Console.ReadLine();
		}
	}
}
