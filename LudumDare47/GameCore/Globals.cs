using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum GameStateType
    {
        None,
        Menu,
        GamePlay,
        Settings,
        Exit,
    }
}
