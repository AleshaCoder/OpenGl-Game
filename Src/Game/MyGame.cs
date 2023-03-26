using Engine.Components;
using Engine.Core;
using Engine.Graphics;
using Engine.Graphics.Loaders;
using Engine.Input;
using OpenTK;
using OpenTK.Input;

namespace Game
{
    class MyGame : GameScreen
    {
        private const string Quit = "Quit";
        private const string DuckModel = "Content/Models/duck.dae";
        private const string DuckTexture = "Content/Models/duckCM.bmp";
        Mapping _quitMap;

        GameObject _cameraObject;
        GameObject _duckObject;

        public override void Awake()
        {
            base.Awake();

            CoreEngine.Input.AddKeyMap(Quit, Key.Escape);
            _quitMap = CoreEngine.Input.Mapping(Quit);

            MeshRenderer meshRenderer = new MeshRenderer(ModelLoader.LoadModel(DuckModel), new Material(TextureLoader.LoadTexture(DuckTexture)));

            _duckObject = new GameObject();
            _duckObject.AddComponent(meshRenderer);
            _duckObject.Transform.Scale = new Vector3(0.15f);
            _duckObject.Transform.Position = new Vector3(0, 0, 0);

            Root.AddChild(_duckObject);

            _cameraObject = new GameObject();
            _cameraObject.AddComponent(new Camera(Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.DegreesToRadians(90), (float)CoreEngine.Width / (float)CoreEngine.Height, 0.3f, 1000f)));
            _cameraObject.AddComponent(new FreeMove(10));
            _cameraObject.AddComponent(new FreeLook(10));
            Root.AddChild(_cameraObject);
            _cameraObject.Transform.Position = new Vector3(0, 0, -15);

        }

        public override void Input()
        {
            base.Input();

            if (_quitMap.Up)
                CoreEngine.Exit();
        }


        public override void Update()
        {
            base.Update();
            //cameraObject.Transform.LookAt(new Vector3(0, 0, 0));

            //duckObject.Transform.LookAt(cameraObject.Transform.Position);
            //cameraObject.Transform.LookAt(new Vector3((float)(Math.Sin(temp * (Math.PI / 180)) * distance), distance / 3, (float)(Math.Cos(temp * (Math.PI / 180)) * distance)), new Vector3(0, 0, 0));
        }

    }
}
