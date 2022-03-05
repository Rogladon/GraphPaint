using Components.CommandMemento.Command;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graphs {
	public class NodeAppearance : Appearance<Node>, IMovable {

		#region Fields
		private Node _node;
		#endregion

		#region Properties
		protected override string templatePath => "Templates/Node";
		public Node node => _node;

		public Vector2 position => node.position;
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

		#region IMovable
		public void Move(Vector2 position) {
			transform.position = position;
		}

		public ICommand[] GetCommands() {
			return new ICommand[] { new Node.CmdMove() { position = transform.position, owner = _node } };
		}
		#endregion

		#region Static Methods
		public static void Create(Node node) =>
			Creator.Create<NodeAppearance>().Initialize(node);

		
		#endregion
	}
}