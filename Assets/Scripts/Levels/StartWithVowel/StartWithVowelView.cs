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

		StartWithVowelController controller;

		public void Controller (StartWithVowelController controller){
			this.controller = controller;
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

			for (int i = 0; i < objects.Count; i++) {
				Views.PaintButton (objects[i], Color.white);
				Views.SetButtonSprite (objects[i], model[i].Item1.Sprite());
				if (model [i].Item2)
					SetWord (model[i].Item1, i, controller.IsCorrect(i));
			}
		}

		void ResetObjectsText () {
			foreach (Button o in objects) {
				o.GetComponentInChildren<Text> ().text = "";
				Views.ButtonsEnabled (objects.ToArray (), true);
			}
		}

		public void Answer (Word word, int index, bool correct) {
			SetWord (word, index, correct);
			if (correct)
				PlayRightSound ();
			else
				PlayWrongSound ();
		}

		void SetWord (Word word, int index, bool correct) {
			objects [index].GetComponentInChildren<Text> ().text = word.Name ();
			objects [index].enabled = false;
			if(correct){
				Views.PaintButton (objects [index], Color.green);
			} else {
				Views.PaintButton (objects [index], Color.red);
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
			foreach(Button b in soundButtons) Views.SetActiveButton (b, active);
		}

		public override void EndGame() { }
	}
}