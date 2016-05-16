using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.MayusMin {
	public class MayusMinView : LevelView {
		public List<Image> letters;
		public Image sentencePanel;
		public Text hintText;
		public Button nextBtn;

		public static int MAX_CHARS = 16;

		private List<GameObject> sentenceLines = new List<GameObject>();
		private List<GameObject> slots = new List<GameObject> ();

		private MayusMinController controller;

		public void NextChallenge(string sentence, List<string> letters){
			ResetView ();

			SetLetters (Randomizer.RandomizeList (letters));
			SetSentence (sentence);
		}

		private void SetSentence (string sentence) {
			int lineCount = 0;
			int charCount = 0;
			List<string> words = SplitWithSpaces(sentence);
			foreach (string w in words) {
				if (charCount + w.Length > MAX_CHARS) {
					lineCount++;
					charCount = 0;
				}
				GameObject line = sentenceLines.Count == lineCount ? NewLine(sentenceLines, sentencePanel) : sentenceLines [lineCount];
				GameObject word = GetWord ();
				GameObject slot = w.Contains ("_") ? GetSlot () : null;
				if(slot != null){
					//slot.GetComponentInChildren<Text>().text = "";
					slot.transform.SetParent (line.transform, true);
					slot.transform.localScale = Vector3.one;
					slot.AddComponent<LetterSlot>().View (this);
					slots.Add (slot);
				}
				word.GetComponentInChildren<Text>().text = slot == null ? w : w.Substring (1);
				word.transform.SetParent (line.transform, true);
				word.transform.localScale = Vector3.one;

				charCount += w.Length;
			}
		}

		private List<string> SplitWithSpaces (string sentence) {
			string[] split = sentence.Split (' ');
			List<string> result = new List<string> ();
			foreach (string word in split) {
				if(word.Contains ("_")) result.Add (" ");
				result.Add (word);
//				
			}
			return result;
		}

		public void LetterClick(int index){
			SoundManager.instance.PlayClicSound ();
			Image letter = letters [index];
			string text = letter.GetComponentInChildren<Text> ().text;
			letter.GetComponentInChildren<Text> ().text = text.ToUpper () == text ? text.ToLower () : text.ToUpper ();
		}

		private void SetLetters (List<string> letterStrings) {
			for (int i = 0; i < letters.Count; i++) {
				letters [i].GetComponentInChildren<Text> ().text = letterStrings [i].ToLower ();
			}
		}

		public override void ShowHint () {
			controller.ShowHint ();
			DisableHint ();
			hintText.gameObject.SetActive (true);
		}

		private GameObject NewLine (List<GameObject> lines, Image panel) {
			GameObject line = GetLine ();
			line.transform.SetParent (panel.transform, true);
			line.transform.localScale = Vector3.one;
			lines.Add (line);
			return line;
		}

		public GameObject GetWord(){
			return Instantiate<GameObject>(Views.LoadPrefab("Levels/MayusculasMinusculas/word"));
		}

		public GameObject GetSlot(){
			return Instantiate<GameObject>(Views.LoadPrefab("Levels/MayusculasMinusculas/Slot"));
		}

		public GameObject GetLine(){
			return Instantiate<GameObject>(Views.LoadPrefab("Levels/MayusculasMinusculas/SentenceLine"));
		}

		public void Dropped (LetterSlot letterSlot, GameObject letter) {
			string draggedLetter = letter.GetComponentInChildren<Text>().text;
			int letterSlotNumber = slots.IndexOf (letterSlot.gameObject);

			if(controller.IsCorrect (draggedLetter, letterSlotNumber)){
				PlayRightSound ();
				letterSlot.transform.GetChild (1).GetComponentInChildren <Text>().text = draggedLetter;
				letter.SetActive (false);
				CheckFinished ();
			} else {
				PlayWrongSound ();
			}
		}

		private void CheckFinished () {
			foreach (Image letter in letters) {
				if (letter.IsActive ()) return;
			}
			nextBtn.gameObject.SetActive (true);
		}

		public void NextClick () {
			SoundManager.instance.PlayClicSound ();
			controller.NextChallenge ();
		}

		private void ResetView () {
			hintText.gameObject.SetActive (false);
			EnableHint ();
			nextBtn.gameObject.SetActive (false);

			foreach (GameObject line in sentenceLines) {
				Destroy (line);
			}

			foreach (Image letter in letters) {
				letter.gameObject.SetActive (true);
			}
			sentenceLines = new List<GameObject> ();
			slots = new List<GameObject> ();
		}

		public override void EndGame () { }
		public void Controller(MayusMinController controller){ this.controller = controller; }
	}
}

