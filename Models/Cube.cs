using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace ParticleSystem.Models
{
    public class Cube
    {
        private Vector3 position;

        public Cube()
        {
            position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Render()
        {
            GL.PushMatrix();

            GL.Translate(position);
            GL.Scale(0.5f, 0.5f, 0.5f);

            GL.Begin(PrimitiveType.Quads);

            // Front face
            GL.Color3(1.0f, 0.0f, 0.0f); // Red
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            // Back face
            GL.Color3(0.0f, 1.0f, 0.0f); // Green
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            // Left face
            GL.Color3(0.0f, 0.0f, 1.0f); // Blue
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            // Right face
            GL.Color3(1.0f, 1.0f, 0.0f); // Yellow
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);

            // Top face
            GL.Color3(1.0f, 0.0f, 1.0f); // Magenta
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            // Bottom face
            GL.Color3(0.0f, 1.0f, 1.0f); // Cyan
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);

            GL.End();
            GL.PopMatrix();
        }
    }
}
