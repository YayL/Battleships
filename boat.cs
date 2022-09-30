using System;

namespace Battleships {

	enum Rotation {
		UP = 0,
		RIGHT,
		DOWN,
		LEFT
	};

	public class Boat {
		
		private byte width;
		private byte height;

		private sbyte xStart;
		private sbyte yStart;

		private byte rotation;

		public bool sunk = false;

		public Boat(byte width, byte height) {
			this.width = width;
			this.height = height;
			this.xStart = 5;
			this.yStart = 5;
			this.rotation = (byte)Rotation.UP;
		}

		public void rotate(sbyte rotation) {
			byte prev = this.rotation;
			this.rotation = (byte)((4 + this.rotation + rotation) % 4);
			if (this.is_out_of_bounds()) {
				this.rotation = prev;
			}
		}

		public void transform(sbyte x, sbyte y) {
			this.xStart = (sbyte)(this.xStart + x);
			this.yStart = (sbyte)(this.yStart + y);
			if (this.is_out_of_bounds()) { 
				this.yStart = (sbyte)(this.yStart - y);
				this.xStart = (sbyte)(this.xStart - x);
			}
		}

		public void set_x(sbyte x) {
			this.xStart = x;
		}

		public void set_y(sbyte y) {
			this.yStart = y;
		}

		public sbyte[,] get() {
			switch (this.rotation) {
				case (byte)Rotation.UP:
					return new sbyte[2,2]
					{{this.xStart, (sbyte)(this.yStart - this.height)}, 
						{(sbyte)(this.xStart + this.width), this.yStart}};
				case (byte)Rotation.RIGHT:
					return new sbyte[2,2]
					{{this.xStart, (sbyte)(this.yStart - this.width)},
						{(sbyte)(this.xStart + this.height), this.yStart}};
				case (byte)Rotation.DOWN:
					return new sbyte[2,2]
					{{(sbyte)(this.xStart - this.width + 1), (sbyte)(this.yStart - 1)}, 
						{(sbyte)(this.xStart + 1), (sbyte)(this.yStart + this.height - 1)}};
				case (byte)Rotation.LEFT:
					return new sbyte[2,2]
					{{(sbyte)(this.xStart - this.height + 1), (sbyte)(this.yStart - 1)}, 
						{(sbyte)(this.xStart + 1), (sbyte)(this.yStart + this.width - 1)}};
				default:
					Console.WriteLine("Error: Incorrect rotation value");
					System.Environment.Exit(1);
					break;
			}
			return new sbyte[2,2];
		}

		private bool is_out_of_bounds() {
			sbyte[,] coords = this.get();
			if (0 > coords[0,0])
				return true;
			else if (0 > coords[0,1])
				return true;
			else if (coords[1,0] > Program.WIDTH)
				return true;
			else if (coords[1,1] > Program.HEIGHT)
				return true;
			return false;
		}
	}
}
