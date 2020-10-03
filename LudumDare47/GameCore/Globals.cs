using Microsoft.Xna.Framework.Graphics;
using PandaMonogame;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
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
        Exit,
    }

    public static class Globals
    {
        public static DynamicSpriteFont DefaultFont;

        public static void Load(GraphicsDevice graphics)
        {
            DefaultFont = ModManager.Instance.AssetManager.LoadDynamicSpriteFont("LatoBlack");
        }
    }
}
