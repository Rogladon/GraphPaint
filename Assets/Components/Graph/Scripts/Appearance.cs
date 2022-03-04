using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graphs {
	public abstract class Appearance<T> : MonoBehaviour {
		#region Fields
		protected GameObject appearance;
		#endregion

		#region Properties
		protected abstract string templatePath { get; }
		#endregion

		#region Public Methods

		#endregion

		#region Private Methods
		public void Initialize(T t) {
			var template = Resources.Load<GameObject>(templatePath);
			if (template != null) appearance = Instantiate(template, transform);
			OnInitialize(t);
		}
		protected abstract void OnInitialize(T t);
		#endregion
	}
}