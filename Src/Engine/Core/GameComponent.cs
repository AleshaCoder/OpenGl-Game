using Engine.Graphics.Shaders;

namespace Engine.Core
{
    public class GameComponent
    {
        public GameObject Parent { get; set; }

        public Transform Transform => Parent.Transform;

        public virtual void Input() { }
        public virtual void FixedUpdate() { }
        public virtual void Update() { }
        public virtual void Render(Shader shader) { }
        public virtual void Resize() { }
        public virtual void Dispose() { }
        public virtual void AddToEngine() { }
    }
}
