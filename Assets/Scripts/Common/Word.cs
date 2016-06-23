using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.App;
using I18N;

namespace Assets.Scripts.Common {
	public class Word : IEquatable<Word> {
		private int spriteNumber;

		private AudioClip audio;
		
		public Word(AudioClip audio, int spriteNumber) {
			this.audio = audio;
			this.spriteNumber = spriteNumber;
		}

		public string Name(){
			return audio.name.ToUpper ();
		}

		public int SpriteNumber(){
			return spriteNumber;
		}

		public bool Equals (Word other) {
			return Name() == other.Name();
		}

		public Sprite Sprite () {
			Sprite[] s = Resources.LoadAll<Sprite>("Sprites/" + I18n.Msg ("words.spritePath"));
			return s[spriteNumber];
		}

		public void PlayWord () {
			SoundManager.instance.PlayClip(audio);

		}

		public string StartLetter () {
			return StartLetters (1);
		}

		public string StartLetters (int quantity) {
			return Name ().Substring (0, quantity);
		}

		public float AudioLength () {
			return audio.length;
		}

		public AudioClip GetAudio(){
			return audio;
		}
	}
}