using System;
using System.Collections.Generic;

namespace Battleships {
	
	enum Tile {
		UNKNOWN,
		MISS,
		HIT,
	}

	public class Board {

		public List<Boat> boats = new List<Boat>();
		private byte[,] board = new byte[Program.WIDTH, Program.HEIGHT];

		public int xSelected = -1, ySelected = -1;

		public int ship_tile_count;

		public Board() {
			
			this.place_boat(new Boat(1, 4));
			this.place_boat(new Boat(1, 4));
			this.place_boat(new Boat(1, 3));
			this.place_boat(new Boat(1, 3));
			this.place_boat(new Boat(1, 2));

			this.ship_tile_count = 16;

 		}

		private void place_boat(Boat boat) {
			this.boats.Add(boat);
			while (true) {
				this.print();
				this.print_boats();
				Console.SetCursorPosition(0, Program.HEIGHT + 3);
				_start:
				ConsoleKeyInfo pressed = Console.ReadKey(true);
				switch (pressed.Key) {
					case ConsoleKey.W:
						boat.transform(0, -1);
						break;
					case ConsoleKey.D:
						boat.transform(1, 0);
						break;
					case ConsoleKey.S:
						boat.transform(0, 1);
						break;
					case ConsoleKey.A:
						boat.transform(-1, 0);
						break;
					case ConsoleKey.Q:
						boat.rotate(-1);
						break;
					case ConsoleKey.E:
						boat.rotate(1);
						break;
					case ConsoleKey.Enter:
						goto _end;
					default:
						goto _start;
				}
				Console.SetCursorPosition(0, 0);
			}
			_end:
			Console.Clear();
		}	

		public byte slot(int x, int y) {
			return this.board[x, y];
		}

		public void set_tile(int x, int y, byte tile) {
			this.board[x, y] = tile;
		}

		public void print() {
			Console.Clear();
			int i = 0;
			for (byte y = 0; y < Program.HEIGHT; ++y) {
				for (byte x = 0; x < Program.WIDTH; ++x) {
					switch (board[x, y]) {
						case (byte)Tile.UNKNOWN:
							if ((i++ & 1) == 0)
								this.print_pixel(x, y, ConsoleColor.Blue);
							else
								this.print_pixel(x, y, ConsoleColor.DarkBlue);
							break;
						case (byte)Tile.HIT:
							this.print_pixel(x, y, ConsoleColor.DarkRed);
							break;
						case (byte)Tile.MISS:
							this.print_pixel(x, y, ConsoleColor.DarkGray);
							break;
					}
				}
				++i;
			}
		}

		public void print_boats() {
			foreach (Boat boat in boats) {
				sbyte[,] coords = boat.get();
				for (sbyte y = (coords[0, 1]); y < coords[1, 1]; ++y) {
					for (sbyte x = coords[0, 0]; x < coords[1, 0]; ++x) {
						this.print_pixel((byte)x, (byte)y, ConsoleColor.DarkGray);
					}
				}
			}
			Console.ResetColor();
		}

		private void print_pixel(byte x, byte y, ConsoleColor color) {
			Console.BackgroundColor = color;
			string text = new String(' ', Program.SIZE << 1);
			if (x == xSelected && y == ySelected)
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			int _x = (Console.WindowWidth >> 1) - (Program.WIDTH * Program.SIZE);
			int _y = (Console.WindowHeight - Program.HEIGHT * Program.SIZE) / 2;
			for (int i = 0; i < Program.SIZE; ++i) {
				Console.SetCursorPosition(
						_x + ((x * Program.SIZE) << 1),
						_y + (y * Program.SIZE) + i);
				Console.Write(text);
			}
			Console.ResetColor();
		}
	}
}
