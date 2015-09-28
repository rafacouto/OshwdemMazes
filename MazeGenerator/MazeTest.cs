using System;
using NUnit.Framework;

namespace Treboada.Net.Ia
{
	[TestFixture()]
	public class MazeTest
	{
		[Test()]
		public void TestProperties ()
		{
			// constructor
			Maze maze = new Maze (13, 10, false);

			// size
			Assert.AreEqual (maze.Count, 13*10);

			// rows and columns
			Assert.AreEqual (13, maze.Rows);
			Assert.AreEqual (10, maze.Cols);

			// walls
			Assert.IsTrue (maze.IsOpen (0, 0, Maze.Direction.N));
			maze.SetWall (0, 0, Maze.Direction.N);
			Assert.IsFalse (maze.IsOpen (0, 0, Maze.Direction.N));
			maze.UnsetWall (0, 0, Maze.Direction.N);
			Assert.IsTrue (maze.IsOpen (0, 0, Maze.Direction.N));
		}

		[Test()]
		public void TestWalls ()
		{
			Maze maze;

			maze = new Maze (2, 2, false);
			for (int c = 0; c < maze.Count; c++) {
				Assert.AreEqual (maze[c], Maze.Cell_0);
			}

			maze = new Maze (3, 3, true);
			byte[] expected = new byte[] { 
				Maze.Cell_NW, Maze.Cell_N, Maze.Cell_NE,
				Maze.Cell_W, Maze.Cell_0, Maze.Cell_E,
				Maze.Cell_SW, Maze.Cell_S, Maze.Cell_ES,
			};
			for (int c = 0; c < maze.Count; c++) {
				Assert.AreEqual (expected [c], maze [c]);
			}

		}

		[Test()]
		public void TestStringBuilder ()
		{
			Maze maze = new Maze (2, 2, false);

			string[] lines = {
				"      ",
				"      ",
				"      ",
				"      ",
				"      ",
				"      ",
			};
			Assert.AreEqual (lines, maze.StrLines(3));

		}
	}

}

