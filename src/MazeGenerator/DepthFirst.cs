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
using System.Collections.Generic;

namespace Treboada.Net.Ia
{
	/// <summary>
	/// Depth first algorithm <see cref="https://en.wikipedia.org/wiki/Maze_generation_algorithm#Depth-first_search"/>
	/// </summary>
	public class DepthFirst : MazeGenerator
	{
		
		private Maze Maze; 

		private bool[,] Visited;

		public float Straightness;


		public DepthFirst (Maze maze)
		{
			this.Maze = maze;
			this.Visited = new bool[maze.Cols, maze.Rows];

			// no persistent direction by default
			this.Straightness = 0.0f; 
		}


		public bool IsVisited(int col, int row)
		{
			return Visited[col, row];
		}


		public void SetVisited(int col, int row, bool visited)
		{
			Visited[col, row] = visited;
		}


		public void SetVisited(int index, bool visited)
		{
			int col = Maze.getCol (index);
			int row = Maze.getRow (index);

			Visited[col, row] = visited;
		}


		public void Generate(int col, int row)
		{
			// limits
			if (this.Straightness < 0.0f) this.Straightness = 0.0f;
			if (this.Straightness > 1.0f) this.Straightness = 1.0f;

			// explore recursively
			VisitRec (col, row, Maze.Direction.E);
		}


		private static Maze.Direction[] FourRoses = new Maze.Direction[] {
			Maze.Direction.N,
			Maze.Direction.E,
			Maze.Direction.S,
			Maze.Direction.W,
		};


		private void VisitRec(int col, int row, Maze.Direction previous)
		{
			// mark visited
			Visited [col, row] = true;

			// list of possible destinations
			List<Maze.Direction> pending = new List<Maze.Direction> (FourRoses);
			bool previous_elegible = true;

			// while possible destinations
			while (pending.Count > 0) {

				Maze.Direction direction;

				// try to persist straightforward under Straightness probability
				if (previous_elegible && RndFactory.Next () < Straightness * Int32.MaxValue) {
					// sack
					direction = previous;
					pending.Remove (direction);
					previous_elegible = false;
				} else {
					// extract one from the list
					int index = RndFactory.Next () % pending.Count;
					direction = pending [index];
					pending.RemoveAt (index);
				}

				// relative to this cell
				int c = col, r = row;

				// calculate the next one
				switch (direction) {

				case Maze.Direction.N:
					r--;
					break;

				case Maze.Direction.E: 
					c++;
					break;

				case Maze.Direction.S: 
					r++;
					break;

				case Maze.Direction.W: 
					c--;
					break;
				}

				// if destination is valid
				if (InBounds (c, r) && !Visited [c, r]) {

					// open the wall and explore recursively
					Maze.UnsetWall (col, row, direction);
					VisitRec (c, r, direction);
				}
			}

		}


		private bool InBounds(int col, int row)
		{
			return (col >= 0 && col < Maze.Cols && row >= 0 && row < Maze.Rows);
		}
	}
}

