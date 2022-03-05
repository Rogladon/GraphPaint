using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Components.CommandMemento.Memento;
using Components.CommandMemento.Command;

namespace Components.Graphs {
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
		private void RemoveNode(Node node) {
			nodes.Remove(node);
			node.Destroy();
			OnChange(this);
		}
		private void AddNode(Node node) {
			nodes.Add(node);
			NodeAppearance.Create(node);
			OnChange(this);
		}
		private void AddEdge(Node fn, Node sn) {
			var edge = new Edge(fn, sn);
			fn.AddEdge(edge);
			sn.AddEdge(edge);
			EdgeAppearance.Create(edge);
			OnChange(this);
		}
		private void RemoveEdge(Node fn, Node sn) {
			RemoveEdge(new Edge(fn, sn));
			OnChange(this);
		}
		private void RemoveEdge(Edge edge) {
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

		#region Commands
		public class CmdAddNode : ACommand {
			public Graph graph;
			public Vector2 position;

			protected override void OnExecute() {
				graph.AddNode(new Node(position));	
			}
		}
		public class CmdRemoveNode : ACommand {
			public Graph graph;
			public Node node;

			protected override void OnExecute() {
				graph.RemoveNode(node);
			}
		}
		#endregion
	}
}