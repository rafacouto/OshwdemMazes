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
using Mono.Options;

namespace Treboada.Net.Ia
{
	class Oshwdem
	{
		public bool ShouldShowHelp = false;

		public float Straightforward = 0.50f;

		public static void Main (string[] args)
		{
			// show the version
			Console.WriteLine ("\nOSHWDEM Maze Generator v{0}.{1} R{2}", Version.Major, Version.Minor, Version.Revision);

			Oshwdem oshwdem = new Oshwdem ();

			oshwdem.CommandLineArgs (args);
			if (oshwdem.ShouldShowHelp) 
			{
				oshwdem.ShowHelp ();
			}
			else 
			{
				oshwdem.Run ();
			}
		}

		private void ShowHelp()
		{
			Console.WriteLine ("\n-h --help");
			Console.WriteLine ("    Shows this help");
			Console.WriteLine ("\n-s --straightforward");
			Console.WriteLine ("    Generates more straightness paths; float value (0.00 - 1.00), default is 0.00\n");
		}

		public void CommandLineArgs(string[] args)
		{
			try 
			{
				var options = new OptionSet { 
					{ "s|straightforward=", "Probability to generate straightforward paths (0.0 - 1.0).", s => 	float.TryParse(s, out Straightforward) }, 
					{ "h|help", "Show this message and exit", h => ShouldShowHelp = (h != null) },
				};

				//System.Collections.Generic.List<string> extra = 
				options.Parse (args);
			} 
			catch (OptionException e) 
			{
				Console.WriteLine ("Command line arguments error: {0}", e.Message);
				Console.WriteLine ("Try `--help' for more information.");
				ShouldShowHelp = true;
			}
		}

		private void Run()
		{
			// create a square maze with 13 cells on every side
			Maze maze = CreateMaze (16);

			// the finish door
			maze.UnsetWall (8, 7, Maze.Direction.E);

			// prepare de generator
			MazeGenerator generator = SetupGenerator (maze);

			// DepthFirst options
			DepthFirst df = generator as DepthFirst;
			if (df != null) 
			{
				df.Straightforward = Straightforward;
				Console.WriteLine ("Algorithm: depth-first [straightforward probability {0:P0}]", Straightforward);
			}

			// generate from top-left corner, next to the starting cell
			generator.Generate (0, maze.Cols - 2);

			// output to the console
			Console.Write (maze);

			// wait for <enter>
			Console.ReadLine ();
		}


		private Maze CreateMaze(int side)
		{
			// square and fully walled
			Maze maze = new Maze (side, side, Maze.WallInit.Full);

			// set the starting cell
			maze.UnsetWall (0, maze.Cols - 1, Maze.Direction.N);

			// clear the walls inside 2x2 center cells
			maze.UnsetWall (7, 7, Maze.Direction.S);
			maze.UnsetWall (7, 7, Maze.Direction.E);
			maze.UnsetWall (8, 7, Maze.Direction.S);
			maze.UnsetWall (7, 8, Maze.Direction.E);

			return maze;
		}


		private MazeGenerator SetupGenerator (Maze maze)
		{
			// pretty algorithm to generate mazes
			DepthFirst generator = new DepthFirst (maze);

			// starting cell is set
			generator.SetVisited (0, maze.Cols - 1, true);

			// dont enter into the 3x3 center
			generator.SetVisited (7, 7, true);
			generator.SetVisited (8, 7, true);
			generator.SetVisited (7, 8, true);
			generator.SetVisited (8, 8, true);

			return generator;
		}


		public static Version Version { 
			get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version; } 
		}
	}
}
