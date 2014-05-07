using UnityEngine;
using System.Collections;

public class menu_volume_watcher : MonoBehaviour 
{
    TextMesh textmesh;

    string VolumeValue
    {
        get
        {
            if (AudioListener.volume > 0.99) return "10";
            else if (AudioListener.volume > 0.89) return "9";
            else if (AudioListener.volume > 0.79) return "8";
            else if (AudioListener.volume > 0.69) return "7";
            else if (AudioListener.volume > 0.59) return "6";
            else if (AudioListener.volume > 0.49) return "5";
            else if (AudioListener.volume > 0.39) return "4";
            else if (AudioListener.volume > 0.29) return "3";
            else if (AudioListener.volume > 0.19) return "2";
            else if (AudioListener.volume > 0.09) return "1";
            else return "0";
        }
    }

	void Start () 
    {
        textmesh = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (textmesh == null) return;

        textmesh.text = "Volume: " + VolumeValue;
	}
}
