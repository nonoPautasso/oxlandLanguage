using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.App;


namespace Assets.Scripts.MainMenu{

public class MainMenuView : MonoBehaviour {

	
	public const int TOTAL_LEVELS = 21;
	public Button settingsBtn,metricsBtn;
	public List<Button> levelBtns;
	public Text welcomeText,vladimirText;
	public Text[] levelNumbers;

	// 0:blocked  1:blank  2:*  3:**  4:***
	public List<Sprite> levelStateSprites;
	//0: blocked 1:play
	public List<Sprite> infoPanelStatus;

	public GameObject infoPanel;
	
	public Animator selectorAnimator;
	public ScrollRect scrollRect;

	private int infoLevel;
	
	private float[] selectorPositions;

	void Start(){
			//Position stops in animation currentFrame/totalFrames
			selectorPositions = new float[] {0, 0.0943f, 0.3207f, 0.4339f, 0.4698f,
				0.5094f,0.5660f,0.6415f,0.6981f,0.7169f,
				0.7358f,0.7736f,0.8113f,0.8358f,0.8698f,
				0.8886f,0.9075f,0.9283f,0.9396f,0.9547f,
				0.9716f};
			
			SoundManager.instance.PlayMusic ();
			if (SettingsController.instance.GetLanguage () == 0) {
				welcomeText.text = "¡BIENVENIDO!";
				vladimirText.text = "SOY JACQUES BULLA";
			} else {
				welcomeText.text = "WELCOME!";
				vladimirText.text = "I'm Jacques Bulla";
			}
				
			if (SettingsController.instance.GetMode () == 0) {
				AppController.instance.SetMaxLevelPossible (TOTAL_LEVELS);
				ShowLevelNumbers (true);
			} else {
				AppController.instance.SetMaxLevelPossible (GetMaxLevel ());
				ShowLevelNumbers (false);
				ResetSelectorPosition ();
			}

		
		RefreshLevelBtnsStatus ();
		int  currentLevel = AppController.instance.GetCurrentLevel();
		SendSelectorToLevelPosition (currentLevel);
		AdaptScrollRect (currentLevel);
	}

		void SendSelectorToLevelPosition(int level){
			selectorAnimator.speed = 0;
			float pos = selectorPositions[(level-1)];
			selectorAnimator.Play("levelTwoTransition",-1, pos);

	}

		void ShowLevelNumbers (bool showLevels)
		{
			foreach (Text txt in levelNumbers) {
				txt.gameObject.SetActive (showLevels);
			}
		}

		void ResetSelectorPosition(){
			//VER QUE PASA CON ESTO
			//selectorAnimator.Play("levelTwoTransition",-1, 0f);
			//scrollRect.verticalNormalizedPosition = 1;
			//selectorAnimator.SetFloat ("Speed",-1);
		}

		void AdaptScrollRect(int level){
			float pos = 1;
			if (level > 5 && level < 9)
				pos = 0.75f;
			else if (level>8&&level < 12)
				pos = 0.5f;
			else if(level>10)
				pos = 0.25f;
			
			scrollRect.verticalNormalizedPosition = pos;
		}

	public void OnClickSettings(){
		SoundManager.instance.PlayClickSound ();
			ViewController.instance.LoadSettings();
	}

	public void OnClickMetrics(){
		SoundManager.instance.PlayClickSound ();
		ViewController.instance.LoadMetrics();
	}

	void RefreshLevelBtnsStatus(){
		int i,levels;

		int unblockedLevels = AppController.instance.GetMaxLevelPossible();
		
		for(i=0,levels=levelBtns.Count;i<levels;i++){
			if(i<unblockedLevels){
					if (SettingsController.instance.GetMode () == 1) {
						int stars = MetricsManager.instance.GetLevelStars (i);
						levelBtns [i].GetComponent<Image> ().sprite = levelStateSprites [stars + 1];
					} else {
						levelBtns [i].GetComponent<Image> ().sprite = levelStateSprites [1];
					}
			}else{
					levelBtns[i].GetComponent<Image>().sprite = levelStateSprites[0];
			}
		}
	}

		public void OnClickLevel(int level){
			SoundManager.instance.PlayClickSound ();
			ShowActivityInfo (level);

		}

		public void OnClickSelector(){
			SoundManager.instance.PlayClickSound ();
			ShowActivityInfo (AppController.instance.GetCurrentLevel ());
		}

		public void OnClickCrossBtn(){
			CloseGameInfo ();
		}

		public void OnClickPlayGame(){
			DisableButtons ();
			CloseGameInfo();
			int oldLevel = AppController.instance.GetCurrentLevel();
			AppController.instance.SetCurrentLevel (infoLevel);
			MainMenuController.instance.SetSelectorInTransition (true);
		
			int currentLevel = AppController.instance.GetCurrentLevel ();
			int levelGap;
			if (oldLevel == currentLevel) {
				AppController.instance.StartLevel ();
			}else if (oldLevel < currentLevel){
				if (currentLevel - oldLevel > 4){
					levelGap = (currentLevel - 5>0) ? currentLevel-5 : 1;
					SendSelectorToLevelPosition(levelGap);
				}
				selectorAnimator.SetFloat ("Speed",1);
			}else {
				if (oldLevel - currentLevel > 4){
					levelGap = (currentLevel + 5>0) ? currentLevel+5 : 16;
					SendSelectorToLevelPosition(levelGap);
				}	
				selectorAnimator.SetFloat ("Speed",-1);
			}
			selectorAnimator.GetComponent<Animator> ().speed = 1;
		}

		void CloseGameInfo(){
			SoundManager.instance.PlayClickSound ();
			infoPanel.SetActive (false);
		}

		public void ShowActivityInfo(int level){
			infoPanel.SetActive (true);
			infoLevel = level;
			if(LevelIsLocked(level)){
				infoPanel.GetComponentsInChildren<Button> () [1].image.sprite = infoPanelStatus[0];
				infoPanel.GetComponentsInChildren<Button> () [1].enabled = false;
			}else{
				infoPanel.GetComponentsInChildren<Button> () [1].image.sprite = infoPanelStatus[1];
				infoPanel.GetComponentsInChildren<Button> () [1].enabled = true;
			}

			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				infoPanel.GetComponentsInChildren<Text> () [0].text = AppController.instance.appModel.GameTitlesSpanish [level-1];
				infoPanel.GetComponentsInChildren<Text> () [1].text = AppController.instance.appModel.GameInfoSpanish [level-1];
				break;
			case 1:
				infoPanel.GetComponentsInChildren<Text>()[0].text =  AppController.instance.appModel.GameTitlesEnglish [level-1];
				infoPanel.GetComponentsInChildren<Text>()[1].text =  AppController.instance.appModel.GameInfoEnglish [level-1];
				break;
				
			}

		}

		bool LevelIsLocked(int level){
			return level > AppController.instance.GetMaxLevelPossible ();
		}


		void DisableButtons(){
			for (int i = 0; i < levelBtns.Count; i++) {
				levelBtns [i].enabled = false;
			}
		}

		void EnableButtons(){
			for (int i = 0; i < levelBtns.Count; i++) {
				levelBtns [i].enabled = true;
			}
		}

		int GetMaxLevel(){
			int counter = 1;
			for (int i = 0; i < levelBtns.Count; i++) {
				if (MetricsManager.instance.metricsModel.LevelHasMetrics (1, i))
					counter++;
				else
					break;
			}
			return (counter > 16) ? 16 : counter;
		}
}
}
