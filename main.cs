using System.Data;
using Raylib_cs;

namespace RaylibGame;

class Program
{
    public static int maxX = 6;
    public static int maxY = 6;
    public static Button buttonMinusX = new Button("-", 30, 40);
    public static Button buttonPlusX = new Button("+", 100, 40);
    public static Button buttonMinusY = new Button("-", 30, 70);
    public static Button buttonPlusY = new Button("+", 100, 70);
    private static string ascii = ".";
    public static string[,] map = new string[80, 80];
    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Ascii Map Editor");

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Raylib.BeginDrawing();
            Draw();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public static void Update()
    {
        buttonMinusX.Update();
        buttonPlusX.Update();
        buttonMinusY.Update();
        buttonPlusY.Update();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            maxX += (buttonPlusX.isHover ? 1 : 0) - (buttonMinusX.isHover ? 1 : 0);
            maxY += (buttonPlusY.isHover ? 1 : 0) - (buttonMinusY.isHover ? 1 : 0);
        }
        var chr = Raylib.GetCharPressed();
        if (chr > 0)
        {
            ascii = ((char)chr).ToString();
        }

        // File Save
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            var saveString = "";
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    saveString += map[x, y] == "" || map[x, y] == null ? " " : map[x, y];
                }
                saveString += "\n";
            }
            File.WriteAllText("./ascii.txt", saveString);
        }
    }

    public static void Draw()
    {
        Raylib.ClearBackground(Color.White);
        Raylib.SetWindowState(ConfigFlags.ResizableWindow);

        Raylib.DrawText("[LMB]SetAscii | [RMB]RemoveAscii | [Anykey]ChooseAscii | [Enter]Save", 12, 12, 20, Color.Black);
        // UI
        Raylib.DrawText("X", 4, 34, 16, Color.Black);
        buttonMinusX.Draw();
        buttonPlusX.Draw();
        Raylib.DrawText(maxX.ToString(), 60, 36, 10, Color.Black);

        Raylib.DrawText("Y", 4, 64, 16, Color.Black);
        buttonMinusY.Draw();
        buttonPlusY.Draw();
        Raylib.DrawText(maxY.ToString(), 60, 66, 10, Color.Black);

        // Grid
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Raylib.DrawRectangleLines(10 + x * 32, 80 + y * 32, 32, 32, Color.Black);
                if (Raylib.GetMouseX() > 10 + x * 32 && Raylib.GetMouseX() < 42 + x * 32 &&
                Raylib.GetMouseY() > 80 + y * 32 && Raylib.GetMouseY() < 112 + y * 32)
                {
                    Raylib.DrawText(ascii, 10 + x * 32 + 10, 80 + y * 32, 30, Color.Gray);
                    if (Raylib.IsMouseButtonDown(MouseButton.Left))
                    {
                        map[x, y] = ascii;
                    }
                    if (Raylib.IsMouseButtonDown(MouseButton.Right))
                    {
                        map[x, y] = "";
                    }
                }
                Raylib.DrawText(map[x, y], 10 + x * 32 + 10, 80 + y * 32, 30, Color.Black);
            }
        }
    }
}