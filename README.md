Surface Nets SDF Visualizer (Unity 6+)

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