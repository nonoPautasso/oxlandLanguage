using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
public class CatModel : LevelModel {

		private int currentGame;
		private int candyPrice;
		private int alreadyPaid;
		private int totalCandy;

		private int[] billNumbers; 

		public CatModel(int currentGame)
		{
			this.currentGame = currentGame;

			switch (currentGame) {
			case 0:
				minSeconds = 39;
				pointsPerError = 400;
				pointsPerSecond = 100;
				break;
			case 1:
				minSeconds = 46;
				pointsPerError = 400;
				pointsPerSecond = 100;
				break;
			case 2:
				minSeconds = 50;
				pointsPerError = 400;
				pointsPerSecond = 100;
				break;
			case 3:
				minSeconds = 35;
				pointsPerError = 600;
				pointsPerSecond = 100;
				break;

			}

			StartGame();
		}
	
		public int CurrentGame {
			get {
				return currentGame;
			}
		}

		public override void StartGame ()
		{

		}

		public override void NextChallenge ()
		{

			if (currentGame != 3) {
				int billOneNumber = Random.Range (1, 11);
				int billTwoNumber = Random.Range (1, 11);
				int billThreeNumber = Random.Range (1, 11);
				while (billTwoNumber == billOneNumber) {
					billTwoNumber = Random.Range (1, 11);
				}
				while (billThreeNumber == billOneNumber || billThreeNumber == billTwoNumber) {
					billThreeNumber = Random.Range (1, 11);
				}

				billNumbers = new int[] { billOneNumber, billTwoNumber, billThreeNumber };
				Shuffle (billNumbers);

				switch (currentGame) {
				case 0:
					candyPrice = billNumbers [0] + billNumbers [1] + billNumbers [2] * Random.Range (0, 2);
					Shuffle (billNumbers);
					break;
				case 1:
					if (CatController.instance.GameCounter < 5) {
						alreadyPaid = billNumbers [0];
						candyPrice = billNumbers [0] + billNumbers [1];
					} else {
						alreadyPaid = billNumbers [0] + billNumbers [1] * Random.Range (0, 2);
						candyPrice = billNumbers [0] + billNumbers [1] + billNumbers [2];
					}
					Shuffle (billNumbers);
					break;
				case 2:
					candyPrice = Random.Range (1, 11);
					alreadyPaid = billNumbers [0] + candyPrice;
					Shuffle (billNumbers);
					break;
				}
			} else {
				candyPrice = Random.Range (1, 4);
				totalCandy = Random.Range (1, 6);
				alreadyPaid = candyPrice * totalCandy;
			}


		}

		public override void RequestHint ()
		{
			
		}

		public bool IsCorrectAnswer(int answer){
			switch (currentGame) {
			case 0:
				return answer == candyPrice;
			case 1:
				return answer + alreadyPaid == candyPrice;
			case 2:
				return answer + candyPrice == alreadyPaid;
			
			case 3:
				return answer * candyPrice == alreadyPaid;
			}	
			return false;

		}

		static void Shuffle<T>(T[] array)
		{	
			
			int n = array.Length;
			for (int i = 0; i < n; i++)
			{
				int r = i + (int)(Random.value * (n - i));
				T t = array[r];
				array[r] = array[i];
				array[i] = t;
			}
		}

		public int[] BillNumbers {
			get {
				return billNumbers;
			}
		}

		public int CandyPrice {
			get {
				return candyPrice;
			}
		}

		public int AlreadyPaid {
			get {
				return alreadyPaid;
			}
		}
}

}