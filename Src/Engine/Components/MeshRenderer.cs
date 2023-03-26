using Engine.Core;
using Engine.Graphics;
using Engine.Graphics.Mesh;
using Engine.Graphics.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Components
{
    public class MeshRenderer : GameComponent
    {
        private const string Diffuse = "diffuse";
        private const string MVP = "MVP";

        private Mesh _mesh;
        private Material _material;

        public MeshRenderer(Mesh mesh, Material material)
        {
            _mesh = mesh;
            _material = material;
        }

        public override void Render(Shader shader)
        {
            base.Render(shader);

            shader.Bind();
            _material.Diffuse.Bind(TextureUnit.Texture0);
            shader.Uniformi(Diffuse, 0);
            shader.Uniform(MVP, Matrix4.Mult(Transform.Transformation(), CoreEngine.GraphicsEngine.MainCamera.VP));
            _mesh.Draw();
        }
    }
}
