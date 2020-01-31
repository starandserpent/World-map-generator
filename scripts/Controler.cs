using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;
using Godot;

public class Controler : Node
{

    [Export]private string BIOME_DISTRIBUTION_PATH =  "./assets/Biome_distribution.png";
    [Export]private string EARTH_IMAGE_PATH =  "./assets/earth.png";
    [Export]private string IMAGE_SAVE_NAME =  "map.png";
    [Export] private int SEED = 1234;
    [Export] private int TERRAIN_MULTIPLAIER = 2;
    [Export] private int AVERAGE_TERRAIN_HIGHT = 50;
    [Export] private int MAX_ELEVATION = 100;
    [Export] private float FREQUENCY = 0.1F;
    [Export] private bool USE_DEFAULT_CONFIG = true;
    [Export] private int LATITUDE = 500;
    [Export] private int LONGITUDE = 500;
    [Export] private bool GENERATE_NOISE = false;
    [Export] private bool USE_EARTH_IMAGE = false;

    private Weltschmerz weltschmerz;
    private TextureRect canvas;
    private Image map;
    private Image biomMap;
    private Config config;

    private List<int> precipitationValues;
    private List<int> temperatureValues;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        precipitationValues = new List<int>();
        temperatureValues = new List<int>();
      
        weltschmerz = new Weltschmerz();

        canvas = new TextureRect();

        biomMap = IOManager.LoadImage(BIOME_DISTRIBUTION_PATH);
        biomMap.Lock();

        if(GENERATE_NOISE){
            GenerateNoiseImage();
        }else{
            GenerateBiomImage();
        }
    }

    private void GenerateBiomImage(){
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        SetConfiguration();

        weltschmerz.Configure(config);

        if(USE_EARTH_IMAGE){
            Image map = IOManager.LoadImage(EARTH_IMAGE_PATH);
            config.latitude = map.GetHeight();
            config.longitude = map.GetWidth();

            ImageNoiseGenerator generator = new ImageNoiseGenerator(map);
            weltschmerz.NoiseGenerator = generator;
            weltschmerz.Configure(config);
        }

        map = new Image();
        map.Create(config.longitude, config.latitude, false, biomMap.GetFormat());
        double maxTemperature = Math.Abs(config.minTemperature) + config.maxTemperature;

        map.Lock();
        for(int x = 0; x < config.longitude; x++){
            for(int y = 0; y < config.latitude; y ++){

        double elevation = weltschmerz.GetElevation(x, y);
        double temperature = weltschmerz.GetTemperature(y, elevation);

        System.Numerics.Vector2 airFlow = weltschmerz.GetAirFlow(x, y);
        double precipitation = weltschmerz.GetPrecipitation(x, y, elevation, temperature, airFlow);
        precipitationValues.Add((int)precipitation);

        precipitation = (biomMap.GetWidth() * precipitation)/biomMap.GetHeight();

        temperature = (biomMap.GetHeight()*((temperature + Math.Abs(config.minTemperature)) 
        * (biomMap.GetWidth()/(maxTemperature + Math.Abs(config.minTemperature)))))/biomMap.GetWidth();
        precipitation = Math.Min(Math.Max(precipitation, 0), biomMap.GetHeight() - 1);
        temperature =  Math.Min(Math.Max(temperature, 0), biomMap.GetWidth() - 1);

        int posY = (int) Math.Min(precipitation, temperature);
        int posX = (int)temperature;

        if(!WeltschmerzUtils.IsLand(elevation)){
            map.SetPixel(x, y, new Color( 0, 0, 1, 1 ));
        }else{
            map.SetPixel(x, y, biomMap.GetPixel(posX, posY));
        }
            }
        }

        map.Unlock();
        stopwatch.Stop();

        GD.Print("Finished in:" + stopwatch.Elapsed.TotalSeconds);
        
        GD.Print("Precipitation");
        GD.Print("Min " + precipitationValues.Min());
        GD.Print("Max "+ precipitationValues.Max());
        GD.Print("Average "+ precipitationValues.Average());

        ImageTexture texture = new ImageTexture();
        texture.CreateFromImage(map);
        canvas.SetTexture(texture);
        IOManager.SaveImage("./", IMAGE_SAVE_NAME, map);
        AddChild(canvas);
    }

    private void GenerateNoiseImage(){
        SetConfiguration();

        weltschmerz.Configure(config);

        map = new Image();
        map.Create(config.longitude, config.latitude, false, biomMap.GetFormat());

        map.Lock();
        for(int x = 0; x < config.longitude; x++){
            for(int y = 0; y < config.latitude; y ++){
                float elevation = (float)weltschmerz.GetElevation(x, y)/(float)(Math.Abs(config.minElevation) + config.maxElevation);
                map.SetPixel(x, y, new Color(elevation, elevation, elevation, 1));
            }
        }

        map.Unlock();

        ImageTexture texture = new ImageTexture();
        texture.CreateFromImage(map);
        canvas.SetTexture(texture);
        //IOManager.SaveImage("./", IMAGE_SAVE_NAME, map);
        AddChild(canvas);
    }

    private void SetConfiguration(){
          if(USE_DEFAULT_CONFIG){
            weltschmerz = new Weltschmerz();
            config = weltschmerz.GetConfig();
        }else{
            config = new Config();
            config.seed = SEED;
            config.avgTerrain = AVERAGE_TERRAIN_HIGHT;
            config.frequency = FREQUENCY;
            config.latitude = LATITUDE;
            config.longitude = LONGITUDE;
            config.maxElevation = MAX_ELEVATION;
            config.terrainMP = TERRAIN_MULTIPLAIER;
            weltschmerz = new Weltschmerz(config);
        }
    }
}
