using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PathFinder.Model;
using PathFinder.View;

namespace PathFinder
{
	public partial class MainForm : Form
	{
		public static MainForm instance;
		public static Graph graph;
		public static Painter painter;
		
		public MainForm()
		{
			instance = this;
			
			InitializeComponent();
		}
		
		private void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			graph = new Graph(Convert.ToInt32(numericVertexCount.Value), Convert.ToInt32(numericConnectionsCount.Value));
			if (graphicsOutputOn.Checked) 
			{
				painter = new Painter();
				picture1.Image = painter.GetImageFromGraph(graph);
				
				if (ApfCheckBox.Checked) 
				{
					var dijkstra = new DijkstraAlgorithm(graph);
					painter.DrawPath(graph, dijkstra.GetPath());
				}
			}
			else
			{
				picture1.Image = null;
			}
		}
		
		private void Picture1Click(object sender, EventArgs e)
		{
			NewToolStripMenuItemClick(null, null);
		}
		
		private void Button1Click(object sender, EventArgs e)
		{
			var dijkstra = new DijkstraAlgorithm(graph);
			painter.DrawPath(graph, dijkstra.GetPath());
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			button1.Visible = !ApfCheckBox.Checked;
		}
	}
}
