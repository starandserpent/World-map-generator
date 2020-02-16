using Godot;

public class GeneralControler : Controler{
    private SpinBox seed;
    private SpinBox latitude;
    private SpinBox longitude;
    public override void _Ready()
    {
        FindRoot();

        seed = (SpinBox)FindNode("Seed").GetChild(1);
        longitude = (SpinBox)FindNode("Longitude").GetChild(1);
        latitude = (SpinBox)FindNode("Latitude").GetChild(1);

        seed.SetValue(config.seed);
        latitude.SetValue(config.latitude);
        longitude.SetValue(config.longitude);

        seed.Connect("value_changed", this, "BoxTriggered");
        longitude.Connect("value_changed", this, "BoxTriggered");
        latitude.Connect("value_changed", this, "BoxTriggered");
    }

    public override void BoxTriggered(int value){
        config.seed = (int)seed.GetValue();
        config.latitude = (int) latitude.GetValue();
        config.longitude = (int) longitude.GetValue();

        root.Generate();
    }
    
    public override void SliderTriggered(int value){}
}