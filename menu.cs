using System;
using System.Collections.Generic;

namespace Battleships {

	public class Menu {
		
		private string title;
		private List<string> entries = new List<string>();
		private short selected = 0;
		private short title_offset = 0;
		
		public void set_title(string title) {
			this.title = title;
		}

		public void add_entry(string entry) {
			this.entries.Add(entry);
		}

		public void change_entry(string new_entry, int index) {
			this.entries[index] = new_entry;
		}

		public void set_title_offset(short new_value) {
			this.title_offset = new_value;
		}

		public int start() {
			Console.Clear();

			Program.print_center(this.title);
			if (title_offset != 0)
				Console.Write(new String('\n', title_offset));

			while (true) {
				print_entries();
				while (true) {
					ConsoleKeyInfo pressed = Console.ReadKey(true);
				
					if (pressed.Key == ConsoleKey.Enter)
						return this.selected;
					else if (entries.Count == 0)
						continue;

					if (pressed.Key == ConsoleKey.W || pressed.Key == ConsoleKey.UpArrow) {
						this.set_selected(-1);
						this.go_up(this.entries.Count);
						break;
					} else if (pressed.Key == ConsoleKey.S || pressed.Key == ConsoleKey.DownArrow) {
						this.set_selected(1);
						this.go_up(this.entries.Count);
						break;
					}
				}
			}
		}

		private void go_up(int n) {
			Console.SetCursorPosition(0, Console.CursorTop - n);
		}

		private void set_selected(int inc) {
			this.selected = (short)((this.entries.Count + this.selected + inc) 
					% this.entries.Count);
		}

		private void print_entries() {
			for (int i = 0; i < this.entries.Count; ++i) {
				if (i == this.selected) {
					this.print_center(this.entries[i], ConsoleColor.White, ConsoleColor.Black);
				}
				else {
					Program.print_center(this.entries[i]);
				}
			}
		}

		private void print_center(string message, ConsoleColor bColor, ConsoleColor fColor) {
			Console.ResetColor();
			Console.Write(new String(' ', (Console.WindowWidth - message.Length) / 2));
			Console.BackgroundColor = bColor;
			Console.ForegroundColor = fColor;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}
