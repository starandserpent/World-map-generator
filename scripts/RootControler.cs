using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;

public class RootControler : Node {
	[Export] private string BIOME_DISTRIBUTION_PATH = "assets/Biome_distribution.png";
	[Export] private string EARTH_IMAGE_PATH = "assets/earth.png";
	[Export] private string IMAGE_SAVE_NAME = "map.png";
	[Export] private bool USE_EARTH_IMAGE = false;
	[Export] private int THREADS = 4;

	private Weltschmerz weltschmerz;
	private TextureRect canvas;
	private Label pathLabel;
	private Image map;
	private Image biomMap;
	private Button save;
	private Button load;
	private Button saveas;
	private CheckBox useEarth;

	private OptionButton maps;
	private FileDialog loadDialog;
	private FileDialog saveasDialog;
	private AcceptDialog saveDialog;

	//Scenes with configuration
	private Node parent;
	private Config config;

	private String currentConfigPath;
	private String currentDirectoryPath;

	private Thread generationThread;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready () {

		currentDirectoryPath = ConfigManager.BASE_CONFIG_FILE_DIRECTORY_PATH;
		currentConfigPath = ConfigManager.BASE_CONFIG_FILE_PATH;
		config = ConfigManager.GetConfig (currentConfigPath);

		weltschmerz = new Weltschmerz (config);

		biomMap = IOManager.LoadImage (BIOME_DISTRIBUTION_PATH);
		biomMap.Lock ();
	}

	private void Init () {
		canvas = (TextureRect) FindNode ("MapImage");

		useEarth = (CheckBox) FindNode ("UseEarth");
		maps = (OptionButton) FindNode ("Maps");
		save = (Button) FindNode ("Save");
		saveas = (Button) FindNode ("Save as");
		load = (Button) FindNode ("Load");
		pathLabel = (Label) FindNode ("Path");
		loadDialog = (FileDialog) FindNode ("LoadDialog");
		saveasDialog = (FileDialog) FindNode ("SaveAsDialog");
		saveDialog = (AcceptDialog) FindNode ("SaveDialog");

		useEarth.Pressed = USE_EARTH_IMAGE;
		useEarth.Connect ("pressed", this, "Generate");

		UpdatePath ();

		maps.AddItem ("General", 0);
		maps.AddItem ("Elevation", 1);
		maps.AddItem ("Temperature", 2);
		maps.AddItem ("Humidity", 3);
		maps.AddItem ("Circulation", 4);
		maps.AddItem ("Precipitation", 5);

		maps.Connect ("item_selected", this, nameof (SelectMap));
		save.Connect ("pressed", this, nameof (SaveDialog));
		saveas.Connect ("pressed", this, nameof (SaveAsDialog));
		load.Connect ("pressed", this, nameof (LoadDialog));

		loadDialog.Connect ("file_selected", this, nameof (LoadConfig));
		loadDialog.Connect ("dir_selected", this, nameof (UpdateDirectory));

		saveasDialog.Connect ("file_selected", this, nameof (SaveConfig));
		saveasDialog.Connect ("dir_selected", this, nameof (UpdateDirectory));

		parent = FindNode ("Sliders");
	}

	public void Generate () {
		SelectMap (maps.Selected);
	}

	private void SaveAsDialog () {
		saveasDialog.CurrentDir = currentDirectoryPath;
		saveasDialog.Show ();
	}

	private void SaveDialog () {
		SaveConfig (currentConfigPath);
		saveDialog.Show ();
	}

	private void LoadDialog () {
		loadDialog.CurrentDir = currentDirectoryPath;
		loadDialog.Show ();
	}

	private void SaveConfig (string path) {
		currentConfigPath = path;
		System.IO.File.WriteAllText (@path, Newtonsoft.Json.JsonConvert.SerializeObject (config));
		UpdatePath ();
	}

	private void UpdateDirectory (string directory) {
		currentDirectoryPath = directory;
	}

	private void LoadConfig (string path) {
		currentConfigPath = path;
		config = ConfigManager.GetConfig (path);
		UpdatePath ();
		Generate ();
	}

	private void SelectMap (int id) {

		if (generationThread != null) {
			generationThread.WaitToFinish ();
		}

		foreach (Node node in parent.GetChildren ()) {
			if (node.Name.Equals ("Children")) {
				parent.RemoveChild (node);
			}
		}

		if (useEarth.Pressed) {
			Image map = IOManager.LoadImage (EARTH_IMAGE_PATH);
			config.map.latitude = map.GetHeight ();
			config.map.longitude = map.GetWidth ();

			ImageElevationGenerator generator = new ImageElevationGenerator (map, weltschmerz, config);
			weltschmerz.ElevationGenerator = generator;
			weltschmerz.Update ();
		} else {
			Elevation noise = new Elevation (weltschmerz, config);
			weltschmerz.ElevationGenerator = noise;
			weltschmerz.Update ();
		}

		map = new Image ();
		map.Create (config.map.longitude, config.map.latitude, false, biomMap.GetFormat ());
		map.Lock ();

		generationThread = new Thread ();

		generationThread.Start (this, nameof (InitThreads), id);
	}

	private void InitThreads (int id) {
		List<Thread> threads = new List<Thread> ();
		Stopwatch stopwatch = new Stopwatch ();
		stopwatch.Start ();

		for (int t = 1; t < THREADS; t++) {
			Thread thread = new Thread ();
			threads.Add (thread);
			switch (id) {
				case 0:
					thread.Start (this, nameof (GenerateBiomeMap), t);
					break;
				case 1:
					thread.Start (this, nameof (GenerateElevationMap), t);
					break;
				case 2:
					thread.Start (this, nameof (GenerateTemperatureMap), t);
					break;
				case 3:
					thread.Start (this, nameof (GenerateEvapotranspirationMap), t);
					break;
				case 4:
					thread.Start (this, nameof (GenerateHumidityMap), t);
					break;
				case 5:
					thread.Start (this, nameof (GeneratePrecipitationMap), t);
					break;
			}
		}

		foreach (Thread thread in threads) {
			thread.WaitToFinish ();
		}

		map.Unlock ();

		stopwatch.Stop ();
		GD.Print ("Finished in:" + stopwatch.Elapsed.TotalSeconds);

		ImageTexture texture = new ImageTexture ();
		texture.CreateFromImage (map);
		canvas.Texture = texture;
	}

	private void UpdatePath () {
		if (currentConfigPath.Length - 75 < 0) {
			pathLabel.Text = currentConfigPath;
		} else {
			pathLabel.Text = ("... " + currentConfigPath.Substring (currentConfigPath.Length - 75));
		}
	}

	private void GenerateBiomeMap (int t) {
		int width = config.map.longitude / THREADS;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y < config.map.latitude; y++) {

				double elevation = weltschmerz.GetElevation (posX, y);
				double temperature = weltschmerz.TemperatureGenerator.GetTemperature (y, elevation);

				double precipitation = weltschmerz.PrecipitationGenerator.GetPrecipitation (posX, y, elevation, temperature) / 4;

				precipitation = (biomMap.GetWidth () * precipitation) / biomMap.GetHeight ();

				temperature = temperature / config.temperature.min_temperature;
				temperature *= biomMap.GetWidth () / 4;

				temperature = (biomMap.GetWidth () / 4) - temperature;

				temperature = Math.Min (Math.Max (temperature, 0), biomMap.GetWidth () - 1);
				precipitation = Math.Min (Math.Max (precipitation, 0), temperature);

				if (!weltschmerz.ElevationGenerator.IsLand (elevation)) {
					map.SetPixel (posX, y, new Color (0, 0, 1, 1));
				} else {
					map.SetPixel (posX, y, biomMap.GetPixel ((int) temperature, (int) precipitation));
				}
			}
		}
		IOManager.SaveImage ("./", IMAGE_SAVE_NAME, map);
	}

	private void GenerateElevationMap (int t) {
		int width = config.map.longitude / THREADS;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y < config.map.latitude; y++) {
				float elevation = (float) weltschmerz.GetElevation (posX, y) / (config.elevation.max_elevation - config.elevation.min_elevation);
				map.SetPixel (posX, y, new Color (elevation, elevation, elevation, 1f));
			}
		}
	}

	private void GenerateDensityMap (int t) {
		int width = config.map.longitude / THREADS;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y < config.map.latitude; y++) {
				double elevation = weltschmerz.GetElevation (posX, y);
				float pressure = (float) (weltschmerz.CirculationGenerator.GetAirPressure (posX, y, elevation) / 1000000);
				map.SetPixel (posX, y, new Color (0, pressure, 0, 1f));
			}
		}
	}

	private void GenerateTemperatureMap (int t) {
		int width = config.map.longitude / THREADS;
		float minTemperature = Math.Abs (config.temperature.min_temperature);
		float maxTemperature = minTemperature + config.temperature.max_temperature;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y <  config.map.latitude; y++) {
				float temperature = ((float) weltschmerz.GetTemperature (posX, y) + minTemperature) / maxTemperature;
				map.SetPixel (posX, y, new Color (temperature, 0, 0, 1f));
			}
		}
	}

	private void GenerateEvapotranspirationMap (int t) {
		int width = config.map.longitude / THREADS;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y < config.map.latitude; y++) {
				double elevation = weltschmerz.GetElevation (posX, y);
				float moisture = (float) weltschmerz.PrecipitationGenerator.GetEvapotranspiration (y, weltschmerz.ElevationGenerator.IsLand (elevation)) / config.precipitation.max_precipitation;
				map.SetPixel (posX, y, new Color (0, 0, moisture, 1f));
			}
		}
	}

	private void GenerateHumidityMap (int t) {
		int width = config.map.longitude / THREADS;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y < config.map.latitude; y++) {
				double elevation = weltschmerz.GetElevation (posX, y);
				float humidity = (float) weltschmerz.PrecipitationGenerator.GetHumidity (posX, y, elevation) / config.precipitation.max_precipitation;
				map.SetPixel (posX, y, new Color (0, 0, humidity, 1f));
			}
		}
	}

	private void GeneratePrecipitationMap (int t) {
		int width = config.map.longitude / THREADS;
		for (int x = 0; x < width; x++) {
			int posX = x + (width * t);
			for (int y = 0; y < config.map.latitude; y++) {
				float precipitation = ((float) weltschmerz.GetPrecipitation (posX, y)) / config.precipitation.max_precipitation;
				map.SetPixel (posX, y, new Color (0, 0, precipitation, 1f));
			}
		}
	}

	public Config GetConfig () {
		return config;
	}
}
