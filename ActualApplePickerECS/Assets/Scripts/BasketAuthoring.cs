using Unity.Entities;
using UnityEngine;

public class BasketAuthoring : MonoBehaviour
{
    private class BasketBaker : Baker<BasketAuthoring>
    {
        public override void Bake(BasketAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<BasketTag>(entity);
        }
    }
}


public struct BasketTag : IComponentData
{
}
