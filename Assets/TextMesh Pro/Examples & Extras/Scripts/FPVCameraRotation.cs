using UnityEngine;

public class FixCameraRotation : MonoBehaviour
{
    public Transform carTransform;

    void LateUpdate()
    {
        Quaternion carYawRotation = Quaternion.Euler(0, carTransform.eulerAngles.y, 0);
        transform.rotation = carYawRotation;
    }
}