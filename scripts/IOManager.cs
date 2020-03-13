using Godot;

public class IOManager {
    public static Image LoadImage (string path) {
        Image image = new Image ();
        image.Load (path);
        return image;
    }

    public static void SaveImage (string path, string name, Image image) {
        image.SavePng (path + name);
    }
}