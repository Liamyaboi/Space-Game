ReadMe

For this project, I created a game using Unity DOTS. I started by setting up the player and input system, using Unity’s new Input System. The player is designed to rotate and always face the mouse cursor, with shooting mechanics that fire bullets in the direction the spaceship is facing.
To keep things organized, I wrote separate scripts for each functionality
Player Input Handles input from the player.
Player System Contains all the core functions and logic related to the player.
Bullet Component A simple script to store data for the bullets.
Bullets are instantiated directly through the Player System.
Once the bullet mechanics were done, I worked on an Enemy Spawner that generates enemies outside the visible camera area. All entities and systems were set up in a sub-scene to keep things clean. The last step was to built an Enemy AI System to make the enemies move toward the player.
