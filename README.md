Enhanced-Defence
======================

##About:
Discussion and Latest Download available on the Official Rimworld forums at:  
https://ludeon.com/forums/index.php?topic=6636.0

With a Download Mirror on the Nexus:  
http://www.nexusmods.com/rimworld/mods/62/?

View the Readme document (including changelog) online at GitHub for the correct formatting and most recent document updates:  
GitHub is a source only release, if you want to run the binaries please get them from nexus or mediafire (links on the forum thread).
https://github.com/jaxxa/RimWorld-Enhanced-Defence  

Also check the GitHub wiki for more detailed information about the individual Modules:  
https://github.com/jaxxa/RimWorld-Enhanced-Defence/wiki  

If you are having trouble please Read this FAQ, it has instructions on logging an issue at the end of it:  
https://github.com/jaxxa/RimWorld-Enhanced-Defence/wiki/FAQ-Problems

Note that the downloads on Github are to be considered development builds and may not be working at any time.  
They also do not provide binaries and will have to be compiled from source in order to be played. Most people will want to download a binary release from the Forum page.

##Overview:
This mod has been created with a number of distinct modules. Theses are designed to allow you to only have to use the parts of the mod that you want to.

##Installation instructions

1. Extract the .zip file somewhere temporarily (desktop works)
2. Enter the folder that you extracted until you go into the folder that contains README.md (this document) and a number of folder starting with "ED-", these folders are the different modules that are available in this mod.
3. Find your Rimworld mods folder. This will be wherever you installed/extracted Rimworld to, and the "Mods" folder will be inside it. It will contain a folder called "Core" and folders for any other mods that you have installed previously.
4. If you have an old version please delete the existing ED- Modules. (or move them, see next section on Upgrading)
5. Copy the modules (the different folders from step 2) into the Rimworld mods folder (found in step 3)
6. Open Rimworld and Enable the Enhanced Defence Core Mod "ED-Core" (because other things depend on the core module this has to be enabled first)
7. Restart Rimworld
8. Enable any other modules that you want to use
9. Play the game

If you are having trouble please Read this FAQ:  
https://github.com/jaxxa/RimWorld-Enhanced-Defence/wiki/FAQ-Problems

##Upgrading

I cant guarantee that upgrading mid game will work and I would suggest that it would be better to start a completely new colony.  
That said a lot of the time it has worked perfectly and if you want to try this I suggest doing the following steps.

1. Before upgrading create a save called something like "Pre upgrade" of your current colony.
2. Follow the Installation instructions in the previous section, with the exception of moving the existing ED modules instead of deleting them.
3. Fire up the game and test if it is working, if so then all is good.
4. If not you can delete the new modules that you installed and copy back the old working modules
5. Load your saved game and de-construct all of the buildings that were added by this mod
6. Save the game
7. Follow the Installation instructions in the previous section again.
8. Fire up the game again and see if it works, if so you should be able to now build the new buildings
9. If it still does not work you will have to make a choice to continue with your current colony or get the updated version now.

##Details:
For details on the individual modules please look at the GitHub wiki:  
https://github.com/jaxxa/RimWorld-Enhanced-Defence/wiki

##ChangeLog:  
~~~
0.01  
- Initial Release  
0.02  
- Reduced Research Cost  
0.03  
- Changed Personal Shields to be Completely Standalone  
	- No research requirements for base shields  
	- Separate copies of the Textures  
- Added Embrasures  
- Update description of OmniGel Research
- Added Wireless power increase / decrease by 1000 options
0.04
- Fixed Wireless Power Node not loading icons correctly after the game has been reset.
- Fix to the starting Ammo of some types of Mortars
- Fixed what walls / doors can be covered by SIF Shields
0.05
- Fix for Memory leak relating to the Shield Display
- Added option to Hide Shield visual display
- Only one Laser Drill Can be active at a time
- Power carrying Embrasures.
0.06
- Fix for SIF Shield not restoring correctly when loading a saved game.
- Fix for potential null pointer errors relating to projectiles without a faction
- Changed OmniGel textures to use the ones provided by Omni
0.07
-Changes to Shields
--Standard / Small / Fortress / SIF Shields
-Allow Artillery to FlyOver Shields
-DropPod Intercept Mode
-Fix to Toggle of Visually Showing Shields not being restored on Save Load
-Shield Texture Updates
0.08
-Added ED-VisibleRadius module
--Allows you to keep the radius up for Trade beacons and SunLamps after deselecting them
-Rebalanced (Reduced) various research costs
-Added ED-WallConnect module
--Visual update to allow walls and natural rock to connect with each other
--Also applied to Embrasures (even without this module)
-Added ED-Plants24H module
--Plants will grow 24 Hours a day if they have sufficient lighting
--Effects PlantPotato, PlantStrawberry, PlantCotton, PlantDevilstrand, PlantRose, PlantDaylily
--Also changes OmniGel (even without this module)
-Fixed bug that allowed EMRG to spawn on mechanoids
-Fortress shield is now 2x2 in size
-SIF Shield should now function for any Wall and Door
-Droppod will not activate undercover
0.09
-Updated to Alpha8
-Removed ED-WallConnect
--Now implemented in new Alpha of base Game
0.10
-Removed VisibleRadius for TradeBeacons until it can work properly
-Fixed Omnigel not being processed into anything
-Added more things that can be made from Omnigel
--Balancing ideas welcome
0.11
-Added Vent System
--Shares graphics with cooler
--Power it on to equalise the temperature between two rooms
--Made out of Stuff, same health as a wall from that stuff
--Omnidirectional
--All the buttons except the power button do nothing
0.12
-Updated Graphic for Vent
-More specific install instructions
0.13
-Initial Stargate Release
-Moved Common Textures into Core Mod
--Mainly UI in modules that already require Core
0.14
-Fixes to Mortar turrets to bring them in line with Alpha 8 changes
-Increased Cost of Stargate
-Increased Stargate Power Consumption
-Added charge options
--Increase the power consumption of the Stargate for a reduction in charge time
-Removed Autoloader Research Requirement
--Would have been problematic to be used with offworld gates
-Stargate Dynamic Save location
--Change where the stargate .xml files are saved and read from
--Most people will want to leave this alone, but it might be helpful for trying more complex stuff or bug fixing.
-Updating / Balancing OmniGel recipes based on resource cost
0.15
-Redone vent logic
-increased stack amount for OmniGel
-Adds reverse cycle Cooler
--Identical to the standard cooler except that it can be freely rotated after being placed.
0.16
-Update to Alpha 9
-Removed "ED-MortarAmmo"
--Similar feature now implemented in the base game
-Removed "ED-EMRG turret"
--Was mainly a demo for turrets using Ammo and that is now in the base game
--Might come back later in some form.
-Increased size of Lazer Drill, brought in line with geothermal plant
-Reduced size of Laser Filler to better fill in tight spaces to remove useless geothermal vents.
-Reworking Personal Shields
--Will not require custom Pawns anymore 
--I got part way through updating this when Latta uploaded an awesome mod with a bunch of assets and code I could use.
---"Shooter's shield / Customizable personal shield" https://ludeon.com/forums/index.php?topic=10994.0
0.16.1
-Changes To personal Shields
--Renamed Personal shields to be "Nanite Shield Modulator"
---To better differentiate between stock shields / shields added in other mods.
--Removed tags from personal shields to stop pawns from spawning with them initially
---Not that it did them much good because they did not start charged.
--Changed layers Overhead, should now fit over armour
--Require Personal shields to be fully charged before being brought on-line
0.17.0
-Updating to Alpha 10
-Fixed Repair mode display bug
-Removed ED-VisibleRadius
--Now Sunlamps can create grow zones and Trade beacons can create stockpiles
---This removes the main use for this module
-Personal Shields Ignore Certain damage types:
--HealGlobal
--HealInjury
--Repair
--RestoreBodyPart
--SurgicalCut
0.17.1
-Fix to OmniGel giving potatoes
0.18.0
-Initial Alpha of Vehicle module
-Fix for Personal Shields
--Not recognising when charger is disabled
--Attempting to charge full shields
-Laser Filler now works on Area of Effect
-Removed EmbrasureConduit 
-SIF Shielded buildings are now read in from XML to be easily edited.
--Included by default are:
---Sandbags
---Wall
---Door
---Autodoor
---Embrasure
0.18.1
-Fix for error in loading / saving SIF shields
0.18.2
-Fix for Show Visually / Repair mode
0.19.0
-Update to Alpha 11
~~~

##License:
This work is licensed under http://creativecommons.org/licenses/by/4.0/

##Credit / Thanks:

Thanks to Alphathon for:
* Creating the Stargate graphic that I edited slightly
 * http://commons.wikimedia.org/wiki/File:Stargate.svg

Thanks to Architect for:
* Allowing the Code from BetterPower+ to be used.
 * Currently using the LaserDrill

Thanks to Darker for:
* making the original Shield mod, and thanks for allowing anything to be done with it.
 * http://ludeon.com/forums/index.php?topic=2677.msg24772#msg24772

Thanks to EdB for:
* Allowing people to look through his UI code.

Thanks to Haplo for:
* Providing a tutorial on animating images.
* Providing the MAI source code, was invaluable in figuring out how to work with Pawns for the initial personal shields and vehicles.

Thanks to Kulverstukass for:
* Awesome bug reporting and feedback.

Thanks to Latta for:
* Allowing people to use his personal shields mod.
 * https://ludeon.com/forums/index.php?topic=10994.0
 
Thanks to mrofa for:
* Providing code in "Clutter Mod" that was helpful to look at for figuring out better ways to do Fire Suppression and Command Switches
* Allowing me to use his art from the BetterPower+ Mod
 * Laser Drill
 * NuclearLamp used for Power Node  

Thanks to obuw for:
* Providing the base of some new icons

Thanks to Omni for:
* Providing new textures for the OmniGel system
 
Thanks to PunisheR007 for:
* Creating the Cannons and Turrets mod that is the basis for the Cannon Ammo and Turrets module (Unreleased)
 * http://ludeon.com/forums/index.php?topic=3878.0
* Creating the Embrasures  mod that is the basis for the Embrasures module  
 * http://ludeon.com/forums/index.php?topic=3961.0

Thanks to skullywag for:
* The use of his StunGun Graphic (unreleased)

--Jaxxa
