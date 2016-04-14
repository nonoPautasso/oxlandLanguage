using System;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Common {
	public class Views {
		private Views() { }

		/** If using toggle group, make sure that 'Allow switch off' in toggle group is on. */
		public static void TogglesOff(Toggle[] toggles) {
			for(int i = 0; i < toggles.Length; i++) toggles[i].isOn = false;
		}

		public static void TogglesEnabled(Toggle[] toggles, bool enabled) {
			for (int i = 0; i < toggles.Length; i++) toggles[i].enabled = enabled;
		}

		public static void ButtonsEnabled(Button[] buttons, bool enabled) {
			for (int i = 0; i < buttons.Length; i++) buttons[i].enabled = enabled;
		}

		public static void SetActiveToggle(Toggle toggle, bool active){
			toggle.gameObject.SetActive(active);
		}

		public static void SetActiveButton(Button button, bool active){
			button.gameObject.SetActive(active);
		}

		public static void SetButtonSprite(Button button, Sprite sprite){
			button.GetComponentInChildren<Image>().sprite = sprite;
		}

		public static bool IsAnyActive(Toggle[] pizzas) {
			foreach(Toggle pizza in pizzas) {
				if(pizza.IsActive()) return true;
			}
			return false;
		}
	}
}