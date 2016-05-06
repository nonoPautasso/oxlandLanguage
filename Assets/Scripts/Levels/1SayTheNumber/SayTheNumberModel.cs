using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
	public class SayTheNumberModel : LevelModel
    {
        
        public int currentNumber { get; set; }
        public List<int> numbers { get; set; }
        private bool lastAnswerIsCorrect {get; set;}
        public List<int> disabledNumbers { get; set; }


		public SayTheNumberModel(){
			minSeconds = 20;
			pointsPerError = 500;
			pointsPerSecond = 100;
			StartGame ();
		}

		public override void StartGame(){
            currentNumber = -1;
            lastAnswerIsCorrect = true;
            numbers = new List<int> {1,2,3,4,5,6,7,8,9,10};
            disabledNumbers = new List<int>();          
        }

   

        public override void NextChallenge()
        {
            disabledNumbers.Clear();
            int index = UnityEngine.Random.Range(0, numbers.Count);
            Debug.Log("Index entre :  0 y " + (numbers.Count-1) );
            Debug.Log("Index a sacar : " + index);
            currentNumber = numbers[index];
            Debug.Log("Numero : " + currentNumber);
            numbers.RemoveAt(index);
            Debug.Log("Quedan: " + numbers.Count);


        }

        
		public override void RequestHint()
        {
            int toDisable = UnityEngine.Random.Range(0, 9);

            while (disabledNumbers.Count < 6)
            {
                if (toDisable != (currentNumber-1) && disabledNumbers.IndexOf(toDisable) == -1)
                {
                    disabledNumbers.Add(toDisable);
                    Debug.Log("Disabled: " + toDisable);

                }
                toDisable = UnityEngine.Random.Range(0, 9);
            }
        }

		public bool CheckAnswer(int number)
		{
			return currentNumber == number;         
		}


    }
}
