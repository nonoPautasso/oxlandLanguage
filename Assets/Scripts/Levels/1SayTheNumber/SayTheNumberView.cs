using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; 
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
	public class SayTheNumberView : LevelView
	{

		public GameObject refreshBtn;
		public GameObject nextBtn;
		public Button soundBtn;
		public Image selectedNumber;
		public Sprite[] images;
		public Button[] buttonNumbers;



		void Start(){
			refreshBtn.SetActive(false);
			nextBtn.SetActive(false);
			selectedNumber.enabled = false;
		}

		public void NextChallenge(){
			selectedNumber.enabled = false;
			nextBtn.SetActive(false);

		}

		public void OnClickNextBtn(){
			PlaySoundClic ();
			NextChallenge ();
			SayTheNumberController.instance.NextChallenge();

		}

		public void RightAnswer(int number){
			DisableHint ();
			selectedNumber.sprite = images [number-1];
			nextBtn.SetActive (true);
			PlayRightSound ();
		}

		public void WrongAnswer(){
			selectedNumber.sprite = images [10];
			refreshBtn.SetActive (true);
		}

		public override void EndGame(){
			refreshBtn.SetActive(false);
			nextBtn.SetActive(false);
			selectedNumber.enabled = false;
		}
	
		public void DisableHintNumberButtons(List<int> disabledNumbers)
		{
			for (int i = 0; i < disabledNumbers.Count; i++)
			{
				Color color = buttonNumbers[disabledNumbers[i]].image.color;
				color.a = 0.3f;
				buttonNumbers[disabledNumbers[i]].image.color = color;
				buttonNumbers[disabledNumbers[i]].enabled = false;

			}
		}

		public void EnableHintNumberButtons(List<int> disabledNumbers)
		{
			for (int i = 0; i < disabledNumbers.Count; i++)
			{
				Color color = buttonNumbers[disabledNumbers[i]].image.color;
				color.a = 1f;
				buttonNumbers[disabledNumbers[i]].image.color = color;
				buttonNumbers[disabledNumbers[i]].enabled = true;
			}
		}

		public void EnableNumberButtons()
		{
			soundBtn.enabled = true;
			for (int i = 0; i < buttonNumbers.Length; i++)
			{
				buttonNumbers[i].enabled = true;

			}
		}

		public void DisableNumberButtons()
		{
			soundBtn.enabled = false;

			for (int i = 0; i < buttonNumbers.Length; i++)
			{            
				buttonNumbers[i].enabled = false;

			}
		}


		public void PlaySoundAnimation(int digits)
		{	
            SpeakerScript.instance.PlaySound(digits);
			DisableSoundBtn ();
			Invoke("EnableSoundBtn", 0.7f);
		}
			
		public void EnableSoundBtn()
		{
			soundBtn.enabled = true;
		}

		public void DisableSoundBtn()
		{
			soundBtn.enabled = false;
		}


		public void Refresh()
		{
			PlaySoundClic ();
			selectedNumber.enabled = false;
			refreshBtn.SetActive(false);
			EnableNumberButtons();
			DisableHintNumberButtons(SayTheNumberController.instance.model.disabledNumbers);
			SayTheNumberController.instance.PlaySoundNumber();
		}
			

		public override void ShowHint()
		{
			if (hintBtn.enabled)
			{
				DisableHint();
				SayTheNumberController.instance.ShowHint ();
				DisableHintNumberButtons(SayTheNumberController.instance.model.disabledNumbers);
			}


		}
	
	}

}

