# LittlePlatformerDemo
## Overview
Small demo of a 2D platformer game. Technologies: Unity, Unity ads, Extenject(Zenject), DoTween, UniRx, LunarConsole, Newtonesoft Json.
![game screenshot](https://github.com/IcticStep/LittlePlatformerDemo/assets/59373161/6bbd3118-2142-442e-b933-be2d702f9dbe)


## Setting up
1. Download the project.
2. Open with Unity 2021.3.16f1 or higher.

## Technical details
- The architecture is based on dependency inversion through dependency injection. Extenject (Zenject) is used for that purpose. Installers are implemented in  [Assets/Scripts/DependenciesManagement](Assets/Scripts/DependenciesManagement) and the project context can be found in [Assets/Configurations/Resources/ProjectContext.prefab](Assets/Configurations/Resources/ProjectContext.prefab).
- Unity ads are implemented in [Assets/Scripts/Ads](Assets/Scripts/Ads) using abstractions to make adding new ad platforms easy.
- The JSON-based save system is implemented in [Assets/Scripts/Entities/System/Savers](Assets/Scripts/Entities/System/Savers); Newtonesoft-library is used.
- DoTween is used for animations and delays in [Assets/Scripts/Entities/Viewers/CoinViewer.cs](Assets/Scripts/Entities/Viewers/CoinViewer.cs) and [Assets/Scripts/Entities/Functions/DeathMaker.cs](Assets/Scripts/Entities/Functions/DeathMaker.cs).
- UniRx used for timers in [Assets/Scripts/Entities/Functions/OnFallRestarter.cs](Assets/Scripts/Entities/Functions/OnFallRestarter.cs) and [Assets/Scripts/Entities/System/Savers/ProjectSaver.cs](Assets/Scripts/Entities/System/Savers/ProjectSaver.cs).
- New Input System is used. Configuration can be found in [Assets/Configurations/Input](Assets/Configurations/Input), with usages in  [Assets/Scripts/Entities/Controls/Player.cs](Assets/Scripts/Entities/Controls/Player.cs).
- LunarConsole is integrated for easier debugging on mobile.
