/***
 * 
 *   MazeGenerator - It generates mazes for OSHWDEM's robot contest.
 * 
 *   Copyright (C) 2015 Bricolabs (bricolabs.cc) & Rafa Couto (aka caligari)
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Text;

namespace Treboada.Net.Ia
{
    public class Maze
    {
		// bit mapped directions
        public enum Direction : byte
        {
            N = 1 << 0,
            E = 1 << 1,
            S = 1 << 2,
            W = 1 << 3,
        }

		// wall configuration at contructor
		public enum WallInit
		{
			None,
			Perimeter,
			Full,
		}

		// possible wall configurations
		public const byte Cell_0 = 0;
		public const byte Cell_N = 1;
		public const byte Cell_E = 2;
		public const byte Cell_S = 4;
		public const byte Cell_W = 8;
		public const byte Cell_NE = 3;
		public const byte Cell_NS = 5;
		public const byte Cell_NW = 9;
		public const byte Cell_ES = 6;
		public const byte Cell_EW = 10;
		public const byte Cell_SW = 12;
		public const byte Cell_NES = 7;
		public const byte Cell_NEW = 11;
		public const byte Cell_NSW = 13;
		public const byte Cell_ESW = 14;
		public const byte Cell_NESW = 15;

		// dimensions
        public int Rows { get; private set; }
        public int Cols { get; private set; }

		// count of cells
		public int Count { get; private set; }

		// the cells
        byte[] Cells;

		// constructor
		public Maze(int cols, int rows, WallInit init)
        {
			// basic properties
            Cols = cols;
			Rows = rows;
            Count = rows * cols;

			// array with the cells
            Cells = new byte[Count];

			if (init == WallInit.Perimeter) {

				// top and down
				int br = rows - 1;
				for (int c = 0; c < cols; c++) {
					this [c, 0] |= (byte)Direction.N;
					this [c, br] |= (byte)Direction.S;
				}

				// left and right
				int bc = cols - 1;
				for (int r = 0; r < rows; r++) {
					this [0, r] |= (byte)Direction.W;
					this [bc, r] |= (byte)Direction.E;
				}

			} else if (init == WallInit.Full) {

				for (int r = 0; r < rows; r++) {
					for (int c = 0; c < cols; c++) {

						// four sides
						this [r, c] = Cell_NESW;
					}
				}
			}
        }

        public byte this[int index]
        {
            get { return Cells[index]; }
            private set { Cells[index] = value; }
        }

        public byte this[int col, int row]
        {
            get { return Cells[(row * Cols) + col]; }
			private set { Cells[(row * Cols) + col] = value; }
        }

		public bool IsOpen(int col, int row, Direction wall)
        {
            int cell = this[col, row];
            int mask = (int)wall;
            return ((cell & mask) == 0);
        }

		public void SetWall(int col, int row, Direction wall)
		{
			SetWall (col, row, wall, true);
		}

		private void SetWall(int col, int row, Direction wall, bool first)
		{
			int cell = this [col, row];
			int mask = (int)wall;
			this[col, row] = (byte)(cell | mask);

			// neighbour walls
			if (first) {

				if (wall == Direction.N && row > 0)
					SetWall (col, row - 1, Direction.S, false);

				if (wall == Direction.W && col > 0)
					SetWall (col - 1, row, Direction.E, false);

				if (wall == Direction.S && row < Rows - 1)
					SetWall (col, row + 1, Direction.N, false);

				if (wall == Direction.E && col < Cols - 1)
					SetWall (col + 1, row, Direction.W, false);
			}
		}

		public void UnsetWall(int col, int row, Direction wall)
		{
			UnsetWall (col, row, wall, true);
		}

		private void UnsetWall(int col, int row, Direction wall, bool first)
		{
			int cell = this [col, row];
			int mask = (int)wall;
			this[col, row] = (byte)(cell & ~mask);

			// neighbour walls
			if (first) {

				if (wall == Direction.N && row > 0)
					UnsetWall (col, row - 1, Direction.S, false);

				if (wall == Direction.W && col > 0)
					UnsetWall (col - 1, row, Direction.E, false);

				if (wall == Direction.S && row < Rows - 1)
					UnsetWall (col, row + 1, Direction.N, false);

				if (wall == Direction.E && col < Cols - 1)
					UnsetWall (col + 1, row, Direction.W, false);
			}
		}

		public override string ToString ()
		{
			StringBuilder str = new StringBuilder ();

			foreach (string s in StrLines(4, 2)) {
				str.AppendLine (s);
			}

			return str.ToString ();
		}

		public string[] StrLines(int cellSizeWidth, int cellSizeHeight)
        {
			string[] lines = new string[(Rows * cellSizeHeight) + 1];

			// the big buffer of chars
			char[,] buffer = new char[(Cols * cellSizeWidth) + 1, (Rows * cellSizeHeight) + 1];
			for (int yy = buffer.GetLength(1) - 1; yy >= 0; yy--) {
				for (int xx = buffer.GetLength(0) - 1; xx >= 0; xx--) {
					buffer [xx, yy] = ' ';
				}
			}

			// render every cell
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
					BuildStringCell(buffer, c, r, cellSizeWidth, cellSizeHeight);
                }
            }

			// A and B marks
			buffer [cellSizeWidth / 2, cellSizeHeight / 2] = 'A';
			buffer [Cols * cellSizeWidth / 2, Rows * cellSizeHeight / 2] = 'B';

			// convert the big buffer to an array of lines
			int length = Cols * cellSizeWidth + 1;
            for (int r = 0; r < Rows; r++) {
				for (int rr = 0; rr <= cellSizeHeight; rr++) {

					int l = (r * cellSizeHeight) + rr;
					StringBuilder lineBuffer = new StringBuilder(length + 1);

					for (int c = 0; c < length; c++) {
						lineBuffer.Append(buffer[c, l]);
					}

					lines[l] = lineBuffer.ToString();
				}
            }

            return lines;
        }

		private void BuildStringCell(char[,] buffer, int col, int row, int cellSizeWidth, int cellSizeHeight)
        {
			int x = col * cellSizeWidth;
			int y = row * cellSizeHeight;

			// top
            if (!IsOpen(col, row, Direction.N))
            {
				buffer[x, y] = '+';
				for (int c = 1; c < cellSizeWidth; c++)
                {
                    buffer[x + c, y] = '-';
                }
				buffer[x + cellSizeWidth, y] = '+';
            }

			// bottom
			if (!IsOpen(col, row, Direction.S))
			{
				buffer[x, y + cellSizeHeight] = '+';
				for (int c = 1; c < cellSizeWidth; c++)
				{
					buffer[x + c, y + cellSizeHeight] = '-';
				}
				buffer[x + cellSizeWidth, y + cellSizeHeight] = '+';
			}

			// left
			if (!IsOpen(col, row, Direction.W))
			{
				buffer[x, y] = '+';
				for (int c = 1; c < cellSizeHeight; c++)
				{
					buffer[x, y + c] = '|';
				}
				buffer[x, y + cellSizeHeight] = '+';
			}

			// right
			if (!IsOpen(col, row, Direction.E))
			{
				buffer[x + cellSizeWidth, y] = '+';
				for (int c = 1; c < cellSizeHeight; c++)
				{
					buffer[x + cellSizeWidth, y + c] = '|';
				}
				buffer[x + cellSizeWidth, y + cellSizeHeight] = '+';
			}
		}

    }

}

