# FemboySurvivalCheats
 A simple BepInEx cheat plugin for [Femboy Survival](https://niemand2d.itch.io/femboy-survival) by [2DNiem](https://www.patreon.com/2dniem). The cheat is only guarenteed to work for Demo 7, so if it doesn't work on any other demo, let me know and I might try to fix it. [You can also try to fix it yourself](https://github.com/JMVRy/FemboySurvivalCheats/pulls) 😏😏😏

# Installation
 Download the zip file from the [Releases section](https://github.com/JMVRy/FemboySurvivalCheats/releases) and unzip it into the game's directory (should contain "Femboy Survival.exe")

# Keybinds
 The cheats have the following keybinds, with the corresponding command:
 * `F1`: toggles the visibility of the cheat menu
 * `O`: toggles player intangibility (nothing can grab you)
 * `P`: toggles player invulnerability (nothing can hurt you)
 * `V`: toggles the player's ability to always masturbate, even if their lust value is too low
 * `K`: kills all enemies currently spawned in
 * `L`: ends any event that is currently active, including masturbation or corruption events
 * `J`: sets the player's health to max
 * `I`: sets the wave's countdown to max
 * `Y`: levels the normal stats of the player up
 * `;`: levels the sexual stats of the player up

 The keybinds might appear random, but that's because it practically is. In the beginning, I wanted them to correspond to some specific key (`k` is kill), but halfway through coding this, I realized that I didn't have much room for the keybinds, so I just chose whatever I could get. Rather than remembering these keybinds, I suggest just using the menu lol
 
# Building or Modifying
 Create a directory named "lib" in the same directory as the "FemboySurvivalCheats.csproj" file, and copy the following files from the game's `Femboy Survival_Data/Managed` directory:
 * `Assembly-CSharp.dll`
 * `Unity.InputSystem.dll`
 * `UnityEngine.CoreModule.dll`
 * `UnityEngine.dll`
 
 If you don't want to copy the files to the directory, you can also [change the Assembly reference](https://learn.microsoft.com/en-us/visualstudio/ide/managing-references-in-a-project) for the project, but that may have unusual consequences, so beware. 
