# FemboySurvivalCheats
 A simple BepInEx cheat plugin for [Femboy Survival](https://niemand2d.itch.io/femboy-survival) by [2DNiem](https://www.patreon.com/2dniem). The cheat is only guarenteed to work for Demo 7, so if it doesn't work on any other demo, let me know and I might try to fix it. [You can also try to fix it yourself](https://github.com/JMVRy/FemboySurvivalCheats/pulls) 😏😏😏

# Installation
 Download the zip file from the [Releases section](https://github.com/JMVRy/FemboySurvivalCheats/releases) and unzip it into the game's directory (should contain "Femboy Survival.exe")
 
# Building or Modifying
 Create a directory named "lib" in the same directory as the "FemboySurvivalCheats.csproj" file, and copy the following files from the game's `Femboy Survival_Data/Managed` directory:
 * `Assembly-CSharp.dll`
 * `Unity.InputSystem.dll`
 * `UnityEngine.CoreModule.dll`
 * `UnityEngine.dll`
 
 If you don't want to copy the files to the directory, you can also [change the Assembly reference](https://learn.microsoft.com/en-us/visualstudio/ide/managing-references-in-a-project) for the project, but that may have unusual consequences, so beware. 