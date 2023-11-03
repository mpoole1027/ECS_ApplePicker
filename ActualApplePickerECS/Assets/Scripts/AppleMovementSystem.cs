using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct AppleMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>().WithEntityAccess())
        {
            var pos = transform.ValueRO.Position;
            var speed = properties.ValueRO.FallSpeed;

            pos.y -= speed * SystemAPI.Time.DeltaTime;
            transform.ValueRW.Position = pos;

            if(pos.y < properties.ValueRO.BottomY)
            {
                ecb.DestroyEntity(entity);
            }
        }

    }

}