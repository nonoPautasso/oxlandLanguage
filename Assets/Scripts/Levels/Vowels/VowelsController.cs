using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Levels.Vowels
{
    public class VowelsController : LevelController
    {
        public VowelsView view;

        VowelsModel model;

        void Start()
        {
            InitGame();
        }

        public override void RestartGame()
        {
            InitGame();
            view.EnableHint();
        }

        public override void InitGame()
        {
            // Creates model
            model = new VowelsModel();
            // Basically sets initial values for model
            model.StartGame();
            // Sets all viewable letters to blank
            for (int i = 0; i < 5; i++)
            {
                view.ShowLetter(i, "");
            }
            // Generates a letter for each bubble
            for (int i = 0; i < 4; i++)
            {
                view.SetLetterButton(i, model.GenerateLetter());
            }
        }

        public override void NextChallenge()
        {

        }

        public override void ShowHint()
        {
            // Asks model for all the vowels that haven't been revealed + their index
            DataPair<string, int>[] letters = model.RequestHintInfo();
            for (int i = 0; i < letters.Length; i++)
            {
                // Calls on the view to show the letter as hint
                view.RevealHint(letters[i].Snd(), letters[i].Fst());
            }
            view.DisableHint();
        }


        public void SubmitLetter(Text letter)
        {
            // Returns the index in the revealed letter space for the clicked letter
            int index = model.IndexOfRevealedVowel(letter.text);
            if (index != -1)
            {
                // Clicked letter is a vowel
                view.ShowLetter(index, letter.text);    // Tells view to show the letter at appropriate index
                model.RevealLetter(index); // Tells model to record that the vowel was revealed
                LogAnswer(true);
            }
            else LogAnswer(false);
            if (model.GetNumRevealedLetters() == 5) EndGame(model.MinSeconds,model.PointsPerSecond, model.PointsPerError);
        }

        public void ResetBubble(Button bubble)
        {
            string letter = model.GenerateLetter(); // Generate a new letter for the bubble
            model.ReturnToStack(bubble.GetComponentInChildren<Text>().text);    // Returns the previous letter to the model stack
            view.ResetAnimation(bubble, letter);    // Tell view to make bubble start at bottom again with new letter
        }

    }
}