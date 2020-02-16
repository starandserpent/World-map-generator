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

        circulationIntensitySlider.SetValue(config.circulationOctaves);
        circulationIntensityBox.SetValue(config.circulationOctaves);        

        orographicEffectSlider.SetValue(config.orographicEffect);
        orographicEffectBox.SetValue(config.orographicEffect);
        
        precipitationIntesitySlider.SetValue(config.precipitationIntensity);
        precipitationIntesityBox.SetValue(config.precipitationIntensity);

        iterationSlider.SetValue(config.iteration);
        iterationBox.SetValue(config.iteration);

        elevationDeltaSlider.SetValue(config.elevationDelta);
        elevationDeltaBox.SetValue(config.elevationDelta);

        maxPrecipitationSlider.SetValue(config.maxPrecipitation);
        maxPrecipitationBox.SetValue(config.maxPrecipitation);

        transpirationSlider.SetValue(config.transpiration);
        transpirationBox.SetValue(config.transpiration);

        evaporationSlider.SetValue(config.transpiration);
        evaporationBox.SetValue(config.transpiration);

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
        config.circulationIntensity = circulationIntensityBox.GetValue();
        config.orographicEffect = orographicEffectBox.GetValue();
        config.precipitationIntensity = precipitationIntesityBox.GetValue();
        config.iteration = iterationBox.GetValue();
        config.elevationDelta = (int) elevationDeltaBox.GetValue();
        config.maxPrecipitation = (int) maxPrecipitationBox.GetValue();
        config.transpiration = transpirationBox.GetValue();
        config.evaporation = evaporationBox.GetValue();

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
        config.circulationIntensity = circulationIntensitySlider.GetValue();
        config.orographicEffect = orographicEffectSlider.GetValue();
        config.precipitationIntensity = precipitationIntesitySlider.GetValue();
        config.iteration = iterationSlider.GetValue();
        config.elevationDelta = (int) elevationDeltaSlider.GetValue();
        config.maxPrecipitation = (int) maxPrecipitationSlider.GetValue();
        config.transpiration = transpirationSlider.GetValue();
        config.evaporation = evaporationSlider.GetValue();

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