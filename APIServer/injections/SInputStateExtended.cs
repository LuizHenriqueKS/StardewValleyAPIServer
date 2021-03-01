using Microsoft.Xna.Framework.Input;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.injections
{
    public class SInputStateExtended : StardewModdingAPI.Framework.Input.SInputState
    {

        public bool SimulateLeftButton;
        public bool SimulateRightButton;
        public ISet<Keys> PressedKeySet;

        public SInputStateExtended(InputState input)
        {
            this.PressedKeySet = new HashSet<Keys>();
        }

        public override void UpdateStates()
        {
            base.UpdateStates();
            SimulateMouse();
            SimulateKeys();
        }

        public void AddPressedKey(int key)
        {
            PressedKeySet.Add((Keys)key);
        }

        public void RemovePressedKey(int key)
        {
            PressedKeySet.Remove((Keys)key);
        }

        private void SimulateMouse()
        {
            int x = _currentMouseState.X;
            int y = _currentMouseState.Y;
            int scrollWheel = _currentMouseState.ScrollWheelValue;
            ButtonState leftButton = SimulateLeftButton ? ButtonState.Pressed : _currentMouseState.LeftButton;
            ButtonState middleButton = _currentMouseState.MiddleButton;
            ButtonState rightButton = SimulateRightButton ? ButtonState.Pressed : _currentMouseState.RightButton;
            ButtonState xButton1 = _currentMouseState.XButton1;
            ButtonState xButton2 = _currentMouseState.XButton2;
            this._currentMouseState = new MouseState(x, y, scrollWheel, leftButton, middleButton, rightButton, xButton1, xButton2);
        }

        private void SimulateKeys()
        {
            if (PressedKeySet.Count > 0)
            {
                Keys[] pressedKeys = _currentKeyboardState.GetPressedKeys();
                List<Keys> keyList = new List<Keys>();
                foreach (Keys key in pressedKeys)
                {
                    keyList.Add(key);
                }
                foreach (Keys key in PressedKeySet)
                {
                    keyList.Add(key);
                }
                this._currentKeyboardState = new KeyboardState(keyList.ToArray());
            }
        }
    }

}
