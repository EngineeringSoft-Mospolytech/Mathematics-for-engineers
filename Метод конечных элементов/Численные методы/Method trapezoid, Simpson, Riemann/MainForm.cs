using System.Windows.Forms.DataVisualization.Charting;

namespace Integrals;

public partial class MainForm : Form
{
    private const float CZoomScale = 2f;

    private readonly Stack<ZoomFrame> _zoomFrames = new();

    private readonly Chart _chart = new();


    public MainForm()
    {
        InitializeComponent();

        ComboBox_Method.SelectedIndex = 0;

        _chart.Parent = this;
        _chart.Height = (int)(Height / 1.5);
        _chart.Dock = DockStyle.Top;

        ChartArea chartArea = new("Math functions");
        chartArea.AxisX.ScaleView.Zoomable = true;
        chartArea.AxisY.ScaleView.Zoomable = true;
        chartArea.CursorY.AutoScroll = true;
        chartArea.AxisX.LabelStyle.Format = "F3";
        chartArea.AxisY.LabelStyle.Format = "F3";
        chartArea.AxisX.LineWidth = 3;
        chartArea.AxisY.LineWidth = 3;
        chartArea.AxisX.Crossing = 0;
        chartArea.AxisY.Crossing = 0;

        _chart.ChartAreas.Add(chartArea);

        _chart.MouseWheel += OnChartMouseWheel;



        Series series = new("Math");
        series.ChartType = SeriesChartType.Spline;
        series.ChartArea = "Math functions";
        series.BorderWidth = 3;
        series.BorderColor = Color.Black;

        Series fillSeries = new("Fill");

        fillSeries.ChartType = SeriesChartType.Range;
        fillSeries.YValuesPerPoint = 2;
        fillSeries.Color = Color.Lavender;
        fillSeries.BorderColor = Color.Cyan;

        _chart.Series.Add(series);
        _chart.Series.Add(fillSeries);
    }


    private void Button_Calculate_Click(object sender, EventArgs e)
    {
        foreach (var series in _chart.Series)
            series.Points.Clear();

        TextBox_Result.Text = "";

        Func<double, double>? f = null;
        var a = 1d;
        var b = 6d;
        var n = 100d;

        var input = TextBox_Formula.Text;

        if (string.IsNullOrWhiteSpace(input))
            f = GetDefaultFormula;
        else if (!FormulaBuilder.TryBuildFormula(input.ToLower(), out f))
        {
            MessageBox.Show("Не удалость распознать формулу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        if (!string.IsNullOrWhiteSpace(TextBox_A.Text) && !double.TryParse(TextBox_A.Text, out a))
        {
            MessageBox.Show("Неверно введена левая граница интегрирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        if (!string.IsNullOrWhiteSpace(TextBox_B.Text) && !double.TryParse(TextBox_B.Text, out b))
        {
            MessageBox.Show("Неверно введена правая граница интегрирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        if (!string.IsNullOrWhiteSpace(TextBox_N.Text) && !double.TryParse(TextBox_N.Text, out n))
        {
            MessageBox.Show("Неверно введена точность интегрирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        var mathSeries = _chart.Series["Math"];
        var fillSeries = _chart.Series["Fill"];

        double? res = ComboBox_Method.SelectedIndex switch
        {
            0 => IntegralMath.RiemannSum(a, b, n, f, cb),
            1 => IntegralMath.TrapezoidalRule(a, b, n, f, cb),
            2 => IntegralMath.SimpsonsRule(a, b, n, f, cb),
            _ => null
        };

        if (res is null)
        {
            MessageBox.Show("Не удалось получить результат", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return;
        }

        foreach (var point in mathSeries.Points)
        {
            var x = point.XValue;
            var y = point.YValues[0];

            fillSeries.Points.AddXY(x, 0d, y);
        }

        TextBox_Result.Text = $"Результат:{Environment.NewLine}{res:F4}";


        void cb(double x, double y) => mathSeries.Points.AddXY(x, y);
    }


    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }


    private void OnChartMouseWheel(object? sender, MouseEventArgs e)
    {
        if (sender is not Chart chart)
            return;

        var xAxis = chart.ChartAreas[0].AxisX;
        var yAxis = chart.ChartAreas[0].AxisY;

        try
        {
            if (e.Delta < 0)
            {
                if (0 < _zoomFrames.Count)
                {
                    var frame = _zoomFrames.Pop();
                    if (_zoomFrames.Count == 0)
                    {
                        xAxis.ScaleView.ZoomReset();
                        yAxis.ScaleView.ZoomReset();
                    }
                    else
                    {
                        xAxis.ScaleView.Zoom(frame.XStart, frame.XFinish);
                        yAxis.ScaleView.Zoom(frame.YStart, frame.YFinish);
                    }
                }
            }
            else if (e.Delta > 0)
            {
                var xMin = xAxis.ScaleView.ViewMinimum;
                var xMax = xAxis.ScaleView.ViewMaximum;
                var yMin = yAxis.ScaleView.ViewMinimum;
                var yMax = yAxis.ScaleView.ViewMaximum;

                _zoomFrames.Push(new ZoomFrame { XStart = xMin, XFinish = xMax, YStart = yMin, YFinish = yMax });

                var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / CZoomScale;
                var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / CZoomScale;
                var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / CZoomScale;
                var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / CZoomScale;

                xAxis.ScaleView.Zoom(posXStart, posXFinish);
                yAxis.ScaleView.Zoom(posYStart, posYFinish);
            }
        }
        catch { }
    }


    private double GetDefaultFormula(double x) => Math.Sin(x);


    private class ZoomFrame
    {
        public double XStart { get; set; }
        public double XFinish { get; set; }
        public double YStart { get; set; }
        public double YFinish { get; set; }
    }
}
