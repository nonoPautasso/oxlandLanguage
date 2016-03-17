using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
	public class OrderNumbersView : LevelView
	{


		public Animator englishAnimator;
		public Animator spanishAnimator;

		public List<Button> numberBtns;
		private List<float> btnPositions;
		public List<Image> numberSlots;
	
		public List<Sprite> sprites;
		public Image border;
		public Button refreshBtn;


		void Start(){
			btnPositions = new List<float>();
			for (int i = 0; i < 10; i++) {
				btnPositions.Add (numberBtns[i].transform.localPosition.x);
			}

			refreshBtn.gameObject.SetActive(false);

			ClearSlots ();
			RandomizeButtonPositions ();

			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				spanishAnimator.gameObject.SetActive (true);
				break;
			case 1:
				englishAnimator.gameObject.SetActive (true);
				break;
			}
		}

		public override void EndGame(){
			refreshBtn.gameObject.SetActive(false);

		}

		void ClearSlots(){
			for (int i = 0; i < numberSlots.Count; i++) {
				numberSlots [i].enabled = false;
			}
		}

		void RandomizeButtonPositions(){
			List<int> usedNumbers = new List<int>();
			int randomIndex;

			for (int i = 0; i < numberBtns.Count; i++) {

				randomIndex = UnityEngine.Random.Range(0, 10);

				while (usedNumbers.Count!=0&&usedNumbers.IndexOf(randomIndex)!=-1)
				{
					randomIndex = UnityEngine.Random.Range(0, 10);
				}
				usedNumbers.Add(randomIndex);
				Vector3 vector = numberBtns [i].transform.localPosition;
				vector.x = btnPositions[randomIndex];
				numberBtns [i].transform.localPosition = vector;
			}
		}

		public void NextChallenge(){
			hintBtn.GetComponent<Button>().enabled = true;
			CustomizeAlpha (hintBtn, 1);
			EnableButtons ();
		}

		void CustomizeAlpha(Button button,float alpha){
			Color color = button.image.color;
			color.a = alpha;
			button.image.color = color;
		}

		void EnableButtons(){
			for (int i = 0; i < numberBtns.Count; i++) {
				CustomizeAlpha (numberBtns[i], 1);
				numberBtns [i].enabled = true;
			}
		}

		public void DisableButtons(){
			for (int i = 0; i < numberBtns.Count; i++) {
				numberBtns [i].enabled = false;
			}
		}

		public void ChangeMode(){
			ClearSlots ();
			RandomizeButtonPositions ();

			AnimateRules ();
		}



		public void ShowNumberInSlot(bool correctSlot,int number){
			int slotIndex = OrderNumbersController.instance.GetSlotIndex ();

			numberSlots [slotIndex].enabled = true;
			if (correctSlot) {
				if (OrderNumbersController.instance.IsAscending())
					numberSlots [slotIndex].sprite = sprites [slotIndex];
				else
					numberSlots [slotIndex].sprite = sprites [9 - slotIndex];
			} else {
				numberSlots [slotIndex].sprite = sprites [number - 1];
			}
		}



		public void RefreshSlot(){
			numberSlots [OrderNumbersController.instance.GetSlotIndex()].enabled = false;
			EnableButtons ();
			refreshBtn.gameObject.SetActive(false);
			border.enabled = false;
		}

		public void ShowBorder(){
			border.enabled = true;
			Vector3 vector = border.transform.localPosition;
			vector.x = btnPositions[OrderNumbersController.instance.GetSlotIndex()];
			border.transform.localPosition = vector;
		}

		public override void ShowHint ()
		{
			OrderNumbersController.instance.ShowHint ();
			CustomizeAlpha (hintBtn, 0.8f);
			hintBtn.GetComponent<Button>().enabled = false;;
			OrderNumbersController.instance.ShowHint ();
			List<int> hintIndexes = OrderNumbersController.instance.GetHintIndexes ();

			for (int i = 0; i < numberBtns.Count; i++) {
				if (!hintIndexes.Contains (i)) {
					CustomizeAlpha (numberBtns[i], 0.3f);
					numberBtns [i].enabled = false;
				}
			}
		}

	

		public void OnClickNumber(int number){
			SoundManager.instance.PlayClicSound ();
			OrderNumbersController.instance.CheckNumber (number);

		}

		public void ShowRightAnswer(int number){
			Debug.Log ("Yay");
			SoundManager.instance.PlayRightAnswerSound ();
			ShowNumberInSlot (true,number);
		}

		public void ShowWrongAnswer(int number){
			Debug.Log ("Nope");
			ShowNumberInSlot (false,number);
			refreshBtn.gameObject.SetActive (true);
			DisableButtons ();
			ShowBorder ();
			SoundManager.instance.PlayFailureSound ();
		}
			

		public void AnimateRules(){
			Animator animator;
			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				animator = spanishAnimator;
				break;	
			case 1:
				animator = englishAnimator;
				break;	
			default:
				animator = spanishAnimator;
				break;
			}
			animator.SetBool ("playAnimation", true);
		}
	}
}

