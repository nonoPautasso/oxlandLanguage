using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.App;
using Assets.Scripts.Levels.Castlenumbers6;

namespace Assets.Scripts.Levels
{
public class CatView : LevelView {
	//Panels
		public GameObject hintBillsPanel;
		public GameObject candyPanel;
		public GameObject billsPanel;

	//Hints
	public List<Image> hintBills;
	public List<Image> hintBillsLevelFour;

	//Buttons
	public List<Toggle> bills;
	public List<Toggle> candy;
	public Button okBtn;
	
	
	public Image candyImage;
	public List<Sprite> candyImages;
	public Text priceText; 

	public GameObject instructions;
	public GameObject instructionsSpanish;
	public GameObject instructionsEnglish;
	public Sprite[] instructionSprites;
	
	private int currentGame;		

		public void InitGame()
		{
			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				instructions = instructionsSpanish;
				break;
			case 1:
				instructions = instructionsEnglish;
				break;
			}
			instructions.SetActive (true);


			currentGame = CatController.instance.GetCurrentGame ();
			HideHints (hintBills);
			HideHints (hintBillsLevelFour);
			switch (currentGame) {
			case 0:
				SetInstructions (0);
				Disable (candyPanel);
				break;
			case 1:
				SetInstructions (1);
				Disable (candyPanel);
				break;
			case 2:
				SetInstructions (2);
				Disable (candyPanel);
				break;
			case 3:
				SetInstructions (3);
				Disable (billsPanel);
				break;

			}

		}

		public void NextChallenge(){
			
			RefreshHintBtn ();
			priceText.text = CatController.instance.GetCandyPrice().ToString();
			candyImage.sprite = candyImages[Random.Range (0, candyImages.Count)];

			if (currentGame!= 0) {
				instructions.GetComponentInChildren<Text>().text = CatController.instance.GetAlreadyPaid ().ToString();
			}
				
			if (currentGame!= 3) {
				RefreshToggleBtns (bills);
				int[] billNums = CatController.instance.GetBillNumbers ();
				for (int i = 0; i < bills.Count; i++) {
					bills [i].GetComponentInChildren<Text> ().text = billNums [i].ToString ();
				}
			} else {
				RefreshToggleBtns (candy);
				for (int i = 0; i < candy.Count; i++) {
					candy [i].GetComponent<Image> ().sprite = candyImage.sprite;
				}
			}


		}

		public void RefreshHintBtn(){
			if (!hintBtn.enabled) {
				
				EnableHint ();
				if (currentGame != 3) {
					HideHints (hintBills);
					if (currentGame == 1 || currentGame == 2) {
						for (int i = 0; i < hintBills.Count; i++) {
							Color color = hintBills [i].color;
							color.a = 1;
							hintBills [i].color = color;
						}
					} 
				}else  {
					HideHints (hintBillsLevelFour);
				}
			}
		}

		void Disable(GameObject panel){
			panel.SetActive (false);
		}

		void Enable(GameObject panel){
			panel.SetActive (true);
		}
	
		void HideHints (List<Image> hints)
		{
			for (int i = 0; i < hints.Count; i++) {
				hints [i].enabled = false;
			}
		}

		public override void EndGame ()
		{
			
		}

		public override void ShowHint ()
		{
			DisableHint ();
			CatController.instance.ShowHint ();

			int price = int.Parse (priceText.text);
			int alreadyPaid = CatController.instance.GetAlreadyPaid ();

			switch (currentGame) {
			case 0:
			case 1:
				for (int i = 0; i < price; i++) {
					hintBills [i].enabled = true;
				}

				if (currentGame == 1) {

					for (int i = alreadyPaid; i < hintBills.Count; i++) {
						Color color = hintBills[i].color;
						color.a = 0.3f;  //  80%  
						hintBills[i].color = color;
					}
				}
			
				break;
			case 2:
				for (int i = 0; i < alreadyPaid; i++) {
					hintBills [i].enabled = true;
					if (i >= price) {
						Color color = hintBills [i].color;
						color.a = 0.3f;  //  80%  
						hintBills [i].color = color;
					}
				}
				break;
			case 3:
				hintBillsLevelFour [price - 1].enabled = true;;
				break;
					
			}

		}

		public void ChangeBillColor(GameObject toggleBtn){
			SoundManager.instance.PlayClicSound ();
			Color color;
			if(toggleBtn.GetComponent<Toggle>().isOn)
				color = new Color32(2,92,99,255);
			else
				color = new Color32(255,255,255,255);
			
			toggleBtn.GetComponent<Image>().color = color;
		}

		public void ChangeCandyColor(GameObject toggleBtn){
			SoundManager.instance.PlayClicSound ();
			Color color;
			if(toggleBtn.GetComponent<Toggle>().isOn)
				color = new Color32(44,234,230,255);
			else
				color = new Color32(255,255,255,255);

			toggleBtn.GetComponent<Image>().color = color;

		}

		public void OnOkClick(){
			DisableButtons ();

			int answer = 0;

			if (currentGame != 3) {
				for (int i = 0; i < bills.Count; i++) {
					if (bills [i].isOn)
						answer += int.Parse (bills [i].GetComponentInChildren<Text> ().text);
				}
			
			} else {
				for (int i = 0; i < candy.Count; i++) {
					if (candy [i].isOn)
						answer += 1;
				}
			}

			CatController.instance.CheckAnswer (answer);
		}

		public void ShowCorrectAnswer(){
			AnswerAnimation.instance.PlayAnimation(true);
		}

		public void ShowWrongAnswer(){
			AnswerAnimation.instance.PlayAnimation(false);
		}


		public void RefreshToggleBtns(List<Toggle> toggleBtns){
			for (int i = 0; i < toggleBtns.Count; i++) {
				toggleBtns [i].isOn = false;
			}

		}

		void SetInstructions(int level){
			instructions.SetActive (true);
			int language = SettingsController.instance.GetLanguage ();

			switch (language) {
			case 0:
				break;
			case 1:
				level += 4;
				break;
			}
			instructions.GetComponentsInChildren<Image> ()[1].sprite = instructionSprites [level];
			Debug.Log (instructions.GetComponentsInChildren<Image> ());

		}

		internal void OnWrongAnswerAnimationEnd()
		{
			EnableButtons ();
		}


		internal void OnRightAnswerAnimationEnd()
		{
			EnableButtons ();
		}

		void DisableButtons(){
			okBtn.interactable = false;
			hintBtn.interactable = false;

			if (currentGame != 3) {
				for (int i = 0; i < bills.Count; i++) {
					bills [i].interactable = false;
				}
			} else {
				for (int i = 0; i < candy.Count; i++) {
					candy [i].interactable = false;
				}			
			}
		}

		void EnableButtons(){
				okBtn.interactable = true;
				hintBtn.interactable = true;

				if (currentGame != 3) {
					for (int i = 0; i < bills.Count; i++) {
						bills [i].interactable = true;
					}
				} else {
					for (int i = 0; i < candy.Count; i++) {
						candy [i].interactable = true;
					}			
				}
		}

	}

}
