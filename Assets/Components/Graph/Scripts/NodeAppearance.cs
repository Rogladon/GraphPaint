using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graph {
	public class NodeAppearance : Appearance<Node> {

		#region Fields
		private Node _node;

		

		#endregion

		#region Properties
		protected override string templatePath => "Templates/Node";
		#endregion

		#region Private Methods

		protected override void OnInitialize(Node t) {
			name = $"{t}";
			_node = t;
			_node.OnChange += Change;
			_node.OnDestroy += DestroyAppearance;
		}
		private void Change(Node node) {
			transform.position = node.position;
			//TOODOO
		}
		private void DestroyAppearance(Node node) {
			Destroy(gameObject);
		}
		#endregion

		#region Static Methods
		public static void Create(Node node) =>
			Creator.Create<NodeAppearance>().Initialize(node);
		#endregion
	}
}