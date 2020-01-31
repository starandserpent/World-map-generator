using Godot;
public class ImageNoiseGenerator : NoiseGenerator{
    private Image map;

    public ImageNoiseGenerator(Image map){
        this.map = map;
        map.Lock();
    }

    public override void Configure(Config config){}

    public override double GetNoise(int posX, int posY){
        return map.GetPixel(posX, posY).b;
    }
}