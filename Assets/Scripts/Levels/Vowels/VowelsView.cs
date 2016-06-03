﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.Vowels
{
    public class VowelsView : LevelView
    {
        public Button[] revealedLetters;
        public Button[] bubbleLetters;

        public override void ShowHint()
        {
            throw new System.NotImplementedException();
        }

        /*
        Shows a letter in a position as hint text
        */
        public void RevealHint(int pos, string letter)
        {
            (revealedLetters[pos].GetComponentInChildren<Text>() as Text).text = letter;
            (revealedLetters[pos].GetComponentInChildren<Text>() as Text).color = new Color(0, 0, 0, 0.5f);
        }

		/*
        Hides a letter in a position as hint text
        */
		public void HideHint(int pos, string letter)
		{
			(revealedLetters[pos].GetComponentInChildren<Text>() as Text).text = letter;
			(revealedLetters[pos].GetComponentInChildren<Text>() as Text).color = new Color(0, 0, 0, 0);
		}

        public override void EndGame()
        {
            throw new System.NotImplementedException();
        }

        /*
        Shows a letter in a position as normal text
        */
        public void ShowLetter(int pos, string letter)
        {
            (revealedLetters[pos].GetComponentInChildren<Text>() as Text).text = letter;
            (revealedLetters[pos].GetComponentInChildren<Text>() as Text).color = new Color(0, 0, 0, 1f);
        }

        /*
        Sets the letter of a bubble given a position
        */
        public void SetLetterButton(int pos, string letter)
        {
            (bubbleLetters[pos].GetComponentInChildren<Text>() as Text).text = letter;
        }

        /*
        Resets the animation to the beginning and changes the letter of the bubble
        */
        public void ResetAnimation(Button bubble, string letter)
        {
			bubble.GetComponent<Image> ().color = Color.white;
            bubble.GetComponent<Animator>().SetTime(0);
            bubble.GetComponentInChildren<Text>().text = letter;
        }

        public void PlaySoundClick()
        {
            PlaySoundClick();
        }

        public void PlaySoundRight()
        {
            PlayRightSound();
        }

        public void PlaySoundWrong()
        {
            PlayWrongSound();
        }

		public void SetBubbleCorrect(Button bubble) {
			bubble.GetComponent<Image> ().color = Color.green;
		}

		public void SetBubbleWrong(Button bubble) {
			bubble.GetComponent<Image> ().color = Color.red;
		}

    }
}