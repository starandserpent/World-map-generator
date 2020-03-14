using Godot;
public class ElevationControler : Controler {
	private SpinBox minElevation;
	private SpinBox maxElevation;
	private SpinBox waterLevel;
	private SpinBox frequencyBox;
	private Slider frequencySlider;
	private SpinBox octavesBox;
	private Slider octavesSlider;

	public override void _Ready () {
		FindRoot ();

		minElevation = (SpinBox) FindNode ("Min elevation").GetChild (1);
		maxElevation = (SpinBox) FindNode ("Max elevation").GetChild (1);
		waterLevel = (SpinBox) FindNode ("Water Level").GetChild (1);
		frequencySlider = (Slider) FindNode ("Frequency").GetChild (1);
		frequencyBox = (SpinBox) FindNode ("Frequency").GetChild (2);
		octavesSlider = (Slider) FindNode ("Octaves").GetChild (1);
		octavesBox = (SpinBox) FindNode ("Octaves").GetChild (2);

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

	public override void BoxTriggered (int value) {
		config.elevation.max_elevation = (int) maxElevation.Value;
		config.elevation.min_elevation = (int) minElevation.Value;
		config.elevation.water_level = (int) waterLevel.Value;
		config.elevation.frequency = frequencyBox.Value;

		frequencySlider.Value = frequencyBox.Value;
		octavesSlider.Value = octavesBox.Value;

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.elevation.frequency = frequencySlider.Value;
		config.elevation.octaves = (int) octavesSlider.Value;

		frequencyBox.Value = frequencySlider.Value;
		octavesBox.Value = octavesSlider.Value;

		root.Generate ();
	}
}
