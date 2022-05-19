using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Components.Graphs.File {
    public class ConvertColToGraph {
		public Graph Execute(string str) {
			var arr = str.Split("\n");
			int countNode = 0;
			arr.First(p => p[0] == 'p')
				.Split(' ')
				.First(p => int.TryParse(p, out countNode));
			var edges = arr.Where(p => p[0] == 'e').ToArray();

			Graph g = new Graph();
			GetNodes(countNode).ForEach(p => g.AddNode(p));

			edges.ForEach(p => {
				var split = p.Split(' ');
				Node n1 = g.nodes[int.Parse(split[1])-1];
				Node n2 = g.nodes[int.Parse(split[2])-1];
				g.AddEdge(new Edge(n1, n2));
			});
			return g;
		}

		private List<Node> GetNodes(int allCount) {
			List<Node> nodes = new List<Node>();

			float dR = 2;
			float r = dR;
			while (allCount > 0) {
				float len = 2 * Mathf.PI * r;
				int count = Mathf.Min((int)(len / dR), allCount);
				float dist = len / count;
				float p = Random.Range(0, len);
				for (int i = 0; i < count; i++) {
					float x = Mathf.Cos((p / len) * Mathf.PI * 2) * r;
					float y = Mathf.Sin((p / len) * Mathf.PI * 2) * r;
					nodes.Add(new Node(new Vector2(x,y)));
					p += dist;
					allCount--;
				}
				r += dR;
			}
			return nodes;
		}


	}
}
