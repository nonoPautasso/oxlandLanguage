using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEditor;

namespace Assets.Scripts.Levels.SplitSentences {
	public class SplitSentencesView : LevelView {
		public List<GameObject> sentenceLines;
		public Image lettersPanel;
		public Image sentencePanel;

		private static List<Color> colors = new List<Color> {Color.blue, Color.green, Color.red, Color.yellow, Color.cyan, Color.magenta};
		public Randomizer colorRandomizer;
		public Color currentColor;

		private bool paintMode;

		public static int MAX_CHARS = 30;

		private SplitSentencesController controller;

		public void NextChallenge (string sentence) {
			ResetView ();
			SetSentence (sentence);
			currentColor = Color.white;
			colorRandomizer = Randomizer.New (colors.Count - 1);
		}

		private void SetSentence (string sentence) {
			char[] charArray = sentence.ToCharArray ();
			int charNumber = 0;
			foreach (Toggle letterToggle in lettersPanel.GetComponentsInChildren<Toggle>()) {
				if (charNumber < charArray.Length && charArray [charNumber] == ' ') charNumber++;
				if (charNumber < charArray.Length){
					ResetLetterToggle (letterToggle);
					letterToggle.GetComponentInChildren<Text>().text = charArray[charNumber].ToString ();
				} else {
					Views.SetActiveToggle (letterToggle, false);
				}
				charNumber++;
			}
		}

		void ResetLetterToggle (Toggle letterToggle) {
			Views.SetActiveToggle (letterToggle, true);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void PaintModeOn (TogglePaint togglePaint) {
			paintMode = true;
		}

		public void PaintModeOff (TogglePaint togglePaint) {
			paintMode = false;
			currentColor = Color.white;

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
				if (line != sentenceLines [0]) Destroy (line);
			}
		}

		private void AddColoredWords () {
			TogglePaint[] togglePaints = lettersPanel.GetComponentsInChildren<TogglePaint> ();
			List<string> words = new List<string> ();
			string currentWord = "";
			Color currentColor = togglePaints[0].GetColor ();
			foreach (TogglePaint toggle in togglePaints) {
				if(toggle.GetColor () == currentColor && currentColor != Color.white) {
					currentWord += toggle.GetComponentInChildren <Text> ().text;
				}
				else {
					currentColor = toggle.GetColor ();
					if (currentWord.Length != 0) words.Add (currentWord);
					currentWord = "";
					if (currentColor != Color.white) currentWord = toggle.GetComponentInChildren <Text> ().text;
				}
			}
			if (currentWord.Length != 0) words.Add (currentWord);

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
			Destroy (word);
			//todo unpaint letters.
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
			ClearWordsAndLines ();
		}

		public void TryClick(){
			
		}

		public bool PaintMode () {
			return paintMode;
		}

		public Color GetColor () {
			if (currentColor == Color.white) currentColor = colors [colorRandomizer.Next ()];
			return currentColor;
		}

		public void Controller (SplitSentencesController controller) { this.controller = controller; }
		public override void EndGame () { }
	}
}