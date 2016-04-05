using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.Vowels
{
    public class VowelsView : LevelView
    {
        public Button[] revealedLetters;
        public Button[] bubbleLetters;

        void Start()
        {
            /*   Debug.Log("Creating arrays");
               revealedLetters = new Button[] { GameObject.Find("upVowel1Button").GetComponent<Button>(),
               GameObject.Find("upVowel2Button").GetComponent<Button>(),
               GameObject.Find("upVowel3Button").GetComponent<Button>(),
               GameObject.Find("upVowel4Button").GetComponent<Button>(),
               GameObject.Find("upVowel5Button").GetComponent<Button>()};
               bubbleLetters = new Button[] { GameObject.Find("letter1Button").GetComponent<Button>(),
               GameObject.Find("letter2Button").GetComponent<Button>(),
               GameObject.Find("letter3Button").GetComponent<Button>(),
               GameObject.Find("letter4Button").GetComponent<Button>()};
               Debug.Log("Revealed Letters array size: " + revealedLetters.Length);
               Debug.Log("Bubble Letters array size: " + bubbleLetters.Length);
               */
        }

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

        public override void EndGame()
        {
            throw new System.NotImplementedException();
        }

        /*
        Shows a letter in a position as normal text
        */
        public void ShowLetter(int pos, string letter)
        {
            Debug.Log("Showing letter:" + letter + " in position " + pos.ToString());
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
            bubble.GetComponent<Animator>().SetTime(0);
            bubble.GetComponentInChildren<Text>().text = letter;
        }
    }
}