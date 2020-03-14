using Godot;
public class TemperatureControler : Controler {
	private Slider maxTemperatureSlider;
	private SpinBox maxTemperatureBox;

	private Slider minTemperatureSlider;
	private SpinBox minTemperatureBox;

	public override void _Ready () {
		FindRoot ();

		maxTemperatureSlider = (Slider) FindNode ("Max Temperature").GetChild (1);
		maxTemperatureBox = (SpinBox) FindNode ("Max Temperature").GetChild (2);

		minTemperatureSlider = (Slider) FindNode ("Min Temperature").GetChild (1);
		minTemperatureBox = (SpinBox) FindNode ("Min Temperature").GetChild (2);

		minTemperatureSlider.Value =config.temperature.min_temperature;
		minTemperatureBox.Value = config.temperature.min_temperature;

		maxTemperatureSlider.Value = config.temperature.max_temperature;
		maxTemperatureBox.Value = config.temperature.max_temperature;

		maxTemperatureBox.Connect ("value_changed", this, "BoxTriggered");
		minTemperatureBox.Connect ("value_changed", this, "BoxTriggered");

		maxTemperatureSlider.Connect ("value_changed", this, "SliderTriggered");
		minTemperatureSlider.Connect ("value_changed", this, "SliderTriggered");
	}

	public override void BoxTriggered (int value) {
		config.temperature.max_temperature = (int) maxTemperatureBox.Value;
		config.temperature.min_temperature = (int) minTemperatureBox.Value;

		maxTemperatureSlider.Value = maxTemperatureBox.Value;
		minTemperatureSlider.Value = minTemperatureBox.Value;

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.temperature.max_temperature = (int) maxTemperatureSlider.Value;
		config.temperature.min_temperature = (int) minTemperatureSlider.Value;

		maxTemperatureBox.Value = maxTemperatureSlider.Value;
		minTemperatureBox.Value = minTemperatureSlider.Value;

		root.Generate ();
	}
}
