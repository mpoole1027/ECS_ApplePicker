using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial struct CollisionSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties, entity1) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketTag>>().WithEntityAccess())
        {
            //Debug.Log("Apple Collided");
            var BasketPos = transform.ValueRO.Position;

            foreach (var (transform1, properties1, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>().WithEntityAccess())
            {
                var ApplePos = transform1.ValueRO.Position;

                if (ApplePos.x >= BasketPos.x - 2 && ApplePos.y <= BasketPos.y + 1 && ApplePos.x <= BasketPos.x + 2 && ApplePos.y >= BasketPos.y + 0.5)
                {
                    //Debug.Log("Apple Collided");
                    ecb.DestroyEntity(entity);

                }

                else if (ApplePos.y <= BasketPos.y - 6)
                {
                

                    ecb.DestroyEntity(entity1);
                    ecb.DestroyEntity(entity);

                    foreach (var (transform3, properties3, entity2) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>().WithEntityAccess())
                    {
                        ecb.DestroyEntity(entity2);
                    }

                    foreach (var (transform2, properties2) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SpawnerProperties>>())
                    {
                        properties2.ValueRW.Timer = UnityEngine.Random.value * 2;
                    }

                    foreach (var (transform4, properties4, entity4) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketSpawnerProperties>>().WithEntityAccess())
                    {
                        properties4.ValueRW.NumMissed = properties4.ValueRO.NumMissed + 1;

                        if (properties4.ValueRO.NumMissed >= 3)
                        {
                            Debug.Log("Game Reset Time");
                            SceneManager.LoadScene("StartScene");
                        }
                    }
                    
                }


            }
        }

    }

}