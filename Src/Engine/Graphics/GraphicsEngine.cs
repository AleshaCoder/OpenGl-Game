using Engine.Components;
using Engine.Core;
using Engine.Graphics.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics
{
    public class GraphicsEngine
    {
        private BasicShader _basicShader;

        public Camera MainCamera { get; set; }

        public void Init()
        {
            MainCamera = new Camera(Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.DegreesToRadians(90), (float)CoreEngine.Width / (float)CoreEngine.Height, 0.3f, 1000f));
            _basicShader = new BasicShader();
        }

        public void Render(GameObject gameObject)
        {
            GraphicsUtil.ClearScreen();
            GL.Viewport(CoreEngine.Window.ClientRectangle);
            gameObject.Render(_basicShader);
        }
    }
}
