using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.Abc {
	public class AbcView : LevelView {
		private AbcController controller;

		public Text letterText;
		public Button nextBtn;
		public List<Button> objects;
		public List<Button> soundButtons;

		private Sprite originalSound;

		public void Controller (AbcController controller){
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

		public void LetterClick(){
			controller.LetterClick ();
		}

		public void SetCurrentPage (string currentLetter, List<Tuple<Word, bool>> model) {
			ResetObjectsText ();
			nextBtn.gameObject.SetActive (false);
			EnableHint ();
			letterText.text = currentLetter;

			SoundButtonsActive(false);

			for (int i = 0; i < objects.Count; i++) {
				soundButtons[i].image.sprite = originalSound;
				Views.SetButtonSprite (objects[i], model[i].Item1.Sprite());
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
			soundButtons [index].gameObject.SetActive (true);
			if(correct){
				soundButtons [index].image.sprite = Resources.Load<Sprite>("Sprites/rightLengua");
			} else {
				soundButtons [index].image.sprite = Resources.Load<Sprite>("Sprites/wrongLengua");
			}
		}

		public void PageEnded () {
			DisableHint ();
			Views.ButtonsEnabled (objects.ToArray (), false);
			nextBtn.gameObject.SetActive (true);
		}

		public void NextClick(){
			controller.NextClick ();
		}

		void SoundButtonsActive(bool active) {
			foreach(Button b in soundButtons) Views.SetActiveButton (b, active);
		}

		public override void EndGame() { }
	}
}