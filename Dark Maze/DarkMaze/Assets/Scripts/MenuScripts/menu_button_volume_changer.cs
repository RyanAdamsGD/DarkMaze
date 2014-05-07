using UnityEngine;
using System.Collections;

public class menu_button_volume_changer : MonoBehaviour
{
    public float increment;

    void Start()
    {
    }

    void OnMouseDown()
    {
        AudioListener.volume += increment;
        AudioListener.volume = Mathf.Clamp(AudioListener.volume, 0, 1);
            

        Debug.Log(AudioListener.volume);
    }
}
