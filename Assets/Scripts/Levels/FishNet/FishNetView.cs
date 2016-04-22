using System;
using Assets.Scripts.App;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Levels.FishNet {
	public class FishNetView : LevelView {
		public List<Image> letters;
		public List<Image> objects;
		public List<Button> answers;
		public List<Image> crosses;

		private List<Word> playingAudios;
		private Word currentAudio;

		private int active;

		private FishNetController controller;

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint();
		}

		public override void EndGame () { }

		public void Hint (List<int> grey) {
			foreach (int i in grey) {
				Color c = answers [i].GetComponentInChildren<Image>().color;
				c.a = 0.5f;
				answers [i].GetComponentInChildren<Image>().color = c;
			}
		}

		public void WrongAnswer (int index, string wordText) {
			PlayWrongSound ();
			crosses [index].gameObject.SetActive (true);
			answers [index].GetComponentInChildren<Text> ().text = wordText;
		}

		public void CorrectAnswer (List<Word> activeObjects, int index) {
			Views.ButtonsEnabled (answers.ToArray (), false);
			answers [index].GetComponentInChildren<Text> ().text = activeObjects [activeObjects.Count - 1].Name ();
			playingAudios = activeObjects;
			Invoke ("PlayCurrentAudios", 0.1f);
		}

		public void PlayCurrentAudios(){
			if (currentAudio == null)
				currentAudio = playingAudios [0];
			else if (playingAudios.IndexOf (currentAudio) == playingAudios.Count - 1) {
				currentAudio = null;
				controller.AudioDone ();
				return;
			} else
				currentAudio = playingAudios [playingAudios.IndexOf (currentAudio) + 1];

			currentAudio.PlayWord ();
			Invoke ("PlayCurrentAudios", currentAudio.AudioLength ());
		}

		public void SetAnswers (List<Word> words) {
			for (int i = 0; i < words.Count; i++) {
				answers[i].GetComponentInChildren<Image>().sprite = words[i].Sprite ();
			}
		}

		public void SetActiveObjects (List<Word> activeWords) {
			for (int i = 0; i < activeWords.Count; i++) {
				letters [i].GetComponentInChildren<Text> ().text = activeWords [i].StartLetter ();
				objects [i].gameObject.SetActive (true);
				objects [i].sprite = activeWords [i].Sprite ();
			}
		}

		public void AnswerClick(Button answer){
			controller.AnswerClick (answers.IndexOf (answer));
		}

		public void StartGame () {
			ResetViews ();
		}

		public void ResetViews () {
			EnableHint ();
			foreach (Image letter in letters) {
				letter.GetComponentInChildren<Text>().text = "";
			}
			foreach (Image obj in objects) {
				obj.gameObject.SetActive (false);
			}
			Views.ButtonsEnabled (answers.ToArray (), true);
			for (int i = 0; i < answers.Count; i++) {
				answers[i].GetComponentInChildren<Text>().text = "";
				Color c = answers[i].GetComponentInChildren<Image>().color;
				c.a = 1f;
				answers[i].GetComponentInChildren<Image>().color = c;
				crosses [i].gameObject.SetActive (false);
			}
		}
		
		public void Controller (FishNetController controller) {
			this.controller = controller;
		}
	}
}