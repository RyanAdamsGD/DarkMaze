using UnityEngine;
using System.Collections;

public class menu_button_open_scene : MonoBehaviour 
{
    public string SceneName;

	// Use this for initialization
	void Start () 
    {
	}

    void OnMouseDown()
    {
        Application.LoadLevel(SceneName);
    }
}
