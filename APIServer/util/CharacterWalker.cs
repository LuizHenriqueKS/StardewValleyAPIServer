using APIServer.core;
using APIServer.enums;
using APIServer.events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.util
{
    public class CharacterWalker
    {
        private readonly Character character;
        private List<Microsoft.Xna.Framework.Point> path;
        private bool activated;
        private Request request;

        public CharacterWalker(Character character)
        {
            this.character = character;
        }

        public void Walk(Request request, List<Point> path)
        {
            if (this.request!=null)
            {
                FireFinishedEvent();
            }
            this.request = request;
            this.path = path;
            this.activated = true;
        }
        public void Walk(Request request, string path)
        { 
            List<Point> pathList = ParseList(path);
            Walk(request, pathList);
        }

        public void Stop(Request request)
        {
            this.request.Reply(ResponseType.Walking, new WalkingEvent(false, true, this.character));
            this.activated = false;
            SetMovingTo(Direction.UP, false);
            SetMovingTo(Direction.DOWN, false);
            SetMovingTo(Direction.LEFT, false);
            SetMovingTo(Direction.RIGHT, false);
        }

        public void Update()
        { 
            if (this.activated)
            {
                SetMovingTo(Direction.UP, false);
                SetMovingTo(Direction.DOWN, false);
                SetMovingTo(Direction.LEFT, false);
                SetMovingTo(Direction.RIGHT, false);
                Point? nextTileLocation = this.NextTileLocation();
                if (nextTileLocation==null)
                {
                    this.activated = false;
                    FireFinishedEvent();
                    return;
                }
                if (getCharTileLocation().X < nextTileLocation.Value.X)
                {
                    SetMovingTo(Direction.RIGHT, true);
                }
                else if (getCharTileLocation().X > nextTileLocation.Value.X)
                {
                    SetMovingTo(Direction.LEFT, true);
                }
                else if (getCharTileLocation().Y < nextTileLocation.Value.Y)
                {
                    SetMovingTo(Direction.DOWN, true);
                }
                else if (getCharTileLocation().Y > nextTileLocation.Value.Y)
                {
                    SetMovingTo(Direction.UP, true);
                }
            }
        }

        private List<Point> ParseList(string path)
        {
            List<Point> list = new List<Point>();
            string[] array = path.Split(',');
            foreach (string pos in array){
                string[] xy = pos.Split(':');
                list.Add(new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1])));
            }
            return list;
        }

        private void FireFinishedEvent()
        {
            this.request.Reply(ResponseType.Walking, new WalkingEvent(true, false, this.character));
            SetMovingTo(Direction.UP, false);
            SetMovingTo(Direction.DOWN, false);
            SetMovingTo(Direction.LEFT, false);
            SetMovingTo(Direction.RIGHT, false);
        }

        private Point? NextTileLocation()
        {
            Point charTileLocation = getCharTileLocation();
            while (this.path.Count > 0)
            {
                Point next = this.path[0];
                if (next.X == charTileLocation.X && next.Y == charTileLocation.Y)
                {
                    this.request.Reply(ResponseType.Walking, new WalkingEvent(false, false, this.character));
                    this.path.RemoveAt(0);
                } else
                {
                    return this.path[0];
                }
            }
            return null;
        }

        private Point getCharTileLocation()
        {
            Vector2 charTileLocation = this.character.getTileLocation();
            return new Point((int)charTileLocation.X, (int)charTileLocation.Y);
        }

        private void SetMovingTo(Direction direction, bool moving)
        {
            Keys? key = null;
            switch (direction)
            {
                case Direction.UP:
                    character.SetMovingUp(moving);
                    key = Keys.W;
                    break;
                case Direction.DOWN:
                    character.SetMovingDown(moving);
                    key = Keys.S;
                    break;
                case Direction.RIGHT:
                    character.SetMovingRight(moving);
                    key = Keys.D;
                    break;
                case Direction.LEFT:
                    character.SetMovingLeft(moving);
                    key = Keys.A;
                    break;
            }
            if (character is Farmer&&key!=null){
                if (moving)
                {
                    GameJS.Input.PressedKeySet.Add(key.Value);
                } else
                {
                    GameJS.Input.PressedKeySet.Remove(key.Value);
                }
            }
        }

    }
}
