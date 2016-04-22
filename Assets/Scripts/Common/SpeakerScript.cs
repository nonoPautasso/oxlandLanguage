using UnityEngine;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
    public class SpeakerScript : MonoBehaviour
    {
        public static SpeakerScript instance;
        public Animator animator;

        void Awake() {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(this);
        }

        // Use this for initialization
        void Start() { 
        }        

        public void PlaySound(int digits)
        {
            animator.SetBool("isPlayingSound", true);        
            Invoke("silence", digits == 1 ? 0.6f : 1.2f);          
        }

        void silence()
        {
           animator.SetBool("isPlayingSound", false);

        }
    }
}