# UltraAchievement Core
BepInEx plugin made to add support for simple custom achievements to (theorically) any BepInEx 5 compatible game
This mod does nothing on its own. At first, I made it for Ultrakill but decided to make it without any dependency to anything so anyone can use it anywhere.

# Installing
Download the latest realeased .DLL from the releases page and put it in your BepInEx plugin folder or if you use a modloader (ie UMM for ultrakill), put it in its mod folder

# Adding support for your mod
Adding support for this mod is extremely simple, just add the logic for detecting when the achievement should be given to the player then call 
```cs
Core.ShowAchievement(icon,string name,string description, string mod)
```
`icon` can either be a path to the icon or a `Sprite`. The mod will take care of the rest and keep track of previously acquired achievements so not duplicates shall occur.
Currently, the mod only allows you to show an achievement but in the future there may be more features.
`mod` lets you know what mod the achievement comes from
Also feel free to fork if you want to add anything. 


```cs
Core.ShowAchievementI(icon,string name,string description, string sprite, string mod)
```

`sprite` allows you to add a custom achievement background instead of using the default one