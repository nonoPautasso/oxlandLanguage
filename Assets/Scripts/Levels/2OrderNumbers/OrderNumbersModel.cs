using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.App;


namespace Assets.Scripts.Levels
{
	public class OrderNumbersModel : LevelModel
	{

		private int currentNumber;
		private bool ascending;
		private int slotIndex;
		private List<int> hintIndexes;

	
		public OrderNumbersModel(){
			minSeconds = 20;
			pointsPerError = 500;
			pointsPerSecond = 100;
			StartGame ();
		}

		public override void StartGame(){
			ascending = true;
			slotIndex = -1;       
		}


		public override void NextChallenge(){
			if (ascending)
				currentNumber++;
			else
				currentNumber--;
			
			slotIndex++;

		}
			

		public override void RequestHint ()
		{
			hintIndexes = new List<int> ();
			int randomIndex;
			hintIndexes.Add (currentNumber-1);
			while (hintIndexes.Count < 4) {
				randomIndex = Random.Range (1, 11);
				if (!hintIndexes.Contains (randomIndex))
					hintIndexes.Add (randomIndex);
			}
		}

		public bool NumberIsCorrect(int number){
			return number == currentNumber;
		}

		public void ChangeMode(){
			ascending = false;
			slotIndex = 0;
		}

		public int CurrentNumber {
			get {
				return currentNumber;
			}
		}

		public bool Ascending {
			get {
				return ascending;
			}
		}

		public int SlotIndex {
			get {
				return slotIndex;
			}
		}

		public List<int> HintIndexes {
			get {
				return hintIndexes;
			}
		}
	}
}

