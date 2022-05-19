using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Components.CommandMemento.Memento;
using Components.CommandMemento.Command;

namespace Components.Graphs {
	public class Graph : IMementable, IAppearanceble<Graph> {

		#region Fields
		private List<Node> _nodes = new List<Node>();
		private List<Edge> _edges = new List<Edge>();
		#endregion

		#region Properties
		public int count => _nodes.Count;
		public event System.Action<Graph> OnChange;
		public event System.Action<Graph> OnDestroy;
		public List<Node> nodes => _nodes;
		public List<Edge> edges => _edges;
		#endregion

		public Graph() {
			MementoManager.instance.Registry(this);
			GraphAppearance.Create(this);
			Change();
		}
		#region Public Methods
		internal void RemoveNode(Node node) {
			_nodes.Remove(node);
			node.Destroy();
			OnChange(this);
		}
		internal void AddNode(Node node) {
			_nodes.Add(node);
			OnChange(this);
		}
		internal void AddEdge(Edge edge) {
			edge.firstNode.AddEdge(edge);
			edge.secondNode.AddEdge(edge);
			
			OnChange(this);
		}
		internal void RemoveEdge(Edge edge) {
			edge.Destroy();
			_edges.Remove(edge);
			OnChange(this);
		}
		#endregion

		#region Appearanceble
		public void Change() {
			OnChange(this);
		}

		public void Destroy() {
			_nodes.ForEach(p => p.Destroy());
			if(OnDestroy != null)
				OnDestroy(this);
		}
		#endregion

		#region Mementable
		public IMemento CreateMemento() {
			return new Memento() { owner = this, nodes = _nodes.ToList() };
		}
		private class Memento : IMemento {
			public Graph owner;
			public List<Node> nodes;
			public void Restore() {
				owner._nodes.Where(p => !nodes.Contains(p)).ForEach(p => owner.RemoveNode(p));
				nodes.Where(p => !owner._nodes.Contains(p)).ForEach(p => owner.AddNode(p));
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
		public class CmdAddEdge : ACommand {
			public Graph graph;
			public Node firstNode;
			public Node secondNode;

			protected override void OnExecute() {
				graph.AddEdge(new Edge(firstNode, secondNode, false));
			}
		}
		#endregion
	}
}