using System;
using System.Text;

namespace Treboada.Net.Ia
{
	public class PrintableMaze
	{
		public Maze Maze { get; private set; }

		public char[,] Buffer { get; private set; }

		public int CellSizeWidth { get; private set; }
		public int CellSizeHeight { get; private set; }

		/// <summary>
		/// Gets the drawing chars. Defaults are "-|o" 
		/// </summary>
		/// <value>The drawing chars.</value>
		/// <see cref="https://github.com/micromouseonline/mazefiles"/>
		public string DrawingChars { get; set; }

		public bool ShowMarks { get; set; }


		public PrintableMaze (Maze maze, int cellSizeWidth = 4, int cellSizeHeight = 2)
		{
			this.Maze = maze;
			this.CellSizeWidth = cellSizeWidth;
			this.CellSizeHeight = cellSizeHeight;
			this.DrawingChars = "-|o";
			this.ShowMarks = true;

			Buffer = new char[(maze.Cols * cellSizeWidth) + 1, (maze.Rows * cellSizeHeight) + 1];
		}


		public override string ToString ()
		{
			StringBuilder str = new StringBuilder ();

			foreach (var s in StrLines()) {
				str.AppendLine (s);
			}

			return str.ToString ();
		}


		public string[] StrLines()
		{
			string[] lines = new string[(Maze.Rows * CellSizeHeight) + 1];

			int length = Maze.Cols * CellSizeWidth + 1;
			for (int r = 0; r < Maze.Rows; r++) {
				for (int rr = 0; rr <= CellSizeHeight; rr++) {

					int l = (r * CellSizeHeight) + rr;
					StringBuilder lineBuffer = new StringBuilder(length + 1);

					for (int c = 0; c < length; c++) {
						lineBuffer.Append(Buffer[c, l]);
					}

					lines[l] = lineBuffer.ToString();
				}
			}
		
			return lines;
		}


		public void Update()
		{
			// the big buffer of chars
			for (int yy = Buffer.GetLength(1) - 1; yy >= 0; yy--) {
				for (int xx = Buffer.GetLength(0) - 1; xx >= 0; xx--) {
					Buffer [xx, yy] = ' ';
				}
			}

			// render every cell
			for (int r = 0; r < Maze.Rows; r++)
			{
				for (int c = 0; c < Maze.Cols; c++)
				{
					BuildStringCell(Buffer, c, r);
				}
			}

			// render start and goal cells
			if (ShowMarks) {
				if (Maze.StartCell.HasValue) PutMark (Maze.StartCell.Value, 'S');
				foreach (var g in Maze.GoalCells) PutMark (g, 'G');
			}
		}


		private void PutMark(int cell_index, char mark)
		{
			int col = Maze.getCol (cell_index);
			int row = Maze.getRow (cell_index);
			Buffer[(col * CellSizeWidth) + 2, (row * CellSizeHeight) + 1] = mark;
		}


		private void BuildStringCell(char[,] buffer, int col, int row)
		{
			char horiChar = DrawingChars[0];
			char vertChar = DrawingChars[1];
			char joinChar = DrawingChars[2];

			int x = col * CellSizeWidth;
			int y = row * CellSizeHeight;

			if (!Maze.IsOpen(col, row, Maze.Direction.N)) {
				buffer[x, y] = joinChar;
				for (int c = 1; c < CellSizeWidth; c++) buffer[x + c, y] = horiChar;
				buffer[x + CellSizeWidth, y] = joinChar;
			}

			if (!Maze.IsOpen(col, row, Maze.Direction.S)) {
				buffer[x, y + CellSizeHeight] = joinChar;
				for (int c = 1; c < CellSizeWidth; c++) buffer[x + c, y + CellSizeHeight] = horiChar;
				buffer[x + CellSizeWidth, y + CellSizeHeight] = joinChar;
			}

			if (!Maze.IsOpen(col, row, Maze.Direction.W)) {
				buffer[x, y] = joinChar;
				for (int c = 1; c < CellSizeHeight; c++) buffer[x, y + c] = vertChar;
				buffer[x, y + CellSizeHeight] = joinChar;
			}

			if (!Maze.IsOpen(col, row, Maze.Direction.E)) {
				buffer[x + CellSizeWidth, y] = joinChar;
				for (int c = 1; c < CellSizeHeight; c++) buffer[x + CellSizeWidth, y + c] = vertChar;
				buffer[x + CellSizeWidth, y + CellSizeHeight] = joinChar;
			}
		}


		public void PrintMarks()
		{
		}


		public void PrintPath(int[] path, char mark = '*')
		{
			for (int p = 0; p < path.Length; p++) {
				
				int cell = path [p];
				int col = Maze.getCol (cell);
				int row = Maze.getRow (cell);

				Buffer [1 + (col * CellSizeWidth), 1 + (row * CellSizeHeight)] = mark;
			}
		}

		public void PrintWeights(int[] weights)
		{
			for (int r = 0; r < Maze.Rows; r++) {
				for (int c = 0; c < Maze.Cols; c++) {

					string str = weights[Maze.getIndex(c, r)].ToString ();
					for (int s = 0; s < str.Length; s++)
						Buffer [(c * CellSizeWidth) + 2 + s, (r * CellSizeHeight) + 1] = str [s];
				}
			}
		}
	}
}

