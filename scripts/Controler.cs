using Godot;
using System.Collections.Generic;

public abstract class Controler : Node{
    private RootControler root;
    public abstract void Configure(Config config);
    protected void FindControler(){
        GetParent().FindNode("Root");
    }
    public abstract Config GetValues();
}