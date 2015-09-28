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
			Maze maze = new Maze (10, 13, Maze.WallInit.None);

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

			maze = new Maze (2, 2, Maze.WallInit.None);
			for (int c = 0; c < maze.Count; c++) {
				Assert.AreEqual (maze[c], Maze.Cell_0);
			}

			maze = new Maze (3, 3, Maze.WallInit.Perimeter);
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

			maze = new Maze (2, 2, Maze.WallInit.None);
			Assert.AreEqual (new string[] {
				"     ",
				"     ",
				"     ",
				"     ",
				"     ",
			}, maze.StrLines(2, 2));

			maze = new Maze (2, 2, Maze.WallInit.Perimeter);
			Assert.AreEqual (new string[] {
				"+--+--+",
				"|     |",
				"+     +",
				"|     |",
				"+--+--+",
			}, maze.StrLines(3, 2));

			maze = new Maze (3, 3, Maze.WallInit.Perimeter);
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

			maze = new Maze (4, 3, Maze.WallInit.Perimeter);
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
				"+   +---+---+---+",
				"|       |       |",
				"|       |       |",
				"+---+   +       +",
				"|   |   |       |",
				"|   |   |       |",
				"+---+   +---+---+",
				"                |",
				"                |",
				"+---+---+---+---+",
			}, maze.StrLines(4, 3));
		}
	}

}

