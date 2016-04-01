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

        public void InitModel()
        {

        }

        public DataTrio<Image[], AudioClip[], int> LoadResources(string letter)
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
                    intValue = 0;
                    break;
            }
            DataTrio<Image[], AudioClip[], int> toReturn;
            AudioClip[] clips = Resources.LoadAll("Audio/Spanish/" + letter + "Words") as AudioClip[];
            Image[] images = Resources.LoadAll("Sprites/" + letter + "Images") as Image[];
            toReturn = new DataTrio<Image[], AudioClip[], int>(new Image[clips.Length], new AudioClip[clips.Length], intValue);
            for (int i = 0; i < clips.Length; i++)
            {
                toReturn.Fst()[i] = images[i];
                toReturn.Snd()[i] = clips[i];
            }
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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}