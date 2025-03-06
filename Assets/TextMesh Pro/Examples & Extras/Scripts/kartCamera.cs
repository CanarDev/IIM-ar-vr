using UnityEngine;
using KartGame.KartSystems;

public class KartCamera : MonoBehaviour
{
    public GameObject kartTarget;
    
    // Référence à notre QuestInput modifié
    private QuestInput questInput;
    
    // Sauvegarder la position de la caméra avant verrouillage
    private Vector3 lastCameraPosition;
    private Quaternion lastCameraRotation;
    
    void Start()
    {
        // Trouver la référence au QuestInput
        if (kartTarget != null)
        {
            questInput = kartTarget.GetComponent<QuestInput>();
            
            // Si le kartTarget n'a pas directement le QuestInput, chercher dans ses enfants
            if (questInput == null)
                questInput = kartTarget.GetComponentInChildren<QuestInput>();
        }
        
        if (questInput == null)
            Debug.LogWarning("KartCamera ne peut pas trouver QuestInput sur le kart cible");
            
        // Initialiser les dernières positions connues
        lastCameraPosition = transform.position;
        lastCameraRotation = transform.rotation;
    }
    
    void LateUpdate()
    {
        // Si nous n'avons pas trouvé le QuestInput, on utilise le comportement normal
        if (questInput == null)
            return;
            
        // Vérifier si la caméra doit être verrouillée
        if (questInput.ShouldLockCamera())
        {
            // Utiliser les dernières positions connues
            transform.position = lastCameraPosition;
            transform.rotation = lastCameraRotation;
        }
        else
        {
            // Sauvegarder la position actuelle pour une utilisation ultérieure
            lastCameraPosition = transform.position;
            lastCameraRotation = transform.rotation;
        }
    }
}