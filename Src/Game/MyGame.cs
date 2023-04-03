using Engine.Components;
using Engine.Core;
using Engine.Util;
using Engine.Graphics;
using Engine.Graphics.Loaders;
using Engine.Input;
using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;
using System;

namespace Game
{
    class MyGame : GameScreen
    {
        private const string Quit = "Quit";
        private const string PlayerModel = "Content/Models/Player.dae";
        private const string PlayerTexture = "Content/Models/Player.png";
        private const string PlatformModel = "Content/Models/Platform.dae";
        private const string PlatformTexture = "Content/Models/Platform.png";
        private const string CoinModel = "Content/Models/Coin.dae";
        private const string CoinTexture = "Content/Models/Coin.png";

        private Mapping _quitMap;

        private GameObject _cameraObject;
        private GameObject _playerObject;
        private GameObject _platformObject;
        private List<GameObject> _coinObjects;

        public override void Awake()
        {
            base.Awake();

            InitExitButton();

            LoadPlatform();
            LoadPlayer();
            LoadCoins();
            LoadCamera();

            AttachToRootAllObjects();
        }

        private void InitExitButton()
        {
            CoreEngine.Input.AddKeyMap(Quit, Key.Escape);
            _quitMap = CoreEngine.Input.Mapping(Quit);
        }

        private void LoadPlatform()
        {
            MeshRenderer platformMeshRenderer = new MeshRenderer(ModelLoader.LoadModel(PlatformModel),
                new Material(TextureLoader.LoadTexture(PlatformTexture)));

            _platformObject = new GameObject();
            _platformObject.AddComponent(platformMeshRenderer);
            _platformObject.Transform.Scale = new Vector3(2500, 10, 100000);
            _platformObject.Transform.Position = new Vector3(0.5f, 35f, 0);
        }

        private void LoadPlayer()
        {
            MeshRenderer playerMeshRenderer = new MeshRenderer(ModelLoader.LoadModel(PlayerModel),
                new Material(TextureLoader.LoadTexture(PlayerTexture)));

            _playerObject = new GameObject();
            _playerObject.AddComponent(playerMeshRenderer);
            _playerObject.Transform.Scale = Vector3.One * 10f;
            _playerObject.Transform.Position = new Vector3(0, 0, 0.2f);
            _playerObject.Transform.Rotation = MathUtil.CreateFromEuler(Vector3.Zero);
            _playerObject.AddComponent(new LockedHorizontalMovement(10, new Vector3(1.5f, 0, 0), new Vector3(-1.5f, 0, 0)));
        }

        private void LoadCoins()
        {
            _coinObjects = new List<GameObject>();

            for (int i = 0; i < 10; i++)
            {
                MeshRenderer coinMeshRenderer = new MeshRenderer(ModelLoader.LoadModel(CoinModel),
                    new Material(TextureLoader.LoadTexture(CoinTexture)));
                GameObject coin = new GameObject();
                coin.AddComponent(coinMeshRenderer);
                coin.Transform.Scale = Vector3.One * 10f;
                coin.Transform.Position = new Vector3(0.3f * i - 1.5f, 0, -10f - 10f * i);
                _coinObjects.Add(coin);
            }
        }

        private void LoadCamera()
        {
            _cameraObject = new GameObject();
            _cameraObject.AddComponent(new Camera(Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.DegreesToRadians(120), (float)CoreEngine.Width / (float)CoreEngine.Height, 0.1f, 10000f)));
            _cameraObject.Transform.Position = new Vector3(2, 25, -15);
        }

        private void AttachToRootAllObjects()
        {
            Root.AddChild(_platformObject);

            foreach (var coin in _coinObjects)
                Root.AddChild(coin);

            Root.AddChild(_playerObject);
            Root.AddChild(_cameraObject);
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

            HidePenetratedCoins();
            DisposeInactiveCoins();
        }

        private void DisposeInactiveCoins()
        {
            int count = _coinObjects.Count;
            for (int i = 0; i < count; i++)
            {
                if (_coinObjects[i].IsActive == false)
                {
                    _coinObjects[i].Dispose();
                    _coinObjects.RemoveAt(i);
                    i = 0;
                    count--;
                }
            }
        }

        private void HidePenetratedCoins()
        {
            foreach (GameObject coin in _coinObjects)
            {
                coin.Transform.Position += coin.Transform.Forward * Time.DeltaTime;
                if ((Math.Abs(coin.Transform.Position.Z - _playerObject.Transform.Position.Z) < 0.3f) &&
                    ((Math.Abs(coin.Transform.Position.X - _playerObject.Transform.Position.X) < 1f)))
                {
                    coin.IsActive = false;
                    CoreEngine.DebugLog(Math.Abs(coin.Transform.Position.Y - _playerObject.Transform.Position.Y).ToString());
                }
            }
        }
    }
}
