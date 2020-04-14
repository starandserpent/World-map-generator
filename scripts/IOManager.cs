using Godot;

public class IOManager {
    public static Image LoadImage (string path) {
        Texture texture = (Texture) GD.Load (path);
        return texture.GetData();
    }

    public static void SaveImage (string path, string name, Image image) {
        image.SavePng (path + name);
    }
}