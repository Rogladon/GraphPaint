using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.CommandMemento.Memento;
using Components.CommandMemento.Command;

namespace Components.Graphs {
	public class Edge : IMementable, IAppearanceble<Edge> {
		#region Fields
		private Node _firstNode;
		private Node _secondNode;
		private bool _isOriented = false;
		#endregion

		#region Properties
		public Node firstNode => _firstNode;
		public Node secondNode => _secondNode;
		public bool isOriented => _isOriented;
		public event System.Action<Edge> OnChange;
		public event System.Action<Edge> OnDestroy;
		#endregion

		public Edge(Node fn, Node sn, bool isOriented = false) {
			MementoManager.instance.Registry(this);
			_firstNode = fn;
			_secondNode = sn;
			_isOriented = isOriented;
			EdgeAppearance.Create(this);
			Change();
		}

		#region Appearancable
		public void Change() {
			OnChange(this);
		}
		public void Destroy() {
			firstNode.RemoveEdge(this);
			secondNode.RemoveEdge(this);
			if (OnDestroy != null)
				OnDestroy(this);
		}
		#endregion

		#region Mementable
		public IMemento CreateMemento() {
			return new Memento() {
				firstNode = firstNode,
				secondNode = secondNode,
				isOriented = isOriented
			};
		}

		private class Memento : IMemento {
			public Edge owner;
			public Node firstNode;
			public Node secondNode;
			public bool isOriented;
			public void Restore() {
				owner._firstNode = firstNode;
				owner._secondNode = secondNode;
				owner._isOriented = isOriented;
				owner.OnChange(owner);
			}
		}
		#endregion

		#region override object
		public override bool Equals(object obj) {
			var e = obj as Edge;
			return (firstNode == e.firstNode && secondNode == e.secondNode);
		}
		public override int GetHashCode() {
			return base.GetHashCode();
		}
		public override string ToString() {
			return $"edge-{firstNode.id}:{secondNode.id}. isOriented: {isOriented}";
		}
		#endregion
	}
}
