using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CadMath;

public class MathMethodInfo : INotifyPropertyChanged
{
    private string _name;
    private string? _description;


    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(_name));
        }
    }

    public string? Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged(nameof(_description));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;


    public MathMethodInfo(string name)
    {
        _name = name;
    }


    public void OnPropertyChanged([CallerMemberName] string prop = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
