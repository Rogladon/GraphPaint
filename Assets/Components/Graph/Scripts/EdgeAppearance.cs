using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graphs {
	public class EdgeAppearance : Appearance<Edge> {

		#region Fields
		private Edge _edge;
		private LineRenderer line;
		#endregion

		#region Properties
		public Edge edge => _edge;
		protected override string templatePath => "Templates/Edge";
		#endregion

		#region Public Methods

		#endregion

		#region Private Methods
		protected override void OnInitialize(Edge t) {
			name = $"{t}";
			t.OnChange += Change;
			t.OnDestroy += DestroyAppearance;
			_edge = t;
			line = appearance.GetComponent<LineRenderer>();
			line.SetPositions(new Vector3[] { t.firstNode.position, t.secondNode.position });
			Mesh mesh = new Mesh();
			line.BakeMesh(mesh);
			appearance.GetComponent<MeshCollider>().sharedMesh = mesh;
		}

		private void Change(Edge edge) {
			line.SetPositions(new Vector3[] { edge.firstNode.position, edge.secondNode.position });
		}

		private void DestroyAppearance(Edge edge) {
			Destroy(gameObject);
		}
		#endregion

		public static void Create(Edge edge) =>
			Creator.Create<EdgeAppearance>().Initialize(edge);
	}
}