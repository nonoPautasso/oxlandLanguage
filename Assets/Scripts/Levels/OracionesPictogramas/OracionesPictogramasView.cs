using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;
using Assets.Scripts.Common.Dragger;

namespace Assets.Scripts.Levels.OracionesPictogramas {
	public class OracionesPictogramasView : DraggerView {

	public Button tryBtn;
	public Button nextBtn;
	public Button soundBtn;
	public Text sentence;
	public List<Image> slotImages;
	public List<DraggerSlot> slots;
	public List<Button> draggables;

	private List<Word> answers;

	public Color32 GREEN_COLOR = new Color32 (81, 225, 148, 225);
	public Color32 WHITE_COLOR = new Color32 (255, 225, 255, 225);



	private OracionesPictogramasController controller;
	
	public void NextChallenge (string currentSentence,List<Word> currentAnswers,List<Word> words) {
		ResetView ();
		answers = currentAnswers;
		ActiveButtons (true, false, true);
		SetWords (words);
		tryBtn.interactable = false;
		ShowSentence (currentSentence);
		//controller.PlaySentenceAudios ();
	
	}

	public void CheckTryButton() {
		foreach(Image slotImage in slotImages) {
			if(!slotImage.IsActive()) {
				tryBtn.interactable = false;
				return;
			}
		}
		DisableHint();
		tryBtn.interactable = true;
	}

	public override void Dropped(DraggerHandler dropped, DraggerSlot where) {
			int index = slots.IndexOf (where);
			slotImages [index].gameObject.SetActive(true);
			slotImages [index].sprite = dropped.GetComponentInChildren<Image> ().sprite;

			CheckTryButton ();
	}

	public override bool CanDropInSlot(DraggerHandler dropper, DraggerSlot slot) {
			return !slotImages [slots.IndexOf (slot)].IsActive();
	}

	public void SlotClick(int index){
	
			if(slotImages[index].IsActive()&&!nextBtn.IsActive()&&soundBtn.enabled) {
				SoundManager.instance.PlayClickSound ();
				draggables.Find((Button draggable) => draggable.GetComponentInChildren<Image>().sprite == slotImages[index].GetComponentInChildren<Image>().sprite).gameObject.SetActive(true);
				slotImages [index].gameObject.SetActive (false);
				CheckTryButton ();
		}
	}

	private void ShowSentence (string currentSentence)
	{
			sentence.text = currentSentence.ToUpper();	
	}

	private void ActiveButtons (bool tryB, bool next, bool sound) {
		Views.SetActiveButton (tryBtn, tryB);
		Views.SetActiveButton (nextBtn, next);
		soundBtn.enabled = sound;
	}

		public void Wrong ()
		{
			SoundManager.instance.PlayFailureSound ();
			for(int i=0;i<slots.Count;i++){
				draggables.Find((Button draggable) => draggable.GetComponentInChildren<Image>().sprite == slotImages[i].GetComponentInChildren<Image>().sprite).gameObject.SetActive(true);
				slotImages [i].gameObject.SetActive (false);
			}
			CheckTryButton ();
		}

		public void Correct ()
		{
			PlayRightSound ();
			foreach (DraggerSlot slot in slots) {
				Views.PaintImage(slot.GetComponent<Image>(),GREEN_COLOR);
			}
			DisableHint ();
			ActiveButtons(false, true,false);
			DraggablesEnabled(draggables.ToArray(), false);

		
		//	nextBtn.interactable = false;
		
		}

	private void SetWords (List<Word> words) {
		List<Word> randomWords = Randomizer.RandomizeList (words);
			for (int i = 0; i < draggables.Count; i++) {
				draggables [i].GetComponentInChildren <Image> ().sprite = randomWords [i].Sprite ();
				//if(draggables[i].GetComponent <ImageTextDragger>() == null) draggables [i].gameObject.AddComponent <ImageTextDragger> ();
				//objImages [i].sprite = randomWords [i].Sprite ();
				draggables [i].enabled = true;
				//draggables [i].transform.SetParent (draggablePanel.transform);
				//draggables [i].transform.position = originalPositions [i];
			}
	}

	public void TicClick(){
			List<string> answers = new List<string>();
			foreach(Image slotImage in slotImages) {
				answers.Add (slotImage.sprite.name);
			}
			controller.Try(answers);
		}

	public void NextClick(){

		controller.NextClick();

	}

	private void ResetView(){
		EnableHint ();
		ResetSlotImages ();
		ShowDraggables (draggables.ToArray(),true);
		DraggablesEnabled (draggables.ToArray(),true);
		SlotsEnabled (slots.ToArray(),true);
	}

	void ResetSlotImages ()
	{
			for(int i=0;i<slots.Count;i++) {
				Views.PaintImage (slots[i].GetComponent<Image>(), WHITE_COLOR);	
			slotImages[i].gameObject.SetActive (false);
		}	
	}

	public override void ShowHint () {
			DisableHint ();
			PaintAnswers ();
			controller.ShowHint ();
	}

	void PaintAnswers ()
	{
			string newString = sentence.text;


			string[] hintTexts = new string[3];
			for(int j=0;j<answers.Count;j++){
				hintTexts [j] = answers [j].Name ();
			}

			for (int i = 0; i < hintTexts.Length; i++) {

				int hintIndex = newString.IndexOf(hintTexts[i]);
				int hintLength = hintTexts[i].Length;

				string start = newString.Substring (0, hintIndex);
				string middle = newString.Substring (hintIndex, hintLength);
				string end = newString.Substring (hintIndex+hintLength, newString.Length-(hintIndex+hintLength));

				newString = start+"<color=green>" +middle + "</color>"+end;

			}

			sentence.text = newString;

	}

	public void SoundClick(){
		soundBtn.enabled = false;
		
		controller.PlaySentenceAudios ();
			DisableAllButtons ();
	}

	void DisableAllButtons ()
	{
		ActiveButtons (false, false, false);
		DisableHint ();
		DraggablesEnabled(draggables.ToArray(), false);
		SlotsEnabled (slots.ToArray(),false);
	}

	void EnableAllButtons(){
		EnableHint ();
		ActiveButtons (true, false, true);
		DraggablesEnabled (draggables.ToArray(),true);
		SlotsEnabled (slots.ToArray(),true);		
	}
	
	public void AudioDone () {
		soundBtn.enabled = true;
		EnableAllButtons ();
		CheckTryButton ();
	}

	public static void DraggablesEnabled(Button[] buttons, bool isActive) {
			for (int i = 0; i < buttons.Length; i++)
				buttons [i].GetComponent<DraggerHandler> ().SetActive (isActive);
				//buttons[i].enabled=isActive;
				//buttons[i].gameObject.SetActive (isActive);

	}

	public static void ShowDraggables(Button[] buttons, bool isActive) {
			for (int i = 0; i < buttons.Length; i++)
				buttons[i].gameObject.SetActive (isActive);
		
	}

	public static void SlotsEnabled(DraggerSlot[] slots, bool isActive) {
			for (int i = 0; i < slots.Length; i++)
				slots[i].enabled= isActive;
				//slots[i].gameObject.SetActive (isActive);
	}

	public override void EndGame () { }
	public void Controller (OracionesPictogramasController controller) { this.controller = controller; }

}
}
