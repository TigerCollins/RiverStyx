using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(90, 0, 0);
    }
}
