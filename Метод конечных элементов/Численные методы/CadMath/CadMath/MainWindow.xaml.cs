using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Integrals;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace CadMath;

public partial class MainWindow : Window
{
    private readonly Dictionary<string, MathMethodInfo> _mathMethodInfos;

    public SeriesCollection Series { get; set; }

    public Func<double, string> YFormatter { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        YFormatter = y => y.ToString("F2");



        Series = new();

        _mathMethodInfos = new()
        {
            {
                "Сумма Римана (левые прямоугольники)",
                new("Сумма Римана (левые прямоугольники)")
                {
                    Description = "Наиболее широко используемый вид определённого интеграла" +
                    "\nОчень часто под термином «определённый интеграл» понимается именно интеграл Римана, " +
                    "и он изучается самым первым из всех определённых интегралов во всех курсах математического анализа." +
                    "\nВведён Бернхардом Риманом в 1854 году, и является одной из первых формализаций понятия интеграла" +
                    "\nЗаданная функция — положительная и возрастающая, то эта формула выражает площадь ступенчатой фигуры, " +
                    "составленной из «входящих» прямоугольников, также называемая формулой левых прямоугольников"
                }
            },
            {
                "Сумма римана (правые прямоугольники)",
                new("Метод средних прямоугольников")
                {
                    Description = "Наиболее широко используемый вид определённого интеграла" +
                    "\nОчень часто под термином «определённый интеграл» понимается именно интеграл Римана, " +
                    "и он изучается самым первым из всех определённых интегралов во всех курсах математического анализа." +
                    "\nВведён Бернхардом Риманом в 1854 году, и является одной из первых формализаций понятия интеграла" +
                    "\nЗаданная функция выражает площадь ступенчатой фигуры, состоящей из «выходящих» прямоугольников, " +
                    "также называемая формулой правых прямоугольников"
                }
            },
            {
                "Сумма римана (средние прямоугольники)",
                new("Метод средних прямоугольников")
                {
                    Description = "Наиболее широко используемый вид определённого интеграла" +
                    "\nОчень часто под термином «определённый интеграл» понимается именно интеграл Римана, " +
                    "и он изучается самым первым из всех определённых интегралов во всех курсах математического анализа." +
                    "\nВведён Бернхардом Риманом в 1854 году, и является одной из первых формализаций понятия интеграла" +
                    "\nЗаданная функция берет в качестве опорной точки для нахождения высоты точку посередине промежутка, " +
                    "также называемая формулой средних прямоугольников"
                }
            },
            {
                "Метод трапеций",
                new("Метод трапеций")
                {
                    Description = "Метод численного интегрирования функции одной переменной, " +
                    "заключающийся в замене на каждом элементарном отрезке подынтегральной функции на многочлен первой степени, то есть линейную функцию. " +
                    "\nПлощадь под графиком функции аппроксимируется прямоугольными трапециями."
                }
            },
            {
                "Метод Симпсона",
                new("Метод Симпсона")
                {
                    Description = "Метод Симпсона заключается в интегрировании интерполяционного многочлена второй степени функции f(x) с узлами интерполяции" +
                    "\na, b и m = (a+b)/2 — параболы p(x)." +
                    "\nДля повышения точности имеет смысл разбить отрезок интегрирования на N равных промежутков(по аналогии с методом трапеций), " +
                    "на каждом из которых применить метод Симпсона."
                }
            },
            {
                "Метод Монте-Карло",
                new("Метод Монте-Карло")
                {
                    Description = "Суть метода заключается в следующем: " +
                    "\nпроцесс описывается математической моделью с использованием генератора случайных величин, модель многократно обсчитывается, " +
                    "на основе полученных данных вычисляются вероятностные характеристики рассматриваемого процесса."
                }
            }
        };

        MethodsList.ItemsSource = _mathMethodInfos.Keys;
        MethodsList.SelectedItem = _mathMethodInfos.Keys.First();

        DataContext = this;
    }

    private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
    {
        var formula = Formula.Text?.ToLower();

        Func<double, double>? f = null;

        if (string.IsNullOrWhiteSpace(formula))
            f = GetDefaultFormula;
        else if (!FormulaBuilder.TryBuildFormula(formula, out f))
            return;

        var a = 0d;
        var b = 5d;
        var n = 50;

        if (!string.IsNullOrWhiteSpace(LeftLimit.Text) && !double.TryParse(LeftLimit.Text, out a))
        {
            MessageBox.Show("Неверно введена левая граница интегрирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!string.IsNullOrWhiteSpace(RightLimit.Text) && !double.TryParse(RightLimit.Text, out b))
        {
            MessageBox.Show("Неверно введена правая граница интегрирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!string.IsNullOrWhiteSpace(SplitsCount.Text) && !int.TryParse(SplitsCount.Text, out n))
        {
            MessageBox.Show("Неверно введена точность интегрирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            return;
        }

        ChartValues<Point> points = new();

        double? res = MethodsList.SelectedIndex switch
        {
            0 => IntegralMath.LeftRiemannSum(a, b, n, f, (x, y) => cb(x, y)),
            1 => IntegralMath.RightRiemannSum(a, b, n, f, (x, y) => cb(x, y)),
            2 => IntegralMath.MiddleRiemannSum(a, b, n, f, (x, y) => cb(x, y)),
            3 => IntegralMath.TrapezoidalRule(a, b, n, f, (x, y) => cb(x, y)),
            4 => IntegralMath.SimpsonsRule(a, b, n, f, (x, y) => cb(x, y)),
            5 => IntegralMath.MonteCarlo(a, b, n, f, (x, y) => cb(x, y)),
            _ => null
        };


        Result.Text = res?.ToString("F4");


        var lineSeries = new LineSeries
        {
            Title = MethodsList.SelectionBoxItem.ToString(),
            Configuration = new CartesianMapper<Point>()
                .X(point => point.X)
                .Y(point => point.Y),
            Values = points,
            PointGeometrySize = 4,
            LabelPoint = cp => cp.Y.ToString("F2")
        };

        Series.Add(lineSeries);

        DataContext = this;


        void cb(double x, double y) => points.Add(new(x, y));

        static double GetDefaultFormula(double x) => Math.Sin(x) * Math.Cos(x * x);
    }


    private void MethodsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var methodName = MethodsList.SelectedItem.ToString();

        MethodDescription.Text = _mathMethodInfos.GetValueOrDefault(methodName ?? string.Empty)?.Description;
    }

    private void ClearChart_Click(object sender, RoutedEventArgs e)
    {
        Series.Clear();
    }

    public void DeletableChartLegend_Remove(Brush lineSeriesStroke)
    {
        var series = Series.First(seriesView => seriesView is Series s && s.Stroke == lineSeriesStroke);

        Series.Remove(series);
    }
}
