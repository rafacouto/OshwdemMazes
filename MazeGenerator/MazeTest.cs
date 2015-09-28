using System;
using NUnit.Framework;

namespace Treboada.Net.Ia
{
    [TestFixture()]
    public class MazeTest
    {
        [Test()]
        public void TestCase()
        {
            // constructor
            Maze m = new Maze(13, 10, true);

            // rows and columns
            Assert.AreEqual(13, m.Rows);
            Assert.AreEqual(10, m.Cols);

            //
        }
    }
}

