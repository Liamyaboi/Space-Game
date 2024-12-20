using Unity.Entities;
using Random = Unity.Mathematics.Random;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using System.Runtime.CompilerServices;

public partial class EnemySpawnerSystem : SystemBase
{
    private EnemySpawnerConponment enemySpawnerComponent;
    private EnemyData enemyDataConponment;
    private Entity enemySpawnerEntity;
    private float nextSpawnTime;
    private Random random;

    protected override void OnCreate()
    {
        random = Random.CreateFromIndex((uint)enemySpawnerComponent.GetHashCode());
    }

    protected override void OnUpdate()
    {
        if (!SystemAPI.TryGetSingletonEntity<EnemySpawnerConponment>(out enemySpawnerEntity)) 
        {  
            return;
        }

        enemySpawnerComponent = EntityManager.GetComponentData<EnemySpawnerConponment>(enemySpawnerEntity);
        enemyDataConponment = EntityManager.GetComponentObject<EnemyData>(enemySpawnerEntity);

        if (SystemAPI.Time.ElapsedTime > nextSpawnTime)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        int level = 2;
        List<EnemyDatas> availableEnemies = new List<EnemyDatas>();

        foreach (EnemyDatas enemyDatas in enemyDataConponment.enemies)
        {
            if  (enemyDatas.level <= level)
            {
                availableEnemies.Add(enemyDatas);
            } 
        }

        int index = random.NextInt(availableEnemies.Count);

        Entity newEnemy = EntityManager.Instantiate(availableEnemies[index].prefab);
        EntityManager.SetComponentData(newEnemy, new LocalTransform
        {
            Position = GetPositionOutsideOfCameraRange(),
            Rotation = Quaternion.identity,
            Scale = 1
        });
        EntityManager.AddComponentData(newEnemy, new EnemyComponent { currentHealth = availableEnemies[index].health });

        nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + enemySpawnerComponent.spawnCooldown;
    }

   private float3 GetPositionOutsideOfCameraRange()
    {
        float3 position = new float3(random.NextFloat2(-enemySpawnerComponent.cameraSize * 2, enemySpawnerComponent.cameraSize * 2), 0);

        while (position.x < enemySpawnerComponent.cameraSize.x && position.x > -enemySpawnerComponent.cameraSize.x
            && position.y < enemySpawnerComponent.cameraSize.y &&  position.y > -enemySpawnerComponent.cameraSize.y)
        {
            position = new float3(random.NextFloat2(-enemySpawnerComponent.cameraSize * 2, enemySpawnerComponent.cameraSize * 2), 0);
        }

        position += new float3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

        return position;
    }

}
