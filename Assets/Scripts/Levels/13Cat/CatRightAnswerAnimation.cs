using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{

	public class CatRightAnswerAnimation : MonoBehaviour
	{
//		public CatView view;

		// Use this for initialization
		void Start()
		{
			gameObject.SetActive(false);
		}   

		public void OnRightAnswerAnimationEnd()
		{
			gameObject.GetComponent<Animator>().SetBool("play", false);
			gameObject.SetActive(false);
			CatController.instance.OnRightAnswerAnimationEnd();
		}

		public void PlayRightSound()
		{
			SoundManager.instance.PlayRightAnswerSound ();
//			view.PlayRightSound();
		}


	}
}
