using APIServer.model;
using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.events
{
    public class WalkingEvent
    {
        public readonly bool Finished;
        public readonly bool Canceled;
        public readonly TileLocation TileLocation;
        public readonly Vector2 Position;

        public WalkingEvent(bool finished, bool Canceled, Character character)
        {
            this.Finished = finished;
            this.Canceled = Canceled;
            this.TileLocation = new TileLocation(character);
            this.Position = character.Position;
        }
    }
}
