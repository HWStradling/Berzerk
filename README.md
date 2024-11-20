# Berzerk (Project Revival - Work in Progress)

This is a **top-down shooter** game made with unity engine.
It is loosely based upon the arcade game 'Berzerk' developed in 1980 by Alan McNeil. 
The project was originally a university coursework i developed from scratch to demonstrate a number of different game development patterns,
however I think, given some tweaks and polishing, it could be an enjoyable little game.


## Project Status:

I've revived this project after shelving it for a while., I have fixed some simple bugs, found more, and have planned some new features and tweaks.

## Current Features:

### Player:
- Partial Berzerk style coupled movement and aiming direction (aiming allowed in faced direction).
- Player can switch between multiple weapons.
- Facilitates movement inputs and sprinting with corresponding directional walking and running animations.
- Berzerk style slow projectile but using weapons, each weapon has different firing characteristics.
- Delayed healing functionality with animations and a three life respawn mechanic. I used the observer pattern with unity events for decoupling.

### Enemy:
- Basic Enemy AI utilising randomised patrol patterns and last known location player detection.
- Enemy combat utilises Berzerk style slow projectiles.
- Multiple enemy types with modifications to speed, attack delay, and max health.

### Pickup System:
- Multiple pickups in the form of equipable weapons.
- Pickups stored in a player inventory.

### Achievements Queue:
- Singleton based unity event queue for decoupling the triggering event from displaying the notification.

### Save System:
- Serializes and stores the players inventory, max level reached, and unlocked achievements.

### UI:
- Basic UI with a health bar, save and exit butttons, an animated text window to display achievements, icons to display the unlocked and currently equipt weapons.

### Menu System:
- Basic menu system including an options page to change the music volume, switch profiles, and to display achievements and unlocked levels.

## Planned Fixes/Improvements:

- Couple chosen volume with player profile/saves,
- Allow custom profile creation and rework profile UI dropdown element population on options page.
- Modify the achivements window to display input validation and other notifications.
- Decouple movement and shooting (while Berzerk-like, it gives a clunky gameplay experience).
- Rework gun animations to function with decoupled movement&shooting.
- Generally improve game feel, faster player/enemy movement speed, tweak projectile speed, tweak sprinting speed, modify strafing logic.