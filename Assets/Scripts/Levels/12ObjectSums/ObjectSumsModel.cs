using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Levels.ObjectSums
{

    public class ObjectSumsModel : LevelModel
    {

        private int firstNumber;
        private int secondNumber;
      
        // This method selects two random values for the first and second numbers.
        // If both of the numbers are the same as the last two, the process starts again and 
        // two new values come up. 
        public override void NextChallenge(){   
            int lastFirstNumber = firstNumber;
            int lastSecondNumber = secondNumber;
            do{
                firstNumber = UnityEngine.Random.Range(1, 11);
                secondNumber = UnityEngine.Random.Range(1, 11);
            } while (lastFirstNumber == firstNumber && lastSecondNumber == secondNumber);
        }

        public override void RequestHint(){}

        public override void StartGame(){
            pointsPerSecond = 100;
            pointsPerError = 400;
            minSeconds = 27;
            firstNumber = -1;
            secondNumber = -1;
        }

        public int GetFirstNumber(){
            return firstNumber;
        }
        public int GetSecondNumber()
        {
            return secondNumber;
        }
        // This method checks the answer and returns true if is correct, and false if it is wrong.
        internal bool CheckAnswer(int answer)
        {
            return answer == firstNumber + secondNumber;
        }
    }
}
