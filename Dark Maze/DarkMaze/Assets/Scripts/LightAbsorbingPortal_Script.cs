using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LightAbsorbingPortal_Script : MonoBehaviour 
{
    public float amountNeededToFill = 0.75f;
    public PowerUpType AbsorbtionType = PowerUpType.White;
    Colour colour;
    public bool IsFull()
    {
        return colour.amount > amountNeededToFill;
    }

	// Use this for initialization
	void Start () 
    {
        colour = new Colour(AbsorbtionType, 0);
        gameObject.GetComponent<ParticleSystem>().startColor = colour.color;
    }
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnTriggerEnter(Collider other)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (!IsFull())
        {
            colour.amount += GameObject.Find("Player").GetComponent<Player_Script>().AbsorbColor(colour.Type);            
        }
        if (particleSystem != null && IsFull())
        {
            Component.Destroy(particleSystem);
        }
    }
}
