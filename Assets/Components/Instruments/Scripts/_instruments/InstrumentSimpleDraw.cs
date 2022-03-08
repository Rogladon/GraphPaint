using Components.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.CommandMemento.Command;
using UnityEngine;

namespace Components.Instruments._instruments {
    [System.Serializable]
	public class SimpleDraw : IInstrument {
        class node {
            public Node _node;
            public int color = -1;

            public node(Node n) {
                _node = n;
			}
		}
		public void FinishExecute(Graph graph, Selected selected) {
			
		}

		public void StartExecute(Graph graph, Selected selected) {
            List<ACommand> commands = new List<ACommand>();
            List<node> dict = new List<node>();
            foreach (var i in graph.nodes) {
                dict.Add(new node(i));
                //i.SetColor(-1);
            }
            int color = 0;
            int count = graph.count;
            int id = 0;
            while (count > 0 || id > 100) {
                foreach(var i in dict) {
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
                        //i.SetColor(color);
                        count--;
                    }
                }
                id++;
                color++;
            }
            new CommandStack(commands.ToArray()).Execute();
        }

		public void UpdateExecute(Graph graph, Selected selected) {
			
		}
	}
    [CreateAssetMenu(fileName = "SimpleDraw", menuName = "Instruments/SimpleDraw")]
    public class InstrumentSimpleDraw : InstrumentHolder {
        [SerializeField] private SimpleDraw instrument;

        public override IInstrument component => instrument;
    }
}
