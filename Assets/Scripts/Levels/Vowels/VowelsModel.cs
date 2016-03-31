using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts.Levels.Vowels
{
    public class VowelsModel : LevelModel
    {
        private bool[] revealedLetters;
        private int numRevealedLetters;
        private int numVowelsInBubbles;


        private static string[] vowels;
        private static string[] consonants;
        private Queue<string> vowelQueue;
        private Queue<string> consonantQueue;

        public override void StartGame()
        {
            numRevealedLetters = 0;
            numVowelsInBubbles = 0;
            vowels = new string[] { "A", "E", "I", "O", "U" };
            vowelQueue = new Queue<string>();
            consonants = new string[]{"B","C","D","F","G","H","J",
                "K","L","M","N", "Ñ","P","Q","R","S","T","V","W","X","Y","Z"};
            consonantQueue = new Queue<string>();
            revealedLetters = new bool[5];
            // Places all vowels in the vowels array into a queue in a random order
            while (vowels.Length != 0)
            {
                int index = UnityEngine.Random.Range(0, vowels.Length);
                vowelQueue.Enqueue(vowels[index]);
                vowels = RemoveFromArray(vowels, index);
            }
            // Places all consonants in the consonants array into a queue in a random order
            while (consonants.Length != 0)
            {
                int index = UnityEngine.Random.Range(0, consonants.Length);
                consonantQueue.Enqueue(consonants[index]);
                consonants = RemoveFromArray(consonants, index);
            }

        }
        /*
        From an element from an array and returns the new array
        */
        private string[] RemoveFromArray(string[] vowels, int index)
        {
            string[] toReturn = new string[vowels.Length - 1];
            for (int i = 0; i < index; i++)
            {
                toReturn[i] = vowels[i];
            }
            for (int i = index + 1; i < vowels.Length; i++)
            {
                toReturn[i - 1] = vowels[i];
            }
            return toReturn;
        }

        public override void NextChallenge()
        {
            revealedLetters = new bool[5];
        }

        public override void RequestHint()
        {
            throw new System.NotImplementedException();
        }


        /*
        Return an array of letters and their index for revealing in the answer
        */
        public DataPair<string, int>[] RequestHintInfo()
        {
            int sizeCounter = 0;
            for (int i = 0; i < revealedLetters.Length; i++)
            {
                if (!revealedLetters[i]) sizeCounter++;
            }
            DataPair<string, int>[] toReturn = new DataPair<string, int>[sizeCounter];
            int j = 0;
            for (int i = 0; i < revealedLetters.Length; i++)
            {
                if (!revealedLetters[i])
                {
                    string letter = "";
                    switch (i)
                    {
                        case 0:
                            letter = "A";
                            break;
                        case 1:
                            letter = "E";
                            break;
                        case 2:
                            letter = "I";
                            break;
                        case 3:
                            letter = "O";
                            break;
                        case 4:
                            letter = "U";
                            break;

                    }
                    toReturn[j++] = new DataPair<string, int>(letter, i);
                }
            }
            return toReturn;
        }

        /*
        Returns the index of the clicked vowel. Returns -1 if the letter is a consonant
        */
        public int IndexOfRevealedVowel(string letter)
        {
            int toReturn;
            if (letter.Equals("A") && !revealedLetters[0])
                toReturn = 0;
            else if (letter.Equals("E") && !revealedLetters[1])
                toReturn = 1;
            else if (letter.Equals("I") && !revealedLetters[2])
                toReturn = 2;
            else if (letter.Equals("O") && !revealedLetters[3])
                toReturn = 3;
            else if (letter.Equals("U") && !revealedLetters[4])
                toReturn = 4;
            else
                toReturn = -1;

            return toReturn;
        }

        /*
        Sets that the letter in the position has been revealed
        */
        public void RevealLetter(int pos)
        {
            revealedLetters[pos] = true;
            numRevealedLetters++;
        }

        /*
        Returns a vowel if there are vowels remaining based on a random number, else returns a consonant
        */
        public string GenerateLetter()
        {
            string letter;
            if (CheckGenerateVowel() && vowelQueue.Count() != 0)
            {
                letter = GenerateVowel();
                numVowelsInBubbles++;
            }
            else
                letter = GenerateConsonant();
            return letter;
        }
        /*
        Random to decide if a vowel or consonant is returned. 75% change of consonant, 25% chance of vowel
        */
        private bool CheckGenerateVowel()
        {
            if (UnityEngine.Random.value <= 0.75 && numVowelsInBubbles != 0)
                return false;
            else
                return true;
        }

        private string GenerateVowel()
        {
            return vowelQueue.Dequeue();
        }

        private string GenerateConsonant()
        {
            return consonantQueue.Dequeue();
        }

        /*
        Returns the letter to its corresponding queue. If it is a vowel, the number of vowels in bubbles variable is decreased
        */
        public void ReturnToStack(string letter)
        {
            if (!(letter.Equals("A") || letter.Equals("E") || letter.Equals("I") || letter.Equals("O") || letter.Equals("U")))
                consonantQueue.Enqueue(letter);
            else
            {
                switch (letter)
                {
                    case "A":
                        if (!revealedLetters[0]) vowelQueue.Enqueue(letter);
                        break;
                    case "E":
                        if (!revealedLetters[1]) vowelQueue.Enqueue(letter);
                        break;
                    case "I":
                        if (!revealedLetters[2]) vowelQueue.Enqueue(letter);
                        break;
                    case "O":
                        if (!revealedLetters[3]) vowelQueue.Enqueue(letter);
                        break;
                    case "U":
                        if (!revealedLetters[4]) vowelQueue.Enqueue(letter);
                        break;

                }
                numVowelsInBubbles--;
            }
        }

        public int GetNumRevealedLetters()
        {
            return numRevealedLetters;
        }
    }
}