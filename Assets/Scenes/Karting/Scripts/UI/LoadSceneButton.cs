using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        private InputDevice rightController;

        void Start()
        {
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        void Update()
        {
            if (rightController.isValid)
            {
                bool aButtonPressed = false;
                bool bButtonPressed = false;

                rightController.TryGetFeatureValue(CommonUsages.primaryButton, out aButtonPressed); // "A" button
                rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bButtonPressed); // "B" button

                if (aButtonPressed || bButtonPressed)
                {
                    LoadTargetScene();
                }
            }
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadSceneAsync(SceneName);
        }
    }
}
