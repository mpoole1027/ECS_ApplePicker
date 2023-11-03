using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnerAuthoring : MonoBehaviour
{

    public GameObject applePrefab;
    public float delay;
    public bool isMediumMode = false;

    private class SpawnBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            var propertiesComponent = new SpawnerProperties
            {
                ApplePrefab = GetEntity(authoring.applePrefab, TransformUsageFlags.Dynamic),
                Delay = authoring.delay,
                IsMediumMode = authoring.isMediumMode,
                Timer = UnityEngine.Random.value * 1.5f
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct SpawnerProperties : IComponentData
{
    public Entity ApplePrefab;
    public float Delay;
    public float Timer;
    public bool IsMediumMode;
}
