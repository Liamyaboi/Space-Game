using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;
public partial struct EnemyAISystem : ISystem
{
    private EntityManager entityManager;
    private Entity playerEntity;

    private void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();

        foreach (var (enemyComponent, transformComponent) in SystemAPI.Query<EnemyComponent, RefRW<LocalTransform>>())
        {
           
            float3 direction = entityManager.GetComponentData<LocalTransform>(playerEntity).Position - transformComponent.ValueRO.Position;

          
            if (math.lengthsq(direction) > 0.0001f)  
            {
               
                float angle = math.atan2(direction.y, direction.x);
                transformComponent.ValueRW.Rotation = quaternion.Euler(new float3(0, 0, angle));

               
                float speed = 5f;
                transformComponent.ValueRW.Position += math.normalize(direction) * speed * SystemAPI.Time.DeltaTime;
            }
        }
    }
}

