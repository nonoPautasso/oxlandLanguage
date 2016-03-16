using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.App{
    public class SoundManager : MonoBehaviour{
	    public static SoundManager instance;


	    public AudioClip failureSound;
	    public AudioClip rightAnswerSound;
	    public AudioClip levelCompleteSound;
	    public AudioClip music;
	    public AudioClip clickSound;


	    public AudioSource mySource;
	    public AudioSource musicSource;
	    public List<List<AudioClip>> instructionSounds;


    //Awake is always called before any Start functions
        void Awake()
    {
	    if (instance == null)           
		    instance = this;
	    else if (instance != this)
		    Destroy(gameObject);


	    DontDestroyOnLoad(transform.root.gameObject);
    }


        public void PlayClip(AudioClip myClip)
    {
	    mySource.clip = myClip;
	    mySource.Play();
    }

        public void PlayFailureSound()
    {
	    if (SettingsController.instance.SfxOn()) {
		    mySource.clip = failureSound;
		    mySource.Play();
	    }
	
    }

        public void PlayClicSound()
    {
	    if (SettingsController.instance.SfxOn()) {
		    mySource.clip = clickSound;
		    mySource.Play();
	    }

    }

        public void PlayRightAnswerSound()
    {
	    if (SettingsController.instance.SfxOn()) {
		    mySource.clip = rightAnswerSound;
		    mySource.Play();
	    }
    }

    public void PlayLevelCompleteSound()
    {
	    if (SettingsController.instance.SfxOn()) {
		    mySource.clip = levelCompleteSound;
		    mySource.Play();
	    }
    }

    public void PlayInstruction(int level, int language)
    {
	    if (SettingsController.instance.SfxOn()) {
		    mySource.clip = instructionSounds[language][level];
		    mySource.Play();         	
	    }
    }

    public void PlayMusic()
   	{
			if (SettingsController.instance.MusicOn()&&!musicSource.isPlaying) {
				musicSource.clip = music;
		    	musicSource.Play();         	
	    }
    }

        public void StopMusic()
    {
	    musicSource.Stop();
    }

        public void StopSound()
            {
	            mySource.Stop();
            }

    }

}