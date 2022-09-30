using System;

namespace Battleships {

	public class Player {
		
		public int player_number;
		private string name;
		public Board board;
		private int points;
		

		public Player(int num) {
			this.name = Program.input("Name: ");
			this.player_number = num;
		}

		public void inc_points() {
			++this.points;
		}

		public int get_points() {
			return this.points;
		}

		private void draw_points(Player other) {
			int s1, s2;
			if (board.justify == (byte)Justify.LEFT) {
				s1 = this.points;
				s2 = other.points;
			} else {
				s1 = other.points;
				s2 = this.points;
			}
			Console.SetCursorPosition(0, (Console.WindowHeight - Program.HEIGHT) >> 1);
			Program.print_center($"{s1} : {s2}");
		}

		public bool is_sunk(Boat boat) {
			if (boat.sunk)
				return true;

			sbyte[,] coords = boat.get();
			for (int y = coords[0, 1]; y < coords[1, 1]; ++y) {
				for (int x = coords[0, 0]; x < coords[1, 0]; ++x) {
					if (board.slot(x, y) != (byte)Tile.HIT)
						return false;
				}
			}

			boat.sunk = true;
			for (int y = coords[0, 1]; y < coords[1, 1]; ++y) {
				for (int x = coords[0, 0]; x < coords[1, 0]; ++x) {
					this.board.set_tile(x, y, (byte)Tile.SUNK);
				}
			}
			return true;
		}

		public void play(Player player2) {
			this.board.xSelected = Program.WIDTH / 2;
			this.board.ySelected = Program.HEIGHT / 2;
			ConsoleKeyInfo pressed;

			while (true) {
				Console.Clear();
				this.board.print();
				player2.board.print();
				Console.SetCursorPosition(0, (Console.WindowHeight >> 1) - Program.HEIGHT - 3);
				Program.print_center($"{this.name}'s Turn");
				this.draw_points(player2);
				pressed = Console.ReadKey(true);
				
				switch (pressed.Key) {
					case ConsoleKey.W:
						this.board.ySelected = (Program.HEIGHT + this.board.ySelected-1)%Program.HEIGHT;
						break;
					case ConsoleKey.D:
						this.board.xSelected = (Program.WIDTH + this.board.xSelected+1) % Program.WIDTH;
						break;
					case ConsoleKey.S:
						this.board.ySelected = (Program.HEIGHT + this.board.ySelected+1) %Program.HEIGHT;
						break;
					case ConsoleKey.A:
						this.board.xSelected = (Program.WIDTH + this.board.xSelected-1) % Program.WIDTH;
						break;
					case ConsoleKey.Enter:
						switch (this.board.slot(this.board.xSelected, this.board.ySelected)) {
							case (byte) Tile.NONE:
							case (byte) Tile.BOAT:
								return;
						}
						break;
					case ConsoleKey.Escape:
						Console.SetCursorPosition(0, (Console.WindowHeight / 2) + Program.HEIGHT);
						Program.print_center("Would you like to exit? (Y/N)..");
						pressed = Console.ReadKey(true);
						if (pressed.Key == ConsoleKey.Y)
							System.Environment.Exit(1);
						break;
				}
			}

		}

		public int get_selected_x() {
			return this.board.xSelected;
		}

		public int get_selected_y() {
			return this.board.ySelected;
		}

		public void setup_board(string name) {
			this.board = new Board(name);
		}

		public string get_name() {
			return this.name;
		}

	}
}
