using Engine.Core;
using OpenTK;

namespace Engine.Components
{
    public class Camera : GameComponent
    {
        public Matrix4 Projection { get; set; }

        public Matrix4 VP
        {
            get
            {
                Matrix4 v = Transform.Transformation();
                Matrix4 p = Projection;

                Matrix4.Mult(ref v, ref p, out Matrix4 result);

                return result;
            }
        }

        public Camera(Matrix4 projection)
            => Projection = projection;

        public override void AddToEngine()
        {
            base.AddToEngine();
            CoreEngine.GraphicsEngine.MainCamera = this;
        }
    }
}
