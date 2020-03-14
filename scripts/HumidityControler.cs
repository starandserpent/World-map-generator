using Godot;
public class HumidityControler : Controler {
	private Slider transpirationSlider;
	private SpinBox transpirationBox;

	private Slider evaporationSlider;
	private SpinBox evaporationBox;

	public override void _Ready () {
		FindRoot ();

		transpirationSlider = (Slider) FindNode ("Transpiration").GetChild (1);
		transpirationBox = (SpinBox) FindNode ("Transpiration").GetChild (2);

		evaporationSlider = (Slider) FindNode ("Evaporation").GetChild (1);
		evaporationBox = (SpinBox) FindNode ("Evaporation").GetChild (2);

		transpirationSlider.Value = config.humidity.transpiration;
		transpirationBox.Value = config.humidity.transpiration;

		evaporationSlider.Value = config.humidity.evaporation;
		evaporationBox.Value = config.humidity.evaporation;

		transpirationBox.Connect ("value_changed", this, "BoxTriggered");
		evaporationBox.Connect ("value_changed", this, "BoxTriggered");

		transpirationSlider.Connect ("value_changed", this, "SliderTriggered");
		evaporationSlider.Connect ("value_changed", this, "SliderTriggered");
	}

	public override void BoxTriggered (int value) {
		config.humidity.transpiration = transpirationBox.Value;
		config.humidity.evaporation = evaporationBox.Value;

		transpirationSlider.Value = transpirationBox.Value;
		evaporationSlider.Value = evaporationBox.Value;

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.humidity.transpiration = transpirationSlider.Value;
		config.humidity.evaporation = evaporationSlider.Value;

		transpirationSlider.Value = transpirationSlider.Value;
		evaporationSlider.Value = evaporationSlider.Value;

		root.Generate ();
	}
}
