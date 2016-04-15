﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.App;

namespace Assets.Scripts.Common {
	public class Word : IEquatable<Word> {
		private int spriteNumber;

		private AudioClip audio;
		
		public Word(AudioClip audio, int spriteNumber) {
			this.audio = audio;
			this.spriteNumber = spriteNumber;
		}

		public string Name(){
			return audio.name;
		}

		public int SpriteNumber(){
			return spriteNumber;
		}

		public bool Equals (Word other) {
			return Name() == other.Name();
		}

		public Sprite Sprite () {
			Sprite[] s = Resources.LoadAll<Sprite>("Sprites/Spanish/ObjectsCastellano");
			return s[spriteNumber];
		}

		public void PlayWord () {
			SoundManager.instance.PlayClip(audio);
		}
	}
}