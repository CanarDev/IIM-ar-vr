using UnityEngine;
using UnityEngine.XR;

namespace KartGame.KartSystems
{
    public class QuestInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";
        
        // Ajouter un paramètre pour contrôler la caméra
        [Tooltip("Si true, la caméra reste fixe quel que soit l'angle de direction")]
        public bool lockCameraOnFullTurn = true;
        
        // Seuil à partir duquel on considère que le joystick est à fond
        [Range(0.8f, 1.0f)]
        public float fullTurnThreshold = 0.9f;
        
        private InputDevice leftController;
        private InputDevice rightController;
        
        // Cette variable sera utilisée pour communiquer avec la caméra
        private bool isFullTurn = false;
        
        void Start()
        {
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
        
        public override InputData GenerateInput()
        {
            float turnInput = 0f;
            bool accelerate = false;
            bool brake = false;
            
            if (leftController.isValid)
            {
                leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystick);
                
                // Stocker la valeur brute du joystick
                float rawTurnInput = joystick.x;
                
                // Vérifier si le joystick est à fond (gauche ou droite)
                isFullTurn = Mathf.Abs(rawTurnInput) >= fullTurnThreshold;
                
                // On utilise toujours la même valeur pour le kart
                turnInput = rawTurnInput;
            }
            
            if (rightController.isValid)
            {
                rightController.TryGetFeatureValue(CommonUsages.triggerButton, out accelerate);
                leftController.TryGetFeatureValue(CommonUsages.triggerButton, out brake);
            }
            
            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = turnInput
            };
        }
        
        // Méthode pour permettre à la caméra de savoir si elle doit se verrouiller
        public bool ShouldLockCamera()
        {
            return lockCameraOnFullTurn && isFullTurn;
        }
    }
}