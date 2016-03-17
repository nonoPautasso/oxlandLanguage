using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.Castlenumbers6
{

    public class CastleModel : LevelModel {
        private int MAX_NUMBER = 100;

        private int CASTLE1_MAX_VALUE = 5;

        private List<int> numbers;
        private int currentNumber;

        private List<int> disabledRows;
        private List<int> disabledLateralNumbers;

        private List<int> numbersToShow;

        private int currentGame;
        private int currentRow;

        private int currentQuestion;

        private List<int> currentSolutions;
        private List<int> currentErrors;
        private int segment;

        public CastleModel(int currentGame)
        {
            this.currentGame = currentGame;
            AsignMetricsData(currentGame);          
            disabledRows = new List<int>(5);
            disabledLateralNumbers = new List<int>(10);
            currentErrors = new List<int>(10);
            numbersToShow = new List<int>();
            StartGame();
        }

        private void AsignMetricsData(int currentGame){          
            pointsPerSecond = 100;
            switch (currentGame){
                case 0:
                    minSeconds = 35;
                    pointsPerError = 1000;
                    break;
                case 1:
                    minSeconds = 24;
                    pointsPerError = 500;
                    break;
                case 2:
                    minSeconds = 60;
                    pointsPerError = 400;
                    break;
                case 3:
                    minSeconds = 46;
                    pointsPerError = 400;
                    break;
                case 4:
                    minSeconds = 56;
                    pointsPerError = 400;
                    break;
                case 5:
                    minSeconds = 56;
                    pointsPerError = 400;
                    break;
                default:
                    Debug.Log("ERROR EN ASIGN METRICS DATA");
                    break;
            }
        }

        private void fillNumbersList()
        {
            for(int i = 0; i < MAX_NUMBER; i++) { numbers.Add(i);}     
        }

        internal int GetCurrentNumber()
        {
            return currentNumber;
        }

        internal bool CheckAnswer(int answer)
        {
            return currentNumber == answer;
        }

        public override void StartGame()
        {
            numbers = new List<int>();
            fillNumbersList();
            if(currentGame >= 3) { currentSolutions = new List<int>(6); }
        }

        internal List<int> GetDisabledRows()
        {
            return disabledRows;
        }

        public override void NextChallenge()
        {
            if (numbers.Count == 0) { fillNumbersList(); }
            int index = Random.Range(0, numbers.Count);
            currentNumber = numbers[index];
            switch (currentGame)
            {
                case 0:
                    numbers.RemoveAt(index);
                    break;
                case 1:
                    currentQuestion = (Random.Range(0.0f, 0.99f) < 0.5 ? 1 : -1) * Random.Range(1, CASTLE1_MAX_VALUE+1);
                    while(currentNumber + currentQuestion < 0 || currentNumber + currentQuestion > 99){
                        index = Random.Range(0, numbers.Count);
                        currentNumber = numbers[index];
                        currentQuestion = (Random.Range(0.0f, 0.99f) < 0.5 ? 1 : -1) * Random.Range(1, CASTLE1_MAX_VALUE + 1);
                    }
                    break;
                case 2:
                    currentRow++;
                    if(currentRow < 10)
                    {
                        while (currentNumber < (currentRow * 10) || currentNumber > (currentRow * 10 + 9)) {
                            index = Random.Range(0, numbers.Count);
                            currentNumber = numbers[index];
                        }
                    } else if(currentRow == 10)
                    {
                        while (currentNumber == 99 || currentNumber == 0) {
                            index = Random.Range(0, numbers.Count);
                            currentNumber = numbers[index];
                        }
                    } else
                    {
                        while (currentNumber % 10 != 3 && currentNumber % 10 != 4 && currentNumber % 10 != 5 && currentNumber % 10 != 6)
                        {
                            index = Random.Range(0, numbers.Count);
                            currentNumber = numbers[index];
                        }
                    }                                 
                    break;
                case 3:
                    int lastRow = currentRow;
                    do{
                        currentRow = Random.Range(0, 9);
                    } while (lastRow == currentRow || lastRow == currentRow + 1 || lastRow == currentRow - 1);
                    currentSolutions.RemoveAll(e => true);
                    for (int i = 0; i < DefineCurrentSolutions(); i++) {
                        int aSolution;
                        do{
                            aSolution = Random.Range(currentRow * 10, (currentRow + 2) * 10);
                        } while (currentSolutions.Contains(aSolution));
                        currentSolutions.Add(aSolution);                    
                    }
                    break;
                case 4:
                    currentSolutions.RemoveAll(e => true);
                    numbers.RemoveAt(index);
                    if (currentNumber + 10 <= 99) currentSolutions.Add(currentNumber + 10);
                    if (currentNumber - 10 >= 0) currentSolutions.Add(currentNumber - 10);
                    if ((currentNumber + 1) % 10 != 0) currentSolutions.Add(currentNumber + 1);
                    if ((currentNumber - 1) % 10 != 9) currentSolutions.Add(currentNumber - 1);
                    break;
                case 5:
                    currentSolutions.RemoveAll(e => true);
                    currentErrors.RemoveAll(e => true);
                    lastRow = currentRow;
                    currentRow = currentNumber / 10;
                    while (lastRow == currentRow || (currentNumber + 17) > 99){
                        index = Random.Range(0, numbers.Count);
                        currentNumber = numbers[index];
                        currentRow = currentNumber / 10;
                    }               
                    for (int i = 0; i < DefineCurrentSolutions(); i++)
                    {
                        int aSolution;
                        do {
                            aSolution = Random.Range(currentNumber + 1, currentNumber + 17);
                        } while (currentSolutions.Contains(aSolution));
                        currentSolutions.Add(aSolution);
                        currentErrors.Add(generateModifacation(aSolution, currentNumber));
                    }
                    numbers.RemoveAt(index);
                    break;
            }      
        }

        internal List<int> GetCurrentErrors()
        {
            return currentErrors;
        }

        private int generateModifacation(int aSolution, int currentNumber)
        {
            float random = Random.Range(0, 0.99f);
            int toReturn;
            if (random < 0.25) {
                do { toReturn = aSolution + (UnityEngine.Random.Range(0, 0.99f) < 0.5 ? -1 : 1); }
                while (toReturn <= currentNumber || toReturn > currentNumber + 16);
            }
            else if(random < 0.5) {
                do { toReturn = aSolution + (UnityEngine.Random.Range(0, 0.99f) < 0.5 ? -2 : 2); }
                while (toReturn <= currentNumber || toReturn > currentNumber + 16);
            }
            else if(random < 0.75) {
                do { toReturn = aSolution + (UnityEngine.Random.Range(0, 0.99f) < 0.5 ? - 3 : 3); }
                while (toReturn <= currentNumber || toReturn > currentNumber + 16);
            }
            else {
                int decena = (aSolution / 10);
                int unity = (aSolution % 10);
                if(decena == unity) { return generateModifacation(aSolution, currentNumber); }
                else return unity * 10 + decena;
            }
            return toReturn;
        }

        private int DefineCurrentSolutions()
        {
            return Random.Range(segment + 2, segment + (currentGame == 5 ? 4 : 5));           
        }

        internal void NextSegment()
        {
            segment++;
        }

        internal int GetCurrentQuestion()
        {
            return currentQuestion;
        }

        internal List<int> GetCurrentSolutions()
        {
            return currentSolutions;
        }

        internal int GetCurrentRow()
        {
            return currentRow;
        }

        internal int GetCurrentGame()
        {
            return currentGame;
        }

        public override void RequestHint()
        {
            switch (currentGame)
            {
                case 3:
                    disabledLateralNumbers.RemoveAll(e => true);
                    for(int i = 0; i < 10; i++) {
                        bool isNecessary = false;
                        for (int j = 0; j < currentSolutions.Count && !isNecessary; j++) {
                            isNecessary = ("" + currentSolutions[j]).Contains("" + i);                                                   
                        }
                        if (!isNecessary) disabledLateralNumbers.Add(i);
                    }
                    break;
                case 4:
                    disabledLateralNumbers.RemoveAll(e => true);
                    for (int i = 0; i < 10; i++)
                    {
                        bool isNecessary = false;
                        for (int j = 0; j < currentSolutions.Count && !isNecessary; j++)
                        {
                            isNecessary = ("" + currentSolutions[j]).Contains("" + i);
                        }
                        if (!isNecessary) disabledLateralNumbers.Add(i);
                    }
                    break;
                case 5:
                    numbersToShow.RemoveAll(e => true);
                    for (int i = (currentNumber / 10) * 10; i < currentNumber; i++){numbersToShow.Add(i);}
                    for (int i = currentNumber + 17; i < (currentNumber + 17) + 10 - ((currentNumber + 17) % 10); i++) { numbersToShow.Add(i); }
                    break;
                default:
                    disabledRows.RemoveAll(e => true);
                    for (int i = 0; i < 5; i++)
                    {
                        int row = Random.Range(0, 10);
                        while ((row * 10 <= currentNumber && currentNumber <= row * 10 + 9) || disabledRows.Contains(row))
                        {
                            row = Random.Range(0, 10);
                        }
                        disabledRows.Add(row);
                    }
                    break;
            }
            
        }

        internal List<int> GetNumbersToShow()
        {
            return numbersToShow;
        }

        internal bool CheckAnswer(List<int> answers)
        {
            if (answers.Count != currentSolutions.Count) return false;
            for (int i = 0; i < answers.Count; i++)
            {
                if (!answers.Contains(currentSolutions[i])) { return false;}
            }
            return true;
        }

        internal List<int> GetDisabledLateralNumbers()
        {
            return disabledLateralNumbers;
        }

        internal int GetCurrentSegment()
        {
            return segment;
        }
    }
}
