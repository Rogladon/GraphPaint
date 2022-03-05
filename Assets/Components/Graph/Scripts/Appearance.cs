using Components.CommandMemento.Command;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graphs {
	public abstract class Appearance<T> : MonoBehaviour, ISelectable {
		#region Fields
		protected GameObject appearance;
		protected GameObject selectedAppearance;
		#endregion

		#region Properties
		protected abstract string templatePath { get; }
		protected virtual string templateSelectedPath => "Templates/Selected";
		#endregion

		#region Public Methods

		#endregion

		#region Private Methods
		public void Initialize(T t) {
			var template = Resources.Load<GameObject>(templatePath);
			if (template != null) appearance = Instantiate(template, transform);
			selectedAppearance = SelectedAppearance.Create(gameObject);
			OnInitialize(t);
		}
		protected abstract void OnInitialize(T t);
		#endregion

		#region ISelectable
		public void Choice(bool choice) {
			selectedAppearance.SetActive(choice);
		}

		public virtual ISelectable GetHighestParent() {
			return this;
		}
		#endregion
	}
}