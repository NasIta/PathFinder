using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using PathFinder.Model;

namespace PathFinder.View
{
	public class Painter
	{
		private const int vertexRadius = 5;
		
		public static Size pictureSize {get{return MainForm.instance.picture1.Size;}}
		
		public Painter()
		{
		}
		
		public Bitmap GetImageFromGraph(Graph graph)
		{
			var bmp = new Bitmap(pictureSize.Width, pictureSize.Height);
			var graphics = Graphics.FromImage(bmp);
			
			graphics.Clear(Color.FromArgb(192, 255, 192));
			
			var brushWhite = new SolidBrush(Color.White);
			var brushBlack = new SolidBrush(Color.Black);
			var brushGreen = new SolidBrush(Color.FromArgb(128, 255, 128));
			var brushGrey = new SolidBrush(Color.FromArgb(220, 255, 255, 255));
			var penGrey = new Pen(brushGrey, 2);
			Font connectionFont = new Font(new FontFamily("Microsoft Sans Serif"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
			Font pointFont = new Font(new FontFamily("Microsoft Sans Serif"), 18, FontStyle.Bold, GraphicsUnit.Pixel);
			
			for (int y = 0; y < graph.vertexes.Count; y++) for (int x = 0; x < graph.vertexes.Count; x++) 
			{
				if (graph.connections[y, x] != 0)
				{
					graphics.DrawLine(penGrey, graph.vertexes[x], graph.vertexes[y]);
					int angle = Convert.ToInt32(Math.Atan2(graph.vertexes[x].Y - graph.vertexes[y].Y, graph.vertexes[x].X - graph.vertexes[y].X) * 180 / Math.PI);
					graphics.FillPie(brushGrey, graph.vertexes[x].X - vertexRadius * 3, graph.vertexes[x].Y - vertexRadius * 3, vertexRadius * 6, vertexRadius * 6, angle - 15 + 180, 30);
					
					var center = new Point((graph.vertexes[x].X + graph.vertexes[y].X) / 2, (graph.vertexes[x].Y + graph.vertexes[y].Y) / 2);
					graphics.DrawString((graph.vertexes[x].DistanceTo(graph.vertexes[y]) / 10).ToString("0"), connectionFont, brushBlack, center);
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
