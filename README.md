# Mathventures - A Magical Math Learning Adventure

A visually stunning educational game designed for 3rd-4th grade students to learn math through an engaging magical quest.

## Setup Instructions

1. Install Unity Hub and Unity Editor 2022.3 LTS or later
2. Clone this repository
3. Open the project in Unity Hub
4. Open the project in Unity Editor
5. In Unity Editor:
   - Go to File > Build Settings
   - Add all scenes from the assets/scenes folder to the build
   - Set platform to WebGL
   - Click Build
   - Select the 'Build' folder in the project directory
   - Wait for the build to complete

## Running the Game

After building:
1. Open index.html in a web browser
   - For security reasons, you'll need to serve the files through a web server
   - You can use Python's built-in server:
     ```bash
     python -m http.server 8000
     ```
   - Then open http://localhost:8000 in your browser

## Game Features

### Magical Realms
1. **Enchanted Forest of Addition**
   - Master addition with numbers 1-20
   - Help magical creatures count their treasures

2. **Subtraction Caves**
   - Learn subtraction within 100
   - Explore mysterious caves with number dragons

3. **Multiplication Mountain**
   - Practice multiplication facts up to 12
   - Climb to new heights of mathematical understanding

4. **Division Gardens**
   - Master basic division
   - Share magical flowers equally

5. **Fraction Castle**
   - Introduction to fractions
   - Split and combine numbers in magical ways

### Visual Effects
- Magical particle effects
- Dynamic character animations
- Themed environments
- Interactive UI elements
- Celebration effects for achievements

### Educational Content
- Grade-appropriate math problems
- Progressive difficulty system
- Various math operations
- Word problems with magical themes
- Achievement tracking
- Visual feedback for learning

## Controls
- Arrow keys to move
- Spacebar to jump
- Mouse click to select answers
- ESC to pause/menu

## Development

### Project Structure
```
mathventures/
├── assets/
│   ├── art/
│   │   ├── backgrounds/
│   │   └── sprites/
│   ├── audio/
│   ├── scenes/
│   │   ├── mainmenu.unity
│   │   ├── magicalquest.unity
│   │   ├── practice.unity
│   │   └── quiz.unity
│   └── scripts/
│       ├── Effects/
│       ├── managers/
│       ├── Player/
│       ├── Quest/
│       └── UI/
├── Build/
└── index.html
```

### Key Components

1. **Game Managers**
   - GameFlowManager: Controls game progression
   - ProgressManager: Handles save/load
   - MathProblemGenerator: Creates appropriate math challenges

2. **Quest System**
   - QuestSystem: Manages the magical journey
   - QuestConfiguration: Defines themed stages

3. **Visual Systems**
   - VisualEffectsManager: Handles particles and effects
   - PlayerCharacter: Controls character movement and animation
   - QuestUI: Manages game interface

## Requirements

- Unity 2022.3 LTS or later
- Modern web browser with WebGL support
- Minimum screen resolution: 900x600

## Support

For issues or questions:
1. Check the Unity Console for error messages
2. Verify all scenes are included in the build
3. Ensure all required assets are present
4. Check browser console for WebGL-related issues
