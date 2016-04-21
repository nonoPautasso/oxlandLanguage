using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Levels.Vowels;
using Assets.Scripts.Levels.VowelsOral;

namespace Assets.Scripts.Levels.CompleteMissingVowel
{
	public class CompleteMissingVowelModel : LevelModel
	{

		private string easyChar;

		public int wordCount;
		public bool easyMode;

		private string[] hardChars;

		int[] letterAmounts;

		private string[] easyWordList = new string[] {"araña", "arpa", "barba", "botón", "caja",
			"cama", "cámara", "casa", "coco", "flan", "flor", "fósforo", "globo", "gorro", "hongo",
			"horno", "kayak", "kiwi", "lana", "lata", "loro", "leche", "manzana", "mapa", "mono", "moño",
			"moto", "naranja", "ojo", "oso", "pan", "pez", "rana", "raya", "red", "robot", "sal", "sol",
			"taza", "toro", "tren", "tronco", "vaca", "yoyo", "zorro"
		};

		private string[] hardWordList = new string[] {
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

		public override void StartGame ()
		{
		}

		public override void NextChallenge ()
		{
			
		}

		public override void RequestHint ()
		{
			
		}

		public void InitModel ()
		{
			letterAmounts = new int[]{ 2, 2, 2, 2, 2 };
			easyMode = true;
			wordCount = 0;
		}

		public DataTrio<string[], AudioClip[], Sprite[]> LoadResources (bool easy)
		{
			Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Spanish/ObjectsFindVowelSpanish");
			Debug.Log ("Easy word list size: " + easyWordList.Length);
			Debug.Log ("Hard word list size: " + hardWordList.Length);
			Debug.Log("Sprite array size: " + sprites.Length);
			DataTrio<string[], AudioClip[], Sprite[]> toReturn;
			if (easy) {
				toReturn = new DataTrio<string[], AudioClip[], Sprite[]> (new string[easyWordList.Length], 
					new AudioClip[easyWordList.Length], new Sprite[easyWordList.Length]);
				for (int i = 0; i < easyWordList.Length; i++) {
					toReturn.Fst () [i] = easyWordList [i];
					toReturn.Snd () [i] = Resources.Load<AudioClip> ("Audio/Spanish/" + easyWordList [i] [0] +
					"Words/" + easyWordList [i]);
					toReturn.Thd () [i] = sprites [i];
				}
			} else {
				toReturn = new DataTrio<string[], AudioClip[], Sprite[]> (new string[hardWordList.Length], 
					new AudioClip[hardWordList.Length], new Sprite[hardWordList.Length]);
				for (int i = 0; i < hardWordList.Length; i++) {
					toReturn.Fst () [i] = hardWordList [i];
					toReturn.Snd () [i] = Resources.Load<AudioClip> ("Audio/Spanish/" + hardWordList [i] [0] +
					"Words/" + hardWordList [i]);
					toReturn.Thd () [i] = sprites [easyWordList.Length + i];
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
			int value = UnityEngine.Random.Range (0, easyWordList.Length);
			easyChar = FindVowelInWord (easyWordList [value]);
			wordCount++;
			return value;
		}

		public int NextHard ()
		{
			int value = UnityEngine.Random.Range (0, hardWordList.Length);
			hardChars = FindVowelsInWord (hardWordList [value]);
			return value;
		}

		public bool CheckEasyLetter (string letter)
		{
			return letter.Equals (easyChar);
		}

		public bool CheckHardLetter (string letter)
		{
			return System.Array.IndexOf(hardChars, letter) != -1;
		}

		public DataPair<int, int> RequestHintInfo ()
		{
			DataPair<int, int> toReturn;
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
					return c;
			}
			return "Z";
		}

		public string[] FindVowelsInWord(string word) {
			string[] toReturn = new string[2];
			for (int i = 0, j = 0; i < word.Length && j < 2; i++) {
				string c = word [i].ToString ().ToUpper ();
				if (c.Equals ("A") || c.Equals ("E") || c.Equals ("I") || c.Equals ("O") || c.Equals ("U")) {
					if (System.Array.IndexOf(toReturn, c) == -1){ 
						toReturn[j] = c;
						j++;
					}
				}
			}
			return toReturn;
		}
			
	}
}