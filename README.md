# Unity2DGameBase
A basic starting point for unity projects. Includes: a saving system, resolution system, audio system and prefab / scriptable object autoload system

2DGameBase -- Copyright 2020 Cherry Blossom Games LLC.
https://twitter.com/games_blossom

LICENSE - GameBase is Public Domain and is Lisensed under Creative Commons Zero
https://creativecommons.org/share-your-work/public-domain/cc0/
You may use GameBase as you see fit, no warranty or functionality is guaranteed or implied. No credit to the author is required.

**GameBase has the following Features:**
**0) Installing GameBase
**1) AutoLoad Prefabs and Scriptable Objects**
**2) Example UI**
**3) Audio System**
**4) Resolution System**
**5) Save / Load / Delete System**

**2DGameBase was developed on Unity version 2019.4.9**

**0) Installing GameBase**
**GameBase is not a complete unity project. You must create a unity project first with unity hub then copy 0_GameBase into your project's Assets folder.**
1) Download ZIP archive from github. Extract folder: Assets/0_GameBase into your project's asset folder.
2) Update TextMeshPro and import the text mesh pro essentials. You will be prompted to do this the first time you open 
3) Using GameBase:  For your scripts to utilize any of the GameBase systems, put: using GameBase; at the top of your script that needs access to a particular system.

GameMaster NOTE: GameMaster works by using: [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
So that means, it will run before any scene loads. It does not need to be in your game scene.

**1) AutoLoad Prefabs and Scriptable Objects**
Put any prefab or scriptable object asset into: Assets/0_GameBase/Resources/AutoLoad
and it will be loaded into the game before the scene starts by GameMaster.
Scriptable Objects are accessible by using GetSO: GameMaster.GetSO<ScriptableObjectName>();

**2)Example UI**
GameBase/Scenes/BaseUI -- Provides two panels. The first panel shows how to implement Audio Sliders with using the Unity Mixer. The second panel shows how to implement a resolution menu.

**3)Audio System**
Assets/GameBase/Resources/AutoLoad/Audio Group.prefab -- This is the primary audio system.
The audio system is composed of three Channels and a Master Volume.
You can not send any audio to the master channel, its only purpose is to provide a global maximum volume level for the other 3 channels.

Each piece of the Audio System is assigned a static reference.

Send audio to FX: SoundFX.instance.playSoundFx(AudioClipName); //SoundFX by default has 10 audio sources, you can play up to 10 FX at a time
Send audio to Voice: Voice.instance.playDialogue(AudioClipName); //Voice has only 1 audio source
Send audio to Music: Music.instance.playMusic(AudioClipName); //Music has only 1 audio source
Music also has methods to stop, fade, and duck/unduck (decrease volume temporarily / unduck to restore volume).

The 2D audio prefab in: Assets/0_GameBase/Resources/AutoLoad/Audio Group.prefab is suitable for 2D games. It does not have any 3D sound applied to any of its components.
There are four distinct parts to the audio system:

Master Volume: Change master volume by using: MasterAudio.instance.changeMaxVolume(float);

**Music:**
Change music Volume: Music.instance.changeMaxVolume(float);
Play Music: Music.instance.playMusic(AudioClip);
Fade Out Music: Music.instance.fadeOutMusic(float);
Stop Music: Music.instance.stopMusic();
Duck Music: Music.instance.DuckMusic();
UnDuck Music: Music.instance.UnDuckMusic();

**Sound Effects:**
Ten sound 2D effect audio sources are provided. This means up to 10 sound effects can be played simultaneously.
Change Sound Effects Volume: SoundFX.instance.changeMaxVolume(float);
Play Sound Effect: SoundFX.instance.playSoundFx(audioclip);

**Voice / Dialogue Channel:**
Change voice volume: Voice.instance.changeMaxVolume(float);
Play voice clip: Voice.instance.playDialogue(AudioClip clip);
Determine if a voice clip is playing with: if (Voice.instance.isPlaying){ Debug.log("Voice clip is playing"); }

**4) Resolution System**
Resolution System is composed of two scripts:
Assets/GameBase/Scripts/Resolution/ResolutionSetBoot -- This script is called automatically by GameMaster at the start of the game. It sets the resolution the user chose.
Assets/GameBase/Scripts/Resolution/ResolutionMenu -- This script allows the user to change resolution in the game using unity's UI system.


**5) Save / Load / Delete System**
The Scriptable Objects mentioned here are duplicated on Game Start. So it is safe to play the game and make changes during gameplay, it will not affect the scriptable objects in the assets folder.
To make a change permanent in the assets folder the game in the editor must be stopped and then you can make changes that will be saved in the asset.

**Save System 1: Game Preferences**
This is "systems" preferences that would be shared regardless of which savegame the player wants to use.
By default only the volume preferences are saved in GameDataSO. This is a good place to put unlockables and achievements.
The Game Preference Scriptable Object is: Assets/GameBase/Resources/AutoLoad/GameDataSO.asset
The Game Preference Script is: Assets/GameBase/Scripts/ScriptableObjects/GameDataSO.cs

A premade set of functions are made for Save / Load / Delete for Game Preferences:
GameMaster.LoadGameData();
GameMaster.SaveGameData();
GameMaster.DeleteGameData();

Example: Get GameData variables by using: GameMaster.GetSO<GameDataSO>().masterMaxVolume;

**Save System 2: Player Data**
This is where you should save all of your player's progression information, health, collectables, whatever.
The Player Data Scriptable Object is: Assets/GameBase/Resources/AutoLoad/PlayerSO.asset
The Player Data Script is: Assets/GameBase/Scripts/ScriptableObjects/PlayerSO.cs

A premade set of functions are made for Save / Load / Delete for Player Data.
Each of these functions require an integer. This signifies the "slot" of the data file. You can have an almost infinite number of player save slots if you so desire.
GameMaster.LoadPlayerSO(slotNum);
GameMaster.SavePlayerSO(slotNum);
GameMaster.DeletePlayerSO(slotNum);

Example: Get PlayerData variables by loading the slot data first, then by using: GameMaster.GetSO<PlayerSO>().currentHealth;