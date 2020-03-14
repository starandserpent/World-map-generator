using Godot;
public class CirculationControler : Controler {
	private Slider windIntesitySlider;
	private SpinBox windIntesityBox;

	private Slider windRangeSlider;
	private SpinBox windRangeBox;

	private Slider pressureAtSeaLevelSlider;
	private SpinBox pressureAtSeaLevelBox;
	public override void _Ready () {
		FindRoot ();

		windIntesitySlider = (Slider) FindNode ("Wind Intesity").GetChild (1);
		windIntesityBox = (SpinBox) FindNode ("Wind Intesity").GetChild (2);

		windRangeSlider = (Slider) FindNode ("Wind Range").GetChild (1);
		windRangeBox = (SpinBox) FindNode ("Wind Range").GetChild (2);

		pressureAtSeaLevelSlider = (Slider) FindNode ("Pressure at sea level").GetChild (1);
		pressureAtSeaLevelBox = (SpinBox) FindNode ("Pressure at sea level").GetChild (2);

		windIntesitySlider.SetValue (config.circulation.wind_intensity);
		windIntesityBox.SetValue (config.circulation.wind_intensity);

		windRangeSlider.SetValue (config.circulation.wind_range);
		windRangeBox.SetValue (config.circulation.wind_range);

		pressureAtSeaLevelSlider.SetValue (config.circulation.pressure_at_sea_level);
		pressureAtSeaLevelBox.SetValue (config.circulation.pressure_at_sea_level);

		windIntesityBox.Connect ("value_changed", this, "BoxTriggered");
		windRangeBox.Connect ("value_changed", this, "BoxTriggered");
		pressureAtSeaLevelBox.Connect ("value_changed", this, "BoxTriggered");

		windIntesitySlider.Connect ("value_changed", this, "SliderTriggered");
		windRangeSlider.Connect ("value_changed", this, "SliderTriggered");
		pressureAtSeaLevelSlider.Connect ("value_changed", this, "SliderTriggered");
	}

	public override void BoxTriggered (int value) {
		config.circulation.wind_intensity = (double)windIntesityBox.GetValue ();
		config.circulation.wind_range = (int) windRangeBox.GetValue ();
		config.circulation.pressure_at_sea_level = (int) pressureAtSeaLevelBox.GetValue ();

		windIntesitySlider.SetValue (windIntesityBox.GetValue ());
		windRangeSlider.SetValue (windRangeBox.GetValue ());
		pressureAtSeaLevelSlider.SetValue (pressureAtSeaLevelBox.GetValue ());

		root.Generate ();
	}

	public override void SliderTriggered (int value) {
		config.circulation.wind_intensity = windIntesitySlider.GetValue ();
		config.circulation.wind_range = (int) windRangeSlider.GetValue ();
		config.circulation.pressure_at_sea_level = (int) pressureAtSeaLevelSlider.GetValue ();

		windIntesityBox.SetValue (windIntesitySlider.GetValue ());
		windRangeBox.SetValue (windRangeSlider.GetValue ());
		pressureAtSeaLevelBox.SetValue (pressureAtSeaLevelSlider.GetValue ());

		root.Generate ();
	}
}
