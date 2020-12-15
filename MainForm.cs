using System;
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
		
		public MainForm()
		{
			instance = this;
			
			InitializeComponent();
		}
		
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			var graph = new Graph(Convert.ToInt32(numericVertexCount.Value), Convert.ToInt32(numericConnectionsCount.Value));
			var painter = new Painter();
			picture1.Image = painter.GetImageFromGraph(graph);
		}
		
		void Picture1Click(object sender, EventArgs e)
		{
			NewToolStripMenuItemClick(null, null);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			
		}
	}
}
