using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BasketMovementSystem : ISystem
{
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketTag>>())
        {

            var pos = transform.ValueRO.Position;
            foreach (var (transform1, properties1) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketSpawnerProperties>>())
            {
                if (properties1.ValueRO.IsHardMode == true)
                {
                    pos.x = -mousePos3D.x;
                }

                else
                {
                    pos.x = mousePos3D.x;
                }
            }
            

            transform.ValueRW.Position = pos;
        }
    }
    
}
