using System;
using Godot;

public class Controler : Node
{

    [Export]private string BIOME_DISTRIBUTION_PATH =  "./assets/Biome_distribution.png";
    [Export]private string IMAGE_SAVE_NAME =  "lol";
    [Export] private int SEED = 1234;
    [Export] private int TERRAIN_MULTIPLAIER = 2;
    [Export] private int AVERAGE_TERRAIN_HIGHT = 50;
    [Export] private int MAX_ELEVATION = 100;
    [Export] private float FREQUENCY = 0.1F;
    [Export] private bool USE_DEFAULT_CONFIG = true;
    [Export] private int LATITUDE = 500;
    [Export] private int LONGITUDE = 500;
    [Export] private bool GENERATE_NOISE = true;

    private Weltschmerz weltschmerz;
    private TextureRect canvas;
    private Image map;

    private Image biomMap;

    private Config config;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      
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
        SetConfiguration();

        weltschmerz.Configure(config);

        map = new Image();
        map.Create(config.longitude, config.latitude, false, biomMap.GetFormat());
        double maxTemperature = Math.Abs(config.minTemperature) + config.maxTemperature;

        map.Lock();
        for(int x = 0; x < config.longitude; x++){
            for(int y = 0; y < config.latitude; y ++){

        double elevation = weltschmerz.GetElevation(x, y);
        double temperature = weltschmerz.GetTemperature(y, elevation);
        temperature = (biomMap.GetHeight()*((temperature + maxTemperature) * (biomMap.GetWidth()/maxTemperature)))/biomMap.GetWidth();
        System.Numerics.Vector2 airFlow = weltschmerz.GetAirFlow(x, y);
        double precipitation = weltschmerz.GetPrecipitation(x, y, elevation, temperature, airFlow);
        precipitation = (biomMap.GetWidth() * precipitation)/biomMap.GetHeight();

        precipitation = Math.Min(Math.Max(precipitation, 0), biomMap.GetHeight() - 1);
        temperature =  Math.Min(Math.Max(temperature, 0), biomMap.GetWidth() - 1);

        int posY = (int)Math.Min(precipitation, temperature);
        int posX = (int)Math.Min(temperature, precipitation);

        if(!WeltschmerzUtils.IsLand(elevation)){
            map.SetPixel(x, y, new Color( 0, 0, 1, 1 ));
        }else{
            map.SetPixel(x, y, biomMap.GetPixel(posX, posY));
        }
            }
        }

        map.Unlock();

        ImageTexture texture = new ImageTexture();
        texture.CreateFromImage(map);
        canvas.SetTexture(texture);
        //IOManager.SaveImage("./", IMAGE_SAVE_NAME, map);
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
