using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Timer;


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

		public GameObject inGameMenu;
		private GameObject inGameMenuScreen;


		//Awake is always called before any Start functions
		void Awake()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy(gameObject);

			DontDestroyOnLoad(transform.root.gameObject);
			//TODO: UNCOMMENT FOR REAL GAME ORDEN
//			levels = new string[]{"Vowels","VowelsOral","StartWithVowel","CompleteVowel","OracionesPictogramas",
//				"Consonants","ConsonantsOral","ABCWords","CompleteConsonant","ABCOrder",
//				"CountLetters","CombineSounds","SoundsInWords",
//				"LettersComposeWords","OrderLetters", "ListenAndWrite","FindError","CreateSentence",
//				"MayusMin","SplitSentences","IdentifyLettersInWords","ABCBonus","Syllables"
//				};

			//TEMPORARY ORDER FOR TESTING PURPOSES ONLY
			levels = new string[]{"FindError","LettersComposeWords","CreateSentence","IdentifyInitialSound","SoundsInWords","LettersComposeWords",
				"OrderLetters","OrderWordsDictionary","CreateSentence","CompleteConsonant","SoundsInWords",
				"CombineSounds","CombineSounds","SoundsInWords","LettersComposeWords","OrderLetters",
				"ListenAndWrite","FindError","CreateSentence",
				"MayusMin","SplitSentences","IdentifyLettersInWords","ABCBonus","Syllables"
			};

			instructions = new string[]{"VowelsInstructions","VowelsOralInstructions","StartWithVowelInstructions","CompleteVowelInstructions","ABCOrderInstructions",
				"ConsonantsInstructions","ConsonantsOralInstructions","ABCWordsInstructions","VowelsInstructions","ABCOrderInstructions",
				"CountLettersInstructions","VowelsInstructions","VowelsInstructions","VowelsInstructions"};
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
				Destroy(currentGameObject.gameObject);
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
			//Debug.Log (level);
			//Debug.Log (levels[level-1]);
			ChangeCurrentObject (LoadPrefab("Levels/"+levels [level-1]));
		}

		public GameObject LoadInGamePrefab(string screen){
			GameObject loadedPrefab = LoadPrefab(screen);
			GameObject prefabInstance = Instantiate(loadedPrefab);
			FitObjectToScene(prefabInstance);
			return prefabInstance;

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
			Destroy(instructionsScreen.gameObject);
		}

		public int GetLevelAmount(){
			return levels.Length;
		}


		internal void ShowInstructions()
		{
			instructionsScreen = LoadInGamePrefab ("Explanations/"+instructions[AppController.instance.appModel.CurrentLevel-1]);
		}

		internal void ShowInGameMenu(){
			TimerImpl.instance.Pause();
			inGameMenuScreen = LoadInGamePrefab ("InGameMenu");

		}

		internal void HideInGameMenu(){
			Destroy(inGameMenuScreen.gameObject);
		}



	}
}