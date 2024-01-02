using System.Reflection;
using Godot;
using LitJson;

public partial class Dialogue : Node2D
{
    [Export] public string Language = "Eng";
    [Export] private string name;
    [Export] private Label _label;
    private JsonData _dialogue;
    private int _index = 0;
    private bool _interact = false;
    private Player _player = default;

    public override void _Ready()
    {
        _player = GetNodeOrNull<Player>("/root/MainLevel/Player");
        if (_player != null)
            _player.Next += () => OnNextPressed();
        LoadDialogue().SetLine();
    }

    private Dialogue NextLine()
    {
        _index++;
        return this;
    }

    private Dialogue SetLine()
    {
        if (_dialogue[Language][name].ContainsKey(_index.ToString()))
            _label.Text = _dialogue[Language][name][_index.ToString()].ToString();
        return this;
    }


    private Dialogue LoadDialogue()
    {
        var dialogueFile = FileAccess.Open("res://Dialogue/Dialogue.json", FileAccess.ModeFlags.Read);
        _dialogue = JsonMapper.ToObject(dialogueFile.GetAsText());
        return this;
    }

    private void OnBodyEntered(Node2D body)
    {
        _interact = true;
    }

    private void OnBodyExit(Node2D body)
    {
        _interact = false;
    }

    private void OnNextPressed()
    {
        if (_interact)
            NextLine().SetLine();
    }
}
