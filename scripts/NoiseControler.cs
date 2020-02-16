using Godot;
public class NoiseControler : Controler{
    private SpinBox minElevation;
    private SpinBox maxElevation;
    private SpinBox frequencyBox;
    private Slider frequencySlider;

    public override void _Ready()
    {
        FindRoot();

        minElevation = (SpinBox)FindNode("Min elevation").GetChild(1);
        maxElevation = (SpinBox)FindNode("Max elevation").GetChild(1);
        frequencySlider = (Slider)FindNode("Frequency").GetChild(1);
        frequencyBox = (SpinBox)FindNode("Frequency").GetChild(2);

        minElevation.SetValue(config.minElevation);
        maxElevation.SetValue(config.maxElevation);
        frequencyBox.SetValue(config.frequency);
        frequencySlider.SetValue(config.frequency);

        minElevation.Connect("value_changed", this, "BoxTriggered");
        maxElevation.Connect("value_changed", this, "BoxTriggered");
        frequencyBox.Connect("value_changed", this, "BoxTriggered");
        frequencySlider.Connect("value_changed", this, "SliderTriggered");
    }
    
    public override void BoxTriggered(int value){
        config.maxElevation = (int) maxElevation.GetValue();
        config.minElevation = (int) minElevation.GetValue();
        config.frequency = frequencyBox.GetValue();
        frequencySlider.SetValue(frequencyBox.GetValue());

        root.Generate();
    }
    
    public override void SliderTriggered(int value){
        config.frequency = frequencySlider.GetValue();
        frequencyBox.SetValue(frequencySlider.GetValue());

        root.Generate();
    }
}