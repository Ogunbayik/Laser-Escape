using Cysharp.Threading.Tasks;
using OB.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantGroup : MonoBehaviour
{
    [Header("Plant References")]
    [SerializeField] private List<Plant> _plantsList;

    [Header("Delay Settings")]
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    void Start() => Initialize();
    private void Initialize()
    {
        foreach (var plant in _plantsList)
        {
            plant.SetPlantSize(Vector3.zero);
            plant.TogglePlant(false);
        }
    }
    public async UniTask PlayGrowPlantsAnimation()
    {
        var plants = _plantsList;

        plants.Shuffle();

        for (int i = 0; i < plants.Count; i++)
        {
            var delay = RandomUtils.GetRandomFloat(_minDelay, _maxDelay);

            plants[i].PlayGrowAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(delay));
        }
    }
}
