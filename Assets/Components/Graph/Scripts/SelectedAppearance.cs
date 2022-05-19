using Components.CommandMemento.Command;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.Graphs {
	public class SelectedAppearance : MonoBehaviour {

		#region Properties
		protected string templatePath => "Templates/Selected";
		#endregion

		private GameObject Initialize(GameObject go) {
			var template = Resources.Load<GameObject>(templatePath);
			if (template != null) {
				template = Instantiate(template, transform);
				var sr = template.GetComponent<SpriteRenderer>();
				var b = go.transform.GetSummaryBoundsSprite();
				sr.size = b.size+Vector3.one*0.2f;
				template.transform.localPosition = b.center;
				transform.SetParent(go.transform);
			}
			gameObject.SetActive(false);
			return gameObject;
		}

		#region Static Methods
		public static GameObject Create(GameObject go) =>
			Creator.Create<SelectedAppearance>().Initialize(go);
		#endregion
	}
}