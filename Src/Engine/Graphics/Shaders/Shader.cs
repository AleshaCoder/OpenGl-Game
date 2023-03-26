using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shaders
{
    public class Shader
    {
        private Dictionary<string, int> _uniforms = new Dictionary<string, int>();

        private int _program;

        public Shader()
        {
            _program = GL.CreateProgram();

            if (_program == 0)
            {
                Console.WriteLine("Error while creating shader program; Exiting");
                Environment.Exit(1);
            }
        }

        public void Bind() 
            => GL.UseProgram(_program);

        public void AddUniform(string name)
        {
            int uniformLocation = GL.GetUniformLocation(_program, name);

            if (uniformLocation == -1)
            {
                Console.WriteLine("Error: Could not find uniform: " + name);
                Environment.Exit(1);
            }

            _uniforms.Add(name, uniformLocation);
        }

        public void Uniformi(string name, int value) 
            => GL.Uniform1(_uniforms[name], 1, ref value);

        public void Uniformf(string name, float value) 
            => GL.Uniform1(_uniforms[name], 1, ref value);

        public void Uniform(string name, Vector2 value) 
            => GL.Uniform2(_uniforms[name], ref value);

        public void Uniform(string name, Vector3 value) 
            => GL.Uniform3(_uniforms[name], ref value);

        public void Uniform(string name, Matrix4 value) 
            => GL.UniformMatrix4(_uniforms[name], false, ref value);

        public void CompileShader()
        {
            GL.LinkProgram(_program);

            GL.ValidateProgram(_program);
        }

        public void AddProgram(string text, ShaderType type)
        {
            int shader = GL.CreateShader(type);

            if (shader == 0)
            {
                Console.WriteLine("Error while creating shader; Exiting");
                Environment.Exit(1);
            }

            GL.ShaderSource(shader, text);
            GL.CompileShader(shader);

            GL.GetShaderInfoLog(shader, out string info);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);

            if (status != 1)
            {
                Console.WriteLine("Failed to Compile Fragment Shader Source." +
                        Environment.NewLine + info + Environment.NewLine + "Status Code: " + status.ToString());

                GL.DeleteShader(shader);
                GL.DeleteProgram(_program);
                _program = 0;
            }

            GL.AttachShader(_program, shader);
        }

        public void AddVertexShader(string text) 
            => AddProgram(text, ShaderType.VertexShader);

        public void AddFragmentShader(string text) 
            => AddProgram(text, ShaderType.FragmentShader);

        public void AddGeometryShader(string text) 
            => AddProgram(text, ShaderType.GeometryShader);
    }
}
