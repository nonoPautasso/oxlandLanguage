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

        private DataPair<Sprite[], AudioClip[]>[] letters;

        public void InitView()
        {
            letters = new DataPair<Sprite[], AudioClip[]>[5];
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

        public void SetResources(DataTrio<Sprite[], AudioClip[], int> resources)
        {
            letters[resources.Thd()] = new DataPair<Sprite[], AudioClip[]>(resources.Fst(), resources.Snd());
        }

        public void Next(int letter)
        {
			Debug.Log ("Value received by View: " + letter);
			Debug.Log ("Audio size before removing: " + letters [letter].Snd ().Length);
			Debug.Log ("Audio size before removing: " + letters [letter].Fst ().Length);
            currentAudioClip = letters[letter].Snd()[0];
            currentObjectImage.sprite =  letters[letter].Fst()[0];
            letters[letter].SetSnd(RemoveFirst(letters[letter].Snd()));
            letters[letter].SetFst(RemoveFirst(letters[letter].Fst()));
			Debug.Log ("Audio size after removing: " + letters [letter].Snd ().Length);
			Debug.Log ("Audio size after removing: " + letters [letter].Fst ().Length);
            ResetHint();
        }

        static X[] RemoveFirst<X>(X[] array)
        {
            X[] toReturn = new X[array.Length - 1];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = array[i + 1];
            }
            return toReturn;
        }

        public void PlaySound()
        {
            SoundManager.instance.PlayClip(currentAudioClip);
        }

        public void PlaySoundClick()
        {
            PlaySoundClic();
        }

        public void PlaySoundRight()
        {
            PlayRightSound();
        }

        public void PlaySoundWrong()
        {
            PlayWrongSound();
        }
    }
}