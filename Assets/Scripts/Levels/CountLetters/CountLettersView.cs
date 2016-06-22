using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using System.Xml;

namespace Assets.Scripts.Levels.CountLetters {
	public class CountLettersView : LevelView {
		public List<Toggle> clams;
		public List<Image> bubbles;
		public List<Image> littleBubbles;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;

		private List<int> playingBubbles;

		private CountLettersController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			objImage.sprite = word.Sprite ();
			HideBubbles ();
			Views.TogglesEnabled (clams.ToArray (), true);
			Views.TogglesOff (clams.ToArray ());
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			controller.PlayWord ();
		}

		private void HideBubbles () {
			foreach (Image littleBubble in littleBubbles) {
				littleBubble.gameObject.SetActive (false);
			}

			foreach (Image bubble in bubbles) {
				bubble.gameObject.SetActive (false);
			}

			playingBubbles = new List<int> ();
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.enabled = sound;
		}

		public void ToggleChange(Toggle toggle){
			if (toggle.isOn) PlayBubbles (clams.IndexOf (toggle));
			foreach (Toggle clam in clams) {
				if(clam.isOn){
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		private void PlayBubbles (int index) {
			littleBubbles [index].gameObject.SetActive (true);
			playingBubbles.Add (index);
			Invoke ("BubbleDone", 1f);
		}

		public void BubbleDone(){
			int first = playingBubbles [0];
			playingBubbles.Remove (first);
			littleBubbles [first].gameObject.SetActive (false);
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

		public void Correct(Word w){
			PlayRightSound ();
			DisableHint ();
			Views.TogglesEnabled (clams.ToArray (), false);
			ActiveButtons (false, true, false);
			SetBubbles (w.Name ());
		}

		private void SetBubbles (string word) {
			for (int i = 0; i < word.Length; i++) {
				Image bubble = bubbles [i];
				bubble.gameObject.SetActive (true);
				bubble.GetComponentInChildren <Text>().text = word[i].ToString ();
			}
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public override void EndGame () { }
		public void Controller (CountLettersController controller) { this.controller = controller; }
	}
}