using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.model
{
    public class TileLocation
    {
        public String Location;
        public int X;
        public int Y;

        public TileLocation(Character character)
        {
            this.Location = character.currentLocation.Name;
            this.X = character.getTileLocationPoint().X;
            this.Y = character.getTileLocationPoint().Y;
        }
    }
}
