using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts.Timer;

namespace Assets.Scripts.App{

public class AppController : AppElement {

	public static AppController instance;
	public AppModel appModel;
    public GameObject inGameMenu;
	public GameObject[] instructions;

	private GameObject inGameMenuScreen;
	private GameObject instructionsScreen;

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
			ViewController.instance.LoadLevel (GetLevelIndex(appModel.CurrentLevel));
		}

		private int GetLevelIndex(int level)
		{
			if (level < 6)
			{
				return level-1;
			}
			else if (level < 12)
			{
				return 5;
			}
			else {
				if (level==12)
					return 6; 
				else
					return 7; 
				
			}

		}



        private string GetLevelName(int level)
        {
            if (level < 6)
            {
                switch (level)
                {
                    case 1: return "SayTheNumberLevel";
                    case 2: return "OrderNumbers";
                    case 3: return "MonsterCount";
                    case 4: return "MonsterCreator"; 
                    case 5: return "Patterns";
                }
            }
            else if (level < 12)
            {
                return "CastilloNumeros";
            }
            else {
                switch (level)
                {
                    case 12: return "ObjectSum"; 
                    case 13:
                    case 14:
                    case 15:
                    case 16: return "Gato"; 
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
//            DestroyLvl(appModel.CurrentLevel);
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


        internal void ShowInGameMenu(){
            TimerImpl.instance.Pause();
			inGameMenuScreen = Instantiate(inGameMenu);
//			GameObject.Instantiate(inGameMenu);
        }

		internal void HideInGameMenu(){
			Destroy(inGameMenuScreen);
		}

		internal void HideInstructions(){
			Destroy(instructionsScreen);
		}


		internal void ShowInstructions()
		{
			instructionsScreen = Instantiate(instructions[appModel.CurrentLevel-1]);
//			GameObject.Instantiate (instructions[appModel.CurrentLevel-1]);

			//play sound
		}
    }

}