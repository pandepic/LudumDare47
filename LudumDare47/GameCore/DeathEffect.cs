using Microsoft.Xna.Framework;

namespace GameCore
{
    public class DeathEffect
    {
        public DeathEffect(Vector2 pos, float ttl)
        {
            Pos = pos;
            TTL = ttl;
        }

        public Vector2 Pos {
            get;
            set;
        }
        public float TTL {
            get;
            set;
        }
    }
}
