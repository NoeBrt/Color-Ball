<h1 align="center">Color Ball Coding Task</h1>


<h2 align="center">Requirements</h2>

- Windows or WebGL
- Package Unity Universal Pipeline
- Package Splines

<h2 align="center">How To Run?</h2>

### On The Windows Build
Run `colorBall.exe`

### On The WebGL Build
Open the `index.html` file in a web browser.

<h2 align="center">Game Presentation</h2>

### Obstacles
The game is composed of 3 obstacles: a circular one made by the arc asset, a horizontal one made by the triside asset, and the last one made with multiple circles following a spline trajectory.

### Levels
All the levels are generated randomly.

### Game Loop
The player has to jump until reaching the "Finish!" where they can change levels. If the player collides with an obstacle, they respawn at the initial position and have to redo the level. If the player is out of bounds, they die and respawn at the initial position.

### Collectibles
Stars that increment a stars count and a color switcher that changes the player's color.

<h2 align="center">Gameplay Choice</h2>
I wanted to make the game fast to launch and play, without an overwhelming UI. I made the choice to integrate the UI into the player's environment (except for the collectible count). The communication between the UI & Sound and the player is handled with Event Action. A minimalistic choice was made for the sound design by not using sound when jumping, as it could become annoying repetitively. The input is very limited, so I decided to use the Input.Get method for rapid implementation.

<h2 align="center">Project Structure</h2>

### Scripts

- **UiManager.cs**  
  Manages the UI elements such as the finish text and button, and updates the collectible count.

- **globalColor.cs**  
  Defines a set of colors used in the game.

- **SceneManager.cs**  
  Handles scene management, such as restarting the scene.

- **PlayerAction.cs**  
  Defines player actions as static events.

- **SoundManager.cs**  
  Manages the sound effects for different player actions.

- **LevelGenerator.cs**  
  Generates the level by placing obstacles, collectibles, and the finish line.

- **CameraBehaviour.cs**  
  Controls the
