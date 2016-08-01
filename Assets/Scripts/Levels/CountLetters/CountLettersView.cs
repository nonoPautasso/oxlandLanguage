using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using System.Xml;
using UnityEngine;

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
			CloseShells ();
			HideBubbles ();
			Views.TogglesEnabled (clams.ToArray (), true);
			Views.TogglesOff (clams.ToArray ());
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			controller.PlayWord ();
		}

		private void CloseShells(){
			foreach(Toggle toggle in clams){
				toggle.transform.GetChild (0).GetComponent <Image>().sprite = Resources.LoadAll<Sprite>("Sprites/shell")[0];
			}

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
			if (toggle.isOn) {
				toggle.transform.GetChild (0).GetComponent <Image>().sprite = Resources.LoadAll<Sprite>("Sprites/shell")[1];
				PlayBubbles (clams.IndexOf (toggle));
			} else {
				toggle.transform.GetChild (0).GetComponent <Image>().sprite = Resources.LoadAll<Sprite>("Sprites/shell")[0];
			}
			SetEmptyBubbles ();
			foreach (Toggle clam in clams) {
				if(clam.isOn){
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		private void SetEmptyBubbles () {
			int count = 0;
			foreach (Image bubble in bubbles) {
				bubble.gameObject.SetActive (false);
				bubble.GetComponentInChildren <Text>().text = "";
			}
			foreach (Toggle clam in clams) {
				if(clam.isOn){
					bubbles [count].gameObject.SetActive (true);
					count++;
				}
			}
		}

		private void PlayBubbles (int index) {
			littleBubbles [index].gameObject.SetActive (true);
			playingBubbles.Add (index);
			Invoke ("BubbleDone", 1f);
		}

		public void BubbleDone(){
			if (playingBubbles.Count > 0) {
				int first = playingBubbles [0];
				playingBubbles.Remove (first);
				littleBubbles [first].gameObject.SetActive (false);
			}
		}

		public void TryClick(){
			int count = 0;
			foreach (Toggle clam in clams) {
				if (clam.isOn) count++;
			}
			controller.Try (count);
		}

		public void NextClick(){
			PlaySoundClick ();
			controller.NextChallenge ();
		}

		public void SoundClick(){
			string word = controller.PlayWord ();
			SpeakerScript.instance.PlaySound(word.Length < 6 ? 1 : 2);

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
			for (int i = 0; i < bubbles.Count; i++) {
				Image bubble = bubbles [i];
				if (i < word.Length) {
					bubble.gameObject.SetActive (true);
					bubble.GetComponentInChildren <Text> ().text = word [i].ToString ();
				} else bubble.gameObject.SetActive (false);
			}
		}

		public override void ShowHint () {
			DisableHint ();
			tryBtn.interactable = false;
			controller.ShowHint ();
		}

		public void AudioDone () {
			tryBtn.interactable = true;
		}

		public override void EndGame () { }
		public void Controller (CountLettersController controller) { this.controller = controller; }
	}
}