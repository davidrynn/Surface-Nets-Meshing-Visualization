Surface Nets SDF Visualizer – Codex Guide
1. Project Overview

This repository is a Unity project whose goal is to create an interactive 2D visualization of:

A grid of sample points.

An implicit shape (via a signed distance field / SDF).

Sign changes along grid edges (inside vs outside).

Interpolated surface vertices (surface nets logic) along those edges.

A resulting mesh/contour formed from those vertices.

The visualization will be animated step-by-step to help experienced developers understand how SDF sampling and surface nets meshing work.

Important: This project is for learning and visualization first, not for shipping game content. Clarity and correctness are higher priority than runtime performance or advanced optimizations.

2. Codex’s Role in This Project

Codex acts as a coding assistant, not an autonomous architect.

The human (David):

Decides the overall design and learning goals.

Controls the sequence of steps.

Runs Unity, tests changes, and reports back what worked or failed.

Codex:

Writes and refactors C# scripts, shaders (if needed later), and small editor utilities.

Updates the Unity project structure incrementally.

Explains what it changed in clear, concise terms.

Waits for explicit instructions before moving to the next step or adding new features.

Codex should:

Prefer small, focused changes over large, sweeping rewrites.

Treat each user request as a single step in a larger plan.

Assume the Unity scene and project are being tested between steps and that the user will report issues.

3. Tech Stack & Project Setup

Target environment:

Unity: recent LTS version (e.g., 2022 or 2023 LTS), 2D capable.

Language: C#.

Render pipeline: Built-in or URP is acceptable; this project starts in 2D.

Platform: Desktop (editor play mode) only.

When asked to create or modify the Unity project, Codex should:

Assume a new Unity 2D project (or basic 3D with orthographic camera used as 2D).

Use default Unity input & camera unless explicitly asked to customize.

Not introduce unnecessary assets or packages. Avoid:

Random extra Unity packages.

Third-party libraries, unless explicitly requested.

4. Step-by-Step Collaboration Workflow

The project will be built sequentially, with human confirmation at each step.

Key rule for Codex:
Never proceed to the next conceptual step unless the user explicitly asks you to.

Planned High-Level Steps

Codex will receive explicit prompts for each of these; do not skip ahead:

Base project + grid visualization skeleton

Set up basic scene and scripts.

Visualize a simple grid (no points yet).

Grid with sample points

Draw sample points at grid intersections.

Provide hooks for later annotation (colors, labels).

Implicit shape (SDF) inside the grid

Implement an SDF for a simple shape (e.g., circle).

Compute and store SDF values at each sample point.

Sign visualization (inside vs outside)

Color/label sample points based on sign of SDF.

Clearly show sign changes along edges.

SDF vector visualization

For specific points, show an animated vector from sample point toward the surface (distance direction).

Interpolation along edges

Implement interpolation between two sample points with opposite sign.

Animate a marker sliding along the edge into its interpolated position (surface vertex).

Multiple vertices and mesh/contour

Generalize to all relevant edges.

Create and display a line/mesh representing the surface.

Optional polish

Camera moves, timing, UI controls, tooltips, etc.

For every step where Codex modifies code:

Assume the user will:

Save changes.

Switch to Unity and hit Play.

Verify the behavior visually.

Return with feedback or permission to proceed.

Codex should:

Treat “await my confirmation” literally:
If the user hasn’t explicitly said “That works, continue to the next step,” Codex should stay within the current step’s scope (bug fixes, refactor, minor visual tweaks).

5. Coding & Project Structure Conventions
5.1 Namespaces and Folder Structure

Use a simple, consistent structure. Example:

Assets/Scripts/Grid/

GridConfig.cs – simple configuration data (grid size, spacing, etc.).

GridRenderer2D.cs – draws lines and/or grid visuals.

Assets/Scripts/SDF/

SdfShape2D.cs – abstract/base class or interface for 2D SDFs.

SdfCircle2D.cs – concrete implementation of a circle SDF.

Assets/Scripts/Visualization/

SamplePointVisualizer.cs – handles sample point drawing and sign coloring.

SdfFieldVisualizer.cs – orchestrates SDF sampling over the grid.

SurfaceNets2DVisualizer.cs – handles edge interpolation and vertex placement.

Assets/Scenes/

SurfaceNets2D.unity – primary visualization scene.

Namespace suggestion (or similar):

namespace SurfaceNets2D
{
    // Classes here…
}


Codex should avoid creating deeply nested namespaces unless specifically requested.

5.2 MonoBehaviour Style

Prefer one responsibility per MonoBehaviour, roughly:

One script focused on data/config.

One script focused on rendering a visual aspect.

One script focused on orchestration/flow.

Use [SerializeField] private fields instead of public fields for inspector references.

Use clear, descriptive names: gridSize, cellSize, samplePrefab, pointInsideColor, etc.

5.3 Animation Style

Animations can be implemented in simple, script-driven ways:

Use coroutines to control timing and sequences.

For example:

Fade in grid.

After a delay, highlight one cell.

Animate SDF arrows into view.

Animate interpolation marker along an edge.

Keep animation logic readable and separated from the math where possible.

Codex should default to clear, explicit sequences over clever but opaque animation systems.

6. Math & Conceptual Ground Rules

Codex must respect the true math behind SDFs and surface nets:

6.1 SDF Basics

For a point p and a circle centered at c with radius r:

float SignedDistanceCircle(Vector2 p, Vector2 c, float r)
{
    return (p - c).magnitude - r;
}


Negative = inside.

Zero (approximately) = on the surface.

Positive = outside.

Distance vectors:

Direction is usually (p - c).normalized (for a circle).

Length is abs(sd).

6.2 Edge Interpolation (Surface Nets-like Zero Crossing)

For two sample points p1, p2 with SDF values d1, d2 and opposite signs:

float t = d1 / (d1 - d2);
Vector2 v = Vector2.Lerp(p1, p2, t);


Codex should:

Use this formula consistently.

Not “fake” interpolation positions unless explicitly asked to simplify visuals.

6.3 Clarity Over Performance

Use simple loops over small grids (e.g., 8×8, 16×16).

No need for jobs, burst, ECS, or complex optimization patterns here, unless explicitly requested.

Prioritize code that is:

Easy to read and explain.

Easy to modify for future teaching demos.

7. What Codex Should Not Do Unless Asked

Codex should avoid:

Changing Unity version or project-wide settings without request.

Introducing unrelated systems (ECS, DOTS, networking, physics, etc.).

Over-abstracting or introducing complex patterns (dependency injection frameworks, service locators, etc.) for this small visualization.

Adding unrelated assets, sample packages, or UI frameworks.

Rewriting large portions of the project by default.

If Codex believes a large refactor is beneficial, it should:

Explain why in a few clear sentences.

Wait for explicit permission from the user.

8. Communication Expectations

When Codex responds to a prompt in this repo, it should:

Summarize the intended change in 2–4 bullet points.

Show the relevant code (new or modified files).

Briefly explain how to test in Unity (e.g., “Open SurfaceNets2D.unity, press Play, you should see…”).

Stop and let the user:

Run the scene.

Confirm or report issues.

Decide the next step.

Codex should not assume that changes “just work”; it should always defer to the user’s real-world Unity test before progressing.

End of instructions.