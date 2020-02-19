using Godot;
public class PrecipitationControler : Controler {
    private Slider circulationIntensitySlider;
    private SpinBox circulationIntensityBox;

    private Slider orographicEffectSlider;
    private SpinBox orographicEffectBox;

    private Slider precipitationIntesitySlider;
    private SpinBox precipitationIntesityBox;

    private Slider iterationSlider;
    private SpinBox iterationBox;

    private Slider elevationDeltaSlider;
    private SpinBox elevationDeltaBox;

    private Slider maxPrecipitationSlider;
    private SpinBox maxPrecipitationBox;

    private Slider transpirationSlider;
    private SpinBox transpirationBox;

    private Slider evaporationSlider;
    private SpinBox evaporationBox;
    
    public override void _Ready()
    {
        FindRoot();

        circulationIntensitySlider = (Slider)FindNode("Circulation intensity").GetChild(1);
        circulationIntensityBox = (SpinBox)FindNode("Circulation intensity").GetChild(2);

        orographicEffectSlider = (Slider)FindNode("Orographic Effect").GetChild(1);
        orographicEffectBox = (SpinBox)FindNode("Orographic Effect").GetChild(2);
        
        precipitationIntesitySlider = (Slider)FindNode("Precipitation Intensity").GetChild(1);
        precipitationIntesityBox = (SpinBox)FindNode("Precipitation Intensity").GetChild(2);

        iterationSlider = (Slider)FindNode("Iteration").GetChild(1);
        iterationBox = (SpinBox)FindNode("Iteration").GetChild(2);

        elevationDeltaSlider = (Slider)FindNode("Elevation Delta").GetChild(1);
        elevationDeltaBox = (SpinBox)FindNode("Elevation Delta").GetChild(2);

        maxPrecipitationSlider = (Slider)FindNode("Max Precipitation").GetChild(1);
        maxPrecipitationBox = (SpinBox)FindNode("Max Precipitation").GetChild(2);

        transpirationSlider = (Slider)FindNode("Transpiration").GetChild(1);
        transpirationBox = (SpinBox)FindNode("Transpiration").GetChild(2);

        evaporationSlider = (Slider)FindNode("Evaporation").GetChild(1);
        evaporationBox = (SpinBox)FindNode("Evaporation").GetChild(2);

        circulationIntensitySlider.SetValue(config.precipitation.circulation_intensity);
        circulationIntensityBox.SetValue(config.precipitation.circulation_intensity);        

        orographicEffectSlider.SetValue(config.precipitation.orographic_effect);
        orographicEffectBox.SetValue(config.precipitation.orographic_effect);
        
        precipitationIntesitySlider.SetValue(config.precipitation.precipitationI_intensity);
        precipitationIntesityBox.SetValue(config.precipitation.precipitationI_intensity);

        iterationSlider.SetValue(config.precipitation.iteration);
        iterationBox.SetValue(config.precipitation.iteration);

        elevationDeltaSlider.SetValue(config.precipitation.elevation_delta);
        elevationDeltaBox.SetValue(config.precipitation.elevation_delta);

        maxPrecipitationSlider.SetValue(config.precipitation.max_precipitation);
        maxPrecipitationBox.SetValue(config.precipitation.max_precipitation);

        transpirationSlider.SetValue(config.humidity.transpiration);
        transpirationBox.SetValue(config.humidity.transpiration);

        evaporationSlider.SetValue(config.humidity.evaporation);
        evaporationBox.SetValue(config.humidity.evaporation);

        circulationIntensityBox.Connect("value_changed", this, "BoxTriggered");
        orographicEffectBox.Connect("value_changed", this, "BoxTriggered");
        precipitationIntesityBox.Connect("value_changed", this, "BoxTriggered");
        iterationBox.Connect("value_changed", this, "BoxTriggered");
        elevationDeltaBox.Connect("value_changed", this, "BoxTriggered");
        maxPrecipitationBox.Connect("value_changed", this, "BoxTriggered");
        transpirationBox.Connect("value_changed", this, "BoxTriggered");
        evaporationBox.Connect("value_changed", this, "BoxTriggered");

        circulationIntensitySlider.Connect("value_changed", this, "SliderTriggered");
        orographicEffectSlider.Connect("value_changed", this, "SliderTriggered");
        precipitationIntesitySlider.Connect("value_changed", this, "SliderTriggered");
        iterationSlider.Connect("value_changed", this, "SliderTriggered");
        elevationDeltaSlider.Connect("value_changed", this, "SliderTriggered");
        maxPrecipitationSlider.Connect("value_changed", this, "SliderTriggered");
        transpirationSlider.Connect("value_changed", this, "SliderTriggered");
        evaporationSlider.Connect("value_changed", this, "SliderTriggered");
    }

    public override void BoxTriggered(int value){
        config.precipitation.circulation_intensity = circulationIntensityBox.GetValue();
        config.precipitation.orographic_effect = orographicEffectBox.GetValue();
        config.precipitation.precipitationI_intensity = precipitationIntesityBox.GetValue();
        config.precipitation.iteration = iterationBox.GetValue();
        config.precipitation.elevation_delta = (int) elevationDeltaBox.GetValue();
        config.precipitation.max_precipitation = (int) maxPrecipitationBox.GetValue();
        config.humidity.transpiration = transpirationBox.GetValue();
        config.humidity.evaporation = evaporationBox.GetValue();

        circulationIntensitySlider.SetValue(circulationIntensityBox.GetValue());
        orographicEffectSlider.SetValue(orographicEffectBox.GetValue());
        precipitationIntesitySlider.SetValue(precipitationIntesityBox.GetValue());
        iterationSlider.SetValue(iterationBox.GetValue());
        elevationDeltaSlider.SetValue(elevationDeltaBox.GetValue());
        maxPrecipitationSlider.SetValue(maxPrecipitationBox.GetValue());
        transpirationSlider.SetValue(transpirationBox.GetValue());
        evaporationSlider.SetValue(evaporationBox.GetValue());

        root.Generate();
    }
    
    public override void SliderTriggered(int value){
        config.precipitation.circulation_intensity = circulationIntensitySlider.GetValue();
        config.precipitation.orographic_effect = orographicEffectSlider.GetValue();
        config.precipitation.precipitationI_intensity = precipitationIntesitySlider.GetValue();
        config.precipitation.iteration = iterationSlider.GetValue();
        config.precipitation.elevation_delta = (int) elevationDeltaSlider.GetValue();
        config.precipitation.max_precipitation = (int) maxPrecipitationSlider.GetValue();
        config.humidity.transpiration = transpirationSlider.GetValue();
        config.humidity.evaporation = evaporationSlider.GetValue();

        circulationIntensitySlider.SetValue(circulationIntensitySlider.GetValue());
        orographicEffectSlider.SetValue(orographicEffectSlider.GetValue());
        precipitationIntesitySlider.SetValue(precipitationIntesitySlider.GetValue());
        iterationSlider.SetValue(iterationSlider.GetValue());
        elevationDeltaSlider.SetValue(elevationDeltaSlider.GetValue());
        maxPrecipitationSlider.SetValue(maxPrecipitationSlider.GetValue());
        transpirationSlider.SetValue(transpirationSlider.GetValue());
        evaporationSlider.SetValue(evaporationSlider.GetValue());

        root.Generate();
    }
}