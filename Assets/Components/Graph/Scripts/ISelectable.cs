using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Graphs {
	public interface ISelectable {
		public ISelectable GetHighestParent();
		public void Choice(bool choice);
	}
}
