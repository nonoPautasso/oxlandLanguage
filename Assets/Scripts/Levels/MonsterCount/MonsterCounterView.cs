using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.App;

public class MonsterCounterView : LevelView {

	private Image currentMonsterImage;
	private GameObject tick;

	private GameObject goodResultImages;
	private GameObject badResultImages;



	// Use this for initialization
	public void InitView () {
		currentMonsterImage = GameObject.Find("CurrentMonster").GetComponent<Image>();
		tick = GameObject.Find("Tick");

		goodResultImages = GameObject.Find("GoodResult");
		badResultImages = GameObject.Find("BadResult");

		badResultImages.SetActive(false);
		goodResultImages.SetActive(false);
	}
	


	public void ShowImageMonster(Sprite sprite){
		currentMonsterImage.sprite = sprite;
	}

	public void EnableTick(bool enabled){
		tick.GetComponent<Button>().enabled = enabled;
	}
		

	public void ShowGoodResultStatus(bool status){
		goodResultImages.SetActive(status);
	}

	public void ShowBadResultStatus(bool status){
		badResultImages.SetActive(status);

	}


	#region implemented abstract members of LevelView

	public override void ShowHint ()
	{
		throw new System.NotImplementedException ();
	}

	public override void EndGame ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
