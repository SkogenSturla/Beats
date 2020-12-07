using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Beats
{
    internal class SignalMonitor
    {
        private Main main;
        private Texture2D texture;
        private SpriteFont font;
        const string TESTDATAFILE = "SpilleData.bin";

        long recs;
        string err;

        clsRandomAccess myData = new clsRandomAccess(TESTDATAFILE);
       

        List<Signal> signals;

        double BPM = 0;

        bool showBeat = false;
        private int counter;
        
        public SignalMonitor(Main main)
        {
            this.main = main;
            texture = main.Content.Load<Texture2D>("Block");
            font = main.Content.Load<SpriteFont>("Font");

            signals = new List<Signal>();

            counter = 0;
        }

        private void CalculateBPM()
        {
            double totalTime = 0;
           
            int index = signals.Count-1;

            if (counter > 1 && counter < 5 )
            { 
            if (counter == 2)
                totalTime = signals[index ].timeStamp- signals[index-1].timeStamp;
                BPM = (int)(60000  / totalTime);

            if (counter == 3)
                totalTime = signals[index].timeStamp - signals[index - 2].timeStamp;
                BPM = (int)(120000 / totalTime);

            if (counter == 4)
                totalTime = signals[index].timeStamp - signals[index - 3].timeStamp;
                BPM = (int)(180000 / totalTime);
            }

            if (counter > 4)
            {
                totalTime = signals[index].timeStamp - signals[index - 4].timeStamp;
                BPM = (int)(240000 / totalTime);

                int teller = counter - 4;

                err = teller.ToString() + "," + BPM.ToString() + ",";
                clsErrorLog txtWrite = new clsErrorLog(err);
                txtWrite.PathName = @"C:\TekstMappe\";

                txtWrite.WriteErrorLog();
            }


        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            if (showBeat) { spriteBatch.Draw(texture, new Rectangle(0, 0, 100, 100), Color.Red); showBeat = false; }
            spriteBatch.DrawString(font, counter.ToString(), new Vector2(700, 30), Color.Red, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font, BPM.ToString(), new Vector2(30, 300), Color.Black, 0.0f, Vector2.Zero, 5.0f, SpriteEffects.None, 0.0f);   
        }

        internal void RegisterSignal(Signal signal)
        {
            signals.Add(signal);

            counter++;
            showBeat = true;
            CalculateBPM();

        }
    }

    public class Cell
    {
        public Rectangle position;
        public Color color;

        public Cell (Rectangle position,  Color color)
        {
            this.position = position;
            this.color = color;
        }
    }
}