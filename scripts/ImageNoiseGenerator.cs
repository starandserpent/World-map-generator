using System;
using Godot;
public class ImageNoiseGenerator : NoiseGenerator{
    private Image map;
    private int average;
    public ImageNoiseGenerator(Image map, Config config) : base(config){
        this.map = map;
        average = (Math.Abs(config.minElevation) + config.maxElevation)/2;
        map.Lock();
    }

    public override double GetNoise(int posX, int posY){
        return map.GetPixel(posX, posY).b * average;
    }
}