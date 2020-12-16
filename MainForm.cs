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
					AlgorithmDijkstra();
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
			AlgorithmDijkstra();
		}
		
		private struct PathPoint : IComparable<PathPoint>
		{
			public int pathLength;
			public int? idFrom;
			public bool isLocked;
			public readonly int id;
			
			public PathPoint(int pathLength, int? idFrom, int id)
			{
				this.pathLength = pathLength;
				this.idFrom = idFrom;
				this.isLocked = false;
				this.id = id;
			}

			public int CompareTo(PathPoint other)
			{
				if (pathLength > other.pathLength) 
					return 1;
				if (pathLength < other.pathLength) 
					return -1;
				return 0;
			}
		}
		
		private void AlgorithmDijkstra()
		{
			var visited = new List<int>(){0};
			var distances = new PathPoint[graph.vertexes.Count];
			distances[0] = new PathPoint(0, null, 0){isLocked = true};
			for (int i = 1; i < graph.vertexes.Count; i++) 
			{
				distances[i] = new PathPoint(int.MaxValue, null, i);
			}
			
			int currentId = 0;
			while (currentId != graph.vertexes.Count - 1) 
			{
				var connectedIds = GetConnected(graph.connections, currentId);
				for (int i = 0; i < connectedIds.Length; i++) 
				{
					if (!visited.Contains(connectedIds[i]))
					{
						visited.Add(connectedIds[i]);
						int newPathLength = graph.connections[currentId, connectedIds[i]] + distances[currentId].pathLength;
						if(newPathLength < distances[connectedIds[i]].pathLength)
						{
							distances[connectedIds[i]].pathLength = newPathLength;
							distances[connectedIds[i]].idFrom = currentId;
						}
					}
				}
				var minUnlocked = distances.Where(p => !p.isLocked).Min();
				distances[minUnlocked.id].isLocked = true;
				currentId = minUnlocked.id;
			}
			
			var path = new List<int>();
			
			currentId = distances.Last().id;
			while (currentId != 0) 
			{
				path.Insert(0, currentId);
				
				currentId = distances[currentId].idFrom.Value;
			}
			path.Insert(0, 0);
			
			painter.DrawPath(graph, path);
			
			Text = distances.Last().pathLength.ToString();
		}
		
		private int[] GetConnected(int[,] connection, int id)
		{
			var connected = new List<int>();
			for (int i = 0; i < connection.GetLength(1); i++)
			{
				if (connection[id, i] != 0)
				{
					connected.Add(i);
				}
			}
			return connected.ToArray();
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			button1.Visible = !ApfCheckBox.Checked;
		}
	}
}
