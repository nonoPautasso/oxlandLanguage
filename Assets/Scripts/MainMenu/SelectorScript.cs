using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.MainMenu{
public class SelectorScript : MonoBehaviour {

	void Start(){
			gameObject.GetComponent<Animator> ().speed = 0;	
	}

	public void StopAnimation(int level){
			if (MainMenuController.instance.SelectorInTransition) {	
				if (level == AppController.instance.appModel.CurrentLevel) {
					gameObject.GetComponent<Animator> ().speed = 0;
					AppController.instance.StartLevel (level);
				}
			}
	}

	public void ContinueAnimation(){
		gameObject.GetComponent<Animator> ().speed = 1;
	}
}
}