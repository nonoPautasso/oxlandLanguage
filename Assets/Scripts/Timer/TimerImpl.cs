using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Timer
{
    public class TimerImpl : MonoBehaviour
    {
        public static TimerImpl instance;

        private float lapsedSecods;
        private float initTime;

        void Awake(){
            if (instance == null) instance = this;
            else if (instance != this) Destroy(this);

            DontDestroyOnLoad(this);
        }

        public void InitTimer()
        {
            lapsedSecods = 0;
            initTime = Time.time;
        }

        public void Pause(){
            lapsedSecods += Time.time - initTime;
        }

        public void Resume()
        {
            initTime = Time.time;
        }

        public void FinishTimer() {
            lapsedSecods += Time.time - initTime;
        }

        public int GetLapsedSeconds(){
            return (int)lapsedSecods;
        }


        private void SOUT(){
            Debug.Log("Current time: " + lapsedSecods);
        }
        
    }
}
