﻿using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEditor;

namespace Assets.Scripts.Levels.SplitSentences {
	public class SplitSentencesView : LevelView {
		private List<GameObject> sentenceLines = new List<GameObject>();
		private Dictionary<string, int> wordsLastToggleIndex = new Dictionary<string, int> ();
		public Image lettersPanel;
		public Image sentencePanel;
		private TogglePaint currentToggle;

		private static List<Color> colors = new List<Color> {new Color32(255, 73, 235, 255), new Color32(200, 137, 255, 255),
			new Color32(34, 191, 255, 255), new Color32(70, 233, 172, 255), new Color32(158, 255, 66, 255), new Color32(245, 255, 66, 255),
			new Color32(214, 173, 66, 255), new Color32(255, 86, 86, 255), new Color32(173, 81, 98, 255), new Color32(173, 139, 0, 255)};
		public Color currentColor;

		private bool paintMode;

		public static int MAX_CHARS = 20;
		public static int CHARS_PER_LINE = 21;

		private SplitSentencesController controller;

		public void NextChallenge (string sentence) {
			ResetView ();
			SetSentence (sentence);
			currentColor = Color.white;
		}

		private void SetSentence (string sentence) {
			string[] split = sentence.Split (' ');
			int toggleNumber = 0;
			int lineMax = CHARS_PER_LINE;
			Toggle[] toggles = lettersPanel.GetComponentsInChildren<Toggle> (true);

			foreach (string word in split) {
				if(word.Length + toggleNumber >= lineMax){
					for (int i = toggleNumber; i < lineMax; i++) {
						Views.SetActiveToggle (toggles[i], false);
					}
					toggleNumber = CHARS_PER_LINE;
					lineMax = CHARS_PER_LINE * 2;
				}
				foreach (char wordChar in word.ToCharArray ()) {
					ResetLetterToggle (toggles[toggleNumber]);
					toggles[toggleNumber].GetComponentInChildren<Text>().text = wordChar.ToString ();
					toggleNumber++;
				}
				if (toggleNumber == lineMax) lineMax = CHARS_PER_LINE * 2;
			}
			for (int i = toggleNumber; i < toggles.Length; i++) {
				Views.SetActiveToggle (toggles[i], false);
			}
		}

		void ResetLetterToggle (Toggle letterToggle) {
			Views.SetActiveToggle (letterToggle, true);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public bool IsNextToCurrent (TogglePaint togglePaint) {
			TogglePaint[] togglePaints = lettersPanel.GetComponentsInChildren<TogglePaint> ();
			int current = Array.IndexOf (togglePaints, togglePaint);
			int previous = Array.IndexOf (togglePaints, currentToggle);

			return current == (previous + 1) || current == (previous - 1);
		}

		public void PaintModeOn (TogglePaint togglePaint) {
			paintMode = true;
			currentToggle = togglePaint;
		}

		public void PaintModeOff (TogglePaint togglePaint) {
			paintMode = false;
			currentColor = Color.white;
			currentToggle = null;

			RefreshWords ();
		}

		private void RefreshWords () {
			ClearWordsAndLines ();
			AddColoredWords ();
		}

		private void ClearWordsAndLines () {
			foreach (GameObject line in sentenceLines) {
				foreach (Button word in line.GetComponentsInChildren<Button>()) {
					Destroy (word);
				}
				Destroy (line);
			}
		}

		private void AddColoredWords () {
			TogglePaint[] togglePaints = lettersPanel.GetComponentsInChildren<TogglePaint> ();
			List<string> words = new List<string> ();
			wordsLastToggleIndex = new Dictionary<string, int> ();
			sentenceLines = new List<GameObject> ();
			string currentWord = "";
			Color currentColor = togglePaints[0].GetColor ();
			foreach (TogglePaint toggle in togglePaints) {
				if(toggle.GetColor () == currentColor && currentColor != Color.white) {
					currentWord += toggle.GetComponentInChildren <Text> ().text;
				}
				else {
					currentColor = toggle.GetColor ();
					if (currentWord.Length != 0) {
						words.Add (currentWord);
						var lastIndexWord = Array.IndexOf (togglePaints, toggle) - 1;
						wordsLastToggleIndex.Add (currentWord, lastIndexWord);
					}
					currentWord = "";
					if (currentColor != Color.white) currentWord = toggle.GetComponentInChildren <Text> ().text;
				}
			}
			if (currentWord.Length != 0) {
				words.Add (currentWord);
				wordsLastToggleIndex.Add (currentWord, togglePaints.Length - 1);
			}

			int lineCount = 0;
			int charCount = 0;
			foreach (string w in words) {
				if (charCount + w.Length > MAX_CHARS) {
					lineCount++;
					charCount = 0;
				}
				GameObject line = sentenceLines.Count == lineCount ? NewLine(sentenceLines, sentencePanel) : sentenceLines [lineCount];
				GameObject word = GetWord ();
				word.GetComponentInChildren<Text>().text = w;
				word.transform.SetParent (line.transform, true);
				word.transform.localScale = Vector3.one;

				word.GetComponent <Button> ().onClick.AddListener (() => WordClick (word));
				charCount += w.Length;
			}
		}

		public void WordClick(GameObject word){
			PlaySoundClic ();

			string wordText = word.GetComponentInChildren<Text>().text;
			int lastIndex = wordsLastToggleIndex [wordText];
			TogglePaint[] togglePaints = lettersPanel.GetComponentsInChildren<TogglePaint> ();

			for (int i = 0; i < wordText.Length; i++) {
				togglePaints [lastIndex - i].PaintMe (Color.white);
			}

			Destroy (word);
		}

		private GameObject NewLine (List<GameObject> lines, Image panel) {
			GameObject line = GetLine ();
			line.transform.SetParent (panel.transform, true);
			line.transform.localScale = Vector3.one;
			lines.Add (line);
			return line;
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

		public void ResetView () {
			EnableHint ();
			ClearWordsAndLines ();
			sentenceLines.Clear ();
			foreach (TogglePaint toggle in lettersPanel.GetComponentsInChildren<TogglePaint>(true)) {
				toggle.PaintMe (Color.white);
			}
		}

		public void TryClick(){
			List<string> answer = new List<string> ();
			foreach (GameObject line in sentenceLines) {
				foreach (Button word in line.GetComponentsInChildren<Button>()) {
					answer.Add (word.GetComponentInChildren<Text>().text);
				}
			}
			controller.Try (string.Join (" ", answer.ToArray ()));
		}

		public bool PaintMode () {
			return paintMode;
		}

		public Color GetColor () {
			if (currentColor == Color.white) {
				Randomizer colorRandomizer = Randomizer.New (colors.Count - 1);
				currentColor = colors [colorRandomizer.Next ()];
				while(IsCurrentColorInUse()) currentColor = colors [colorRandomizer.Next ()];
			}
			return currentColor;
		}

		private bool IsCurrentColorInUse () {
			foreach (TogglePaint toggle in lettersPanel.GetComponentsInChildren<TogglePaint>(true)) {
				if (toggle.GetColor () == currentColor)
					return true;
			}
			return false;
		}

		public void Correct () {
			PlayRightSound ();
		}

		public void Wrong () {
			PlayWrongSound ();
		}

		public void PaintNextWord (string sentence) {
			int wordIndex = 0;
			TogglePaint[] togglePaints = lettersPanel.GetComponentsInChildren<TogglePaint> ();
			foreach (string word in sentence.Split (' ')) {
				bool canPaint = true;
				for (int i = 0; i < word.Length; i++) {
					if (togglePaints [wordIndex + i].AmIPainted()) {
						wordIndex += word.Length;
						canPaint = false;
						break;
					}
				}
				if (canPaint) {
					PaintWord (togglePaints, wordIndex, word.Length);
					break;
				}
			}
			RefreshWords ();
		}

		private void PaintWord (TogglePaint[] togglePaints, int wordIndex, int wordLength) {
			for (int i = 0; i < wordLength; i++) {
				togglePaints [wordIndex + i].PaintMe (GetColor ());
			}
		}

		public void Controller (SplitSentencesController controller) { this.controller = controller; }
		public override void EndGame () { }
	}
}