using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;

public class NameScreenController : MonoBehaviour {

	public static NameScreenController instance;
	public NameScreenView nameScreenView;


	void Start()
	{
		
		if (instance == null)
			
			instance = this;
		
		else if (instance != this)
			Destroy(gameObject);
	}


	public void SaveUsername(string username){
		SettingsController.instance.SwitchName (username);
        ViewController.instance.LoadScene("ModeScreen");
    }

    internal void GoBack()
    {
        ViewController.instance.LoadScene("Cover");
    }

    internal static void PlayClicSound()
    {
        SoundManager.instance.PlayClicSound();
    }
}
