using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Levels.Castlenumbers6
{
    public class AnswerAnimation : MonoBehaviour
    {

        public static AnswerAnimation instance;

        public Image rightAnswer;
        public Image wrongAnswer;

        void Start()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        internal void PlayAnimation(bool right)
        {

            if (right)
            {
                rightAnswer.gameObject.SetActive(true);
                rightAnswer.GetComponent<Animator>().SetBool("play", true);
            }
            else
            {
                wrongAnswer.gameObject.SetActive(true);
                wrongAnswer.GetComponent<Animator>().SetBool("play", true);
            }
        }  
        
    }
}
