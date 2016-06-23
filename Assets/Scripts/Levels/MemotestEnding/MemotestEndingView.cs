using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.MemotestEnding {
	public class MemotestEndingView : LevelView {
		public List<Button> cards;
		public List<Image> innerImages;
		public Button tryBtn;
		public Button refreshBtn;

		private List<Button> activeCards;
		private List<Button> hintCards;
		private Sprite[] cardStates;
		private MemotestEndingController controller;

		public void StartGame (List<Word> objs) {
			EnableHint ();
			LoadCardSprites ();
			activeCards = new List<Button> ();
			ActiveButtons (true, false);
			tryBtn.interactable = false;
			SetWordSpritesAndText (objs);
		}

		private void LoadCardSprites () {
			cardStates = Resources.LoadAll <Sprite>("Sprites/Spanish/fichaMemotest");
		}

		public void CardClick(Button card){
			PlaySoundClick ();
			if(activeCards.Contains (card)){
				ResetCard (card);
				activeCards.Remove (card);
			} else if(activeCards.Count < 2){
				ShowCard (card);
				activeCards.Add (card);
			}
			CheckTryBtn ();
		}

		private void SetWordSpritesAndText (List<Word> objs) {
			for (int i = 0; i < objs.Count; i++) {
				innerImages [i].sprite = objs [i].Sprite ();
				cards [i].GetComponentInChildren <Text> (true).text = objs [i].Name ();
				ResetCard (cards [i]);
			}
		}

		private void ResetCard (Button card) {
			card.image.sprite = cardStates [0];
			ActiveCardElements (card, false);
		}

		private void ShowCard(Button card){
			card.image.sprite = cardStates [1];
			ActiveCardElements (card, true);
		}

		private void CardsDone(){
			foreach (Button card in activeCards) {
				card.image.sprite = cardStates [2];
				card.enabled = false;
			}
			activeCards = new List<Button> ();
			CheckTryBtn ();
		}

		private void WrongCardsDone(){
			foreach (Button card in activeCards) {
				ResetCard (card);
			}
			activeCards = new List<Button> ();
			CheckTryBtn ();
		}

		private static void ActiveCardElements (Button card, bool active) {
			for (int i = 0; i < card.transform.childCount; i++) {
				card.transform.GetChild (i).gameObject.SetActive (active);
			}
		}

		private void ActiveButtons (bool tryB, bool refresh) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (refreshBtn, refresh);
		}

		private void CheckTryBtn () {
			tryBtn.interactable = activeCards.Count == 2;
		}

		public void TryClick(){
			int index1 = cards.IndexOf (activeCards [0]);
			int index2 = cards.IndexOf (activeCards [1]);
			controller.Try (index1, index2);
		}

		public void RefreshClick(){
			ActiveButtons (true, false);
			WrongCardsDone ();
			EnableHint ();
			EnableCards ();
		}

		public void Wrong (int index1, int index2) {
			PlayWrongSound ();
			ActiveButtons (false, true);
			DisableHint ();
			tryBtn.interactable = false;
			Views.ButtonsEnabled (cards.ToArray (), false);
		}

		public void Correct(int index1, int index2){
			PlayRightSound ();
			CardsDone ();
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
			Hint ();
		}

		public void Hint () {
			hintCards = new List<Button> ();
			tryBtn.interactable = false;
			Invoke ("HintDone", 2f);
			foreach (Button card in cards) {
				if(card.image.sprite == cardStates[0]){
					hintCards.Add (card);
					ShowCard (card);
				}
			}
			Views.ButtonsEnabled (cards.ToArray (), false);
		}

		public void HintDone(){
			EnableHint ();
			CheckTryBtn ();

			foreach (Button card in hintCards) {
				ResetCard (card);
			}
			EnableCards ();
		}

		private void EnableCards () {
			foreach (Button card in cards) {
				if (card.image.sprite != cardStates [2]) card.enabled = true;
			}
		}

		public override void EndGame () { }
		public void Controller (MemotestEndingController controller) { this.controller = controller; }
	}
}