using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Metrics
{
    public class ChangleColour : MonoBehaviour
    {
        private Color SELECTED;
        private Color UNSELECTED;

        void Start()
        {
            SELECTED = new Color32(0, 255, 0, 255);
            UNSELECTED = new Color32(255, 255, 255, 255);
            SwitchColour();
        }

        public void SwitchColour(){
            Debug.Log(gameObject.name);
            if(gameObject.GetComponent<Toggle>().isOn) gameObject.GetComponent<Image>().color = SELECTED;
            else gameObject.GetComponent<Image>().color = UNSELECTED;
        }
    }
}

