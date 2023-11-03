using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SpawnerSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<SpawnerProperties>>())
        {
            
            if( properties.ValueRO.Timer <= 0)
            {
                var apple = ecb.Instantiate(properties.ValueRO.ApplePrefab);

                ecb.SetComponent(apple, LocalTransform.FromPosition(transform.ValueRO.Position));

                if( properties.ValueRO.IsMediumMode)
                {
                    properties.ValueRW.Timer = 2;
                    
                }

                else
                {
                    properties.ValueRW.Timer = properties.ValueRO.Delay;
                }
                


            }

            else
            {
                
             
                properties.ValueRW.Timer = properties.ValueRO.Timer - SystemAPI.Time.DeltaTime;
              
                
            }

        }
    }

}