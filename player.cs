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

		public void play() {
			this.board.xSelected = Program.WIDTH / 2;
			this.board.ySelected = Program.HEIGHT / 2;
			ConsoleKeyInfo pressed;

			while (true) {
				this.board.print();
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
						if (this.board.slot(this.board.xSelected, this.board.ySelected) 
								== (byte) Tile.UNKNOWN) {
							return;
						}
						break;
					case ConsoleKey.Escape:
						Console.SetCursorPosition(0, (Console.WindowHeight / 2) + Program.HEIGHT);
						Program.print_center("Would you like to exit? (Y/N)..");
						string t = Console.ReadLine().ToLower();
						if (t == "y")
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

		public void setup_board() {
			this.board = new Board();
		}

		public string get_name() {
			return this.name;
		}

	}
}
