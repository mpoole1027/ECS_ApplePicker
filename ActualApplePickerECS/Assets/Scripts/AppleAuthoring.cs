using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class AppleAuthoring : MonoBehaviour
{
    public float bottomY = -20f;

    public float fallSpeed = 1f;

    private class AppleBaker : Baker<AppleAuthoring>
    {
        public override void Bake(AppleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            var propertiesComponent = new AppleProperties
            {
                BottomY = authoring.bottomY,
                FallSpeed = authoring.fallSpeed
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct AppleProperties : IComponentData
{
    public float BottomY;
    public float FallSpeed;

}