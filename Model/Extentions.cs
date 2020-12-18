using System;
using System.Drawing;

namespace PathFinder.Model
{
	public static class Extentions
	{
		private static readonly Random rnd;
		
		static Extentions()
		{
			rnd = new Random();
		}
		
		public static double DistanceTo(this Point p1, Point p2)
		{
			return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
		}
		
		public static Point RandomizeBias(this Point p, int segmentation, int border, double modifier)
		{
			var rndPoint = new Point(rnd.Next(0, Convert.ToInt32(Math.Round(segmentation * modifier))), rnd.Next(0, Convert.ToInt32(Math.Round(segmentation * modifier))));
			return new Point(p.X * segmentation + border + rndPoint.X, p.Y * segmentation + border + rndPoint.Y);
		}
	}
}
