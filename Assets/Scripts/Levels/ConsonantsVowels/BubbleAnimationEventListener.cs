using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.ConsonantsVowels {
	public class BubbleAnimationEventListener : MonoBehaviour {
		public ConsonantsVowelsView view;

		public void EventHandlerMethod() {
			view.SetBubble(this.GetComponent<Button>());
		}
	}
}