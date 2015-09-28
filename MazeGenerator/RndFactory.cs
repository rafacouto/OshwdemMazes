using System;

namespace Treboada.Net.Ia
{
	public static class RndFactory
	{
		private static Random Rnd = new Random (DateTime.Now.GetHashCode ());

		public static int Next()
		{
			return Rnd.Next ();
		}
	}
}

