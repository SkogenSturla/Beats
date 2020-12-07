using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Beats
{
    internal class SignalDetector
    {
        private Main main;
        KeyboardState keyboardState, old_keyboardState;
        MouseState mouseState, old_mouseState;
        

        public SignalMonitor monitor;

        public SignalDetector(Main main)
        {
            this.main = main;
            keyboardState = new KeyboardState();
            old_keyboardState = new KeyboardState();
            mouseState = new MouseState();
            old_mouseState = new MouseState();

            monitor = new SignalMonitor(main);
        }

        public void Update(GameTime gameTime)
        {
      
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            
           
            if ( mouseState.LeftButton == ButtonState.Pressed && old_mouseState.LeftButton == ButtonState.Released )
                    { monitor.RegisterSignal(new Signal(gameTime.TotalGameTime.TotalMilliseconds)); }

            if (keyboardState.IsKeyDown(Keys.Space) && old_keyboardState.IsKeyUp(Keys.Space))
                    { monitor.RegisterSignal(new Signal(gameTime.TotalGameTime.TotalMilliseconds)); }


            old_keyboardState = keyboardState;
            old_mouseState = mouseState;
        }


    }

    class Signal
    {
        public double timeStamp;
        public Signal(double timeStamp) { this.timeStamp = timeStamp; }
    }
}