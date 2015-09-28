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
			Maze maze = new Maze (10, 13, false);

			// size
			Assert.AreEqual (maze.Count, 13*10);

			// rows and columns
			Assert.AreEqual (13, maze.Rows);
			Assert.AreEqual (10, maze.Cols);

			// wall setting and unsetting
			Assert.IsTrue (maze.IsOpen (0, 0, Maze.Direction.N));
			maze.SetWall (0, 0, Maze.Direction.N);
			Assert.IsFalse (maze.IsOpen (0, 0, Maze.Direction.N));
			maze.UnsetWall (0, 0, Maze.Direction.N);
			Assert.IsTrue (maze.IsOpen (0, 0, Maze.Direction.N));

			// wall of contiguous cells
			Assert.IsTrue (maze.IsOpen(5,5, Maze.Direction.E));
			Assert.IsTrue (maze.IsOpen(6,5, Maze.Direction.W));
			maze.SetWall (5,5, Maze.Direction.E);
			Assert.IsFalse (maze.IsOpen(5,5, Maze.Direction.E));
			Assert.IsFalse (maze.IsOpen(6,5, Maze.Direction.W));
			maze.UnsetWall (6,5, Maze.Direction.W);
			Assert.IsTrue (maze.IsOpen(5,5, Maze.Direction.E));
			Assert.IsTrue (maze.IsOpen(6,5, Maze.Direction.W));
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
			Maze maze;

			maze = new Maze (2, 2, false);
			Assert.AreEqual (new string[] {
				"     ",
				"     ",
				"     ",
				"     ",
				"     ",
			}, maze.StrLines(2, 2));

			maze = new Maze (2, 2, true);
			Assert.AreEqual (new string[] {
				"+--+--+",
				"|     |",
				"+     +",
				"|     |",
				"+--+--+",
			}, maze.StrLines(3, 2));

			maze = new Maze (3, 3, true);
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
			}, maze.StrLines(3, 3));

			maze = new Maze (4, 3, true);
			maze.UnsetWall (0, 0, Maze.Direction.N);
			maze.UnsetWall (0, 2, Maze.Direction.W);
			maze.SetWall (0, 0, Maze.Direction.S);
			maze.SetWall (1, 0, Maze.Direction.E);
			maze.SetWall (1, 1, Maze.Direction.E);
			maze.SetWall (1, 1, Maze.Direction.W);
			maze.SetWall (0, 2, Maze.Direction.N);
			maze.SetWall (2, 2, Maze.Direction.N);
			maze.SetWall (3, 2, Maze.Direction.N);
			Assert.AreEqual (new string[] {
				"+  +--+--+--+",
				"|     |     |",
				"|     |     |",
				"+--+  +     +",
				"|  |  |     |",
				"|  |  |     |",
				"+--+  +--+--+",
				"            |",
				"            |",
				"+--+--+--+--+",
			}, maze.StrLines(3, 3));
		}
	}

}

