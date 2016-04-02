using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;
using UnityEngine.UI;
using Assets.Scripts.Levels.Vowels;

namespace Assets.Scripts.Levels.VowelsOral
{
    public class VowelsOralView : LevelView
    {

        public Button[] vowelButtons;

        public Image currentObjectImage;
        public AudioClip currentAudioClip;

        private DataPair<Image[], AudioClip[]>[] letters;

        public void InitView()
        {
            letters = new DataPair<Image[], AudioClip[]>[5];
        }

        public override void EndGame()
        {
            throw new NotImplementedException();
        }

        public override void ShowHint()
        {
            throw new NotImplementedException();
        }

        public void ShowHints(DataPair<int, int> letterIndices)
        {
            DisableHint();
            vowelButtons[letterIndices.Fst()].interactable = false;
            vowelButtons[letterIndices.Snd()].interactable = false;
        }

        public void ResetHint()
        {
            EnableHint();
            for (int i = 0; i < vowelButtons.Length; i++)
            {
                vowelButtons[i].interactable = true;
            }
        }

        public void SetResources(DataTrio<Image[], AudioClip[], int> resources)
        {
            letters[resources.Thd()] = new DataPair<Image[], AudioClip[]>(resources.Fst(), resources.Snd());
        }

        public void Next(int letter)
        {
            currentAudioClip = letters[letter].Snd()[0];
            currentObjectImage = letters[letter].Fst()[0];
            RemoveFirst(letters[letter].Snd());
            RemoveFirst(letters[letter].Fst());
        }

        static void RemoveFirst<X>(X[] array)
        {
            X[] toReturn = new X[array.Length - 1];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = array[i + 1];
            }
            array = toReturn;
        }

    }
}