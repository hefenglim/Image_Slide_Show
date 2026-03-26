[🇺🇸 English](README.md) | [🇹🇼 繁體中文](README.zh-TW.md)

# Image SlideShow — Smart Desktop Photo Slideshow with Memory Exploration

> A lightweight Windows desktop photo slideshow tool. Unlike typical slideshows, Image SlideShow lets you **seamlessly switch between Shuffle and Sequential mode with a single keystroke** — when a random photo sparks a memory, instantly switch to Sequential to browse neighboring photos from the same event or trip, then switch back to continue exploring. Drag-and-drop, borderless design. Great as a **digital photo frame**, a **personal memory explorer**, or a **clean presentation display**.

**Keywords:** `desktop slideshow` `photo viewer` `shuffle slideshow` `random photo player` `digital photo frame` `image slideshow windows` `drag and drop slideshow` `borderless slideshow` `fullscreen photo viewer` `photo memory explorer`

## Features

### 🖼️ Core Capabilities
- **Drag & Drop:** Instantly load folders or image files into the app.
- **Formats Supported:** `JPG`, `JPEG`, `PNG`, `GIF`, `BMP`, `TIF`.
- **INI Playlists:** Load predefined image lists directly from `.ini` files.

### 🎮 Easy Controls
| Action | Shortcut / Mouse |
|---|---|
| **Play / Pause** | `S` |
| **Toggle Shuffle / Sequential** | `Spacebar` |
| **Next / Previous Slide** | `→` `←` Arrow Keys or `Mouse Wheel` |
| **Toggle Clean Presentation Mode** | `Click Image` |
| **Enter / Exit Fullscreen** | `F` or `F11` |
| **Exit Presentation / Close App** | `Esc` |

### 🧠 Smart Playback

#### 🔀 Shuffle Mode
- Photos are randomly selected **without repeating**, until every photo has been shown once.
- After all photos have been displayed, the cycle resets automatically (an on-screen "Random Cycle Reset" message appears).
- Use `←` / `→` to navigate back and forth through the **last 32 randomly shown photos**.

#### ▶️ Sequential Mode
- Photos play in file/folder order, one after another.
- Use `←` / `→` to freely browse forward and backward through the photo list.

#### 🔄 Seamless Mode Switching — Relive the Moment
You can switch between Shuffle and Sequential **at any time** with `Spacebar`:

- **Shuffle → Sequential:** The app stays on the current photo. Now `←` / `→` let you explore **neighboring photos** in the original folder order — perfect for browsing photos from the same event or trip.
- **Sequential → Shuffle:** The app returns to where you were in the shuffle history and continues random playback from there.

> **Example:** A random vacation photo catches your eye → press `Spacebar` to switch to Sequential → use Arrow Keys to browse all photos from that day → press `Spacebar` again to resume random playback right where you left off.

#### Other Playback Features
- **Custom Duration:** Set the exact number of seconds each slide displays.
- **Visual Feedback (OSD):** On-screen text overlays for play, pause, shuffle, and navigation actions.

### 📋 Option Menu (via Status Bar)
| Option | Description |
|---|---|
| **Set Duration** | Configure slideshow interval in seconds |
| **Clear All Images** | Remove all loaded images from the playlist |
| **Save Image List** | Export the current playlist as an `.ini` file for later loading |
| **Show Taskbar** | Toggle visibility in the Windows taskbar |
| **Shuffle** | Toggle random / sequential playback |
| **Fullscreen** | Toggle fullscreen mode |
| **Always Top** | Keep window above all other windows |
| **About** | View version info, contact email, and donation link |

### ⚙️ System Integration
- **Persistent Settings:** Automatically saves your preferences (duration, shuffle, fullscreen, always-on-top, taskbar visibility) to `config.ini`.
- **Always on Top:** Keep the slideshow floating above other windows.
- **Borderless Window:** In presentation mode, the window has no title bar. You can still drag the window and resize it from any edge.

## Technical Overview

- **Language:** C#
- **Framework:** .NET Windows Forms
- **UI & Interaction:** Features custom borderless window dragging and resizing implementations directly overridden in the mouse events.
- **State Management:** Custom `IniRW` class used to serialize and deserialize simple application state cleanly.
- **Donation Modules:** Built-in hooks for PayPal donations (`PayPalDonate`).

## Getting Started

1. Set up the project in your preferred IDE (like Visual Studio) by opening `Image_SlideShow.sln`.
2. Build the solution.
3. Run the application.
4. Drag and drop any image file or folder containing images directly into the window to start the slideshow!
