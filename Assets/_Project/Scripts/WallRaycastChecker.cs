using UnityEngine;

public class WallRaycastChecker : MonoBehaviour
{
    [Header("Transform Settings")]
    [SerializeField] private Transform _playerTransform;

    [Header("Camera References")]
    [SerializeField] private Camera gameCamera;

    [Header("Fade Settings")]
    [Range(0.1f, 1f)][SerializeField] private float fadedAlpha;
    [SerializeField] private float fadeDuration;

    private WallFader currentFader;

    private void Start()
    {
        if (gameCamera == null) gameCamera = Camera.main;
    }

    private void Update()
    {
        if (_playerTransform == null || gameCamera == null) return;

        Vector3 direction = _playerTransform.position - gameCamera.transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(gameCamera.transform.position, direction.normalized, distance);

        WallFader hitFader = null;

        Debug.DrawRay(gameCamera.transform.position, direction);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag(GameConstant.GameTag.WALL))
            {
                WallFader fader = hit.collider.GetComponent<WallFader>();
                if (fader != null)
                {
                    hitFader = fader;
                    break;
                }
            }
        }

        if (hitFader != null)
        {
            if (currentFader != hitFader)
            {
                if (currentFader != null)
                {
                    currentFader.FadeIn(fadeDuration);
                }

                currentFader = hitFader;
                currentFader.FadeOut(fadedAlpha, fadeDuration);
            }
        }
        else
        {
            if (currentFader != null)
            {
                currentFader.FadeIn(fadeDuration);
                currentFader = null;
            }
        }
    }
}
