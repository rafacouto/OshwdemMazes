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

namespace Treboada.Net.Ia
{
	class Oshwdem
	{
		private static ProgramOptions Options;

		public static void Main (string[] args)
		{
			// show the version
			Console.WriteLine ("\nOSHWDEM Maze Generator v{0}.{1} R{2}", Version.Major, Version.Minor, Version.Revision);

			Oshwdem.Options = new ProgramOptions();

			// exit if error in command line options or showing help
			if (Oshwdem.Options.CommandLineArgs (args)) return;

			// execute the main program
			Oshwdem oshwdem = new Oshwdem ();
			oshwdem.Run ();
		}


		private void Run()
		{
			// create a square maze with 13 cells on every side
			Maze maze = CreateMaze (16);

			// the finish door
			maze.UnsetWall (8, 8, Maze.Direction.S);

			// prepare de generator
			MazeGenerator generator = SetupGenerator (maze);

			// DepthFirst options
			DepthFirst df = generator as DepthFirst;
			if (df != null) 
			{
				df.Straightness = Options.Straightness;
				Console.WriteLine ("Algorithm: depth-first [straightforward probability {0:P0}]", Options.Straightness);
			}

			// generate from top-left corner, next to the starting cell
			generator.Generate (1, 0);

			// output to the console
			PrintableMaze pmaze = new PrintableMaze(maze);
			pmaze.Update ();
			Console.Write (pmaze);

			// wait for <enter>
			Console.ReadLine ();
		}


		private Maze CreateMaze(int side)
		{
			// square and fully walled
			Maze maze = new Maze (side, side, Maze.WallInit.Full);

			// set the starting cell
			maze.UnsetWall (0, 0, Maze.Direction.E);

			// clear the walls inside 2x2 center cells
			maze.UnsetWall (7, 7, Maze.Direction.S);
			maze.UnsetWall (7, 7, Maze.Direction.E);
			maze.UnsetWall (8, 7, Maze.Direction.S);
			maze.UnsetWall (7, 8, Maze.Direction.E);

			// start
			maze.StartCell = maze.getIndex (0, 0);

			// goal
			maze.GoalCells = new int[] {
				maze.getIndex(7, 7),
				maze.getIndex(7, 8),
				maze.getIndex(8, 7),
				maze.getIndex(8, 8),
			};

			return maze;
		}


		private MazeGenerator SetupGenerator (Maze maze)
		{
			// pretty algorithm to generate mazes
			DepthFirst generator = new DepthFirst (maze);

			// starting cell is set
			if (maze.StartCell.HasValue) {
				generator.SetVisited (maze.StartCell.Value, true);
			}

			// dont enter into the goal areas
			foreach (var g in maze.GoalCells) {
				generator.SetVisited (g, true);
			}

			return generator;
		}


		public static Version Version { 
			get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version; } 
		}
	}
}
