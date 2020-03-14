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

		transpirationSlider.SetValue (config.humidity.transpiration);
		transpirationBox.SetValue (config.humidity.transpiration);

		evaporationSlider.SetValue (config.humidity.evaporation);
		evaporationBox.SetValue (config.humidity.evaporation);

		transpirationBox.Connect ("value_changed", this, "BoxTriggered");
		evaporationBox.Connect ("value_changed", this, "BoxTriggered");

		transpirationSlider.Connect ("value_changed", this, "SliderTriggered");
		evaporationSlider.Connect ("value_changed", this, "SliderTriggered");
	}

	public override void BoxTriggered (int value) {
		config.humidity.transpiration = transpirationBox.GetValue ();
		config.humidity.evaporation = evaporationBox.GetValue ();

		transpirationSlider.SetValue (transpirationBox.GetValue ());
		evaporationSlider.SetValue (evaporationBox.GetValue ());

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.humidity.transpiration = transpirationSlider.GetValue ();
		config.humidity.evaporation = evaporationSlider.GetValue ();

		transpirationSlider.SetValue (transpirationSlider.GetValue ());
		evaporationSlider.SetValue (evaporationSlider.GetValue ());

		root.Generate ();
	}
}
