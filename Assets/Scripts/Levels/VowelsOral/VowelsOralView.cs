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

		static Color32 wrongColor = new Color32 (251,96,96,255);

        public Toggle[] vowelToggles;
		Toggle selectedToggle;


		public Button checkButton,nextButton,soundButton;
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
			PlaySoundClick ();
            vowelToggles[letterIndices.Fst()].interactable = false;
            vowelToggles[letterIndices.Snd()].interactable = false;
        }

        public void ResetHint()
        {
            EnableHint();
            for (int i = 0; i < vowelToggles.Length; i++)
            {
                vowelToggles[i].interactable = true;
            }
        }

        public void SetResources(DataTrio<Sprite[], AudioClip[], int> resources)
        {
            letters[resources.Thd()] = new DataPair<Sprite[], AudioClip[]>(resources.Fst(), resources.Snd());
        }
			
		public void Next(int letter)
        {
			currentAudioClip = letters[letter].Snd()[0];
			currentObjectImage.sprite =  letters[letter].Fst()[0];
			currentObjectImage.GetComponentInChildren<Text> ().text = currentAudioClip.name.ToUpper();
			PaintFirstLetter ();
			currentObjectImage.GetComponentInChildren<Text> ().enabled = false;
			letters[letter].SetSnd(RemoveFirst(letters[letter].Snd()));
			letters[letter].SetFst(RemoveFirst(letters[letter].Fst()));

            ResetHint();
        }

		public void EnableCheckButton(){
			checkButton.interactable = true;
			if (selectedToggle)
				selectedToggle.GetComponent<Image> ().color = Color.white;
		}

		public void DisableCheckButton(){
			checkButton.interactable = false;
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

		public void PrepareForNext(){


			hintBtn.interactable = false;
			soundButton.interactable = false;
			for (int i = 0; i < vowelToggles.Length; i++)
			{
				vowelToggles [i].enabled =false;
			}

			currentObjectImage.GetComponentInChildren<Text>().enabled = true;
			checkButton.gameObject.SetActive(false);
			nextButton.gameObject.SetActive(true);
		}

		public void EnableAllButtons(){
			hintBtn.interactable = true;
			soundButton.interactable = true;
			for (int i = 0; i < vowelToggles.Length; i++)
			{
				vowelToggles [i].enabled =true;
			}

			checkButton.gameObject.SetActive(true);
			checkButton.interactable = false;
			nextButton.gameObject.SetActive(false);
		}

        public void PlaySound()
        {
			
			SpeakerScript.instance.PlaySound(currentAudioClip.name.Length < 6 ? 1 : 2);
        }

       

        public void PlaySoundRight()
        {
            PlayRightSound();
        }

        public void PlaySoundWrong()
        {
            PlayWrongSound();
        }

		public void SetSelectedToggle(Toggle toggle){
			selectedToggle = toggle;
		}

		public void PaintWrongToggle(){
			selectedToggle.GetComponent<Image> ().color = wrongColor;
		}

		public void PaintFirstLetter(){
			
			string oldString = currentObjectImage.GetComponentInChildren<Text>().text;
			string newString ="";

			string start = oldString.Substring (0, 1);
			string end = oldString.Substring (1, oldString.Length-1);

			newString = "<color=green>" +start + "</color>"+end;

			currentObjectImage.GetComponentInChildren<Text> ().text = newString;
		}
   }
}