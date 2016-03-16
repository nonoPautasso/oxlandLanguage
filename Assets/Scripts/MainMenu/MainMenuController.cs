using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.MainMenu{

public class MainMenuController : MonoBehaviour {

	public static MainMenuController instance;
	public MainMenuView mainMenuView;
	
		private bool selectorInTransition;

	void Start()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy(gameObject);

			selectorInTransition = false;
	}

	
	

	
		public bool SelectorInTransition {
			get {
				return selectorInTransition;
			}
		}

		public void SetSelectorInTransition(bool inTransition){
			selectorInTransition = inTransition;
		}
}
}
