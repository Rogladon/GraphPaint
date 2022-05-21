using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


namespace Components.HUD {
	public class Select : MonoBehaviour,  ISelectHandler, IDeselectHandler {
		[SerializeField] private RectTransform content;

		private Button btn;
		private void Start() {
			btn = GetComponent<Button>();

			content.gameObject.SetActive(true);
			GetComponentsInChildren<Button>()
				.Where(p => p.gameObject != gameObject)
				.ForEach(p => 
					p.onClick.AddListener(() => OnDeselect(null)));
			OnDeselect(null);
		}

		public void OnDeselect(BaseEventData eventData) {
			content.gameObject.SetActive(false);
			btn?.OnDeselect(eventData);
		}

		public void OnSelect(BaseEventData eventData) {
			content.gameObject.SetActive(true);
		}
	}
}