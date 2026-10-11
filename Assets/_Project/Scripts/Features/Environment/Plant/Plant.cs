using DG.Tweening;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Animaton Settings")]
    [SerializeField] private float _growDuration;

    public void TogglePlant(bool isActive) => gameObject.SetActive(isActive);
    public void SetPlantSize(Vector3 size) => transform.localScale = size;
    public void PlayGrowAnimation()
    {
        SetPlantSize(Vector3.zero);
        TogglePlant(true);

        transform.DOScale(Vector3.one, _growDuration).SetEase(Ease.InOutSine);
    }
}
