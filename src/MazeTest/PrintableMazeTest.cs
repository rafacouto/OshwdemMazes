using System;
using NUnit.Framework;

namespace Treboada.Net.Ia
{
	public class PrintableMazeTest
	{
		public PrintableMazeTest ()
		{
		}

		[Test()]
		public void TestStringBuilder ()
		{
			Maze maze;
			PrintableMaze pmaze;

			maze = new Maze (2, 2, Maze.WallInit.None);
			pmaze = new PrintableMaze (maze, 2, 2);
			pmaze.Update ();
			Assert.AreEqual (new string[] {
				"     ",
				"     ",
				"     ",
				"     ",
				"     ",
			}, pmaze.StrLines());

			maze = new Maze (2, 2, Maze.WallInit.Perimeter);
			pmaze = new PrintableMaze (maze, 3, 2);
			pmaze.Update ();
			Assert.AreEqual (new string[] {
				"o--o--o",
				"|     |",
				"o     o",
				"|     |",
				"o--o--o",
			}, pmaze.StrLines());

			maze = new Maze (3, 3, Maze.WallInit.Perimeter);
			pmaze = new PrintableMaze (maze, 3, 3);
			pmaze.DrawingChars = "-|+";
			pmaze.Update ();
			Assert.AreEqual (new string[] {
				"+--+--+--+",
				"|        |",
				"|        |",
				"+        +",
				"|        |",
				"|        |",
				"+        +",
				"|        |",
				"|        |",
				"+--+--+--+",
			}, pmaze.StrLines());

			maze = new Maze (4, 3, Maze.WallInit.Perimeter);
			pmaze = new PrintableMaze (maze, 4, 3);
			maze.UnsetWall (0, 0, Maze.Direction.N);
			maze.UnsetWall (0, 2, Maze.Direction.W);
			maze.SetWall (0, 0, Maze.Direction.S);
			maze.SetWall (1, 0, Maze.Direction.E);
			maze.SetWall (1, 1, Maze.Direction.E);
			maze.SetWall (1, 1, Maze.Direction.W);
			maze.SetWall (0, 2, Maze.Direction.N);
			maze.SetWall (2, 2, Maze.Direction.N);
			maze.SetWall (3, 2, Maze.Direction.N);
			pmaze.Update ();
			Assert.AreEqual (new string[] {
				"o   o---o---o---o",
				"|       |       |",
				"|       |       |",
				"o---o   o       o",
				"|   |   |       |",
				"|   |   |       |",
				"o---o   o---o---o",
				"                |",
				"                |",
				"o---o---o---o---o",
			}, pmaze.StrLines());
		}

	}
}

