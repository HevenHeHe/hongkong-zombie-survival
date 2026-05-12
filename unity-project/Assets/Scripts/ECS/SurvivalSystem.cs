using Unity.Entities;
using Unity.Mathematics;

// Player Survival Component
public struct SurvivalData : IComponentData
{
    public float health;      // 0-100
    public float hunger;      // 0-100
    public float stamina;     // 0-100
    public float noiseLevel;  // 0-100, attracts zombies
    
    // Max values
    public float maxHealth;
    public float maxHunger;
    public float maxStamina;
}

// Survival System - handles hunger, health decay, stamina
[BurstCompile]
public partial struct SurvivalSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var job = new SurvivalJob { deltaTime = deltaTime };
        job.ScheduleParallel(state.Dependency).Complete();
    }
}

[BurstCompile]
public struct SurvivalJob : IJobEntityParallel
{
    public float deltaTime;
    
    public void Execute(ref SurvivalData survival)
    {
        // Natural decay
        survival.hunger = math.max(0, survival.hunger - deltaTime * 0.01f);
        survival.stamina = math.min(survival.maxStamina, survival.stamina + deltaTime * 5f);
        
        // Health from hunger
        if (survival.hunger < 10 && survival.health > 20)
        {
            survival.health -= deltaTime * 0.5f;
        }
    }
}