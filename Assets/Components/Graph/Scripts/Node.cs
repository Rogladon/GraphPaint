using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.CommandMemento.Memento;
using Components.CommandMemento.Command;

namespace Components.Graphs {
	public class Node : IMementable, IAppearanceble<Node> {
		#region Fields
		private static int LAST_ID = 0;

		private int _id;
		private List<Edge> _edges = new List<Edge>();
		private int _color = -1;
		private int _number = -1;
		private Vector2 _position;
		#endregion

		#region Properties
		public int id => _id;
		public List<Edge> edges => _edges;
		public int color => _color;
		public int number => _number;
		public Vector2 position => _position;
		public List<Node> neiborhood => edges.Select(p => p.firstNode == this ? p.secondNode : p.firstNode).ToList();
		public event System.Action<Node> OnChange;
		public event System.Action<Node> OnDestroy;
		#endregion

		public Node(Vector2 position) {
			MementoManager.instance.Registry(this);
			_id = LAST_ID++;
			_position = position;
			NodeAppearance.Create(this);
			Change();
		}
		#region Public Methods
		internal Node SetColor(int color) {
			this._color = color;
			OnChange(this);
			return this;
		}
		internal Node SetNumber(int number) {
			this._number = number;
			OnChange(this);
			return this;
		}
		internal Node SetPosition(Vector2 position) {
			this._position = position;
			edges.ForEach(p => p.Change());
			OnChange(this);
			return this;
		}
		internal Node AddEdge(Edge edge) {
			if(edge.firstNode != this && edge.secondNode != this) {
				Debug.LogError($"node: {id}:: Попытка добавить ребро не связанную с данной вершиной");
				return this;
			}
			this._edges.Add(edge);
			return this;
		}
		internal Node RemoveEdge(Edge edge) {
			_edges.Remove(edge);
			return this;
		}
		#endregion

		#region Appearanceble
		public void Change() {
			if (OnChange != null) {
				OnChange(this);
			}
		}
		public void Destroy() {
			_edges.ToArray().ForEach(p => p.Destroy());
			if(OnDestroy != null)
				OnDestroy(this);
		}
		#endregion


		public override string ToString() {
			return $"node-{id}";
		}


		#region Mementable
		public IMemento CreateMemento() {
			return new Memento() {
				owner = this,
				id = id,
				edges = edges.ToList(),
				color = color,
				number = number,
				position = position
			};
		}

		private class Memento : IMemento {
			public Node owner;
			public int id;
			public List<Edge> edges = new List<Edge>();
			public int color = 0;
			public int number = -1;
			public Vector2 position;
			public void Restore() {
				owner._id = id;
				owner._edges = edges;
				owner._color = color;
				owner._number = number;
				owner._position = position;
			}
		}
		#endregion

		#region Commands
		public class CmdMove : ACommand {
			public Node owner;
			public Vector2 position;
			protected override void OnExecute() {
				owner.SetPosition(position);
			}
		}
		public class CmdSetColor : ACommand {
			public Node owner;
			public int color;
			protected override void OnExecute() {
				owner.SetColor(color);
			}
		}
		#endregion
	}
}