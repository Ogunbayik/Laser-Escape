using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    [Header("Impact Prefabs")]
    [SerializeField] private ParticleSystem _sparkPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayOneShotImpact(Vector3 point, Vector3 normal)
    {
        if (_sparkPrefab == null) return;

        ParticleSystem spark = Instantiate(_sparkPrefab, point, Quaternion.LookRotation(normal));
        spark.Play();

        Destroy(spark.gameObject, spark.main.duration + spark.main.startLifetime.constantMax);
    }
}
