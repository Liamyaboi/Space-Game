using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Mathematics;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    public float spawnCooldown = 1;
    public Vector2 cameraSize;
    public List<EnemySO> enemiesSO;

    public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring> 
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            Entity enemySpawnerAuthoring = GetEntity(TransformUsageFlags.None);

            AddComponent(enemySpawnerAuthoring, new EnemySpawnerConponment
            {
                spawnCooldown = authoring.spawnCooldown,
                cameraSize = authoring.cameraSize
            });

            List<EnemyDatas> enemyDatas = new List<EnemyDatas>();

            foreach (EnemySO e in authoring.enemiesSO)
            {
                enemyDatas.Add(new EnemyDatas
                {
                    damage = e.damage,
                    health = e.health,
                    level = e.level,
                    movespeed = e.movespeed,
                    prefab = GetEntity(e.prefab, TransformUsageFlags.None)
                });

            }
            
            AddComponentObject(enemySpawnerAuthoring, new EnemyData { enemies = enemyDatas });
      
        }
    }

}
