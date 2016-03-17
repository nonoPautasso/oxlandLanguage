using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HintManager : MonoBehaviour {

	private Object[] armsImages;
	private Object[] toothImages;
	private Object[] feetImages;
	private Object[] eyesImages;

	private Image currentHint;


	// Use this for initialization
	void Start () {
		eyesImages = Resources.LoadAll ("Monsters/hintOjos");
		armsImages = Resources.LoadAll ("Monsters/hintManos");
		toothImages = Resources.LoadAll ("Monsters/hintTeeth");
		feetImages = Resources.LoadAll ("Monsters/hintPiernas");

		currentHint = GameObject.Find("HintPosition").GetComponent<Image>();
		currentHint.gameObject.SetActive(false);
	}
	
	public void ShowImage(MonsterCreatorController.Stage currentStage, int hint){
		currentHint.gameObject.SetActive(true);
		switch(currentStage){
			case MonsterCreatorController.Stage.Eyes:
				currentHint.sprite = (Sprite)eyesImages[hint];
				break;
			case MonsterCreatorController.Stage.Feet:
				currentHint.sprite = (Sprite)feetImages[hint];
				break;
			case MonsterCreatorController.Stage.Tooth:
				currentHint.sprite = (Sprite)toothImages[hint];
				break;
			case MonsterCreatorController.Stage.Arms:
				currentHint.sprite = (Sprite)armsImages[hint];
				break;
			default:
				break;
		}
	}

	public void HideImage(){
		currentHint.gameObject.SetActive(false);
	}
}
