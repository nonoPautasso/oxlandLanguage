using UnityEngine;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.App{

public class AppController : AppElement {

	public static AppController instance;
	public AppModel appModel;
    
	

	void Awake()
	{
		if (instance == null)           
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
			
		appModel = new AppModel ();
		DontDestroyOnLoad(gameObject);
		
	}

        internal void MainMenu(){
			DestroyLvl (appModel.CurrentLevel);
			ViewController.instance.LoadMainMenu();
        }

		public void StartLevel(){			
			SoundManager.instance.StopMusic ();
			ViewController.instance.LoadLevel (appModel.CurrentLevel);
		}





        private string GetLevelName(int level)
        {
			
            if (level < 6)
            {
                switch (level)
                {
				case 1: return "Vowels";
				case 2: return "VowelsOral";
				case 3: return "StartWithVowel";
				case 4: return "CompleteVowel"; 
				case 5: return "ABCOrder";
                }
            }
            else if (level < 12)
            {
				switch (level)
				{
				case 6: return "ABCWords";
				case 7: return "ABCBonus"; 
					
				case 8: return "Syllables";
				case 9: return "WriteWords";
				case 10: return "MayusMin";
				case 11: return "CreateSentence";
					
				}
            }
            else {
                switch (level)
                {
                    case 12: return "SplitSentences"; 
                    case 13:
                    case 14:
                    case 15:
                    case 16: return ""; 
                }
            }
            return "Error";
        }

        internal void RestartLvl(){
			
//            DestroyLvl(appModel.CurrentLevel);
//            StartLevel(appModel.CurrentLevel);
			StartLevel();
        }

        internal void NextLvl(){
	       appModel.NextLvl();
            StartLevel();
        }

        private void DestroyLvl(int lvl)
        {
            Destroy(GameObject.Find(GetLevelName(lvl)));
        }

        internal void SetCurrentLevel(int v) {
            appModel.SetCurrentLevel(v);
        }

        internal bool IsMaxLevelPossible() {
            return appModel.CurrentLevel == appModel.MaxLevelPossible;
        }

		public int GetMaxLevelPossible(){
			return appModel.MaxLevelPossible;
		}

		public int GetCurrentLevel(){
			return appModel.CurrentLevel;
		}

		public void SetMaxLevelPossible(int level){
			appModel.SetMaxLevelPossible (level);
		}
			
       

    }

}