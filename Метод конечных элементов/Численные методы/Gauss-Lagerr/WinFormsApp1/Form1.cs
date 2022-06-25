namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }


    private void Button_Calculate_Click(object sender, EventArgs e)
    {
        if (!double.TryParse(TextBox_A.Text, out var a))
        {
            MessageBox.Show("Укажите нижний предел интеграла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        if (!double.TryParse(TextBox_B.Text, out var b))
        {
            MessageBox.Show("Укажите верхний предел интеграла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        var accuracy = ComboBox_Accuracy.SelectedIndex + 1;

        if (accuracy <= 0)
        {
            MessageBox.Show("Укажите количество узлов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        if (a > b)
            MessageBox.Show("Нижний предел интеграла больше верхнего", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        Func<double, double> function = (x) => 1f / Math.Sqrt(x + 4);

        var result = MathService.CalculateChebyshevQuadrature(a, b, accuracy, function);

        TextBox_Result.Text = result.ToString("0.######");
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
}
