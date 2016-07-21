using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.ListenAndWrite
{

    public class ListenAndWriteModel : LevelModel {

        private List<LWWord> words;
        private LWWord currentWord;

        public ListenAndWriteModel()
        {
            words = new List<LWWord>();
        }

        public override void NextChallenge()
        {
            int index = Random.Range(0, words.Count);
            currentWord = words[index];
            words.RemoveAt(index);
        }

		public override void RequestHint()
		{
			//throw new NotImplementedException();
		}

        public override void StartGame()
        {
			minSeconds = 90;
			pointsPerError = 1000;
			pointsPerSecond = 100;

        }

        internal void AddWord(LWWord aWord)
        {
            words.Add(aWord);
        }

        internal List<char> GetCurrentHint()
        {
            return currentWord.GetHint();
        }

        internal bool CheckAnswer(string text)
        {
            return currentWord.GetText() == text;
        }

        internal LWWord GetCurrentWord()
        {
            return currentWord;
        }
    }
}