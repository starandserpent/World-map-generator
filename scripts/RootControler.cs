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
	private FileDialog loadDialog;
	private FileDialog saveasDialog;
	private AcceptDialog saveDialog;

	private PackedScene generalConfig;
	private PackedScene noiseConfig;
	private PackedScene temperatureConfig;
	private PackedScene circulationConfig;
	private PackedScene precipitationConfig;

	private Node parent;
	private Config config;

	private String currentConfigPath;
	private String currentDirectoryPath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		precipitationValues = new List<int>();
		temperatureValues = new List<int>();

		currentDirectoryPath = ConfigManager.BASE_CONFIG_DIRECTORY_PATH;
		currentConfigPath = ConfigManager.BASE_CONFIG_FILE_PATH;
		config = ConfigManager.GetConfig(currentConfigPath);
	  
		weltschmerz = new Weltschmerz(config);

		biomMap = IOManager.LoadImage(BIOME_DISTRIBUTION_PATH);
		biomMap.Lock();

		Init();

		Generate();
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
		loadDialog = (FileDialog) FindNode("LoadDialog");
		saveasDialog = (FileDialog) FindNode("SaveAsDialog");
		saveDialog = (AcceptDialog) FindNode("SaveDialog");

		useEarth.Pressed = USE_EARTH_IMAGE;
		useEarth.Connect("pressed", this, "Generate");

		UpdatePath();

		maps.AddItem("General", 0);
		maps.AddItem("Noise", 1);
		maps.AddItem("Temperature", 2);
		maps.AddItem("Circulation", 3);
		maps.AddItem("Precipitation", 4);

		maps.Connect("item_selected", this, nameof(SelectMap));
		save.Connect("pressed", this, nameof(SaveDialog));
		saveas.Connect("pressed", this, nameof(SaveAsDialog));
		load.Connect("pressed", this, nameof(LoadDialog));

		loadDialog.Connect("file_selected", this, nameof(LoadConfig));
		loadDialog.Connect("dir_selected", this, nameof(UpdateDirectory));

		saveasDialog.Connect("file_selected", this, nameof(SaveConfig));
		saveasDialog.Connect("dir_selected", this, nameof(UpdateDirectory));

		parent = FindNode("Sliders");
	}

	public void Generate(){
		SelectMap(maps.Selected);
	}

	private void SaveAsDialog(){
		saveasDialog.SetCurrentDir(currentDirectoryPath);
		saveasDialog.Show();
	}

	private void SaveDialog(){
		SaveConfig(currentConfigPath);
		saveDialog.Show();
	}

	private void LoadDialog(){
		loadDialog.SetCurrentDir(currentDirectoryPath);
		loadDialog.Show();
	}

	private void SaveConfig(string path){
		currentConfigPath = path;
		System.IO.File.WriteAllText(@path, Newtonsoft.Json.JsonConvert.SerializeObject(config));
		UpdatePath();
	}

	private void UpdateDirectory(string directory){
		currentDirectoryPath = directory;
	}

	private void LoadConfig(string path){
		currentConfigPath = path;
		config = ConfigManager.GetConfig(path);
		UpdatePath();
		Generate();
	}

	private void SelectMap(int id){
		foreach(Node node in parent.GetChildren()){
			if(node.Name.Equals("Children")){
				parent.RemoveChild(node);
			}
		}

		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();

		if(useEarth.IsPressed()){
			Image map = IOManager.LoadImage(EARTH_IMAGE_PATH);
			config.map.latitude = map.GetHeight();
			config.map.longitude = map.GetWidth();

			ImageNoiseGenerator generator = new ImageNoiseGenerator(map, config);
			weltschmerz.NoiseGenerator = generator;
		}else{
			Noise noise = new Noise(config);
			weltschmerz.NoiseGenerator = noise;
		}

		map = new Image();
		map.Create(config.map.longitude, config.map.latitude, false, biomMap.GetFormat());
		double maxTemperature = Math.Abs(config.temperature.min_temperature) + config.temperature.max_temperature;

		map.Lock();

		switch(id){
			case 0:
				parent.AddChild(generalConfig.Instance());
				GenerateBiomMap();
				break;
			case 1:
				parent.AddChild(noiseConfig.Instance());
				GenerateNoiseMap();
				break;
			case 2:
				parent.AddChild(temperatureConfig.Instance());
				GenerateTemperatureMap();
				break;
			case 3:
				parent.AddChild(circulationConfig.Instance());
				GenerateCirculationMap();
				break;
			case 4:
				parent.AddChild(precipitationConfig.Instance());
				GeneratePrecipitationMap();
				break;    
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
	}

	private void UpdatePath(){
		if(currentConfigPath.Length - 75 < 0){
			pathLabel.SetText(currentConfigPath);
		}else{
			pathLabel.SetText("... " + currentConfigPath.Substring(currentConfigPath.Length - 75));
		}
	}

	private void GenerateBiomMap(){
		for(int x = 0; x < config.map.longitude; x++){
			for(int y = 0; y < config.map.latitude; y ++){
		
		double elevation = weltschmerz.GetElevation(x, y);
		double temperature = weltschmerz.GetTemperature(y, elevation);

		System.Numerics.Vector2 airFlow = weltschmerz.GetAirFlow(x, y);
		double precipitation = weltschmerz.GetPrecipitation(x, y, elevation, temperature, airFlow);
		precipitationValues.Add((int)precipitation);

		precipitation = (biomMap.GetWidth() * precipitation)/biomMap.GetHeight();

		temperature = (biomMap.GetHeight()*((temperature + Math.Abs(config.temperature.min_temperature)) 
		* (biomMap.GetWidth()/(config.temperature.max_temperature + Math.Abs(config.temperature.min_temperature)))))/biomMap.GetWidth();
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
		IOManager.SaveImage("./", IMAGE_SAVE_NAME, map);
	}

	private void GenerateNoiseMap(){
		bool earth = useEarth.IsPressed();
		for(int x = 0; x < config.map.longitude; x++){
			for(int y = 0; y < config.map.latitude; y ++){
				if(earth){
					float elevation = (float)weltschmerz.GetElevation(x, y);
					map.SetPixel(x, y, new Color(elevation, elevation, elevation, 1f));
				}else{
					float elevation = ((float)weltschmerz.GetElevation(x, y) + 1)/2;
					map.SetPixel(x, y, new Color(elevation, elevation, elevation, 1f));
				}
			}
		}
	}

	private void GenerateDensityMap(){
		List<double> elevations = new List<double>();
		bool earth = useEarth.IsPressed();
		for(int x = 0; x < config.map.longitude; x++){
			for(int y = 0; y < config.map.latitude; y ++){
				double density = weltschmerz.CirculationGenerator.CalculateDensity(x, y);
				map.SetPixel(x, y, new Color(0, 0, (float) density/834529, 1f));
			}
		}
	}


	private void GenerateTemperatureMap(){
		float minTemperature = Math.Abs(config.temperature.min_temperature);
		float maxTemperature = minTemperature + config.temperature.max_temperature;
		for(int x = 0; x < config.map.longitude; x++){
			for(int y = 0; y < config.map.latitude; y ++){
				float temperature = ((float)weltschmerz.GetTemperature(x, y) + minTemperature)/maxTemperature;
				map.SetPixel(x, y, new Color(temperature, 0, 0, 1f));
			}
		}
	}

	private void GenerateCirculationMap(){
		for(int x = 0; x < config.map.longitude; x++){
			for(int y = 0; y < config.map.latitude; y ++){
				System.Numerics.Vector2 circulation = weltschmerz.GetAirFlow(x, y);
				map.SetPixel(x, y, new Color(circulation.X/10, circulation.Y/10, 0, 1f));
			}
		}
	}

	private void GeneratePrecipitationMap(){
		for(int x = 0; x < config.map.longitude; x++){
			for(int y = 0; y < config.map.latitude; y ++){
				float precipitation = ((float)weltschmerz.GetPrecipitation(x, y))/config.precipitation.max_precipitation;
				map.SetPixel(x, y, new Color(0, 0, precipitation, 1f));
			}
		}
	}

	public Config GetConfig(){
		return config;
	}
}
