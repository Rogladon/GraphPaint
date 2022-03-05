using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Components.Graphs;

namespace Components.Instruments {
	public class Selected : IEnumerable<ISelectable> {
		private List<ISelectable> selected = new List<ISelectable>();

		public ISelectable this[int index] => selected[index];

		public IEnumerator<ISelectable> GetEnumerator() {
			return ((IEnumerable<ISelectable>)selected).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ((IEnumerable)selected).GetEnumerator();
		}
		public void Clear() {
			selected.ForEach(p => p?.Choice(false));
			selected.Clear();
		}
		public void Add(ISelectable selectable) {
			selectable.Choice(true);
			selected.Add(selectable);
		}
		public void ClearAndAdd(ISelectable selectable) {
			Clear();
			Add(selectable);
		}
		public void Remove(ISelectable selectable) {
			selectable.Choice(false);
			selected.Remove(selectable);
		}
	}
}
