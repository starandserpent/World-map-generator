using Godot;
public class TemperatureControler : Controler{
    private Slider maxTemperatureSlider;
    private SpinBox maxTemperatureBox;

    private Slider minTemperatureSlider;
    private SpinBox minTemperatureBox;


    private Slider temperatureDecreaseSlider;
    private SpinBox temperatureDecreaseBox;

    public override void _Ready()
    {
        FindRoot();

        maxTemperatureSlider = (Slider)FindNode("Max Temperature").GetChild(1);
        maxTemperatureBox = (SpinBox)FindNode("Max Temperature").GetChild(2);

        minTemperatureSlider = (Slider)FindNode("Min Temperature").GetChild(1);
        minTemperatureBox = (SpinBox)FindNode("Min Temperature").GetChild(2);
        
        temperatureDecreaseSlider = (Slider)FindNode("Temperature decrease").GetChild(1);
        temperatureDecreaseBox = (SpinBox)FindNode("Temperature decrease").GetChild(2);

        minTemperatureSlider.SetValue(config.minTemperature);
        minTemperatureBox.SetValue(config.minTemperature);

        maxTemperatureSlider.SetValue(config.maxTemperature);
        maxTemperatureBox.SetValue(config.maxTemperature);

        temperatureDecreaseSlider.SetValue(config.temperatureDecrease);
        temperatureDecreaseBox.SetValue(config.temperatureDecrease);

        maxTemperatureBox.Connect("value_changed", this, "BoxTriggered");
        minTemperatureBox.Connect("value_changed", this, "BoxTriggered");
        temperatureDecreaseBox.Connect("value_changed", this, "BoxTriggered");

        maxTemperatureSlider.Connect("value_changed", this, "SliderTriggered");
        minTemperatureSlider.Connect("value_changed", this, "SliderTriggered");
        temperatureDecreaseSlider.Connect("value_changed", this, "SliderTriggered");
    }

    public override void BoxTriggered(int value){
        config.maxTemperature = (int) maxTemperatureBox.GetValue();
        config.minTemperature = (int) minTemperatureBox.GetValue();
        config.temperatureDecrease = temperatureDecreaseBox.GetValue();

        maxTemperatureSlider.SetValue(maxTemperatureBox.GetValue());
        minTemperatureSlider.SetValue(minTemperatureBox.GetValue());
        temperatureDecreaseSlider.SetValue(temperatureDecreaseBox.GetValue());

        root.Generate();
    }
    
    public override void SliderTriggered(int value){
        
        config.maxTemperature = (int) maxTemperatureSlider.GetValue();
        config.minTemperature = (int) minTemperatureSlider.GetValue();
        config.temperatureDecrease = temperatureDecreaseSlider.GetValue();

        maxTemperatureBox.SetValue(maxTemperatureSlider.GetValue());
        minTemperatureBox.SetValue(minTemperatureSlider.GetValue());
        temperatureDecreaseBox.SetValue(temperatureDecreaseSlider.GetValue());

        root.Generate();
    }
}
