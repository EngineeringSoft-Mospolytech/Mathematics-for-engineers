namespace WinFormsApp1;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.Button_Calculate = new System.Windows.Forms.Button();
            this.TextBox_A = new System.Windows.Forms.TextBox();
            this.TextBox_B = new System.Windows.Forms.TextBox();
            this.TextBox_Function = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TextBox_Result = new System.Windows.Forms.TextBox();
            this.ComboBox_Accuracy = new System.Windows.Forms.ComboBox();
            this.Label_Accuracy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_Calculate
            // 
            this.Button_Calculate.Location = new System.Drawing.Point(232, 80);
            this.Button_Calculate.Name = "Button_Calculate";
            this.Button_Calculate.Size = new System.Drawing.Size(36, 27);
            this.Button_Calculate.TabIndex = 0;
            this.Button_Calculate.Text = "=";
            this.Button_Calculate.UseVisualStyleBackColor = true;
            this.Button_Calculate.Click += new System.EventHandler(this.Button_Calculate_Click);
            // 
            // TextBox_A
            // 
            this.TextBox_A.Location = new System.Drawing.Point(37, 134);
            this.TextBox_A.Name = "TextBox_A";
            this.TextBox_A.PlaceholderText = "a";
            this.TextBox_A.Size = new System.Drawing.Size(44, 27);
            this.TextBox_A.TabIndex = 1;
            // 
            // TextBox_B
            // 
            this.TextBox_B.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox_B.Location = new System.Drawing.Point(37, 24);
            this.TextBox_B.Name = "TextBox_B";
            this.TextBox_B.PlaceholderText = "b";
            this.TextBox_B.Size = new System.Drawing.Size(44, 27);
            this.TextBox_B.TabIndex = 2;
            // 
            // TextBox_Function
            // 
            this.TextBox_Function.Location = new System.Drawing.Point(87, 80);
            this.TextBox_Function.Name = "TextBox_Function";
            this.TextBox_Function.PlaceholderText = "1 / sqrt(x + 4)";
            this.TextBox_Function.Size = new System.Drawing.Size(125, 27);
            this.TextBox_Function.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::WinFormsApp1.Properties.Resources.integral;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 162);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // TextBox_Result
            // 
            this.TextBox_Result.Location = new System.Drawing.Point(289, 80);
            this.TextBox_Result.Name = "TextBox_Result";
            this.TextBox_Result.PlaceholderText = "Результат";
            this.TextBox_Result.ReadOnly = true;
            this.TextBox_Result.Size = new System.Drawing.Size(206, 27);
            this.TextBox_Result.TabIndex = 5;
            // 
            // ComboBox_Accuracy
            // 
            this.ComboBox_Accuracy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Accuracy.FormattingEnabled = true;
            this.ComboBox_Accuracy.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.ComboBox_Accuracy.Location = new System.Drawing.Point(12, 200);
            this.ComboBox_Accuracy.Name = "ComboBox_Accuracy";
            this.ComboBox_Accuracy.Size = new System.Drawing.Size(101, 28);
            this.ComboBox_Accuracy.TabIndex = 7;
            // 
            // Label_Accuracy
            // 
            this.Label_Accuracy.AutoSize = true;
            this.Label_Accuracy.Location = new System.Drawing.Point(12, 177);
            this.Label_Accuracy.Name = "Label_Accuracy";
            this.Label_Accuracy.Size = new System.Drawing.Size(101, 20);
            this.Label_Accuracy.TabIndex = 8;
            this.Label_Accuracy.Text = "Кол-во узлов";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 324);
            this.Controls.Add(this.Label_Accuracy);
            this.Controls.Add(this.ComboBox_Accuracy);
            this.Controls.Add(this.TextBox_Result);
            this.Controls.Add(this.TextBox_Function);
            this.Controls.Add(this.TextBox_B);
            this.Controls.Add(this.TextBox_A);
            this.Controls.Add(this.Button_Calculate);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button Button_Calculate;
    private TextBox TextBox_A;
    private TextBox TextBox_B;
    private TextBox TextBox_Function;
    private PictureBox pictureBox1;
    private TextBox TextBox_Result;
    private ComboBox ComboBox_Accuracy;
    private Label Label_Accuracy;
}
