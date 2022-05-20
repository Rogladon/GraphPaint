using Components.CommandMemento.Command;
using Components.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using node = Components.Algoritm.DrawGraph.Utilits.Node;

namespace Components.Algoritm.DrawGraph {
	public class DeepSearch : AAlgoritmDraw {
        private class Vertex {
            private int id;
            private Node _node;
            private List<Vertex> _neiborhood = new List<Vertex>();
            private int _color = -1;

            public int color => _color;
            public Node node => _node;

            public Vertex(Node node) {
                this._node = node;
                id = node.id;
            }
            public void SetNeiborhood(List<Vertex> vertices) {
                _neiborhood.Clear();
                vertices.Where(p =>
                    _node.neiborhood.Contains(p._node))
                    .ForEach(p => _neiborhood.Add(p));
            }
            public Vertex PopNeiborhod() {
                return _neiborhood.FirstOrDefault(p => p.color == -1);
			}
            public bool TrySetColor(int color) {
                if (_color != -1) throw new Exception("Error DeepSerch: повторное назначение цвета");
                if (_neiborhood.FirstOrDefault(p => p.color == color) != null) return false;
                _color = color;
                return true;
			}
        }

        protected override async Task<ResultDraw> OnExecute(Graph graph) {
            List<Vertex> vertices = new List<Vertex>();

            await Task.Run(() => {
                vertices = graph.nodes.Select(p => new Vertex(p)).ToList();
                vertices.ForEach(p => p.SetNeiborhood(vertices));
            });
            
            Stack<Vertex> history = new Stack<Vertex>();
            int chromaticNumber = 0;

            Vertex vertex = vertices[0];
            await Task.Run(() => {
                do {
                    chromaticNumber = Math.Max(chromaticNumber, TrySetFreeColor(vertex));

                    while (vertex.PopNeiborhod() == null) {
                        if (history.Count == 0) {
                            vertex = null;
                            break;
                        }
                        vertex = history.Pop();
                    }
                    if (vertex != null) {
                        history.Push(vertex);
                        vertex = vertex.PopNeiborhod();
                    }
                }
                while (history.Count > 0);
            });
            

            return new ResultDraw(chromaticNumber, new CommandStack(
                vertices.Select(p => new Node.CmdSetColor() { owner = p.node, color = p.color }).ToArray()));
        }

        private int TrySetFreeColor(Vertex vertex) {
            for(int i = 0; i< int.MaxValue; i++) {
                if (vertex.TrySetColor(i)) return i;
			}
            return -1;
		}
    }
}
