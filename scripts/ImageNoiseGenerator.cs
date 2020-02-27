using Godot;
public class ImageNoiseGenerator : NoiseGenerator{
    private Image map;
    private int average;

    public ImageNoiseGenerator(Image map, Config config) : base(config){
        this.map = map;
        this.config = config;
        map.Lock();
    }

    public override double GetNoise(int posX, int posY){
        return (map.GetPixel(posX, posY).r8/255f) * config.noise.max_elevation;
    }
}