using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    public enum PowerUpType
    {
        White,
        Blue,
        Red,
        Yellow,
        Green
    }

    public class Colour
    {
        public Color color;
        public float amount;
        PowerUpType type;

        public PowerUpType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                switch (type)
                {
                    case PowerUpType.White:
                        color = new Color(1, 1, 1, 1);
                        break;
                    case PowerUpType.Green:
                        color = new Color(0, 1, 0, 1);
                        break;
                    case PowerUpType.Red:
                        color = new Color(1, 0, 0, 1);
                        break;
                    case PowerUpType.Yellow:
                        color = new Color(1, 1, 0, 1);
                        break;
                    case PowerUpType.Blue:
                        color = new Color(0, 0, 1, 1);
                        break;
                    default:
                        color = new Color(1, 1, 1, 1);
                        break;
                }
            }
        }

        public Colour(PowerUpType type, float amount)
        {
            Type = type;
            this.amount = amount;
        }

        public Colour(Colour colour)
        {
            this.Type = colour.type;
            this.amount = colour.amount;
        }

        public void UsePowerUp(Player_Script script)
        {
            switch (type)
            {
                case PowerUpType.White:
                    whitePowerUp(script);
                    return;
                case PowerUpType.Green:
                    greenPowerUp(script);
                    return;
                case PowerUpType.Red:
                    redPowerUp();
                    return;
                case PowerUpType.Yellow:
                    yellowPowerUp();
                    return;
                case PowerUpType.Blue:
                    bluePowerUp(script);
                    return;
                default:
                    whitePowerUp(script);
                    return;
            }
        }

        private void bluePowerUp(Player_Script script)
        {
            script.GetComponent<Controls>().speed = 20.0f;
        }

        private void yellowPowerUp()
        {
            GameObject[] yellowBlocks = GameObject.FindGameObjectsWithTag("YellowBlock");
            for (int i = 0; i < yellowBlocks.Length; i++)
            {
                Rigidbody blockRigidBody = yellowBlocks[i].GetComponent<Rigidbody>();
                blockRigidBody.isKinematic = false;
            }
        }

        private void redPowerUp()
        {
            GameObject[] redBlocks = GameObject.FindGameObjectsWithTag("RedBlock");
            for (int i = 0; i < redBlocks.Length; i++)
            {
                redBlocks[i].GetComponent<Collider>().enabled = false;
                redBlocks[i].GetComponent<MeshRenderer>().material.SetColor("_Color",new Color(1,0,0,0.3f));
            }
        }

        private void greenPowerUp(Player_Script script)
        {
            throw new NotImplementedException();
        }

        private void whitePowerUp(Player_Script script)
        {
            script.SpotLight.gameObject.transform.localPosition = new Vector3(0, 5, 0);
            script.SpotLight.GetComponent<Light>().spotAngle = 120;
        }
    }
}
