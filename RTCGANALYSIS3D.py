from __future__ import annotations
import numpy as np
import pyvista as pv
import open3d as o3d

# --------------------------
# Load mesh
# --------------------------
mesh_path = 'mesh/SugarSugarHEAVY.fbx'
mesh = o3d.io.read_triangle_mesh(mesh_path, enable_post_processing=True)
mesh.compute_vertex_normals()

# Extract vertices as a NumPy array
points = np.asarray(mesh.vertices)
print("First 5 points (XYZ):")
print(points[:5, :])

# --------------------------
# Create a PyVista point cloud
# --------------------------
point_cloud = pv.PolyData(points)
assert np.allclose(points, point_cloud.points)

point_cloud = point_cloud.rotate_y(90, inplace=False)

# --------------------------
# Plot with eye dome lighting
# --------------------------
point_cloud.plot(eye_dome_lighting=True, cpos='xy')

# --------------------------
# Add a scalar attribute (elevation = Y coordinate)
# --------------------------
points = point_cloud.points 
elevation_data = points[:, 1]
point_cloud['elevation'] = elevation_data

point_cloud.plot(render_points_as_spheres=True, cpos='xy')

# --------------------------
# Compute vectors from mesh center
# --------------------------
def compute_vectors(mesh: pv.PolyData):
    origin = mesh.center
    vectors = mesh.points - origin
    norms = np.linalg.norm(vectors, axis=1)
    norms[norms == 0] = 1
    return vectors / norms[:, None]

vectors = compute_vectors(point_cloud)
point_cloud['vectors'] = vectors

# --------------------------
# Create glyphs from vectors
# --------------------------
arrows = point_cloud.glyph(
    orient='vectors',
    scale=False,
    factor=0.15
)

# --------------------------
# Final visualization
# --------------------------
plotter = pv.Plotter()
plotter.add_mesh(point_cloud, color='maroon', point_size=10.0, render_points_as_spheres=True)
plotter.add_mesh(arrows, color='lightblue')
plotter.show_grid()

plotter.camera_position = 'xy' 

plotter.show()

# --------------------------
# Future me (Terminal Shenanigans)
# --------------------------
# conda create -n open3d_env python=3.11 (if no open3d_env)
# conda activate open3d_env 
# pip install open3d pyvista numpy (if dependencies not installed)
# python RTCGANALYSIS3D.py