using System.Numerics;
using Raylib_cs;

namespace RaylibGame
{
    public class Button : GameObject
    {
        public string Label { get; set; } = "";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public bool isHover = false;
        public override void Draw()
        {
            Raylib.DrawCircle(X, Y, 8, isHover ? Color.Blue : Color.Black);
            Raylib.DrawText(Label, X - 4, Y - 4, 8, Color.White);
        }

        public override void Update()
        {
            if (Math.Sqrt(Math.Pow(X - Raylib.GetMouseX(), 2) + Math.Pow(Y - Raylib.GetMouseY(), 2)) < 8)
            {
                isHover = true;
            }
            else
            {
                isHover = false;
            }
        }

        public Button(string label, int x, int y)
        {
            Label = label;
            X = x;
            Y = y;
        }
    }
}