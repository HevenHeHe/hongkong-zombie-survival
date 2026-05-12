# Hong Kong Zombie Survival - Unity Project

## Architecture
- **Engine**: Unity 2021.3.42f1 LTS
- **Rendering**: URP (Universal Render Pipeline)
- **Code Pattern**: ECS + DOTS (Entities, Jobs, Burst)
- **AI**: Behavior Tree (ScriptableObject)

## Directory Structure
```
Assets/
├── Scripts/
│   ├── ECS/          # ECS Systems
│   │   ├── ZombieData.cs
│   │   ├── ZombieAISystem.cs
│   │   └── SurvivalSystem.cs
│   ├── AI/           # Behavior Trees
│   ├── UI/           # Interface
│   └── Core/
├── Scenes/
│   ├── MainMenu.unity
│   ├── HongKongStreets.unity
│   └── Battleground.unity
├── Materials/        # Low Poly shaders
└── Prefabs/          # Game objects
```

## Next Steps
1. Create Zombie Authoring Component
2. Build Behavior Tree Editor
3. Set up Addressable Groups
4. Player Input System

## Performance Targets
- 60 FPS on PC/iOS/Android
- < 2GB memory
- 10,000+ zombies with ECS