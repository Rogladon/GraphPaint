using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graphs {
	public class GraphAppearance : Appearance<Graph> {

		#region Fields
		private Graph _graph;
		#endregion

		#region Properties
		public Graph graph => _graph;

		protected override string templatePath => "Templates/Graph";


		#endregion

		#region Private Methods
		protected override void OnInitialize(Graph t) {
			name = "graph";
			_graph = t;
			_graph.OnChange += Change;
		}
		private void Change(Graph graph) {
			
		}
		#endregion

		#region Static Methods
		public static void Create(Graph graph) =>
			Creator.Create<GraphAppearance>().Initialize(graph);
		#endregion
	}
}