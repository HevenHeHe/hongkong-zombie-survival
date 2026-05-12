using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// Zombie Component Data
public struct ZombieData : IComponentData
{
    public float health;
    public float speed;
    public float attackDamage;
    public int zombieType; // 0=normal, 1=fast, 2=heavy, 3=splits
    public float3 homePosition;
    public float stateTimer;
}

// Zombie AI State
public enum ZombieState
{
    Idle = 0,
    Patrol = 1,
    Chase = 2,
    Attack = 3,
    Dead = 4
}