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

        minTemperatureSlider.SetValue (config.temperature.min_temperature);
        minTemperatureBox.SetValue (config.temperature.min_temperature);

        maxTemperatureSlider.SetValue (config.temperature.max_temperature);
        maxTemperatureBox.SetValue (config.temperature.max_temperature);

        maxTemperatureBox.Connect ("value_changed", this, "BoxTriggered");
        minTemperatureBox.Connect ("value_changed", this, "BoxTriggered");

        maxTemperatureSlider.Connect ("value_changed", this, "SliderTriggered");
        minTemperatureSlider.Connect ("value_changed", this, "SliderTriggered");
    }

    public override void BoxTriggered (int value) {
        config.temperature.max_temperature = (int) maxTemperatureBox.GetValue ();
        config.temperature.min_temperature = (int) minTemperatureBox.GetValue ();

        maxTemperatureSlider.SetValue (maxTemperatureBox.GetValue ());
        minTemperatureSlider.SetValue (minTemperatureBox.GetValue ());

        root.Generate ();
    }

    public override void SliderTriggered (int value) {
        config.temperature.max_temperature = (int) maxTemperatureSlider.GetValue ();
        config.temperature.min_temperature = (int) minTemperatureSlider.GetValue ();

        maxTemperatureBox.SetValue (maxTemperatureSlider.GetValue ());
        minTemperatureBox.SetValue (minTemperatureSlider.GetValue ());

        root.Generate ();
    }
}