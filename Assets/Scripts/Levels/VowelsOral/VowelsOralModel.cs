using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;
using Assets.Scripts.Levels.Vowels;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.VowelsOral
{
    public class VowelsOralModel : LevelModel
    {

        private string currentCorrectLetter;

        int[] letterAmounts;

        public void InitModel()
        {
            letterAmounts = new int[5];
        }

        public DataTrio<Sprite[], AudioClip[], int> LoadResources(string letter)
        {
            int intValue;
            switch (letter)
            {
                case "A":
                    intValue = 0;
                    break;
                case "E":
                    intValue = 1;
                    break;
                case "I":
                    intValue = 2;
                    break;
                case "O":
                    intValue = 3;
                    break;
                case "U":
                    intValue = 4;
                    break;
                default:
                    intValue = -1;
                    break;
            }
            DataTrio<Sprite[], AudioClip[], int> toReturn;
            AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/Spanish/" + letter + "Words");
            Sprite[] images = Resources.LoadAll<Sprite>("Sprites/Spanish/ObjectsVowels");
            toReturn = new DataTrio<Sprite[], AudioClip[], int>(new Sprite[clips.Length], new AudioClip[clips.Length], intValue);
            for (int i = 0, j = GetLetterStartingIndex(letter); j < GetLetterStartingIndex(letter)
                + GetLetterSize(letter); i++, j++)
            {
                toReturn.Fst()[i] = images[j];
                toReturn.Snd()[i] = clips[i];
            }
            letterAmounts[intValue] = 2;
            return toReturn;
        }

        public override void NextChallenge()
        {
            throw new NotImplementedException();
        }

        public override void RequestHint()
        {
            throw new NotImplementedException();
        }

        public DataPair<int, int> RequestHintInfo()
        {
            DataPair<int, int> toReturn;
            switch (currentCorrectLetter)
            {
                case "A":
                    toReturn = new DataPair<int, int>(2, 4);
                    break;
                case "E":
                    toReturn = new DataPair<int, int>(2, 4);
                    break;
                case "I":
                    toReturn = ChooseTwoRandomOpenVowels();
                    break;
                case "O":
                    toReturn = new DataPair<int, int>(2, 4);
                    break;
                case "U":
                    toReturn = ChooseTwoRandomOpenVowels();
                    break;
                default:
                    toReturn = null;
                    break;
            }
            return toReturn;
        }

        public DataPair<int, int> ChooseTwoRandomOpenVowels()
        {
            DataPair<int, int> toReturn;
            int fst = 0, snd = 0;
            while (fst == snd)
            {
                fst = UnityEngine.Random.Range(0, 3);
                snd = UnityEngine.Random.Range(0, 3);
            }
            if (fst == 2) fst = 3;
            if (snd == 2) snd = 3;
            toReturn = new DataPair<int, int>(fst, snd);
            return toReturn;
        }

        public override void StartGame()
        {
            throw new NotImplementedException();
        }

        static void RandomizeArray<T>(T[] arr)
        {
            for (var i = arr.Length - 1; i > 0; i--)
            {
                var r = UnityEngine.Random.Range(0, i);
                var tmp = arr[i];
                arr[i] = arr[r];
                arr[r] = tmp;
            }
        }

        public int Next()
        {
            if (letterAmounts[0] == 0 && letterAmounts[1] == 0 && letterAmounts[2] == 0
                && letterAmounts[3] == 0 && letterAmounts[4] == 0) return -1;
            int value = UnityEngine.Random.Range(0, 5);
            if (letterAmounts[value] == 0) return Next();
            else letterAmounts[value]--;
            switch (value)
            {
                case 0:
                    currentCorrectLetter = "A";
                    break;
                case 1:
                    currentCorrectLetter = "E";
                    break;
                case 2:
                    currentCorrectLetter = "I";
                    break;
                case 3:
                    currentCorrectLetter = "O";
                    break;
                case 4:
                    currentCorrectLetter = "U";
                    break;
                default:
                    currentCorrectLetter = null;
                    break;
            }
            return value;
        }

        public bool CheckSubmittedLetter(string letter)
        {
            return letter.Equals(currentCorrectLetter);
        }

        int GetLetterStartingIndex(string letter)
        {
            int toReturn;
            switch (letter)
            {
                case "A":
                    toReturn = 0;
                    break;
                case "E":
                    toReturn = 13;
                    break;
                case "I":
                    toReturn = 24;
                    break;
                case "O":
                    toReturn = 29;
                    break;
                case "U":
                    toReturn = 36;
                    break;
                default:
                    toReturn = -1;
                    break;
            }
            return toReturn;
        }

        int GetLetterSize(string letter)
        {
            int toReturn;
            switch (letter)
            {
                case "A":
                    toReturn = 13;
                    break;
                case "E":
                    toReturn = 11;
                    break;
                case "I":
                    toReturn = 5;
                    break;
                case "O":
                    toReturn = 7;
                    break;
                case "U":
                    toReturn = 5;
                    break;
                default:
                    toReturn = -1;
                    break;
            }
            return toReturn;
        }
    }
}