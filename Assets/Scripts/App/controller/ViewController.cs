using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.App{

/*
 * Manages all game load and unload functions
 */
public class ViewController : MonoBehaviour {

		public static ViewController instance;
		public GameObject cover,nameScreen,modeScreen,mainMenu;
		public GameObject[] levels;

		public GameObject viewPanel;
		private GameObject currentGameObject;

        //Awake is always called before any Start functions
        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(transform.root.gameObject);
			LoadInitialCover();
        }

		private void LoadInitialCover(){
			GameObject child = Instantiate(cover);
			currentGameObject = child;   
			FitObjectToScene(child);
		}

		public void ChangeCurrentObject(GameObject newObject)
		{
			GameObject child = Instantiate(newObject);
			FitObjectToScene(child);

			Destroy(currentGameObject);
			currentGameObject = child;            
		}

		private void FitObjectToScene(GameObject child)
		{
			child.transform.SetParent(viewPanel.transform, true);
			child.transform.localPosition = Vector3.zero;
			child.GetComponent<RectTransform>().offsetMax = Vector2.zero;
			child.GetComponent<RectTransform>().offsetMin = Vector2.zero;
			child.transform.localScale = Vector3.one;
		}

		public void LoadScene(string sceneName){
			SceneManager.LoadScene (sceneName);
		}

		public void LoadLevel(int level){
			ChangeCurrentObject (levels [level]);
		}

		public void LoadNameScreen(){
			ChangeCurrentObject (nameScreen);
		}

		public void LoadCover(){
			ChangeCurrentObject (cover);
		}

		public void LoadModeScreen(){
			ChangeCurrentObject (modeScreen);
		}

		public void LoadMainMenu(){
			ChangeCurrentObject (mainMenu);
		}
	
}
}