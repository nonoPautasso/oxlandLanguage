using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.App{

	/*
 * Manages all game load and unload functions
 */
	public class ViewController : MonoBehaviour {

		public static ViewController instance;
		private string[] levels;
		private string[] instructions;

		private GameObject instructionsScreen;
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
			levels = new string[]{"Vowels","VowelsOral","StartWithVowel","CompleteVowel"
				,"ABCOrder","ABCWords","ABCBonus","Syllables", "WriteWords","MayusMin",
				"CreateSentence","SplitSentences"
				};
			instructions = new string[]{"VowelsInstructions","VowelsOralInstructions","StartWithVowelInstructions",
				"CompleteVowelInstructions","ABCOrderInstructions","ABCWordsInstructions","ABCBonusInstructions",
				"VowelsInstructions","VowelsInstructions","VowelsInstructions","VowelsInstructions","VowelsInstructions",
				"VowelsInstructions","VowelsInstructions"};
			LoadCover();
		}

		private GameObject LoadPrefab(string name)
		{

			return Resources.Load<GameObject>("Prefabs/" + name);
		}


		public void ChangeCurrentObject(GameObject newObject)
		{
			GameObject child = Instantiate(newObject);
			FitObjectToScene(child);
			if(currentGameObject)
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



		public void LoadLevel(int level){
			Debug.Log (levels[level-1]);
			ChangeCurrentObject (LoadPrefab("Levels/"+levels [level-1]));
		}

		public void LoadNameScreen(){
			ChangeCurrentObject (LoadPrefab("NameScreen"));
		}

		public void LoadCover(){
			ChangeCurrentObject (LoadPrefab("Cover"));
		}

		public void LoadModeScreen(){
			ChangeCurrentObject (LoadPrefab("ModeScreen"));
		}

		public void LoadMainMenu(){
			ChangeCurrentObject (LoadPrefab("MainMenu"));
		}

		public void LoadLevelComplete(){
			ChangeCurrentObject (LoadPrefab("LevelComplete"));
		}

		public void LoadSettings(){
			ChangeCurrentObject (LoadPrefab("SettingsScreen"));
		}

		public void LoadMetrics(){
			ChangeCurrentObject (LoadPrefab("MetricsScreen"));
		}

		public GameObject CurrentGameObject {
			get {
				return currentGameObject;
			}
		}

		internal void HideInstructions(){
			Destroy(instructionsScreen);
		}


		internal void ShowInstructions()
		{
			instructionsScreen = Instantiate(LoadPrefab("Explanations/"+instructions[AppController.instance.GetCurrentLevel()-1]));
			FitObjectToScene(instructionsScreen);
		}
	}
}