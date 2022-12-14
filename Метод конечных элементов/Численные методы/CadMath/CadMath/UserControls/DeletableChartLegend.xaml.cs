using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace CadMath.UserControls;


public partial class DeletableChartLegend : UserControl, IChartLegend
{
    private List<SeriesViewModel> _series;

    public event Action<Brush>? Remove;

    public DeletableChartLegend()
    {
        InitializeComponent();

        _series = new();

        DataContext = this;
    }

    public List<SeriesViewModel> Series
    {
        get => _series;
        set
        {
            _series = value;
            OnPropertyChanged(nameof(Series));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        if (PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe)
            return;

        if (fe.DataContext is not SeriesViewModel seriesVM)
            return;

        Remove?.Invoke(seriesVM.Stroke);
    }
}
