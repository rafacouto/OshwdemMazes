using System;

namespace Treboada.Net.Ia
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Maze maze = new Maze (13, 13, true);

			// 3x3 finish
			maze.SetWall (5, 5, Maze.Direction.N);
			maze.SetWall (6, 5, Maze.Direction.N);
			maze.SetWall (7, 5, Maze.Direction.N);
			maze.SetWall (7, 5, Maze.Direction.E);
			maze.SetWall (7, 6, Maze.Direction.E);
			maze.SetWall (7, 7, Maze.Direction.E);
			maze.SetWall (7, 7, Maze.Direction.S);
			maze.SetWall (6, 7, Maze.Direction.S);
			maze.SetWall (5, 7, Maze.Direction.S);
			maze.SetWall (5, 7, Maze.Direction.W);
			maze.SetWall (5, 6, Maze.Direction.W);
			maze.SetWall (5, 5, Maze.Direction.W);

			Console.Write (maze);
		}
	}
}
