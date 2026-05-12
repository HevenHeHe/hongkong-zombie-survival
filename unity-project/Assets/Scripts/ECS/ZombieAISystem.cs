using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine;

// Zombie AI System - ECS Version
[BurstCompile]
public partial struct ZombieAISystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        
        // Zombie behavior job
        var zombieJob = new ZombieBehaviorJob
        {
            deltaTime = deltaTime,
            playerPosition = GetPlayerPosition(),
            deltaTime = deltaTime
        };
        
        zombieJob.ScheduleParallel(state.Dependency).Complete();
    }
    
    private float3 GetPlayerPosition()
    {
        // Get from singleton or tag
        var playerQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag>().Build();
        if (playerQuery.IsEmptyIgnoreFilter) return float3.zero;
        var playerEntity = playerQuery.ToEntityArray(Allocator.Temp)[0];
        return SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
    }
}

[BurstCompile]
public struct ZombieBehaviorJob : IJobEntityParallel
{
    public float deltaTime;
    public float3 playerPosition;
    
    public void Execute(ref ZombieData zombie, ref LocalTransform transform)
    {
        if (zombie.health <= 0) return; // Dead
        
        float distance = math.distance(transform.Position, playerPosition);
        float3 direction = math.normalize(playerPosition - transform.Position);
        
        // State machine
        if (distance < 2f) // Attack range
        {
            zombie.stateTimer = 0;
        }
        else if (distance < 15f) // Chase range
        {
            transform.Position += direction * zombie.speed * deltaTime;
        }
        else // Patrol
        {
            zombie.stateTimer += deltaTime;
            if (zombie.stateTimer > 5f)
            {
                transform.Position = math.lerp(transform.Position, zombie.homePosition, deltaTime * 0.5f);
            }
        }
    }
}