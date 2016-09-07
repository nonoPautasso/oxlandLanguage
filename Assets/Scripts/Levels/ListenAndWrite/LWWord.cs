using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using I18N;

namespace Assets.Scripts.Levels.ListenAndWrite
{

    public class LWWord 
    {
        private AudioClip audio;
        private string audioPath;
        private string text;
		private Sprite sprite;
        private List<char> lettersHint;
        
        public LWWord(string audioPath, string text,  JSONArray lettersHint)
        {
            this.audioPath = audioPath;
            this.text = text;
            this.lettersHint = new List<char>(lettersHint.Count); 
            for (int i = 0; i < lettersHint.Count; i++)
            {
                this.lettersHint.Add(lettersHint[i].Value.ToLower()[0]);
            }
        }
        
        internal void Play() {
			if (audio == null) audio = (Resources.Load( "Audio/" + I18n.Msg ("words.locale") + "/"+text[0]+"Words/"+ audioPath.Substring(0, audioPath.Length-4)) as AudioClip);
            Debug.Log(text);
			SoundManager.instance.PlayClip (audio);
           
        }

        internal List<char> GetHint()
        {
            return lettersHint;
        }

        internal string GetText()
        {
            return text;
        }

		internal Sprite GetSprite()
		{
			return Words.GetWord(text).Sprite();
		}
    }
}