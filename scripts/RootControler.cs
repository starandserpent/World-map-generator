using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;
using Godot;

public class RootControler : Node
{
    [Export]private string BIOME_DISTRIBUTION_PATH =  "./assets/Biome_distribution.png";
    [Export]private string EARTH_IMAGE_PATH =  "./assets/earth.png";
    [Export]private string IMAGE_SAVE_NAME =  "map.png";
    [Export] private bool USE_EARTH_IMAGE = false;

    private Weltschmerz weltschmerz;
    private TextureRect canvas;
    private Label pathLabel;
    private Image map;
    private Image biomMap;
    private List<int> precipitationValues;
    private List<int> temperatureValues;

    private Button save;
    private Button load;
    private Button saveas;
    private CheckBox useEarth;
    private OptionButton maps;
    private PackedScene generalConfig;
    private PackedScene noiseConfig;
    private PackedScene temperatureConfig;
    private PackedScene circulationConfig;
    private PackedScene precipitationConfig;

    private Node parent;
    private Config config;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        precipitationValues = new List<int>();
        temperatureValues = new List<int>();

        config = ConfigManager.GetConfig();
      
        weltschmerz = new Weltschmerz(config);

        biomMap = IOManager.LoadImage(BIOME_DISTRIBUTION_PATH);
        biomMap.Lock();

        Init();

        GenerateBiomImage();
    }

    private void Init(){
        generalConfig = (PackedScene) ResourceLoader.Load("res://scenes/GeneralConfig.tscn"); 
        noiseConfig = (PackedScene) ResourceLoader.Load("res://scenes/NoiseConfig.tscn");
        temperatureConfig = (PackedScene) ResourceLoader.Load("res://scenes/TemperatureConfig.tscn");
        circulationConfig = (PackedScene) ResourceLoader.Load("res://scenes/CirculationConfig.tscn");
        precipitationConfig = (PackedScene) ResourceLoader.Load("res://scenes/PrecipitationConfig.tscn");

        canvas = (TextureRect) FindNode("MapImage");

        useEarth = (CheckBox) FindNode("UseEarth");
        maps = (OptionButton) FindNode("Maps");
        save = (Button) FindNode("Save");
        saveas = (Button) FindNode("Save as");
        load = (Button) FindNode("Load");
        pathLabel = (Label) FindNode("Path");

        useEarth.Pressed = USE_EARTH_IMAGE;
        useEarth.Connect("pressed", this, "Toggle");

        UpdatePath();

        maps.AddItem("General", 0);
        maps.AddItem("Noise", 1);
        maps.AddItem("Temperature", 2);
        maps.AddItem("Circulation", 3);
        maps.AddItem("Precipitation", 4);

        maps.Connect("item_selected", this, nameof(SelectMap));

        parent = FindNode("Sliders");
    }

    private void Toggle(){
        SelectMap(maps.Selected);
    }

    private void SelectMap(int id){
        foreach(Node node in parent.GetChildren()){
            if(node.Name.Equals("Children")){
                parent.RemoveChild(node);
            }
        }

        switch(id){
            case 0:
                parent.AddChild(generalConfig.Instance());
                GenerateBiomImage();
                break;
            case 1:
                parent.AddChild(noiseConfig.Instance());
                GenerateNoiseImage();
                break;
            case 2:
                parent.AddChild(precipitationConfig.Instance());
                break;
            case 3:
                parent.AddChild(circulationConfig.Instance());
                break;
            case 4:
                parent.AddChild(temperatureConfig.Instance());
                break;    
        }
    }

    private void UpdatePath(){
        string path = ConfigManager.BASE_CONFIG_PATH;

        if(path.Length - 75 < 0){
            pathLabel.SetText(path);
        }else{
            pathLabel.SetText("... " + path.Substring(path.Length - 75));
        }
    }

    private void GenerateBiomImage(){
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        if(useEarth.IsPressed()){
            Image map = IOManager.LoadImage(EARTH_IMAGE_PATH);
            config.latitude = map.GetHeight();
            config.longitude = map.GetWidth();

            ImageNoiseGenerator generator = new ImageNoiseGenerator(map, config);
            weltschmerz.NoiseGenerator = generator;
        }else{
            Noise noise = new Noise(config);
            weltschmerz.NoiseGenerator = noise;
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
    }

    private void GenerateNoiseImage(){
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
    }
}
