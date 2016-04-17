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
		public List<Button> objects;
		public List<Button> soundButtons;

		public void Controller (AbcController controller){
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

		public void LetterClick(){
			controller.LetterClick ();
		}

		public void SetCurrentPage (string currentLetter, List<Tuple<Word, bool>> model) {
			ResetObjectsText ();
			EnableHint ();
			letterText.text = currentLetter;

			SoundButtonsActive(false);

			for (int i = 0; i < objects.Count; i++) {
				Views.PaintButton (objects[i], Color.white);
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
			if(correct){
				Views.PaintButton (objects [index], Color.green);
			} else {
				Views.PaintButton (objects [index], Color.red);
			}
		}

		void SoundButtonsActive(bool active) {
			foreach(Button b in soundButtons) Views.SetActiveButton (b, active);
		}

		public override void EndGame() { }
	}
}