using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Levels.Castlenumbers6
{

    public class RighAnswerAnimation : MonoBehaviour
    {
        public CastleView view;

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }   

        public void OnRightAnswerAnimationEnd()
        {
            gameObject.GetComponent<Animator>().SetBool("play", false);
            gameObject.SetActive(false);
            CastleController.instance.OnRightAnswerAnimationEnd();
        }

        public void PlayRightSound()
        {
            view.PlayRightSound();
        }
        

    }
}
