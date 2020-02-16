using Godot;

public abstract class Controler : Node{
    protected RootControler root;
    protected Config config;
    protected void FindRoot(){
        root = (RootControler)FindParent("Root");
        config = root.GetConfig();
    }

    public abstract void BoxTriggered(int value);
    public abstract void SliderTriggered(int value);
}