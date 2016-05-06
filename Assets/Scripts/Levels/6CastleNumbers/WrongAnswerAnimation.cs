using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Levels.Castlenumbers6
{

    public class WrongAnswerAnimation : MonoBehaviour
    {

        public CastleView view;

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }   

        public void OnWrongAnswerAnimationEnd()
        {
            gameObject.GetComponent<Animator>().SetBool("play", false);
            gameObject.SetActive(false);
            CastleController.instance.OnWrongAnswerAnimationEnd();
        }

        public void PlayWrongSound()
        {
            view.PlayWrongSound();
        }
    }
}
