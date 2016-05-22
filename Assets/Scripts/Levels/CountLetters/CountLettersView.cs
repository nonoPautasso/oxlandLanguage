using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.CountLetters {
	public class CountLettersView : LevelView {
		public List<Toggle> clams;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;

		private CountLettersController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			objImage.sprite = word.Sprite ();
			Views.TogglesEnabled (clams.ToArray (), true);
			Views.TogglesOff (clams.ToArray ());
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			controller.PlayWord ();
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.enabled = sound;
		}

		public void ToggleChange(){
			foreach (Toggle clam in clams) {
				if(clam.isOn){
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		public void TryClick(){
			int count = 0;
			foreach (Toggle clam in clams) {
				if (clam.isOn) count++;
			}
			controller.Try (count);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			controller.PlayWord ();
		}

		public void Wrong () {
			PlayWrongSound ();
			tryBtn.interactable = false;
			Views.TogglesOff (clams.ToArray ());
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			Views.TogglesEnabled (clams.ToArray (), false);
			ActiveButtons (false, true, false);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public override void EndGame () { }
		public void Controller (CountLettersController controller) { this.controller = controller; }
	}
}

