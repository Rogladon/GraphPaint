using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Graph {
	interface IAppearanceble<T> {
		public event Action<T> OnChange;
		public event Action<T> OnDestroy;

		public void Change();
		public void Destroy();
	}
}
