using Components.CommandMemento.Command;
using Components.Graphs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using node = Components.Algoritm.DrawGraph.Utilits.Node;

namespace Components.Algoritm.DrawGraph {
	public class SimpleDraw : AAlgoritmDraw {
		protected override async Task<ResultDraw> OnExecute(Graph graph) {
            List<ACommand> commands = new List<ACommand>();
            List<node> dict = new List<node>();
            foreach (var i in graph.nodes) {
                dict.Add(new node(i));
            }
            int color = 0;
            int count = graph.count;
            int countToFailed = int.MaxValue;
            
            while (count > 0) {
                if (countToFailed < 0) return ResultDraw.Failed;
                await Task.Run(() => {
                    foreach (var i in dict) {
                        if (i.color != -1) continue;
                        bool draw = true;
                        foreach (var j in i._node.neiborhood) {
                            if (color == dict.First(p => p._node == j).color) {
                                draw = false;
                                break;
                            }
                        }
                        if (draw) {
                            i.color = color;
                            commands.Add(new Node.CmdSetColor() { owner = i._node, color = color });
                            count--;
                        }
                    }
                });
                countToFailed--;
                color++;
            }
            return new ResultDraw(color, new CommandStack(commands.ToArray()));
        }
	}
}