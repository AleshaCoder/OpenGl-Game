using Engine.Graphics;

namespace Engine.Core
{
    public class GameScreen
    {
        private GameObject _root;

        public GameObject Root
            => _root = _root ?? new GameObject();

        public virtual void Awake() { }

        public virtual void Input()
            => Root.Input();

        public virtual void FixedUpdate()
            => Root.FixedUpdate();

        public virtual void Update()
            => Root.Update();

        public virtual void Render(GraphicsEngine graphicsEngine)
            => graphicsEngine.Render(Root);

        public virtual void Resize()
            => Root.Resize();

        public virtual void Dispose()
            => Root.Dispose();
    }
}
