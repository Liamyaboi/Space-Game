using System.Collections.Generic;
using Unity.Entities;


public class EnemyData : IComponentData
{
    public List<EnemyDatas> enemies;
}

public struct EnemyDatas
{
    public int level;
    public Entity prefab;
    public float health;
    public float damage;
    public float movespeed;
}