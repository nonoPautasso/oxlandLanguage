using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.App{

/*
 * Manages all game load and unload functions
 */
public class ViewController : MonoBehaviour {

		public static ViewController instance;

        //Awake is always called before any Start functions
        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(transform.root.gameObject);
			LoadScene("Cover");
        }

        public void LoadScene(string sceneName){
			SceneManager.LoadScene (sceneName);
		}
			
}
}