using Godot;

public class GeneralControler : Controler {
	private SpinBox seed;
	private SpinBox latitude;
	private SpinBox longitude;
	public override void _Ready () {
		FindRoot ();

		seed = (SpinBox) FindNode ("Seed").GetChild (1);
		longitude = (SpinBox) FindNode ("Longitude").GetChild (1);
		latitude = (SpinBox) FindNode ("Latitude").GetChild (1);

		seed.Value  = config.map.seed;
		latitude.Value = config.map.latitude;
		longitude.Value = config.map.longitude;

		seed.Connect ("value_changed", this, "BoxTriggered");
		longitude.Connect ("value_changed", this, "BoxTriggered");
		latitude.Connect ("value_changed", this, "BoxTriggered");
	}

	public override void BoxTriggered (int value) {
		config.map.seed = (int) seed.Value;
		config.map.latitude = (int) latitude.Value;
		config.map.longitude = (int) longitude.Value;

		root.Generate ();
	}

	public override void SliderTriggered (int value) { }
}
