# Color Ball Coding Task

![image](https://github.com/user-attachments/assets/29d88578-356b-407a-9714-ba642ece652f)

## Requirements
- Windows or WebGL

## How To Run?

### On The Windows Build
Run `colorBall.exe`

### On The WebGL Build
Open the `index.html` file in a web browser.

## Project Structure

### Scripts

- **UiManager.cs**
  Manages the UI elements such as the finish text and button, and updates the collectible count.
  ```csharp:Assets/Script/UiManager.cs
  startLine: 1
  endLine: 64
  ```

- **globalColor.cs**
  Defines a set of colors used in the game.
  ```csharp:Assets/Script/globalColor.cs
  startLine: 1
  endLine: 12
  ```

- **SceneManager.cs**
  Handles scene management, such as restarting the scene.
  ```csharp:Assets/Script/SceneManager.cs
  startLine: 1
  endLine: 10
  ```

- **PlayerAction.cs**
  Defines player actions as static events.
  ```csharp:Assets/Script/PlayerAction.cs
  startLine: 1
  endLine: 12
  ```

- **SoundManager.cs**
  Manages the sound effects for different player actions.
  ```csharp:Assets/Script/SoundManager.cs
  startLine: 1
  endLine: 64
  ```

- **LevelGenerator.cs**
  Generates the level by placing obstacles, collectibles, and the finish line.
  ```csharp:Assets/Script/LevelGenerator.cs
  startLine: 1
  endLine: 100
  ```

- **CameraBehaviour.cs**
  Controls the camera to follow the player and reset its position on player death.
  ```csharp:Assets/Script/CameraBehaviour.cs
  startLine: 1
  endLine: 100
  ```

- **playerController.cs**
  Manages player movement, interactions with obstacles, collectibles, and triggers actions on finish or death.
  ```csharp:Assets/Script/playerController.cs
  startLine: 1
  endLine: 100
  ```

- **RotationObstacleBehaviour.cs**
  Controls the rotation of obstacles.
  ```csharp:Assets/Script/RotationObstacleBehaviour.cs
  startLine: 1
  endLine: 20
  ```

- **horizontalObstacleBehaviour.cs**
  Manages the horizontal movement of obstacles.
  ```csharp:Assets/Script/horizontalObstacleBehaviour.cs
  startLine: 1
  endLine: 64
  ```

## License
This project uses the LiberationSans font, which is licensed under the SIL Open Font License, Version 1.1.


