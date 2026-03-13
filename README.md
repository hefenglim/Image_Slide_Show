# Image SlideShow

A feature-rich, lightweight C# Windows Forms application for displaying image slideshows. It supports drag-and-drop, randomized order (shuffle), fullscreen mode, and customizable slide durations.

## Features

### 🖼️ Core Capabilities
- **Drag & Drop:** Instantly load folders or image files into the app.
- **Formats Supported:** `JPG`, `PNG`, `GIF`, `BMP`, `TIF`.
- **INI Playlists:** Load predefined image lists directly from `.ini` files.

### 🎮 Easy Controls
| Action | Shortcut / Mouse |
|---|---|
| **Play / Pause** | `S` |
| **Toggle Shuffle / Sequential**| `Spacebar` |
| **Next / Previous Slide** | `Arrow Keys` or `Mouse Wheel` |
| **Toggle Clean Presentation Mode**| `Click Image` |
| **Enter / Exit Fullscreen** | `F` or `F11` |

### 🧠 Smart Playback
- **Dynamic Exploration:** Seamlessly switch between **Sequential** and **Shuffle** modes anytime.
- **Relive the Moment:** Did a random photo catch your eye? Toggle off Shuffle, and use the Arrow Keys to explore other photos taken around that exact time/event!
- **Smart Shuffle:** Remembers the last 32 images to prevent immediate repetition while discovering forgotten memories.
- **Custom Duration:** Set the exact number of seconds each slide displays.
- **Visual Feedback (OSD):** On-screen text overlays for play, pause, and shuffle actions.

### ⚙️ System Integration
- **Persistent Settings:** Automatically saves your preferences to `config.ini`.
- **Always on Top:** Keep the slideshow floating above other windows.

## Technical Overview

- **Language:** C#
- **Framework:** .NET Windows Forms
- **UI & Interaction:** Features custom borderless window dragging and resizing implementations directly overridden in the `Form1.cs` mouse events.
- **State Management:** Custom `IniRW` class used to serialize and deserialize simple application state cleanly.
- **Donation Modules:** Built-in hooks for PayPal donations (`PayPalDonate`).

## Getting Started

1. Set up the project in your preferred IDE (like Visual Studio) by opening `Image_SlideShow.sln`.
2. Build the solution.
3. Run the application.
4. Drag and drop any image file or folder containing images directly into the window to start the slideshow!
