using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{

	public class CatWrongAnswerAnimation : MonoBehaviour
	{

//		public CatView view;

		// Use this for initialization
		void Start()
		{
			gameObject.SetActive(false);
		}   

		public void OnWrongAnswerAnimationEnd()
		{
			gameObject.GetComponent<Animator>().SetBool("play", false);
			gameObject.SetActive(false);
			CatController.instance.OnWrongAnswerAnimationEnd();
		}

		public void PlayWrongSound()
		{
			SoundManager.instance.PlayFailureSound();
//			view.PlayWrongSound();
		}
	}
}
