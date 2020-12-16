using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using PathFinder.Model;

namespace PathFinder.View
{
	public class Painter
	{
		private static Random rnd = new Random();
		private const int vertexRadius = 5;
		
		public static Size pictureSize {get{return MainForm.instance.picture1.Size;}}
		
		Bitmap bmp;
		private Graphics graphics;
		
		
		
		public Painter()
		{
		}

		private void DrawConnection(Graph graph, int x, int y, Color color)
		{
			var brush = new SolidBrush(color);
			var pen = new Pen(brush, 2);
			
			graphics.DrawLine(pen, graph.vertexes[x], graph.vertexes[y]);
			int angle = Convert.ToInt32(Math.Atan2(graph.vertexes[x].Y - graph.vertexes[y].Y, graph.vertexes[x].X - graph.vertexes[y].X) * 180 / Math.PI);
			graphics.FillPie(brush, graph.vertexes[y].X - vertexRadius * 3, graph.vertexes[y].Y - vertexRadius * 3, vertexRadius * 6, vertexRadius * 6, angle - 15, 30);
		}
		
		public void DrawPath(Graph graph, List<int> path)
		{
			var brush = new SolidBrush(Color.Gray);
			var pen = new Pen(brush, 2);
			for (int i = 0; i < path.Count - 1; i++) 
			{
				graphics.DrawLine(pen, graph.vertexes[path[i]], graph.vertexes[path[i + 1]]);
			}
			MainForm.instance.picture1.Image = bmp;
		}
		
		public Bitmap GetImageFromGraph(Graph graph)
		{
			var brushWhite = new SolidBrush(Color.White);
			var brushBlack = new SolidBrush(Color.Black);
			var brushGreen = new SolidBrush(Color.FromArgb(128, 255, 128));
			var connectionFont = new Font(new FontFamily("Microsoft Sans Serif"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
			var pointFont = new Font(new FontFamily("Microsoft Sans Serif"), 18, FontStyle.Bold, GraphicsUnit.Pixel);
		
			bmp = new Bitmap(pictureSize.Width, pictureSize.Height);
			graphics = Graphics.FromImage(bmp);
			
			graphics.Clear(Color.FromArgb(192, 255, 192));
			
			for (int y = 0; y < graph.vertexes.Count; y++) for (int x = 0; x < graph.vertexes.Count; x++) 
			{
				if (graph.connections[x, y] != 0)
				{
					DrawConnection(graph, x, y, Color.FromArgb(220, 255, 255, 255));
					
					var center = new Point((graph.vertexes[x].X + graph.vertexes[y].X) / 2, (graph.vertexes[x].Y + graph.vertexes[y].Y) / 2);
					graphics.DrawString(graph.connections[x, y].ToString(), connectionFont, brushBlack, center);
				}
			}
			
			foreach (var vertex in graph.vertexes) 
			{
				graphics.FillEllipse(brushGreen, vertex.X - vertexRadius - 2, vertex.Y - vertexRadius - 2, vertexRadius * 2 + 4, vertexRadius * 2 + 4);
				graphics.FillEllipse(brushWhite, vertex.X - vertexRadius, vertex.Y - vertexRadius, vertexRadius * 2, vertexRadius * 2);
			}
			
			graphics.DrawString("A", pointFont, brushBlack, new Point(graph.vertexes[0].X - 25, graph.vertexes[0].Y - 25));
			graphics.DrawString("B", pointFont, brushBlack, new Point(graph.vertexes.Last().X + 8, graph.vertexes.Last().Y + 5));
			
			return bmp;
		}
	}
}
