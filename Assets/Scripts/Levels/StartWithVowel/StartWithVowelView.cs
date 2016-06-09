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

		public void WordPlayed (float duration) {
			SoundButtonsEnabled (false);
			Invoke ("SoundFinished", duration);
		}

		public void SoundFinished(){
			SoundButtonsEnabled (true);
		}

		private void SoundButtonsEnabled (bool enabled) {
			foreach (Button btn in soundButtons) {
				btn.enabled = enabled;
			}
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

			var allCorrect = AllCorrect (model);

			pageFinished.gameObject.SetActive (allCorrect);
			if (allCorrect) SetObjectsAsDone (model);

			for (int i = 0; i < objects.Count; i++) {
				soundButtons[i].image.sprite = originalSound;
				Views.SetActiveButton (objects [i], true);
				Views.SetButtonSprite (objects[i], model[i].Item1.Sprite());
				var isCorrect = controller.IsCorrect (i);
				if (allCorrect && !isCorrect) Views.SetActiveButton (objects [i], false);
				else if (model [i].Item2) {
					SetWord (model [i].Item1, i, isCorrect);
				}
			}
		}

		private void SetObjectsAsDone (List<Tuple<Word, bool>> model) {
			for (int i = 0; i < objects.Count; i++) {
				var isCorrect = controller.IsCorrect (i);
				if (!isCorrect) Views.SetActiveButton (objects [i], false);
			}
		}

		private bool AllCorrect (List<Tuple<Word, bool>> model) {
			correctCount = 0;
			for (int i = 0; i < objects.Count; i++) {
				if (model [i].Item2) {
					var isCorrect = controller.IsCorrect (i);
					SetWord (model [i].Item1, i, isCorrect);
					if (isCorrect) correctCount++;
				}
			}
			return correctCount == StartWithVowelModel.CORRECT;
		}

		void ResetObjectsText () {
			foreach (Button o in objects) {
				o.GetComponentInChildren<Text> ().text = "";
				Views.ButtonsEnabled (objects.ToArray (), true);
			}
		}

		public void Answer (Word word, int index, bool correct) {
			SoundButtonsActive (false, false);
			SetWord (word, index, correct);
			if (correct) {
				PlayRightSound ();
				correctCount++;
			}
			else
				PlayWrongSound ();

			if (correctCount == StartWithVowelModel.CORRECT) {
				pageFinished.gameObject.SetActive (correctCount == StartWithVowelModel.CORRECT);
				SetObjectsAsDone (controller.GetCurrentPage());
			}
		}

		void SetWord (Word word, int index, bool correct) {
			objects [index].enabled = false;
			soundButtons [index].gameObject.SetActive (true);
			if(correct){
				objects [index].GetComponentInChildren<Text> ().text = "<color=green>" + word.Name ()[0] + "</color>" + word.Name ().Remove (0, 1);
				soundButtons [index].image.sprite = Resources.Load<Sprite>("Sprites/rightLengua");
			} else {
				objects [index].GetComponentInChildren<Text> ().text = word.Name ();
				soundButtons [index].image.sprite = Resources.Load<Sprite>("Sprites/wrongLengua");
			}
		}

		public void BackButton(){
			PlaySoundClick ();
			controller.BackButton ();
		}

		public void NextButton(){
			PlaySoundClick ();
			controller.NextButton ();
		}

		void SoundButtonsActive(bool active, bool allOfThem = true) {
			for (int i = 0; i < soundButtons.Count; i++) {
				if(!allOfThem){
					if(soundButtons[i].image.sprite != originalSound) continue;
				}
				Views.SetActiveButton (soundButtons[i], active);
			}
			if (!active) EnableHint ();
		}

		public override void EndGame() { }
	}
}