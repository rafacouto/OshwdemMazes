using System;

namespace Treboada.Net.Ia
{
	/// <summary>
	/// Depth first algorithm <see cref="https://en.wikipedia.org/wiki/Maze_generation_algorithm#Depth-first_search"/>
	/// </summary>
	public class DepthFirst
	{
		private Maze Maze; 

		private bool[,] Visited;

		public DepthFirst (Maze maze)
		{
			this.Maze = maze;
			this.Visited = new bool[maze.Cols, maze.Rows];
		}

		bool IsVisited(int col, int row)
		{
			return Visited[col, row];
		}

		void SetVisited(int col, int row, bool visited)
		{
			Visited[col, row] = visited;
		}

		void Generate(int col, int row)
		{
			// ToDo
		}
	}
}

