using Unity.Entities;
using UnityEngine;


public class BasketSpawnerAuthoring : MonoBehaviour
{
    public int basketCount = 3;
    public int numMissed = 0;
    public GameObject basketPrefab;
    public float spacing = 2f;
    public float bottomY = -14f;
    public bool shouldSpawnBaskets = true;
    public bool isHardMode = false;

    private class BasketSpawnerBaker : Baker<BasketSpawnerAuthoring>
    {
        public override void Bake(BasketSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            var propertiesComponent = new BasketSpawnerProperties
            {
                BasketCount = authoring.basketCount,
                BasketPrefab = GetEntity(authoring.basketPrefab, TransformUsageFlags.Dynamic),
                Spacing = authoring.spacing,
                BottomY = authoring.bottomY,
                NumMissed = authoring.numMissed,
                ShouldSpawnBaskets = authoring.shouldSpawnBaskets,
                IsHardMode = authoring.isHardMode
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct BasketSpawnerProperties : IComponentData
{
    public Entity BasketPrefab;
    public int BasketCount;
    public float Spacing;
    public float BottomY;
    public int NumMissed;
    public bool ShouldSpawnBaskets;
    public bool IsHardMode;
}