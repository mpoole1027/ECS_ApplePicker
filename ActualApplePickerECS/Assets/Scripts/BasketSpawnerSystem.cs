using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BasketSpawnerSystem : ISystem
{
    //[BurstCompile]
    /*
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BasketSpawnerProperties>();
    }
    */

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //state.Enabled = false;

        //var properties = SystemAPI.GetSingleton<BasketSpawnerProperties>();

        //var ecb = new EntityCommandBuffer(Allocator.Temp);

        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform4, properties4, entity4) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketSpawnerProperties>>().WithEntityAccess())
        {
            if( properties4.ValueRO.ShouldSpawnBaskets == true)
            {
                for (var i = 0; i < properties4.ValueRO.BasketCount; i++)
                {
                    var basket = ecb.Instantiate(properties4.ValueRO.BasketPrefab);
                    var pos = new float3
                    {
                        y = properties4.ValueRO.BottomY + (properties4.ValueRO.Spacing * i)
                    };

                    ecb.SetComponent(basket, LocalTransform.FromPosition(pos));
                }

                properties4.ValueRW.ShouldSpawnBaskets = false;
            }


        }
        //ecb.Playback(state.EntityManager);

    }
}