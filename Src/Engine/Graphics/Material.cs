namespace Engine.Graphics
{
    public class Material
    {
        public Texture Diffuse { get; set; }

        public Material(Texture diffuse)
            => Diffuse = diffuse;
    }
}
