using Godot;
public class ImageNoiseGenerator : NoiseGenerator{
    private Image map;

    public ImageNoiseGenerator(Image map, Config config) : base(config){
        this.map = map;
        map.Lock();
    }

    public override double GetNoise(int posX, int posY){
        return map.GetPixel(posX, posY).b;
    }
}