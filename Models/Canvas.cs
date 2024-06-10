using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ParticleSystem.Models
{
    public class WaterWindow : GameWindow
    {
        public Cube cube;
        public WaterWindow() 
            : base(GameWindowSettings.Default, new NativeWindowSettings { ClientSize = new Vector2i(800, 600), Title = "Water Simulation" })
        {
            cube = new Cube();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.0f, 0.4f, 1.0f); // Set a blue-ish background color
            GL.Enable(EnableCap.DepthTest); // Enable depth testing
            GL.Enable(EnableCap.CullFace); // Enable face culling

            // Define light parameters
            Vector4 lightPosition = new Vector4(0.0f, 0.0f, 2.0f, 1.0f);
            Vector4 lightAmbient = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            Vector4 lightDiffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

            // Enable lighting
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            // Set light parameters
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);

            SetupCamera(ClientSize.X, ClientSize.Y);
        }

        public void SetupCamera(int width, int height)
        {
            GL.Viewport(0, 0, width, height);

            // Set the projection matrix
            float fov = MathHelper.PiOver4; // Field of view (in radians)
            float aspect = width / (float)height; // Aspect ratio
            float near = 0.1f; // Near clipping plane
            float far = 100.0f; // Far clipping plane
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            // Set the view matrix
            Vector3 eye = new Vector3(0.0f, 0.0f, 3.0f); // Camera position
            Vector3 target = new Vector3(0.0f, 0.0f, 0.0f); // Where the camera is looking
            Vector3 up = Vector3.UnitY; // "Up" direction in 3D space (usually Y)
            Matrix4 view = Matrix4.LookAt(eye, target, up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Clear the color and depth buffers

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-0.5f, -0.5f, 0.0f);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.5f, -0.5f, 0.0f);
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0.0f, 0.5f, 0.0f);
            GL.End();

            // cube.Render();

            SwapBuffers(); // Swap the front and back buffers to display the rendered content
        }
    }
}
