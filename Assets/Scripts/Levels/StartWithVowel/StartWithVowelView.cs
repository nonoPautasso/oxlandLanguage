using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Levels.StartWithVowel {
	public class StartWithVowelView : LevelView {
		public Button backButton;
		public Button nextButton;
		public Image submarine;
		public List<Button> objects;
		public List<Button> soundButtons;
		public Image pageFinished;
		private Sprite originalSound;

		private int correctCount;

		StartWithVowelController controller;

		public void Controller (StartWithVowelController controller){
			this.controller = controller;
			originalSound = soundButtons [0].image.sprite;
		}
		
		public override void ShowHint() {
			SoundButtonsActive(true);
			DisableHint ();
			controller.ShowHint ();
		}

		public void NextChallenge() { }

		public void ObjectClick(int index){
			controller.ObjectClick(index);
		}

		public void SoundButtonClick(int index){
			controller.SoundButtonClick (index);
		}

		public void SubmarineClick(){
			controller.SubmarineClick ();
		}

		public void SetCurrentPage (int currentPage, List<Tuple<Word, bool>> model) {
			ResetObjectsText ();
			EnableHint ();
			Views.SetActiveButton (backButton, currentPage != 0);
			Views.SetActiveButton (nextButton, currentPage != (Words.GetVowels ().Length - 1));
			submarine.sprite = Resources.LoadAll<Sprite> ("Sprites/submarinos") [currentPage];

			SoundButtonsActive(false);

			correctCount = 0;

			for (int i = 0; i < objects.Count; i++) {
				soundButtons[i].image.sprite = originalSound;
				Views.SetButtonSprite (objects[i], model[i].Item1.Sprite());
				var isCorrect = controller.IsCorrect (i);
				if (model [i].Item2) {
					SetWord (model [i].Item1, i, isCorrect);
					if (isCorrect) correctCount++;
				}
			}
			pageFinished.gameObject.SetActive (correctCount == StartWithVowelModel.CORRECT);
		}

		void ResetObjectsText () {
			foreach (Button o in objects) {
				o.GetComponentInChildren<Text> ().text = "";
				Views.ButtonsEnabled (objects.ToArray (), true);
			}
		}

		public void Answer (Word word, int index, bool correct) {
			SetWord (word, index, correct);
			if (correct) {
				PlayRightSound ();
				correctCount++;
			}
			else
				PlayWrongSound ();

			pageFinished.gameObject.SetActive (correctCount == StartWithVowelModel.CORRECT);
		}

		void SetWord (Word word, int index, bool correct) {
			objects [index].GetComponentInChildren<Text> ().text = word.Name ();
			objects [index].enabled = false;
			soundButtons [index].gameObject.SetActive (true);
			if(correct){
				soundButtons [index].image.sprite = Resources.Load<Sprite>("Sprites/rightLengua");
			} else {
				soundButtons [index].image.sprite = Resources.Load<Sprite>("Sprites/wrongLengua");
			}
		}

		public void BackButton(){
			PlaySoundClic ();
			controller.BackButton ();
		}

		public void NextButton(){
			PlaySoundClic ();
			controller.NextButton ();
		}

		void SoundButtonsActive(bool active) {
			for (int i = 0; i < soundButtons.Count; i++) {
				Views.SetActiveButton (soundButtons[i], active);
			}
		}

		public override void EndGame() { }
	}
}