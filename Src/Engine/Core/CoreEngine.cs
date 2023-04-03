using System;
using OpenTK;
using OpenTK.Graphics;
using Engine.Graphics;
using Engine.Util;
using Engine.Input;

namespace Engine.Core
{
    public class CoreEngine
    {

        public static int Width;
        public static int Height;

        public static GameWindow Window;
        public static GameScreen Game;
        public static GameInput Input;
        public static GraphicsEngine GraphicsEngine;

        private double _FPSTimer;

        public CoreEngine(int width, int height, int samples, GameScreen gameScreen)
        {
            Width = width;
            Height = height;

            Game = gameScreen;
            Input = new GameInput();
            GraphicsEngine = new GraphicsEngine();

            Window = new GameWindow(
                 width, height,
                 new GraphicsMode(32, 0, 0, samples),
                 "Game",
                 0,
                 DisplayDevice.Default, 3, 2,
                 GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug
             )
            {
                VSync = VSyncMode.Off
            };

            SetupWindow();
        }

        public static void DebugLog(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = temp;
        }

        private void SetupWindow()
        {
            Window.Load += Start;
            Window.UpdateFrame += Update;
            Window.RenderFrame += Render;
            Window.Resize += Resize;
            Window.Closing += Closing;

            Window.KeyDown += Input.KeyDown;
            Window.KeyPress += Input.KeyPress;
            Window.KeyUp += Input.KeyUp;
            Window.MouseDown += Input.MouseDown;
            Window.MouseUp += Input.MouseUp;
            Window.MouseMove += Input.MouseMoved;

            Window.Run(60);
        }

        private void Start(object sender, EventArgs e)
        {
            GraphicsUtil.InitGL();
            GraphicsEngine.Init();
            Game.Awake();
        }

        private void Update(object sender, FrameEventArgs e)
        {
            Time.FixedDeltaTime = (float)e.Time;
            Game.FixedUpdate();
        }

        private void Render(object sender, FrameEventArgs e)
        {
            CalculateDeltaTime(e);
            ShowFPS(e);

            Game.Input();
            Game.Update();
            Input.Update();

            Game.Render(GraphicsEngine);

            Window.SwapBuffers();
        }

        private void CalculateDeltaTime(FrameEventArgs e) => Time.DeltaTime = (float)e.Time;

        private void ShowFPS(FrameEventArgs e)
        {
            if (_FPSTimer > 1)
            {
                Console.WriteLine("FPS: " + (1.0 / e.Time));
                _FPSTimer = 0;
            }
            else
            {
                _FPSTimer += e.Time;
            }
        }

        private void Resize(object sender, EventArgs e)
        {
            Width = Window.Width;
            Height = Window.Height;

            Game.Resize();
        }

        private void Closing(object sender, System.ComponentModel.CancelEventArgs e) 
            => Game.Dispose();

        public static void Exit()
            => Window.Exit();
    }
}
