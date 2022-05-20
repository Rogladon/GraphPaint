using Components.Graphs;
using Components.CommandMemento.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Algoritm.DrawGraph {
	public class Evristic : IAlgoritmDraw {
		private class Vertex : IComparable<Vertex> {
			private Node _node;
			private List<int> ignoreColors = new List<int>();
			private List<Vertex> neiborhood = new List<Vertex>();
			private int _color;

			private int rank => _node.neiborhood.Count;
			public Node node => _node;

			public Vertex(Node node) {
				this._node = node;
			}
			public void SetNeiborhood(List<Vertex> vertices) {
				neiborhood = vertices.Where(p =>
					_node.neiborhood.Contains(p._node)).ToList();
			}
			public bool IsNeiborhood(Vertex vertex) {
				return neiborhood.Contains(vertex);
			}
			public void TryRemoveNeiborhood(Vertex vertex) {
				if (!IsNeiborhood(vertex)) return;
				neiborhood.Remove(vertex);
			}
			public int CompareTo(Vertex other) {
				return ignoreColors.Count != other.ignoreColors.Count
					? -ignoreColors.Count.CompareTo(other.ignoreColors.Count)
					: -rank.CompareTo(other.rank);
			}
			public bool Contains(int color) {
				return ignoreColors.Contains(color);
			}
			public void SetColor(int color) {
				if (ignoreColors.Contains(color))
					throw new Exception("Algoritm.Evristic: назначение цвета который нельзя");
				_color = color;
				neiborhood.ForEach(p => p.ignoreColors.Add(color));
			}
		}

		private class Color : IComparable<Color> {

			private int _color;
			private List<Vertex> vertices = new List<Vertex>();

			public int color => _color;
			public Color(int color) {
				this._color = color;
			}

			public int CompareTo(Color other) {
				return vertices.Count.CompareTo(other.vertices.Count);
			}
			public void Add(Vertex v) {
				vertices.Add(v);
			}
		}
		public async Task<ResultDraw> Execute(Graph graph) {
			List<Vertex> vertices = graph.nodes.Select(p => new Vertex(p)).ToList();
			vertices.ForEach(p => p.SetNeiborhood(vertices));
			List<Color> colors = new List<Color>();
			List<ICommand> commands = new List<ICommand>();
			int lastColor = 0;

			await Task.Run(() => {
				while (vertices.Count > 0) {
					vertices.Sort();
					colors.Sort();

					var v = vertices[0];
					var c = colors.FirstOrDefault(p => !v.Contains(p.color));
					if (c == null) {
						c = new Color(lastColor++);
						colors.Add(c);
					}
					v.SetColor(c.color);
					vertices.RemoveAt(0);
					vertices.ForEach(p => p.TryRemoveNeiborhood(v));

					commands.Add(new Node.CmdSetColor() { owner = v.node, color = c.color });
				}
			});

			return new ResultDraw(lastColor, new CommandStack(commands.ToArray()));
		}
	}
}
