using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics
{
    public class GraphicsUtil
    {
        public static void ClearScreen() 
            => GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        public static void InitGL()
        {
            GL.ClearColor(Color4.MidnightBlue);

            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.FramebufferSrgb);

            GL.Enable(EnableCap.Texture2D);
        }
    }
}
