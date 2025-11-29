1. Purpose

This project visualizes 2D Surface Nets meshing using a signed distance field (SDF) in Unity 6+.
The visualization animates the full pipeline:

2D sampling grid

SDF values per grid point

Sign visualization (inside / outside)

SDF distance vectors

Edge zero-crossing detection

Linear interpolation to create surface vertices

Mesh/contour generation

Optional camera animation & polish

The primary goal is education and debug clarity, not game-ready performance.

2. Codex / AI Agent Role

Codex acts as a coding assistant that modifies this Unity project incrementally, always following:

2.1 Rules Codex MUST follow

Only modify the project according to this SPEC.

Produce small, scoped changes for each step.

Wait for explicit user confirmation before moving to the next step.

Never restructure the project unless requested.

Never add third-party packages or Editor tooling unless requested.

Avoid unnecessary abstraction or complex patterns.

Use Unity 6+ APIs when relevant (e.g., modern rendering defaults).

2.2 Interaction Model

Every change must be implemented as:

Summary of changes (2–5 bullet points)

Modified or created scripts (show full files)

Scene instructions (how to test in Unity)

Stop and wait for confirmation

3. Project Structure (Unity 6+)
3.1 Folder Layout

Codex must place files only in the following structure:

Assets/
  Scenes/
    SurfaceNets2D.unity
  Scripts/
    Grid/
      GridConfig.cs
      GridRenderer2D.cs
    SDF/
      SdfShape2D.cs
      SdfCircle2D.cs
    Visualization/
      SamplePointVisualizer.cs
      SdfFieldVisualizer.cs
      SurfaceNets2DVisualizer.cs
docs/
  SURFACE_NETS_SPEC.md

3.2 Naming Standards

All classes in namespace:
SurfaceNets2D

Serialized fields use:
[SerializeField] private Type name;

Public fields only when necessary for API clarity.

4. Technical Requirements
4.1 Grid

2D rectangular grid

Configurable via GridConfig:

gridWidth

gridHeight

cellSize

Codex will visualize:

Optional grid lines

Grid points (sample points)

4.2 SDF Implementation

Base class: SdfShape2D

First concrete implementation: SdfCircle2D

SDF rule:
d = length(p - center) - radius

Sign meaning

d < 0 → inside

d == 0 → surface

d > 0 → outside

4.3 SDF Vectors

For selected sample points, Codex must:

Compute SDF value

Compute vector:
direction = normalize(p – center)
magnitude = abs(d)

Visualize using a 2D line (LineRenderer or mesh line)

4.4 Surface Nets Interpolation

Given two grid points (p1, p2) with SDF values (d1, d2):

t = d1 / (d1 - d2)
v = lerp(p1, p2, t)


Codex must use this exact formula.

4.5 Vertex Visualization

A marker moves along the edge from p1 → interpolated point v

Animation via coroutine

Timing controlled by inspector parameters

4.6 Mesh Construction

After vertices are placed:

Create a polyline mesh (2D)

Connect surface-net vertices inside cells

Keep mesh simple (no triangulation required)

5. Scene Requirements
Scene file

SurfaceNets2D.unity

Minimum scene setup

Orthographic camera

Clear 2D background

A GameObject named SurfaceNetsController with:

GridConfig

GridRenderer2D

SdfFieldVisualizer

SurfaceNets2DVisualizer

Testing

Codex must always provide clear testing instructions:

Play mode expectations

What should appear

Which objects are affected

6. Step-by-Step Development Plan

Codex must implement each step only when prompted.

Step 1 – Base Grid Visualization

Create GridConfig

Create GridRenderer2D

Visualize grid lines and/or cell boundaries

No SDF, no shapes yet

Step 2 – Grid Sample Points

Visualize points at cell intersections

Prepare color hooks for later sign visualization
  
Step 3 – Add SDF Shape (Circle)

Implement enum SdfShape with only one shape for now, circle.

Implement circle shape for SdfShape circle

Compute & store SDF values for each point

Step 4 – Visualize Sign

Color sample points according to d < 0 or d > 0

Highlight edges with sign changes

Step 5 – SDF Vectors

Animate SDF arrows on selected sample points

Step 6 – Edge Interpolation

Compute zero-crossing on the demo edge

Animate interpolation marker

Step 7 – Full Vertex Extraction

Compute all surface-net vertices across all relevant edges

Step 8 – Mesh Visualization

Render simple 2D contour mesh from vertices

Add optional animation (fade-in, draw-in)

Step 9 – Optional Polish (User Directed)

Camera motion

UI toggles

Step-by-step playback

7. Unity 6+ Constraints

Codex must assume:

Unity 6+ input & rendering defaults

Scriptable Render Pipeline not required unless explicitly requested

Use new Unity APIs when beneficial (e.g., LineRenderer improvements)

No editor scripting unless asked

8. What Codex MUST NOT DO

Do not skip ahead in the step plan

Do not reorganize folders

Do not introduce new systems (ECS, URP, shader graphs, etc.)

Do not optimize prematurely

Do not generate meta files

Do not overwrite scenes entirely—modify only as needed

9. Prompting Pattern (how Codex is invoked)

When using Codex, prompts must follow this pattern:

Follow /docs/SURFACE_NETS_SPEC.md.
Implement Step X.
Stop after completing Step X and wait for confirmation.


X = step number in Section 6.

Codex will read this SPEC automatically.

10. End of SPEC