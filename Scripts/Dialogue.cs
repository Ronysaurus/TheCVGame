using Godot;
using LitJson;

public partial class Dialogue : Control
{
    [Export] public string Language = "Eng"; 
    [Export] private string name;
    [Export] private Label _label;
    private JsonData _dialogue;
    private int _index = 0; 

    public override void _Ready()
    {
        LoadDialogue().SetLine();
    }

    private Dialogue NextLine()
    {
        _index++;
        return this;
    }

    private Dialogue SetLine()
    {
        _label.Text = _dialogue[Language][name][_index.ToString()].ToString();
        return this;
    }


    private Dialogue LoadDialogue()
    {
        var dialogueFile = FileAccess.Open("res://Dialogue/Dialogue.json", FileAccess.ModeFlags.Read);
        _dialogue = JsonMapper.ToObject(dialogueFile.GetAsText());
        return this;
    }
}
