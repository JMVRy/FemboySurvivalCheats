# FemboySurvivalCheats

A simple BepInEx cheat plugin for [Femboy Survival](https://niemand2d.itch.io/femboy-survival) by [2DNiem](https://www.patreon.com/2dniem).

> **Note:** This cheat is only guaranteed to work for Demos 7 through 16. If it doesn't work on another demo, [let me know](https://github.com/JMVRy/FemboySurvivalCheats/issues) or [submit a pull request](https://github.com/JMVRy/FemboySurvivalCheats/pulls).

## Features
- Toggleable cheat menu
- Player intangibility & invulnerability
- Always allow masturbation
- Instantly kill all enemies
- End any active event
- Max health & wave countdown
- Level up normal and sexual stats

## Installation
1. Download the latest zip from the [Releases section](https://github.com/JMVRy/FemboySurvivalCheats/releases).
2. Unzip it into the game's directory (the one containing `Femboy Survival.exe`).

## Keybinds
| Key | Command                                              |
| --- | ---------------------------------------------------- |
| F1  | Toggle cheat menu visibility                         |
| O   | Toggle player intangibility (nothing can grab you)   |
| P   | Toggle player invulnerability (nothing can hurt you) |
| V   | Always allow masturbation                            |
| K   | Kill all enemies currently spawned in                |
| L   | End any active event                                 |
| J   | Set player's health to max                           |
| I   | Set wave's countdown to max                          |
| Y   | Level up normal stats                                |
| ;   | Level up sexual stats                                |

> The keybinds may seem random due to limited available keys. For convenience, use the in-game menu.

## Building or Modifying
1. Create a directory named `lib` in the same directory as `FemboySurvivalCheats.csproj`.
2. Copy the following files from the game's `Femboy Survival_Data/Managed` directory into `lib/`:
   - `Assembly-CSharp.dll`
   - `Unity.InputSystem.dll`
   - `UnityEngine.CoreModule.dll`
   - `UnityEngine.dll`

   *Alternatively*: You can [change the Assembly reference](https://learn.microsoft.com/en-us/visualstudio/ide/managing-references-in-a-project) for the project, but this may have unintended consequences.

3. Build the project using your preferred .NET compiler (VS2022, dotnet, Mono, etc.).
4. Download a compatible [BepInEx version](https://builds.bepinex.dev/projects/bepinex_be), extract it into the game directory, and place `FemboySurvivalCheats.dll` into the `BepInEx/plugins` directory.

---

Feel free to open an issue or pull request if you have suggestions or improvements!
