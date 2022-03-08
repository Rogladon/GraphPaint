using Components.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Components.CommandMemento.Command;

namespace Components.Instruments._instruments {
    [System.Serializable]
	public class DeepSerch : IInstrument {
        class node {
            public Node n;
            public int color = -1;
            public node(Node n) {
                this.n = n;
			}
		}
		public void FinishExecute(Graph graph, Selected selected) {
			throw new NotImplementedException();
		}

		public void StartExecute(Graph graph, Selected selected) {
            List<ACommand> commands = new List<ACommand>();
            List<node> _nodes = new List<node>();
            foreach (var i in graph.nodes) {
                _nodes.Add(new DeepSerch.node(i));
                //i.SetColor(-1);
            }
            node n = _nodes[0];
            Stack<node> nodes = new Stack<node>();
            Stack<node> neighbouringNodes = new Stack<node>();
            n.color = (0);
            nodes.Push(n);
            n.n.neiborhood.ForEach(p => neighbouringNodes.Push(_nodes.First(p1 => p1.n == p)));
            var colors = Enumerable.Range(0, 6);

            while (neighbouringNodes.Count != 0) {
                n = neighbouringNodes.Pop();
                if (nodes.Contains(n)) continue;
                nodes.Push(n);
                n.color = colors.First(p => !n.n.neiborhood.Select(c => _nodes.First(cc => c == cc.n).color).Contains(p));
                //n.SetColor(colors.First(p => !n.neighbouringNodes.Select(c => graph[c].color).Contains(p)));
                neighbouringNodes.Clear();
                n.n.neiborhood.ForEach(p => neighbouringNodes.Push(_nodes.First(p1 => p1.n == p)));
                //n.neighbouringNodes.ForEach(p => neighbouringNodes.Push(graph[p]));
            }
            new CommandStack(_nodes.Select(p => new Node.CmdSetColor() { owner = p.n, color = p.color }).ToArray()).Execute();

        }

		public void UpdateExecute(Graph graph, Selected selected) {
			throw new NotImplementedException();
		}
	}

    [CreateAssetMenu(fileName = "DeepSearch", menuName = "Instruments/DeepSearch")]
    public class InstrumentDeepSearch : InstrumentHolder {
        [SerializeField] private DeepSerch instrument;

        public override IInstrument component => instrument;
    }
}
