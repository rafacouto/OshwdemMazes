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

        public uint Rows { get; private set; }
        public uint Cols { get; private set; }

        byte[] Cells;

        public Maze(uint rows, uint cols, bool closed)
        {
            Rows = rows;
            Cols = cols;
            int count = (int)(rows * cols);

            Cells = new byte[count];
            while (count > 0)
            {
                Cells[--count] = 0;
            }

            if (closed)
            {
                uint br = rows - 1;
                for (uint c = 0; c < cols; c++)
                {
                    this[0, c] |= (byte)Direction.N;
                    this[br, c] |= (byte)Direction.S;
                }

                uint bc = cols - 1;
                for (uint r = 0; r < rows; r++)
                {
                    this[r, 0] |= (byte)Direction.W;
                    this[r, bc] |= (byte)Direction.E;
                }
            }
        }

        public byte this[uint index]
        {
            get { return Cells[index]; }
            private set { Cells[index] = value; }
        }

        public byte this[uint row, uint col]
        {
            get { return Cells[(row * Rows) + col]; }
            private set { Cells[(row * Rows) + col] = value; }
        }

        public bool IsOpen(uint row, uint col, Direction dir)
        {
            int cell = this[row, col];
            int mask = (int)dir;
            return ((cell & mask) == 0);
        }

        public string[] StrLines(int cellSize)
        {
            string[] lines = new string[Rows * cellSize];

            StringBuilder buffer = new StringBuilder((int)(cellSize * cellSize * Rows * Cols));
            for (uint r = 0; r < Rows; r++)
            {
                for (uint c = 0; c < Cols; c++)
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
                    lines[l] = new string(str, l * (int)Cols, (int)Cols);
                }
            }

            return lines;
        }

        private void BuildStringCell(StringBuilder str, uint r, uint c, int cellSize)
        {
            if (((int)this[r, c] & (int)Direction.N) != 0)
            {
                int w = (int)((r * Cols) + c) * cellSize;
                for (int p = 0; p < cellSize; p++)
                {
                    str[w + p] = '+';
                }
            }

            if (((int)this[r, c] & (int)Direction.S) != 0)
            {
                int w = (int)((r * Cols) + c) * cellSize;
                for (int p = 0; p < cellSize; p++)
                {
                    str[w + p] = '+';
                }
            }
        }

    }

}

