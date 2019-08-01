using Godot;
using System.Linq;

public class ModLoader : Node2D
{
    [Export]
    public PackedScene Crab;

    public OptionButton DropDown;

    public override void _Ready()
    {
        DropDown = (OptionButton)GetNode("UI/Dropdown");
        DropDown.Connect("item_selected", this, "OnModSelected");

        // get an array of the folders in \Mods
        var modsList = System.IO.Directory.GetDirectories($"{System.IO.Directory.GetCurrentDirectory()}\\Mods");

        for(var i = 0; i < modsList.Length; i++)
        {
            // Add each mod to the dropdown list
            DropDown.AddItem(modsList[i].Split('\\').Last(), i);
        }

        LoadModLevel(DropDown.Items[0].ToString());
    }

    public void OnModSelected(int id)
    {
        var selectedMod = DropDown.GetItemText(DropDown.GetItemIndex(id));
        LoadModLevel(selectedMod);
    }

    private void LoadModLevel(string modName)
    {
        ProjectSettings.LoadResourcePack($"res://Mods/{modName}/LevelMod.pck");

        var importedScene = (PackedScene)ResourceLoader.Load($"res://{modName}.tscn");

        // remove all the nodes expect our dropdown
        foreach(Node child in GetChildren())
        {
            if(child.Name == "UI")
            {
                continue;
            }

            child.QueueFree();
        }

        // load the level into the game
        AddChild(importedScene.Instance());

        // add mr crabs back
        AddChild(Crab.Instance());
    }
}
