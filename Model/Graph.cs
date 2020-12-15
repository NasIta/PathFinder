using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using PathFinder.View;

namespace PathFinder.Model
{
	public class Graph
	{
		private const int border = 40;
		
		private static Random rnd;
		private readonly int segmentation;
		
		public List<Point> vertexes;
		public int[,] connections;
		
		static Graph()
		{
			rnd = new Random();
		}
		
		public Graph(int width, int height)
		{
			vertexes = new List<Point>();
			
			segmentation = Math.Min((Painter.pictureSize.Width - border) / width , (Painter.pictureSize.Height - border) / height);
			
			for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) 
			{
				vertexes.Add(new Point(x, y));
			}
			
			connections = new int[vertexes.Count, vertexes.Count];
			
			foreach (var vertex in vertexes) 
			{
				var nearests = vertexes.Where(v => vertex.DistanceTo(v) < 1.3 && vertex != v).ToList();
				
				foreach (var nearest in nearests) 
				{
					var connectionId = new Point(vertexes.FindIndex(p => p == vertex), vertexes.FindIndex(p => p == nearest));
					if (connections[connectionId.Y, connectionId.X] == 0) 
					{
						connections[connectionId.X, connectionId.Y] = 1;
					}
				}
			}
			
			for (int i = 0; i < vertexes.Count; i++) 
			{
				vertexes[i] = Normilize(vertexes[i]);
			}
		}
		
		public Point Normilize(Point p)
		{
			var rndPoint = new Point(rnd.Next(0, segmentation / 5 * 3), rnd.Next(0, segmentation / 5 * 3));
			return new Point(p.X * segmentation + border + rndPoint.X, p.Y * segmentation + border + rndPoint.Y);
		}
	}
}
