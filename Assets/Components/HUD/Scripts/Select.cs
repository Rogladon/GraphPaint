using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


namespace Components.HUD {
	public class Select : MonoBehaviour,  ISelectHandler, IDeselectHandler {
		[SerializeField] private RectTransform content;

		private void Start() {
			GetComponentsInChildren<Button>()
				.Where(p => p.gameObject != gameObject)
				.ForEach(p =>
				p.onClick.AddListener(() => OnDeselect(null)));
		}

		public void OnDeselect(BaseEventData eventData) {
			content.gameObject.SetActive(false);
		}

		public void OnSelect(BaseEventData eventData) {
			content.gameObject.SetActive(true);
		}
	}
}