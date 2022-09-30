using System;
using System.Collections.Generic;

namespace Battleships {

	public class Game {
		
		private Player[] players = new Player[2];

		public Game() {

			Console.Clear();
			this.get_players();
			this.start();
		}

		private void get_players() {
			
			Menu menu = new Menu();
			menu.set_title("Players");
			menu.set_title_offset(2);
			menu.add_entry("<Player 1>");
			menu.add_entry("<Player 2>");
			menu.add_entry("Start");
			Player player;
			while (true) {
				switch (menu.start()) {
					case 0:
						player = new Player(0);
						if (player.get_name() != "") {
							menu.change_entry(player.get_name(), 0);
							players[0] = player;
						}
						break;
					case 1:
						player = new Player(1);
						if (player.get_name() != "") {
							menu.change_entry(player.get_name(), 1);
							players[1] = player;
						}
						break;
					case 2:
						return;
					default:
						Console.WriteLine("Editing Player is not available yet");
						Program.input("Press enter to continue..");
						break;
				}
			}

		}

		private void start() {

			foreach(Player player in players) {
				player.setup_board();
			}

			Console.Clear();
			bool stop = false;

			while (!stop) {
				foreach (Player player in players) {
					player.play();
					this.fire(player);
					player.board.xSelected = player.board.ySelected = -1;
					player.board.print();
					Console.SetCursorPosition(0, (Console.WindowHeight / 2) + Program.HEIGHT);
					Program.print_center("Press enter to continue..");
					Console.ReadLine();
				}
			}

			Menu menu = new Menu();

			menu.set_title("GAME FINISHED!");
			menu.set_title_offset(2);
			menu.add_entry("Main menu");
			menu.add_entry("Exit");

			switch (menu.start()) {
				case 0:
					break;
				case 1:
					System.Environment.Exit(1);
					break;
			}
		}

		public bool fire(Player player) {
			Player p2 = this.players[1 - player.player_number];
			int x = player.board.xSelected, y = player.board.ySelected;
			foreach (Boat boat in p2.board.boats) {
				sbyte[,] coords = boat.get();
				if (coords[0, 0] <= x && x < coords[1,0] && coords[0, 1] == y) {
					player.board.set_tile(x, y, (byte)Tile.HIT);
					return true;
				} else if (coords[0, 1] <= y && y < coords[1, 1] && coords[0, 0] == x) {
					player.board.set_tile(x, y, (byte)Tile.HIT);
					return true;
				}
			}
			player.board.set_tile(x, y, (byte)Tile.MISS);
			return false;
		}

	}
}
