using UnityEngine;
using UnityEngine.XR;
using KartGame.KartSystems;

public class PickupBoost : MonoBehaviour
{
    private bool isGrabbing = false;
    private ArcadeKart kart; // Référence au kart du joueur
    private ArcadeKart.StatPowerup boostPowerup;

    void Update()
    {
        // Vérifie l'état du bouton Grip du Left Controller
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool isPressed))
        {
            if (isPressed && !isGrabbing)
            {
                isGrabbing = true;
                StartBoost();
            }
            else if (!isPressed && isGrabbing)
            {
                isGrabbing = false;
                StopBoost();
            }
        }
    }

    void StartBoost()
    {
        // Vérifie si un kart est proche
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (Collider collider in hitColliders)
        {
            kart = collider.GetComponentInParent<ArcadeKart>();
            if (kart != null)
            {
                // Crée le powerup
                boostPowerup = new ArcadeKart.StatPowerup
                {
                    PowerUpID = "Boost",
                    ElapsedTime = 0,
                    MaxTime = 9999f, // On garde le boost tant que le joueur tient l'objet
                    modifiers = new ArcadeKart.Stats
                    {
                        TopSpeed = 45f, // Augmente encore plus la vitesse
                        Acceleration = 15f
                    }
                };

                kart.AddPowerup(boostPowerup);
                gameObject.SetActive(false); // Cache l'objet car il est "tenu"
                break;
            }
        }
    }

    void StopBoost()
    {
        if (kart != null && boostPowerup != null)
        {
            kart.RemovePowerup(boostPowerup); // Supprime le boost
            kart = null; // Réinitialise
            gameObject.SetActive(true); // Réaffiche l'objet si besoin
        }
    }
}
