using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ParticleSystem.Models
{
    public class Cube
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;
        private int _shaderProgram;

        private readonly float[] _vertices = {
            // Positions          // Colors
            -0.5f, -0.5f, -0.5f,  1.0f, 0.0f, 0.0f,  // Bottom-left-back
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 0.0f,  // Bottom-right-back
             0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f,  // Top-right-back
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  // Top-left-back
            -0.5f, -0.5f,  0.5f,  1.0f, 0.0f, 1.0f,  // Bottom-left-front
             0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 1.0f,  // Bottom-right-front
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f, 1.0f,  // Top-right-front
            -0.5f,  0.5f,  0.5f,  0.5f, 0.5f, 0.5f   // Top-left-front
        };

        private readonly uint[] _indices = {
            0, 1, 2, 2, 3, 0,  // Back face
            4, 5, 6, 6, 7, 4,  // Front face
            0, 1, 5, 5, 4, 0,  // Bottom face
            2, 3, 7, 7, 6, 2,  // Top face
            0, 3, 7, 7, 4, 0,  // Left face
            1, 2, 6, 6, 5, 1   // Right face
        };

        public Cube()
        {
        }

        public void Initialize()
        {
            // Vertex Array Object
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // Vertex Buffer Object
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // Element Buffer Object
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            // Vertex Shader
            var vertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPosition;
                layout(location = 1) in vec3 aColor;
                out vec3 vColor;
                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;
                void main()
                {
                    gl_Position = projection * view * model * vec4(aPosition, 1.0);
                    vColor = aColor;
                }";
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            CheckShaderCompilation(vertexShader);

            // Fragment Shader
            var fragmentShaderSource = @"
                #version 330 core
                in vec3 vColor;
                out vec4 fragColor;
                void main()
                {
                    fragColor = vec4(vColor, 0.2);
                }";
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            CheckShaderCompilation(fragmentShader);

            // Shader Program
            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);
            CheckProgramLinking(_shaderProgram);
            GL.UseProgram(_shaderProgram);

            // Cleanup shaders after linking
            GL.DetachShader(_shaderProgram, vertexShader);
            GL.DetachShader(_shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Position attribute
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Color attribute
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        public void Render(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            GL.UseProgram(_shaderProgram);

            int modelLocation = GL.GetUniformLocation(_shaderProgram, "model");
            int viewLocation = GL.GetUniformLocation(_shaderProgram, "view");
            int projectionLocation = GL.GetUniformLocation(_shaderProgram, "projection");

            GL.UniformMatrix4(modelLocation, false, ref model);
            GL.UniformMatrix4(viewLocation, false, ref view);
            GL.UniformMatrix4(projectionLocation, false, ref projection);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }


        public void Cleanup()
        {
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteProgram(_shaderProgram);
            GL.DeleteBuffer(_elementBufferObject);
        }

        private void CheckShaderCompilation(int shader)
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private void CheckProgramLinking(int program)
        {
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error occurred whilst linking Program({program}).\n\n{infoLog}");
            }
        }
    }
}
