using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Levels.Vowels;
using Assets.Scripts.Levels.VowelsOral;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.CompleteMissingVowel
{
	public class CompleteMissingVowelModel : LevelModel
	{

		private string easyChar;

		public int wordCount;
		public bool easyMode;

		private string currentWord;

		private string[] hardChars;

		private List<int> lettersUsed;

		int[] letterAmounts;

		private string[] easyWordListSpanish = new string[] {"araña", "arpa", "barba", "botón", "caja",
			"cama", "cámara", "casa", "coco", "flan", "flor", "fósforo", "globo", "gorro", "hongo",
			"horno", "kayak", "kiwi", "lana", "lata", "loro", "leche", "manzana", "mapa", "mono", "moño",
			"moto", "naranja", "ojo", "oso", "pan", "pez", "rana", "raya", "red", "robot", "sal", "sol",
			"taza", "toro", "tren", "tronco", "vaca", "yoyo", "zorro"
		};

		private string[] hardWordListSpanish = new string[] {
			"abeja", "árbol", "arco", "avispa", "ballena", "boca", "bomba", "bota", "bote", "bruja",
			"brújula", "caballo", "cardón", "cartel", "cerdo", "cereza", "cierre", "clavo", "cocodrilo",
			"conejo", "crayón", "dado", "dedo", "delfín", "dientes", "dragón", "elefante", "empanada",
			"enchufe", "escalera", "espada", "espejo", "estrella", "faro", "flauta", "flecha",
			"florero", "fuente", "gallina", "gato", "goma", "grillo", "hilo", "hoja", "iglú",
			"imán", "isla", "jabón", "jarrón", "jaula", "jirafa", "jugo", "ketchup", "kilo", "kiosco",
			"koala", "lámpara", "lápiz", "león", "libro", "limón", "lombriz", "luna", "lupa", "mago",
			"mano", "mesa", "miel", "montaña", "nariz", "nido", "nieve", "nube", "nudo", "nuez", "olla",
			"ornitorrinco", "pájaro", "pantalón", "paraguas", "pato", "pera", "perro", "pincel",
			"pipa", "pluma", "pulpo", "ratón", "regla", "reloj", "rosa", "sandía", "sartén", "serpiente",
			"silla", "sillón", "sombrero", "sopa", "tambor", "teléfono", "tesoro", "tigre", "torta",
			"tractor", "tucán", "uno", "uña", "uva", "vacuna", "vaso", "vela", "ventana", "vino", "violín",
			"volcán", "yate", "yema", "yogur", "yunque", "zapallo", "zapatilla", "zapato"
		};

		private string[] easyWordListEnglish = new string[] {
			"ant", "arm", "ball", "banana", "bat", "bed", "bee", "beetle", "bird", "bomb", "book", "boot",
			"bow", "box", "broom", "can", "car", "cat", "cheese", "church", "cow", "crab", "dog", "door",
			"drum", "duck", "egg", "eye", "fan", "fish", "fox", "frog", "ghost", "glass", "ham", "hand",
			"harp", "hat", "hen", "ink", "jam", "jar", "jelly", "key", "kiwi", "knot", "lamp", "leg", "log", "map",
			"milk", "moon", "neck", "nest", "net", "nut", "owl", "ox", "pan", "pig", "pot", "rat", "ring",
			"robot", "salt", "shark", "sheep", "star", "sun", "sword", "teeth", "tree", "van", "witch"
		};

		private string[] hardWordListEnglish = new string[] {
			"apple", "arrow", "axe", "balloon", "bear", "bicycle", "boat", "bottle", "bread", "cake",
			"camel", "candle", "carrot", "coin", "cricket", "dice", "dragon", "ear", "elephant", "finger",
			"flower", "flute", "giraffe", "grape", "heart", "hippo", "ice", "igloo", "iron", "island",
			"jacket", "koala", "lemon", "lettuce", "lion", "lollipop", "magnet", "matches", "mirror",
			"monkey", "mouth", "nail", "oven", "parrot", "pear", "pencil", "planet", "queen", "rabbit",
			"rain", "rocket","rose","rubber", "ruler", "shoes", "snake", "soap", "spider", "table", "tiger", "train",
			"violin", "whale", "zebra"
		};

		string language;

		private string[] currentEasyWordList;
		private string[] currentHardWordList;

		public override void StartGame ()
		{
		}

		public override void NextChallenge ()
		{
			
		}

		public override void RequestHint ()
		{
			
		}

		public void InitModel (int lang)
		{
			lettersUsed = new List<int> ();
			letterAmounts = new int[]{ 2, 2, 2, 2, 2 };
			easyMode = true;
			wordCount = 0;
			if (lang == 0) {
				language = "Spanish";
				currentEasyWordList = easyWordListSpanish;
				currentHardWordList = hardWordListSpanish;
			} else {
				language = "English";
				currentEasyWordList = easyWordListEnglish;
				currentHardWordList = hardWordListEnglish;
			}
		}

		public DataTrio<string[], AudioClip[], Sprite[]> LoadResources (bool easy)
		{
			Sprite[] sprites = Resources.LoadAll<Sprite> ("Sprites/" + language + "/ObjectsFindVowel"+ language);
			Debug.Log ("Easy word list size: " + currentEasyWordList.Length);
			Debug.Log ("Hard word list size: " + currentHardWordList.Length);
			Debug.Log ("Sprite array size: " + sprites.Length);
			DataTrio<string[], AudioClip[], Sprite[]> toReturn;
			if (easy) {
				toReturn = new DataTrio<string[], AudioClip[], Sprite[]> (new string[currentEasyWordList.Length], 
					new AudioClip[currentEasyWordList.Length], new Sprite[currentEasyWordList.Length]);
				for (int i = 0; i < currentEasyWordList.Length; i++) {
					toReturn.Fst () [i] = currentEasyWordList [i];
					toReturn.Snd () [i] = Resources.Load<AudioClip> ("Audio/" + language + "/" + currentEasyWordList [i] [0] +
						"Words/" + currentEasyWordList [i]);
					toReturn.Thd () [i] = sprites [i];
				}
			} else {
				toReturn = new DataTrio<string[], AudioClip[], Sprite[]> (new string[currentHardWordList.Length], 
					new AudioClip[currentHardWordList.Length], new Sprite[currentHardWordList.Length]);
				for (int i = 0; i < currentHardWordList.Length; i++) {
					toReturn.Fst () [i] = currentHardWordList [i];
					toReturn.Snd () [i] = Resources.Load<AudioClip> ("Audio/" + language + "/" + currentHardWordList [i] [0] +
						"Words/" + currentHardWordList [i]);
					toReturn.Thd () [i] = sprites [currentEasyWordList.Length + i];
				}
			}
				
			return toReturn;
		}

		public DataPair<int, int> ChooseTwoRandomOpenVowels ()
		{
			DataPair<int, int> toReturn;
			int fst = 0, snd = 0;
			while (fst == snd) {
				fst = UnityEngine.Random.Range (0, 3);
				snd = UnityEngine.Random.Range (0, 3);
			}
			if (fst == 2)
				fst = 3;
			if (snd == 2)
				snd = 3;
			toReturn = new DataPair<int, int> (fst, snd);
			return toReturn;
		}

		public int NextEasy ()
		{
			int value = UnityEngine.Random.Range (0, currentEasyWordList.Length);
			if (lettersUsed.Contains (value))
				return NextEasy ();
			else {
				easyChar = FindVowelInWord (currentEasyWordList [value]);
				wordCount++;
				lettersUsed.Add (value);
				currentWord = currentEasyWordList [value];
				return value;
			}
		}

		public int NextHard ()
		{
			int value = UnityEngine.Random.Range (0, currentHardWordList.Length);
			if (lettersUsed.Contains (value))
				return NextHard ();
			else {
				hardChars = FindVowelsInWord (currentHardWordList [value]);
				wordCount++;
				lettersUsed.Add (value);
				currentWord = currentHardWordList [value];
				return value;
			}
		}

		public bool CheckEasyLetter (string letter)
		{
			return letter.Equals (easyChar);
		}

		public bool CheckHardLetter (string letter)
		{
			Debug.Log ("Hard letter to be checked: " + letter);
			Debug.Log ("Hard letter #1: " + hardChars [0]);
			Debug.Log ("Hard letter #2: " + hardChars [1]);
			return System.Array.IndexOf (hardChars, letter) != -1;
		}

		public DataPair<int, int> RequestHintInfo ()
		{
			DataPair<int, int> toReturn;
			if (easyMode) {
				switch (easyChar) {
				case "A":
					toReturn = new DataPair<int, int> (2, 4);
					break;
				case "E":
					toReturn = new DataPair<int, int> (2, 4);
					break;
				case "I":
					toReturn = ChooseTwoRandomOpenVowels ();
					break;
				case "O":
					toReturn = new DataPair<int, int> (2, 4);
					break;
				case "U":
					toReturn = ChooseTwoRandomOpenVowels ();
					break;
				default:
					toReturn = null;
					break;
				}
			} else {
				int[] randLetts = GetRandomLetters ();
				toReturn = new DataPair<int,int> (randLetts [0], randLetts [1]);
			}
			return toReturn;
		}

		private int[] GetRandomLetters ()
		{
			int[] toReturn = new int[2];
			int[] hardCharIntArray = new int[2];
			hardCharIntArray [0] = GetLetterPos (hardChars [0]);
			hardCharIntArray [1] = GetLetterPos (hardChars [1]);
			var exclude = new HashSet<int> (hardCharIntArray);
			var range = Enumerable.Range (1, 4).Where (i => !exclude.Contains (i));

			var rand = new System.Random ();
			int index = rand.Next (0, 4 - exclude.Count);
			toReturn [0] = range.ElementAt (index);

			exclude.Add (range.ElementAt (index));
			range = Enumerable.Range (1, 4).Where (i => !exclude.Contains (i));
			index = rand.Next (0, 4 - exclude.Count);
			toReturn [1] = range.ElementAt (index);

			return toReturn;
		}

		public int GetLetterPos (string letter)
		{
			switch (letter) {
			case "A":
				return 0;
			case "E":
				return 1;
			case "I":
				return 2;
			case "O":
				return 3;
			case "U":
				return 4;
			default:
				return -1;
			}
		}

		public string FindVowelInWord (string word)
		{
			for (int i = 0; i < word.Length; i++) {
				string c = word [i].ToString ().ToUpper ();
				if (c.Equals ("A") || c.Equals ("E") || c.Equals ("I") || c.Equals ("O") || c.Equals ("U"))
					return RemoveDiacritics (c);
			}
			return "Z";
		}

		public string[] FindVowelsInWord (string word)
		{
			string[] toReturn = new string[2];
			for (int i = 0, j = 0; i < word.Length && j < 2; i++) {
				string c = RemoveDiacritics (word [i].ToString ().ToUpper ());
				if (c.Equals ("A") || c.Equals ("E") || c.Equals ("I") || c.Equals ("O") || c.Equals ("U")) {
					if (System.Array.IndexOf (toReturn, c) == -1) { 
						toReturn [j] = c;
						j++;
					}
				}
			}
			return toReturn;
		}

		public string GetCurrentWord ()
		{
			return currentWord;
		}

	

		static string RemoveDiacritics (string text)
		{
			var normalizedString = text.Normalize (NormalizationForm.FormD);
			var stringBuilder = new StringBuilder ();

			foreach (var c in normalizedString) {
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory (c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark) {
					stringBuilder.Append (c);
				}
			}

			return stringBuilder.ToString ().Normalize (NormalizationForm.FormC);
		}
	}


}