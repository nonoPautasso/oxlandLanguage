﻿using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.CreateSentences {
	public class CreateSentencesView : LevelView {
		public Image wordsPanel;
		public Image sentencePanel;
		public Button tryBtn;
		public Button soundBtn;
		public Button nextBtn;

		public List<GameObject> sentenceSentenceLines;
		public List<GameObject> wordsSentenceLines;
		private List<GameObject> words;
		private List<Transform> originalParents;

		public static int MAX_CHARS = 20;

		private CreateSentencesController controller;

		public void TryClick(){
			List<string> result = new List<string> ();

			foreach (GameObject line in sentenceSentenceLines) {
				foreach (Button word in line.GetComponentsInChildren <Button>()) {
					result.Add (word.GetComponentInChildren<Text>().text);
				}
			}

			controller.Try (result);
		}

		public void NextClick(){
			PlaySoundClick ();
			tryBtn.gameObject.SetActive (true);
			nextBtn.gameObject.SetActive (false);
			controller.NextChallenge ();
		}

		public void CorrectAudioDone () {
			nextBtn.interactable = true;
		}

		public void Correct () {
			DisableHint ();
			foreach (GameObject word in words) {
				word.GetComponentInChildren<Button> ().enabled = false;
			}

			foreach (GameObject line in sentenceSentenceLines) {
				foreach (Button word in line.GetComponentsInChildren <Button>()) {
					Views.PaintButton (word, new Color32(81,225,148,225));
				}
			}
			
			tryBtn.gameObject.SetActive (false);
			nextBtn.gameObject.SetActive (true);
			nextBtn.interactable = false;
			soundBtn.interactable = false;
		}

		public GameObject GetWord(){
			return Instantiate<GameObject>(LoadPrefab("Levels/CreateSentence/wordButton"));
		}

		public GameObject GetLine(){
			return Instantiate<GameObject>(LoadPrefab("Levels/MayusculasMinusculas/SentenceLine"));
		}

		private GameObject LoadPrefab(string name) {
			return Resources.Load<GameObject>("Prefabs/" + name);
		}

		public void Wrong () {
			PlayWrongSound ();
			AllWordsDone ();
		}

		private void AllWordsDone () {
			foreach (GameObject word in words) {
				if(word.transform.parent.parent == sentencePanel.transform) WordClick (word, true);
			}
		}

		public void SoundClick(){
			controller.PlaySentence ();
			soundBtn.interactable = false;
			tryBtn.enabled = false;
		}

		public void AudioDone () {
			EnableHint ();
			tryBtn.enabled = true;
		}

		public void WordClick(GameObject word, bool wrong = false){
			if (word.transform.parent.parent == wordsPanel.transform) {
				if (CanAddToSentence ()) {
					if(!wrong) PlaySoundClick ();
					word.transform.SetParent (sentenceSentenceLines [0].transform, true);
				}
			} else {
				if(!wrong) PlaySoundClick ();
				word.transform.SetParent (originalParents[words.IndexOf (word)]);
			}
		}

		bool CanAddToSentence () {
			int count = 0;
			foreach (GameObject line in sentenceSentenceLines) {
				count += line.GetComponentsInChildren<Button>().Length;
			}
			return count < 5;
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
			soundBtn.interactable = true;
		}

		public void NextChallenge (List<string> options) {
			ResetView ();
			SetOptions (options);
		}

		private void SetOptions (List<string> options) {
			words = new List<GameObject> ();
			int lineCount = 0;
			int charCount = 0;
			foreach (string option in options) {
				if (charCount + option.Length > MAX_CHARS) {
					lineCount++;
					charCount = 0;
				}
				GameObject line = wordsSentenceLines.Count == lineCount ? NewLine(wordsSentenceLines, wordsPanel) : wordsSentenceLines [lineCount];
				GameObject word = GetWord ();
				word.GetComponentInChildren<Text>().text = option;
				word.transform.SetParent (line.transform, true);
				word.transform.localScale = Vector3.one;
				words.Add (word);

				word.GetComponent <Button> ().onClick.AddListener (() => WordClick (word));
				charCount += option.Length;
			}

			originalParents = new List<Transform> (words.Count);

			for (int i = 0; i < words.Count; i++) {
				originalParents.Add (words [i].transform.parent);
			}
		}

		GameObject NewLine (List<GameObject> lines, Image panel) {
			GameObject line = GetLine ();
			line.transform.SetParent (panel.transform, true);
			line.transform.localScale = Vector3.one;
			lines.Add (line);
			return line;
		}

		public void StartGame () {
			words = new List<GameObject> ();
		}

		public void ResetView () {
			foreach (GameObject word in words) { Destroy (word); }
			for (int i = sentenceSentenceLines.Count - 1; i > 0; i--) {
				GameObject line = sentenceSentenceLines [i];
				sentenceSentenceLines.Remove (line);
				Destroy (line);
			}
			for (int j = wordsSentenceLines.Count - 1; j > 0; j--) {
				GameObject line = wordsSentenceLines [j];
				wordsSentenceLines.Remove (line);
				Destroy (line);
			}

			EnableHint ();

			tryBtn.enabled = true;
			soundBtn.interactable = false;
		}

		public void Controller (CreateSentencesController controller) { this.controller = controller; }
		public override void EndGame () { }
	}
}