using System;
using System.Text;

namespace Treboada.Net.Ia
{
    public class Maze
    {
        public enum Direction : byte
        {
            N = 1 << 0,
            E = 1 << 1,
            S = 1 << 2,
            W = 1 << 3,
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

        public int Rows { get; private set; }
        public int Cols { get; private set; }

		public int Count { get; private set; }

        byte[] Cells;

        public Maze(int cols, int rows, bool closed)
        {
            Cols = cols;
			Rows = rows;
            Count = rows * cols;

            Cells = new byte[Count];
			for (int c = 0; c < Count; c++)
			{
				Cells[c] = Cell_0;
			}

			if (closed) 
            {
                int br = rows - 1;
                for (int c = 0; c < cols; c++)
                {
                    this[c, 0] |= (byte)Direction.N;
                    this[c, br] |= (byte)Direction.S;
                }

                int bc = cols - 1;
                for (int r = 0; r < rows; r++)
                {
                    this[0, r] |= (byte)Direction.W;
                    this[bc, r] |= (byte)Direction.E;
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

			foreach (string s in StrLines(3)) {
				str.AppendLine (s);
			}

			return str.ToString ();
		}

        public string[] StrLines(int cellSize)
        {
			string[] lines = new string[(Rows * cellSize) + 1];

			char[,] buffer = new char[(Cols * cellSize) + 1, (Rows * cellSize) + 1];
			for (int yy = buffer.GetLength(1) - 1; yy >= 0; yy--) {
				for (int xx = buffer.GetLength(0) - 1; xx >= 0; xx--) {
					buffer [xx, yy] = ' ';
				}
			}



            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    BuildStringCell(buffer, c, r, cellSize);
                }
            }

			int length = Cols * cellSize + 1;
            for (int r = 0; r < Rows; r++) {
				for (int rr = 0; rr <= cellSize; rr++) {
					int l = (r * cellSize) + rr;
					StringBuilder lineBuffer = new StringBuilder(length + 1);
					for (int c = 0; c < length; c++) {
						lineBuffer.Append(buffer[c, l]);
					}
					lines[l] = lineBuffer.ToString();
				}
            }

            return lines;
        }

		private void BuildStringCell(char[,] buffer, int col, int row, int cellSize)
        {
			int x = col * cellSize;
			int y = row * cellSize;

            if (!IsOpen(col, row, Direction.N))
            {
				buffer[x, y] = '+';
                for (int c = 1; c < cellSize; c++)
                {
                    buffer[x + c, y] = '-';
                }
				buffer[x + cellSize, y] = '+';
            }

			if (!IsOpen(col, row, Direction.S))
			{
				buffer[x, y + cellSize] = '+';
				for (int c = 1; c < cellSize; c++)
				{
					buffer[x + c, y + cellSize] = '-';
				}
				buffer[x + cellSize, y + cellSize] = '+';
			}

			if (!IsOpen(col, row, Direction.W))
			{
				buffer[x, y] = '+';
				for (int c = 1; c < cellSize; c++)
				{
					buffer[x, y + c] = '|';
				}
				buffer[x, y + cellSize] = '+';
			}

			if (!IsOpen(col, row, Direction.E))
			{
				buffer[x + cellSize, y] = '+';
				for (int c = 1; c < cellSize; c++)
				{
					buffer[x + cellSize, y + c] = '|';
				}
				buffer[x + cellSize, y + cellSize] = '+';
			}
		}

    }

}

