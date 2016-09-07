using SimpleJSON;
using UnityEngine;
using Assets.Scripts.App;
using System;
using System.Collections.Generic;


namespace Assets.Scripts.Levels.ListenAndWrite
{

    public class ListenAndWriteController : LevelController
    {
        private static ListenAndWriteController listenAndWriteController;
        private ListenAndWriteModel listenAndWriteModel;
        public ListenAndWriteView listenAndWriteView;

        private AudioClip instruction;

        private int TO_WIN;

        private int rounds;

        void Awake()
        {
            if (listenAndWriteController == null) listenAndWriteController = this;
            else if (listenAndWriteController != this) Destroy(this);
        }

        void Start()
        {
            InitGame();
        }


        public override void InitGame()
        {
			MetricsManager.instance.GameStart();

			listenAndWriteModel = new ListenAndWriteModel();
            TextAsset JSONstring = Resources.Load("jsons/listenAndWrite") as TextAsset;
            JSONNode data = JSON.Parse(JSONstring.text);
			JSONNode level = data["listenAndWrite"]["levels"][0];       
//            listenAndWriteView.SetSentence(level["sentence"].Value);
            TO_WIN = 10;
//            instruction = (Resources.Load("ListenAndWrite/" + level["instructionAudio"]) as AudioClip);
            FillWordsList(level);
            listenAndWriteModel.StartGame();
            NextChallenge();
        }

		public override void ShowHint()
        {
            LogHint();
            List<char> hintChars = listenAndWriteModel.GetCurrentHint();
            for (int i = 0; i < hintChars.Count; i++)
            {
                listenAndWriteView.PaintKeyboardButton(convertCharToIndexKeyboard(hintChars[i]));
            }
        }

		public override void RestartGame()
		{
			Start ();
		}

        internal void CheckAnswer(string text)
        {
            rounds++;

             text = text.ToUpper();
            if (listenAndWriteModel.CheckAnswer(text))
            {
                Debug.Log("Correct");
				SoundManager.instance.PlayRightAnswerSound ();
				LogAnswer (true);
				listenAndWriteView.PaintTextBox (new Color32(81,225,148,225));
				if (GameIsEnded())
				{
					EndGame (listenAndWriteModel.MinSeconds, listenAndWriteModel.PointsPerSecond, listenAndWriteModel.PointsPerError);
				} else
				{
					listenAndWriteView.EnableNextButton (true);
					//Invoke("NextChallenge", 1f);
					//NextChallenge();
				}
              
            } else
            {
                Debug.Log("Wrong");
				SoundManager.instance.PlayFailureSound ();
				listenAndWriteView.PaintTextBox (new Color32 (251, 96, 96, 255));
				LogAnswer (false);
            }

           
        }

		public void OnClickNext()
		{
			NextChallenge ();
		}

        private bool GameIsEnded()
        {
            return rounds == TO_WIN;
        }

        private int convertCharToIndexKeyboard(char c)
        {
            int value = (int) c;
            if (c >= 97 && c <= 122) return value - 97;
            if (c == 49) return 28;
            if (c == 50) return 27;
            if (c == 51) return 29;

            if (c == 241) return 26;

            return -1;
        }

        private void FillWordsList(JSONNode level)
        {
            for (int i = 0; i < level["words"].Count; i++)
            {
                listenAndWriteModel.AddWord(new LWWord(level["words"][i]["audio"].Value, level["words"][i]["word"].Value, level["words"][i]["hint"].AsArray));
            }
        }

        public override void NextChallenge()
        {
            listenAndWriteModel.NextChallenge();
			listenAndWriteView.NextChallenge(listenAndWriteModel.GetCurrentWord());
        }

        public static ListenAndWriteController GetController()
        {
            return listenAndWriteController;
        }

        internal void PlayCurrentWord()
        {
            listenAndWriteModel.GetCurrentWord().Play();
        }

        
    }
}