namespace Engine.Graphics.Shaders
{
    public class BasicShader : Shader
    {
        private const string MVP = "MVP";
        private const string Diffuse = "diffuse";

        public BasicShader()
        {
            AddVertexShader(System.IO.File.ReadAllText("Content/Shaders/basic.vs"));
            AddFragmentShader(System.IO.File.ReadAllText("Content/Shaders/basic.fs"));

            CompileShader();

            AddUniform(MVP);
            AddUniform(Diffuse);
        }
    }
}
