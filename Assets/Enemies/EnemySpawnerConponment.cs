using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct EnemySpawnerConponment : IComponentData
{
    public float spawnCooldown;
    public float2 cameraSize;
}
