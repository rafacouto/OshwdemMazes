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

        public Maze(int rows, int cols, bool closed)
        {
            Rows = rows;
            Cols = cols;
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
                    this[0, c] |= (byte)Direction.N;
                    this[br, c] |= (byte)Direction.S;
                }

                int bc = cols - 1;
                for (int r = 0; r < rows; r++)
                {
                    this[r, 0] |= (byte)Direction.W;
                    this[r, bc] |= (byte)Direction.E;
                }
            }
        }

        public byte this[int index]
        {
            get { return Cells[index]; }
            private set { Cells[index] = value; }
        }

        public byte this[int row, int col]
        {
            get { return Cells[(row * Cols) + col]; }
			private set { Cells[(row * Cols) + col] = value; }
        }

		public bool IsOpen(int row, int col, Direction wall)
        {
            int cell = this[row, col];
            int mask = (int)wall;
            return ((cell & mask) == 0);
        }

		public void SetWall(int row, int col, Direction wall)
		{
			int cell = this [row, col];
			int mask = (int)wall;
			this[row, col] = (byte)(cell | mask);
		}

		public void UnsetWall(int row, int col, Direction wall)
		{
			int cell = this [row, col];
			int mask = (int)wall;
			this[row, col] = (byte)(cell & ~mask);
		}

        public string[] StrLines(int cellSize)
        {
            string[] lines = new string[Rows * cellSize];

			int capacity = cellSize * cellSize * Rows * Cols;
            StringBuilder buffer = new StringBuilder(capacity);
			buffer.Append (' ', capacity);

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    BuildStringCell(buffer, r, c, cellSize);
                }
            }

            char[] str = buffer.ToString().ToCharArray();

            for (int r = 0; r < Rows; r++)
            {
                for (int rr = 0; rr < cellSize; rr++)
                {
                    int l = (r * cellSize) + rr;
                    lines[l] = new string(str, (l * Cols), (cellSize * Cols));
                }
            }

            return lines;
        }

        private void BuildStringCell(StringBuilder str, int r, int c, int cellSize)
        {
			int topLeft = (r * Cols * cellSize) + (c * cellSize);

            if (!IsOpen(r, c, Direction.N))
            {
                int w = ((r * Cols) + c) * cellSize;
                for (int p = 0; p < cellSize; p++)
                {
                    str[(w + p)] = '+';
                }
            }

			if (!IsOpen(r, c, Direction.S))
            {
                int w = ((r * Cols) + c) * cellSize;
                for (int p = 0; p < cellSize; p++)
                {
                    str[(w + p)] = '+';
                }
            }
        }

    }

}

