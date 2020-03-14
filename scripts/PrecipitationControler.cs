using Godot;
public class PrecipitationControler : Controler {
	private Slider circulationIntensitySlider;
	private SpinBox circulationIntensityBox;

	private Slider precipitationIntesitySlider;
	private SpinBox precipitationIntesityBox;

	private Slider maxPrecipitationSlider;
	private SpinBox maxPrecipitationBox;

	public override void _Ready () {
		FindRoot ();

		circulationIntensitySlider = (Slider) FindNode ("Circulation intensity").GetChild (1);
		circulationIntensityBox = (SpinBox) FindNode ("Circulation intensity").GetChild (2);

		precipitationIntesitySlider = (Slider) FindNode ("Precipitation Intensity").GetChild (1);
		precipitationIntesityBox = (SpinBox) FindNode ("Precipitation Intensity").GetChild (2);

		maxPrecipitationSlider = (Slider) FindNode ("Max Precipitation").GetChild (1);
		maxPrecipitationBox = (SpinBox) FindNode ("Max Precipitation").GetChild (2);

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
	}

	public override void BoxTriggered (int value) {
		config.precipitation.circulation_intensity = circulationIntensityBox.Value;
		config.precipitation.precipitation_intensity = precipitationIntesityBox.Value;
		config.precipitation.max_precipitation = (int) maxPrecipitationBox.Value ;

		circulationIntensitySlider.Value = circulationIntensityBox.Value;
		precipitationIntesitySlider.Value = precipitationIntesityBox.Value;
		maxPrecipitationSlider.Value = maxPrecipitationBox.Value;

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.precipitation.circulation_intensity = circulationIntensitySlider.Value;
		config.precipitation.precipitation_intensity = precipitationIntesitySlider.Value;
		config.precipitation.max_precipitation = (int) maxPrecipitationSlider.Value;

		circulationIntensitySlider.Value = circulationIntensitySlider.Value;
		precipitationIntesitySlider.Value = precipitationIntesitySlider.Value;
		maxPrecipitationSlider.Value = maxPrecipitationSlider.Value;

		root.Generate ();
	}
}
