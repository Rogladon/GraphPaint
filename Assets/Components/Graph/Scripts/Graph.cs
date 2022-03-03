using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Components.CommandMemento.Memento;

namespace Components.Graph {
	public class Graph : IMementable, IAppearanceble<Graph> {

		#region Fields
		private List<Node> nodes = new List<Node>();
		private List<Edge> edges = new List<Edge>();
		#endregion

		#region Properties
		public int count => nodes.Count;
		public event System.Action<Graph> OnChange;
		public event System.Action<Graph> OnDestroy;
		#endregion

		public Graph() {
			MementoManager.instance.Registry(this);
			GraphAppearance.Create(this);
			Change();
		}
		#region Public Methods
		public void RemoveNode(Node node) {
			nodes.Remove(node);
			node.Destroy();
			OnChange(this);
		}
		public void AddNode(Node node) {
			nodes.Add(node);
			OnChange(this);
		}
		public void AddEdge(Node fn, Node sn) {
			var edge = new Edge(fn, sn);
			fn.AddEdge(edge);
			sn.AddEdge(edge);
			OnChange(this);
		}
		public void RemoveEdge(Node fn, Node sn) {
			RemoveEdge(new Edge(fn, sn));
			OnChange(this);
		}
		public void RemoveEdge(Edge edge) {
			edge.Destroy();
			edges.Remove(edge);
			OnChange(this);
		}
		#endregion

		#region Appearanceble
		public void Change() {
			OnChange(this);
		}

		public void Destroy() {
			nodes.ForEach(p => p.Destroy());
			OnDestroy(this);
		}
		#endregion

		#region Mementable
		public IMemento CreateMemento() {
			return new Memento() { owner = this, nodes = nodes.ToList() };
		}
		private class Memento : IMemento {
			public Graph owner;
			public List<Node> nodes;
			public void Restore() {
				owner.nodes.Where(p => !nodes.Contains(p)).ForEach(p => owner.RemoveNode(p));
				nodes.Where(p => !owner.nodes.Contains(p)).ForEach(p => owner.AddNode(p));
			}
		}
		#endregion
	}
}