using Godot;
public class CirculationControler : Controler {
	private Slider windIntesitySlider;
	private SpinBox windIntesityBox;

	private Slider windRangeSlider;
	private SpinBox windRangeBox;

	private SpinBox pressureAtSeaLevelBox;

	public override void _Ready () {
		FindRoot ();

		windIntesitySlider = (Slider) FindNode ("Wind Intesity").GetChild (1);
		windIntesityBox = (SpinBox) FindNode ("Wind Intesity").GetChild (2);

		windRangeSlider = (Slider) FindNode ("Wind Range").GetChild (1);
		windRangeBox = (SpinBox) FindNode ("Wind Range").GetChild (2);

		pressureAtSeaLevelBox = (SpinBox) FindNode ("Pressure at sea level").GetChild (1);

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
	}

	public override void BoxTriggered (int value) {
		config.circulation.wind_intensity = windIntesityBox.Value;
		config.circulation.wind_range = (int) windRangeBox.Value;
		config.circulation.pressure_at_sea_level = (int) pressureAtSeaLevelBox.Value;

		windIntesitySlider.Value = windIntesityBox.Value;
		windRangeSlider.Value = windRangeBox.Value;

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.circulation.wind_intensity = windIntesitySlider.Value;
		config.circulation.wind_range = (int) windRangeSlider.Value;

		windIntesityBox.Value = windIntesitySlider.Value;
		windRangeBox.Value = windRangeSlider.Value;

		root.Generate ();
	}
}
