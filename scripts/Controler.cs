using Godot;

public class Controler : Node {
    private Config config;
    
    //General
    private SpinBox seed;
	private SpinBox latitude;
	private SpinBox longitude;

        //Precipititation
    private Slider circulationIntensitySlider;
	private SpinBox circulationIntensityBox;

	private Slider precipitationIntesitySlider;
	private SpinBox precipitationIntesityBox;

	private Slider maxPrecipitationSlider;
	private SpinBox maxPrecipitationBox;

    //Circulation
    private Slider windIntesitySlider;
	private SpinBox windIntesityBox;

	private Slider windRangeSlider;
	private SpinBox windRangeBox;

	private SpinBox pressureAtSeaLevelBox;

    //Humidity
    private Slider transpirationSlider;
	private SpinBox transpirationBox;

	private Slider evaporationSlider;
	private SpinBox evaporationBox;

    //Temperature
    private Slider maxTemperatureSlider;
	private SpinBox maxTemperatureBox;

	private Slider minTemperatureSlider;
	private SpinBox minTemperatureBox;

    //Elevation
    private SpinBox minElevation;
	private SpinBox maxElevation;
	private SpinBox waterLevel;
	private SpinBox frequencyBox;
	private Slider frequencySlider;
	private SpinBox octavesBox;
	private Slider octavesSlider;

	public override void _Ready () {

        //General
        seed = (SpinBox) FindNode ("Seed").GetChild (1);
		longitude = (SpinBox) FindNode ("Longitude").GetChild (1);
		latitude = (SpinBox) FindNode ("Latitude").GetChild (1);

        //Precipitation
        circulationIntensitySlider = (Slider) FindNode ("Circulation intensity").GetChild (1);
		circulationIntensityBox = (SpinBox) FindNode ("Circulation intensity").GetChild (2);

		precipitationIntesitySlider = (Slider) FindNode ("Precipitation Intensity").GetChild (1);
		precipitationIntesityBox = (SpinBox) FindNode ("Precipitation Intensity").GetChild (2);

		maxPrecipitationSlider = (Slider) FindNode ("Max Precipitation").GetChild (1);
		maxPrecipitationBox = (SpinBox) FindNode ("Max Precipitation").GetChild (2);

        //Circulation
        windIntesitySlider = (Slider) FindNode ("Wind Intesity").GetChild (1);
		windIntesityBox = (SpinBox) FindNode ("Wind Intesity").GetChild (2);

		windRangeSlider = (Slider) FindNode ("Wind Range").GetChild (1);
		windRangeBox = (SpinBox) FindNode ("Wind Range").GetChild (2);

		pressureAtSeaLevelBox = (SpinBox) FindNode ("Pressure at sea level").GetChild (1);

        //Humidity
        
		transpirationSlider = (Slider) FindNode ("Transpiration").GetChild (1);
		transpirationBox = (SpinBox) FindNode ("Transpiration").GetChild (2);

		evaporationSlider = (Slider) FindNode ("Evaporation").GetChild (1);
		evaporationBox = (SpinBox) FindNode ("Evaporation").GetChild (2);

        //Temperature
        
		maxTemperatureSlider = (Slider) FindNode ("Max Temperature").GetChild (1);
		maxTemperatureBox = (SpinBox) FindNode ("Max Temperature").GetChild (2);

		minTemperatureSlider = (Slider) FindNode ("Min Temperature").GetChild (1);
		minTemperatureBox = (SpinBox) FindNode ("Min Temperature").GetChild (2);

        //Elevation
        
        minElevation = (SpinBox) FindNode ("Min elevation").GetChild (1);
		maxElevation = (SpinBox) FindNode ("Max elevation").GetChild (1);
		waterLevel = (SpinBox) FindNode ("Water Level").GetChild (1);
		frequencySlider = (Slider) FindNode ("Frequency").GetChild (1);
		frequencyBox = (SpinBox) FindNode ("Frequency").GetChild (2);
		octavesSlider = (Slider) FindNode ("Octaves").GetChild (1);
		octavesBox = (SpinBox) FindNode ("Octaves").GetChild (2);
    }

    public void Init(Config config){
        
        this.config = config;

        //General
        
		seed.Value  = config.map.seed;
		latitude.Value = config.map.latitude;
		longitude.Value = config.map.longitude;

		seed.Connect ("value_changed", this, "BoxTriggered");
		longitude.Connect ("value_changed", this, "BoxTriggered");
		latitude.Connect ("value_changed", this, "BoxTriggered");

             //Precipitation
        circulationIntensitySlider.Value = config.precipitation.circulation_intensity;
		circulationIntensityBox.Value = config.precipitation.circulation_intensity;

		precipitationIntesitySlider.Value = config.precipitation.precipitation_intensity;
		precipitationIntesityBox.Value = config.precipitation.precipitation_intensity;

		maxPrecipitationSlider.Value = config.precipitation.max_precipitation;
		maxPrecipitationBox.Value = config.precipitation.max_precipitation;

		circulationIntensityBox.Connect ("value_changed", this, "BoxTriggered");
		precipitationIntesityBox.Connect ("value_changed", this, "BoxTriggered");
		maxPrecipitationBox.Connect ("value_changed", this, "BoxTriggered");

		circulationIntensitySlider.Connect ("value_changed", this, "SliderTriggered");
		precipitationIntesitySlider.Connect ("value_changed", this, "SliderTriggered");
		maxPrecipitationSlider.Connect ("value_changed", this, "SliderTriggered");

        //Circulation

	    windIntesitySlider.Value = config.circulation.wind_intensity;
		windIntesityBox.Value = config.circulation.wind_intensity;

		windRangeSlider.Value = config.circulation.wind_range;
		windRangeBox.Value = config.circulation.wind_range;

		pressureAtSeaLevelBox.Value = config.circulation.pressure_at_sea_level;

		windIntesityBox.Connect ("value_changed", this, "BoxTriggered");
		windRangeBox.Connect ("value_changed", this, "BoxTriggered");
		pressureAtSeaLevelBox.Connect ("value_changed", this, "BoxTriggered");

		windIntesitySlider.Connect ("value_changed", this, "SliderTriggered");
		windRangeSlider.Connect ("value_changed", this, "SliderTriggered");

        //Transpiration
        transpirationSlider.Value = config.humidity.transpiration;
		transpirationBox.Value = config.humidity.transpiration;

		evaporationSlider.Value = config.humidity.evaporation;
		evaporationBox.Value = config.humidity.evaporation;

		transpirationBox.Connect ("value_changed", this, "BoxTriggered");
		evaporationBox.Connect ("value_changed", this, "BoxTriggered");

		transpirationSlider.Connect ("value_changed", this, "SliderTriggered");
		evaporationSlider.Connect ("value_changed", this, "SliderTriggered");

        //Temperature
        minTemperatureSlider.Value =config.temperature.min_temperature;
		minTemperatureBox.Value = config.temperature.min_temperature;

		maxTemperatureSlider.Value = config.temperature.max_temperature;
		maxTemperatureBox.Value = config.temperature.max_temperature;

		maxTemperatureBox.Connect ("value_changed", this, "BoxTriggered");
		minTemperatureBox.Connect ("value_changed", this, "BoxTriggered");

		maxTemperatureSlider.Connect ("value_changed", this, "SliderTriggered");
		minTemperatureSlider.Connect ("value_changed", this, "SliderTriggered");

        //Elevation
        minElevation.Value = config.elevation.min_elevation;
		maxElevation.Value = config.elevation.max_elevation;
		waterLevel.Value = config.elevation.water_level;
		frequencyBox.Value = config.elevation.frequency;
		frequencySlider.Value = config.elevation.frequency;
		octavesBox.Value = config.elevation.octaves;
		octavesSlider.Value = config.elevation.octaves;

        minElevation.Connect ("value_changed", this, "BoxTriggered");
		maxElevation.Connect ("value_changed", this, "BoxTriggered");
		waterLevel.Connect ("value_changed", this, "BoxTriggered");
		frequencyBox.Connect ("value_changed", this, "BoxTriggered");
		frequencySlider.Connect ("value_changed", this, "SliderTriggered");
		octavesBox.Connect ("value_changed", this, "BoxTriggered");
		octavesSlider.Connect ("value_changed", this, "SliderTriggered");
    }   

	private void BoxTriggered (int value) {

        //General
        config.map.seed = (int) seed.Value;
		config.map.latitude = (int) latitude.Value;
		config.map.longitude = (int) longitude.Value;

   
        config.precipitation.circulation_intensity = circulationIntensityBox.Value;
		config.precipitation.precipitation_intensity = precipitationIntesityBox.Value;
		config.precipitation.max_precipitation = (int) maxPrecipitationBox.Value ;

		circulationIntensitySlider.Value = circulationIntensityBox.Value;
		precipitationIntesitySlider.Value = precipitationIntesityBox.Value;
		maxPrecipitationSlider.Value = maxPrecipitationBox.Value;

        //Circulation
		config.circulation.wind_intensity = windIntesityBox.Value;
		config.circulation.wind_range = (int) windRangeBox.Value;
		config.circulation.pressure_at_sea_level = (int) pressureAtSeaLevelBox.Value;

		windIntesitySlider.Value = windIntesityBox.Value;
		windRangeSlider.Value = windRangeBox.Value;

        //Humidity
        config.humidity.transpiration = transpirationBox.Value;
		config.humidity.evaporation = evaporationBox.Value;

		transpirationSlider.Value = transpirationBox.Value;
		evaporationSlider.Value = evaporationBox.Value;
        
        //Temperature
        config.temperature.max_temperature = (int) maxTemperatureBox.Value;
		config.temperature.min_temperature = (int) minTemperatureBox.Value;

		maxTemperatureSlider.Value = maxTemperatureBox.Value;
		minTemperatureSlider.Value = minTemperatureBox.Value;

        //Elevation
        config.elevation.max_elevation = (int) maxElevation.Value;
		config.elevation.min_elevation = (int) minElevation.Value;
		config.elevation.water_level = (int) waterLevel.Value;
		config.elevation.frequency = frequencyBox.Value;

		frequencySlider.Value = frequencyBox.Value;
		octavesSlider.Value = octavesBox.Value;
	}

	private void SliderTriggered (int value) {

        //Precipitation
        config.precipitation.circulation_intensity = circulationIntensitySlider.Value;
		config.precipitation.precipitation_intensity = precipitationIntesitySlider.Value;
		config.precipitation.max_precipitation = (int) maxPrecipitationSlider.Value;

		circulationIntensityBox.Value = circulationIntensitySlider.Value;
		precipitationIntesityBox.Value = precipitationIntesitySlider.Value;
		maxPrecipitationBox.Value = maxPrecipitationSlider.Value;

        //Circulation
		config.circulation.wind_intensity = windIntesitySlider.Value;
		config.circulation.wind_range = (int) windRangeSlider.Value;

		windIntesityBox.Value = windIntesitySlider.Value;
		windRangeBox.Value = windRangeSlider.Value;

        //Humidity
        config.humidity.transpiration = transpirationSlider.Value;
		config.humidity.evaporation = evaporationSlider.Value;

		transpirationBox.Value = transpirationSlider.Value;
		evaporationBox.Value = evaporationSlider.Value;

        //Temperature
        config.temperature.max_temperature = (int) maxTemperatureSlider.Value;
		config.temperature.min_temperature = (int) minTemperatureSlider.Value;

		maxTemperatureBox.Value = maxTemperatureSlider.Value;
		minTemperatureBox.Value = minTemperatureSlider.Value;

        //Elevation
        config.elevation.frequency = frequencySlider.Value;
		config.elevation.octaves = (int) octavesSlider.Value;

		frequencyBox.Value = frequencySlider.Value;
		octavesBox.Value = octavesSlider.Value;
	}
}