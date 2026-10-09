using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;

    [Header("Check Point References")]
    [SerializeField] private CheckPoint _initialCheckPoint;
    
    [HideInInspector] public CheckPoint CurrentCheckPoint;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start() => SetCheckPoint(_initialCheckPoint);
    public void SetCheckPoint(CheckPoint nextCheckPoint)
    {
        if (nextCheckPoint == null)
            return;

        if (CurrentCheckPoint == nextCheckPoint)
            return;

        if (CurrentCheckPoint != null && CurrentCheckPoint.CheckPointID >= nextCheckPoint.CheckPointID)
            return;

        CurrentCheckPoint = nextCheckPoint;
    }
}
