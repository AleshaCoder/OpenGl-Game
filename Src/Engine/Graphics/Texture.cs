using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics
{
    public class Texture
    {
        private static TextureUnit ActiveSampler = TextureUnit.Texture0;
        private static int ActiveID = 0;

        public int ID { get; private set; }

        public Texture()
        {
            ID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        public void InitGL()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public void Bind(TextureUnit sampler)
        {
            if (ActiveSampler != sampler)
            {
                GL.ActiveTexture(sampler);
                ActiveSampler = sampler;
            }

            if (ActiveID != ID)
            {
                GL.BindTexture(TextureTarget.Texture2D, ID);
                ActiveID = ID;
            }
        }
    }
}
