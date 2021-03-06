﻿using Microsoft.Xna.Framework;
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
        Caveman,
        Cyborg,
        CaveBorg,
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

        public const int countdown_time = 90;
        public const int spawnroomID = 2;
        public static Vector2 spawn_location = new Vector2(120, 105);

        public static readonly float SpawnEffectDuration = 2000;
        public static Texture2D SpawnEffectTexture;
        public static Animation SpawnEffectAnimation = new Animation(1, 21, SpawnEffectDuration);

        public static DynamicSpriteFont DefaultFont;
        public static Texture2D PlaceholderTexture;
        public static Effect Ripple;
        public static Effect Wind;
        public static Effect RippleDeath;

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
            Ripple.Parameters["amplitude"].SetValue(0.005f);
            Ripple.Parameters["frequency"].SetValue(100f);
            Ripple.Parameters["size"].SetValue(1f);

            Wind = content.Load<Effect>("Wind");
            Wind.Parameters["wind_strength"].SetValue(0.01f);
            Wind.Parameters["magic_number"].SetValue(20f);
            Wind.Parameters["wind_direction"].SetValue(new Vector2(1f, 1f));

            RippleDeath = content.Load<Effect>("RippleDeath");
            RippleDeath.Parameters["center"].SetValue(new Vector2(0.5f, 0.5f));
            RippleDeath.Parameters["amplitude"].SetValue(0.005f);
            RippleDeath.Parameters["frequency"].SetValue(100f);
            RippleDeath.Parameters["size"].SetValue(1f);
        }
    }
}
