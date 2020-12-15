﻿using System;
using System.Drawing;

namespace PathFinder.Model
{
	public static class Extentions
	{
		public static double DistanceTo(this Point p1, Point p2)
		{
			return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
		}
	}
}
