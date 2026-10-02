using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _mainCameraTransform;

    private void Start()
    {
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (_mainCameraTransform != null)
        {
            transform.rotation = _mainCameraTransform.rotation;
        }
    }
}
