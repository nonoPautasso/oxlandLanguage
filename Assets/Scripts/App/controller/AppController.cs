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
            ViewController.instance.LoadScene("MainMenu");
        }

		public void StartLevel(int level){			
			SoundManager.instance.StopMusic ();
			ViewController.instance.LoadScene (GetLevelName(appModel.CurrentLevel));
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
            DestroyLvl(appModel.CurrentLevel);
            StartLevel(appModel.CurrentLevel);
        }

        internal void NextLvl(){
            DestroyLvl(appModel.CurrentLevel);
            appModel.NextLvl();
            StartLevel(appModel.CurrentLevel);
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
            GameObject.Instantiate(inGameMenu);
        }


		internal void ShowInstructions()
		{
			GameObject.Instantiate (instructions[appModel.CurrentLevel-1]);

			//play sound
		}
    }

}