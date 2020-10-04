using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public static GraphicsDevice GraphicsDevice;

        public static readonly float SpawnEffectDuration = 2000;
        public static Texture2D SpawnEffectTexture;
        public static Animation SpawnEffectAnimation = new Animation(1, 21, SpawnEffectDuration);

        public static DynamicSpriteFont DefaultFont;
        public static Texture2D PlaceholderTexture;
        public static Effect Ripple;

        public static void Load(GraphicsDevice graphics, ContentManager content)
        {
            DefaultFont = ModManager.Instance.AssetManager.LoadDynamicSpriteFont("LatoBlack");
            PlaceholderTextureInit(graphics);
            LoadShaders(content);
        }

        private static void PlaceholderTextureInit(GraphicsDevice graphics)
        {
            PlaceholderTexture = new RenderTarget2D(graphics, 1, 1);
            graphics.SetRenderTarget((RenderTarget2D)PlaceholderTexture);
            graphics.Clear(Color.White);
            graphics.SetRenderTarget(null);
        }
        private static void LoadShaders(ContentManager content)
        {
            Ripple = content.Load<Effect>("Ripple");
            Ripple.Parameters["center"].SetValue(new Vector2(0.5f, 0.5f));
            Ripple.Parameters["amplitude"].SetValue(0.05f);
            Ripple.Parameters["frequency"].SetValue(100f);
            Ripple.Parameters["size"].SetValue(1f);
        }
    }
}
