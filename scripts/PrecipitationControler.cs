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

		circulationIntensitySlider.SetValue (config.precipitation.circulation_intensity);
		circulationIntensityBox.SetValue (config.precipitation.circulation_intensity);

		precipitationIntesitySlider.SetValue (config.precipitation.precipitation_intensity);
		precipitationIntesityBox.SetValue (config.precipitation.precipitation_intensity);

		maxPrecipitationSlider.SetValue (config.precipitation.max_precipitation);
		maxPrecipitationBox.SetValue (config.precipitation.max_precipitation);

		circulationIntensityBox.Connect ("value_changed", this, "BoxTriggered");
		precipitationIntesityBox.Connect ("value_changed", this, "BoxTriggered");
		maxPrecipitationBox.Connect ("value_changed", this, "BoxTriggered");

		circulationIntensitySlider.Connect ("value_changed", this, "SliderTriggered");
		precipitationIntesitySlider.Connect ("value_changed", this, "SliderTriggered");
		maxPrecipitationSlider.Connect ("value_changed", this, "SliderTriggered");
	}

	public override void BoxTriggered (int value) {
		config.precipitation.circulation_intensity = circulationIntensityBox.GetValue ();
		config.precipitation.precipitation_intensity = precipitationIntesityBox.GetValue ();
		config.precipitation.max_precipitation = (int) maxPrecipitationBox.GetValue ();

		circulationIntensitySlider.SetValue (circulationIntensityBox.GetValue ());
		precipitationIntesitySlider.SetValue (precipitationIntesityBox.GetValue ());
		maxPrecipitationSlider.SetValue (maxPrecipitationBox.GetValue ());

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.precipitation.circulation_intensity = circulationIntensitySlider.GetValue ();
		config.precipitation.precipitation_intensity = precipitationIntesitySlider.GetValue ();
		config.precipitation.max_precipitation = (int) maxPrecipitationSlider.GetValue ();

		circulationIntensitySlider.SetValue (circulationIntensitySlider.GetValue ());
		precipitationIntesitySlider.SetValue (precipitationIntesitySlider.GetValue ());
		maxPrecipitationSlider.SetValue (maxPrecipitationSlider.GetValue ());

		root.Generate ();
	}
}
