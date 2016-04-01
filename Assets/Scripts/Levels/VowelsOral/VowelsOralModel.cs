using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;
using Assets.Scripts.Levels.Vowels;

namespace Assets.Scripts.Levels.VowelsOral
{
    public class VowelsOralModel : LevelModel
    {

        private string currentCorrectLetter;


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

        public DataPair<int,int> ChooseTwoRandomOpenVowels()
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