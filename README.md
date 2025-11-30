Surface Nets SDF Visualizer (Unity 6+)

** HUMAN PART **
Hello fellow humans, if we haven't been exterminated by the time of your reading this. I, David, am writing this part - the rest has been written by AI, though with extensive prompting. 
I have a "passion-esque" project ("passion" is a little strong) of writing a crafting game with focus on "fun" procedural generated infinite worlds. I feel like many crafting games these days are missing part of what makes them so much fun, and I want to focus on what that means to me. I'm well versed in iOS coding, but not at all in anything else. So I decided to use AI to help me. This has had some great advantages and a ton of disadvantages, as I'm able to do things I couldn't or would have to take a long time to learn, but at the same time I'm somewhat dependent on AI which consistently leads me down the wrong path.
Fairly deep into the project, though I had multiple times laid out my plan for terrain destruction, AI told me that I'd need to use voxels for terrain manipulation beyond up and down. Not cool AI, as we'd been through this before. At that point, AI became useful again and we discussed using different methods of destruction. I don't want a minecraft clone, but, at the same time, want a low-poly aesthetic. So marching cubes or surface nets was the way to go. Surface nets eventually won out since it seems to be more efficient and more accurate at the same time. I wanted to understand how it works and found almost nothing online that explained, step-by-step.
After some sample projects, I thought I'd use agentic-AI to create visualization that I could post and hopefully help someone else.
I created a spec and posted it to Codex, so that I could work on other things while Codex creates this project.
As per usual, what it did was incredible and incredibly flawed at the same time. Great for proof-of-concept, not useable as something to post.
Currently in the process of debugging and will hopefully get something out. But at the same time, I don't want to spend what little time I have (with a wife and baby and full-time job) working on this instead of the main project... we'll see.
** END HUMAN PART ***

A step-by-step, visual explanation of Surface Nets meshing using Signed Distance Fields (SDFs) inside a 2D Unity 6+ project.
This project is designed for developers who want to see how SDF sampling, sign changes, interpolation, and mesh reconstruction work in practice.

The project uses a strict step-driven workflow executed with Codex / AI agents following a formal project SPEC.

📘 Overview

This Unity project visualizes:

A 2D sampling grid

Signed Distance Field (SDF) values at each sample point

Inside/outside sign coloring

Animated SDF vectors

Edge zero-crossing detection

Linear interpolation to place surface vertices

A full Surface Nets contour derived from those vertices

Optional camera/visual polish

The result is an educational, debugging-friendly visualization of the Surface Nets workflow.

📁 Project Structure
Assets/
  Scenes/
    SurfaceNets2D.unity
  Scripts/
    Grid/
    SDF/
    Visualization/
docs/
  SURFACE_NETS_SPEC.md   <-- Main AI specification
README.md


All AI-assisted changes follow the rules and architecture defined in
docs/SURFACE_NETS_SPEC.md.

🤖 AI Workflow (Codex / Cursor)

This repository uses a SPEC → PROMPT → CODE → TEST → CONTINUE workflow.

1. SPEC (persistent rules)

All architecture, rules, coding standards, and the step-by-step build plan live in:

docs/SURFACE_NETS_SPEC.md

2. PROMPT (task request)

When using Codex or Cursor Agent Mode, prompts must be structured like:

Follow /docs/SURFACE_NETS_SPEC.md.
Implement Step X.
Stop after Step X and wait for confirmation.


Codex reads the SPEC directly from the repo and applies the rules.

3. CODE (AI output)

Codex produces only the code defined in the requested step.

4. TEST (User)

You run Unity 6+, play the scene, verify behavior, and confirm.

5. CONTINUE

You request the next step only when ready.

🧩 Build Plan (Summary)

The SPEC defines these steps:

Base grid visualization

Grid sample points

SDF shape (circle)

Sign visualization

SDF vector animation

Edge interpolation

Full vertex extraction

Mesh reconstruction

Optional polish

Codex should implement these steps one at a time, waiting for confirmation between steps.

🎯 Goals of This Project

Create the clearest possible visualization of Surface Nets

Build a reusable tool for future SDF/debugging work

Provide a teaching-quality breakdown for video/YouTube explanations

Build an AI-driven Unity project with predictable, controllable behavior

🛠️ Requirements

Unity 6+

Git + GitHub

Recommended: Cursor IDE or OpenAI Codex workflow

Environment tested on:

macOS / Windows

Unity 6.x

C# 9 and up

🚀 Getting Started

Clone the repo:

git clone https://github.com/yourusername/SurfaceNetsVisualizer.git


Open Unity 6+.

Load the scene:

Assets/Scenes/SurfaceNets2D.unity


Use Codex/Cursor prompts to build the project incrementally following the SPEC.

📜 License

MIT License (customize if needed).

💬 Questions / Next Steps

Ask Codex to begin with:

Follow /docs/SURFACE_NETS_SPEC.md.
Implement Step 1: Base Grid Visualization.
Stop after Step 1 and wait for confirmatio
