using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.App;
using I18N;

namespace Assets.Scripts.Common {
	public class Word : IEquatable<Word> {
		private int spriteNumber;
		private string realWriteName;

		private AudioClip audio;
		
		public Word(AudioClip audio, int spriteNumber, string realWriteName = null) {
			this.audio = audio;
			this.spriteNumber = spriteNumber;
			this.realWriteName = realWriteName;
		}

		public string Name(){
			return realWriteName == null ? audio.name.ToUpper () : realWriteName.ToUpper ();
		}

		public string AudioName(){
			return audio.name.ToUpper ();
		}

		public int SpriteNumber(){
			return spriteNumber;
		}

		public bool Equals (Word other) {
			return Name() == other.Name();
		}

		public Sprite Sprite () {
			Sprite[] s = Resources.LoadAll<Sprite>("Sprites/" + I18n.Msg ("words.locale") + "/Separadas/" + StartLetter ().ToLower ());
			return s[spriteNumber];
		}

		public void PlayWord () {
			SoundManager.instance.PlayClip(audio);

		}

		public string StartLetter () {
			return AudioName().Substring (0, 1);
//			return StartLetters (1);
		}

		public string StartLetters (int quantity) {
			return Name ().Substring (0, quantity);
		}

		public string EndLetters (int quantity) {
			return Name ().Substring (Name ().Length - quantity, quantity);
		}

		public float AudioLength () {
			return audio.length;
		}

		public AudioClip GetAudio(){
			return audio;
		}
	}
}