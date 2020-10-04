using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{

    public enum EnemyType
    {
        Null,
        Caveman

    }

    public enum AnimationState
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }

    public enum Directions
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public enum SoundType
    {
        Music,
        SoundEffect,
        UI
    }

    public enum GameStateType
    {
        None,
        Menu,
        GamePlay,
        Settings,
        GameOver,
        Exit,
    }

    public static class Globals
    {
        public static DynamicSpriteFont DefaultFont;
        public static Texture2D PlaceholderTexture;

        public static void Load(GraphicsDevice graphics)
        {
            DefaultFont = ModManager.Instance.AssetManager.LoadDynamicSpriteFont("LatoBlack");
            PlaceholderTextureInit(graphics);
        }

        private static void PlaceholderTextureInit(GraphicsDevice graphics)
        {
            PlaceholderTexture = new RenderTarget2D(graphics, 1, 1);
            graphics.SetRenderTarget((RenderTarget2D)PlaceholderTexture);
            graphics.Clear(Color.White);
            graphics.SetRenderTarget(null);
        }
    }
}
