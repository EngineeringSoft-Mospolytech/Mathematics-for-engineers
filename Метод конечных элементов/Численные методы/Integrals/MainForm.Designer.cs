namespace Integrals;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.ComboBox_Method = new System.Windows.Forms.ComboBox();
            this.Button_Calculate = new System.Windows.Forms.Button();
            this.TextBox_Formula = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label_N = new System.Windows.Forms.Label();
            this.Label_B = new System.Windows.Forms.Label();
            this.Label_A = new System.Windows.Forms.Label();
            this.Label_Method = new System.Windows.Forms.Label();
            this.TextBox_Result = new System.Windows.Forms.TextBox();
            this.TextBox_N = new System.Windows.Forms.TextBox();
            this.TextBox_B = new System.Windows.Forms.TextBox();
            this.TextBox_A = new System.Windows.Forms.TextBox();
            this.Label_MethodType = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBox_Method
            // 
            this.ComboBox_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Method.FormattingEnabled = true;
            this.ComboBox_Method.Items.AddRange(new object[] {
            "Сумма римана",
            "Метод трапеций",
            "Метод симпсона"});
            this.ComboBox_Method.Location = new System.Drawing.Point(12, 28);
            this.ComboBox_Method.Name = "ComboBox_Method";
            this.ComboBox_Method.Size = new System.Drawing.Size(151, 28);
            this.ComboBox_Method.TabIndex = 0;
            // 
            // Button_Calculate
            // 
            this.Button_Calculate.Location = new System.Drawing.Point(420, 37);
            this.Button_Calculate.Name = "Button_Calculate";
            this.Button_Calculate.Size = new System.Drawing.Size(165, 35);
            this.Button_Calculate.TabIndex = 1;
            this.Button_Calculate.Text = "Рассчитать";
            this.Button_Calculate.UseVisualStyleBackColor = true;
            this.Button_Calculate.Click += new System.EventHandler(this.Button_Calculate_Click);
            // 
            // TextBox_Formula
            // 
            this.TextBox_Formula.Location = new System.Drawing.Point(268, 4);
            this.TextBox_Formula.Name = "TextBox_Formula";
            this.TextBox_Formula.PlaceholderText = "sin(x)";
            this.TextBox_Formula.Size = new System.Drawing.Size(317, 27);
            this.TextBox_Formula.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Label_N);
            this.panel1.Controls.Add(this.Label_B);
            this.panel1.Controls.Add(this.Label_A);
            this.panel1.Controls.Add(this.Label_Method);
            this.panel1.Controls.Add(this.TextBox_Result);
            this.panel1.Controls.Add(this.TextBox_N);
            this.panel1.Controls.Add(this.TextBox_B);
            this.panel1.Controls.Add(this.TextBox_A);
            this.panel1.Controls.Add(this.Label_MethodType);
            this.panel1.Controls.Add(this.Button_Calculate);
            this.panel1.Controls.Add(this.TextBox_Formula);
            this.panel1.Controls.Add(this.ComboBox_Method);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 172);
            this.panel1.TabIndex = 3;
            // 
            // Label_N
            // 
            this.Label_N.AutoSize = true;
            this.Label_N.Location = new System.Drawing.Point(235, 106);
            this.Label_N.Name = "Label_N";
            this.Label_N.Size = new System.Drawing.Size(20, 20);
            this.Label_N.TabIndex = 11;
            this.Label_N.Text = "N";
            // 
            // Label_B
            // 
            this.Label_B.AutoSize = true;
            this.Label_B.Location = new System.Drawing.Point(237, 73);
            this.Label_B.Name = "Label_B";
            this.Label_B.Size = new System.Drawing.Size(18, 20);
            this.Label_B.TabIndex = 10;
            this.Label_B.Text = "B";
            // 
            // Label_A
            // 
            this.Label_A.AutoSize = true;
            this.Label_A.Location = new System.Drawing.Point(237, 40);
            this.Label_A.Name = "Label_A";
            this.Label_A.Size = new System.Drawing.Size(19, 20);
            this.Label_A.TabIndex = 9;
            this.Label_A.Text = "A";
            // 
            // Label_Method
            // 
            this.Label_Method.AutoSize = true;
            this.Label_Method.Location = new System.Drawing.Point(231, 7);
            this.Label_Method.Name = "Label_Method";
            this.Label_Method.Size = new System.Drawing.Size(31, 20);
            this.Label_Method.TabIndex = 8;
            this.Label_Method.Text = "f(x)";
            // 
            // TextBox_Result
            // 
            this.TextBox_Result.Location = new System.Drawing.Point(420, 78);
            this.TextBox_Result.Multiline = true;
            this.TextBox_Result.Name = "TextBox_Result";
            this.TextBox_Result.PlaceholderText = "Результат";
            this.TextBox_Result.ReadOnly = true;
            this.TextBox_Result.Size = new System.Drawing.Size(165, 52);
            this.TextBox_Result.TabIndex = 7;
            // 
            // TextBox_N
            // 
            this.TextBox_N.Location = new System.Drawing.Point(268, 103);
            this.TextBox_N.Name = "TextBox_N";
            this.TextBox_N.PlaceholderText = "100";
            this.TextBox_N.Size = new System.Drawing.Size(125, 27);
            this.TextBox_N.TabIndex = 6;
            // 
            // TextBox_B
            // 
            this.TextBox_B.Location = new System.Drawing.Point(268, 70);
            this.TextBox_B.Name = "TextBox_B";
            this.TextBox_B.PlaceholderText = "6";
            this.TextBox_B.Size = new System.Drawing.Size(125, 27);
            this.TextBox_B.TabIndex = 5;
            // 
            // TextBox_A
            // 
            this.TextBox_A.Location = new System.Drawing.Point(268, 37);
            this.TextBox_A.Name = "TextBox_A";
            this.TextBox_A.PlaceholderText = "1";
            this.TextBox_A.Size = new System.Drawing.Size(125, 27);
            this.TextBox_A.TabIndex = 4;
            // 
            // Label_MethodType
            // 
            this.Label_MethodType.AutoSize = true;
            this.Label_MethodType.Location = new System.Drawing.Point(28, 4);
            this.Label_MethodType.Name = "Label_MethodType";
            this.Label_MethodType.Size = new System.Drawing.Size(124, 20);
            this.Label_MethodType.TabIndex = 3;
            this.Label_MethodType.Text = "Выберите метод";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 556);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Численные методы интегрирования";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private ComboBox ComboBox_Method;
    private Button Button_Calculate;
    private TextBox TextBox_Formula;
    private Panel panel1;
    private Label Label_MethodType;
    private TextBox TextBox_B;
    private TextBox TextBox_A;
    private TextBox TextBox_N;
    private TextBox TextBox_Result;
    private Label Label_N;
    private Label Label_B;
    private Label Label_A;
    private Label Label_Method;
}
