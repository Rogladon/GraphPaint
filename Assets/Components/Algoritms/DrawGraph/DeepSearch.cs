using Components.CommandMemento.Command;
using Components.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using node = Components.Algoritm.DrawGraph.Utilits.Node;

namespace Components.Algoritm.DrawGraph {
	public class DeepSearch : IAlgoritmDraw {
		public ResultDraw Execute(Graph graph) {
            List<ACommand> commands = new List<ACommand>();
            List<node> _nodes = new List<node>();
            foreach (var i in graph.nodes) {
                _nodes.Add(new node(i));
                //i.SetColor(-1);
            }
            node n = _nodes[0];
            Stack<node> nodes = new Stack<node>();
            Stack<node> neighbouringNodes = new Stack<node>();
            n.color = (0);
            nodes.Push(n);
            n._node.neiborhood.ForEach(p => neighbouringNodes.Push(_nodes.First(p1 => p1._node == p)));
            var colors = Enumerable.Range(0, _nodes.Count);
            int chromaticNumber = 0;

            while (neighbouringNodes.Count != 0) {
                n = neighbouringNodes.Pop();
                if (nodes.Contains(n)) continue;
                nodes.Push(n);
                n.color = colors.First(p => !n._node.neiborhood.Select(c => _nodes.First(cc => c == cc._node).color).Contains(p));
                chromaticNumber = n.color > chromaticNumber ? n.color : chromaticNumber;
                //n.SetColor(colors.First(p => !n.neighbouringNodes.Select(c => graph[c].color).Contains(p)));
                neighbouringNodes.Clear();
                n._node.neiborhood.ForEach(p => neighbouringNodes.Push(_nodes.First(p1 => p1._node == p)));
                //n.neighbouringNodes.ForEach(p => neighbouringNodes.Push(graph[p]));
            }

            return new ResultDraw(chromaticNumber + 1, new CommandStack(_nodes.Select(p => new Node.CmdSetColor() { owner = p._node, color = p.color }).ToArray()));
        }
    }
}
