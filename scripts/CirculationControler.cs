using Godot;
public class CirculationControler : Controler{
    private Slider exchangeCoeficientSlider;
    private SpinBox exchangeCoeficientBox;

    private Slider circulationOctavesSlider;
    private SpinBox circulationOctavesBox;

    private Slider circulationDeclineSlider;
    private SpinBox circulationDeclineBox;
    public override void _Ready()
    {
        FindRoot();

        exchangeCoeficientSlider = (Slider)FindNode("Exchange Coefficient").GetChild(1);
        exchangeCoeficientBox = (SpinBox)FindNode("Exchange Coefficient").GetChild(2);

        circulationOctavesSlider = (Slider)FindNode("Circulation Octaves").GetChild(1);
        circulationOctavesBox = (SpinBox)FindNode("Circulation Octaves").GetChild(2);
        
        circulationDeclineSlider = (Slider)FindNode("Circulation Decline").GetChild(1);
        circulationDeclineBox = (SpinBox)FindNode("Circulation Decline").GetChild(2);

        exchangeCoeficientSlider.SetValue(config.exchangeCoefficient);
        exchangeCoeficientBox.SetValue(config.exchangeCoefficient);

        circulationOctavesSlider.SetValue(config.circulationOctaves);
        circulationOctavesBox.SetValue(config.circulationOctaves);

        circulationDeclineSlider.SetValue(config.circulationDecline);
        circulationDeclineBox.SetValue(config.circulationDecline);

        exchangeCoeficientBox.Connect("value_changed", this, "BoxTriggered");
        circulationOctavesBox.Connect("value_changed", this, "BoxTriggered");
        circulationDeclineBox.Connect("value_changed", this, "BoxTriggered");

        exchangeCoeficientSlider.Connect("value_changed", this, "SliderTriggered");
        circulationOctavesSlider.Connect("value_changed", this, "SliderTriggered");
        circulationDeclineSlider.Connect("value_changed", this, "SliderTriggered");
    }

    public override void BoxTriggered(int value){
        config.exchangeCoefficient = exchangeCoeficientBox.GetValue();
        config.circulationOctaves = (int) circulationOctavesBox.GetValue();
        config.circulationDecline = (int) circulationDeclineBox.GetValue();

        exchangeCoeficientSlider.SetValue(exchangeCoeficientBox.GetValue());
        circulationOctavesSlider.SetValue(circulationOctavesBox.GetValue());
        circulationDeclineSlider.SetValue(circulationDeclineBox.GetValue());

        root.Generate();
    }
    
    public override void SliderTriggered(int value){
        config.exchangeCoefficient = exchangeCoeficientSlider.GetValue();
        config.circulationOctaves = (int) circulationOctavesSlider.GetValue();
        config.circulationDecline = (int) circulationDeclineSlider.GetValue();

        exchangeCoeficientBox.SetValue(exchangeCoeficientSlider.GetValue());
        circulationOctavesBox.SetValue(circulationOctavesSlider.GetValue());
        circulationDeclineBox.SetValue(circulationDeclineSlider.GetValue());

        root.Generate();
    }
}