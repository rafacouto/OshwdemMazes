using System;

namespace Treboada.Net.Ia
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Maze maze = new Maze (13, 13, true);
			Console.Write (maze);
		}
	}
}
