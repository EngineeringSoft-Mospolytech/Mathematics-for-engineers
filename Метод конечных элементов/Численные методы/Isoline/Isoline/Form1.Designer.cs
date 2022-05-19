namespace Isoline
{
    partial class Isoline
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ContourIines = new System.Windows.Forms.TextBox();
            this.ComboBoxlines = new System.Windows.Forms.ComboBox();
            this.TextBoxFormula = new System.Windows.Forms.TextBox();
            this.ButtonApplication = new System.Windows.Forms.Button();
            this.ButtonMinus = new System.Windows.Forms.Button();
            this.ButtonPlus = new System.Windows.Forms.Button();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.ChartGraphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChartGraphic)).BeginInit();
            this.SuspendLayout();
            // 
            // ContourIines
            // 
            this.ContourIines.Location = new System.Drawing.Point(12, 50);
            this.ContourIines.Name = "ContourIines";
            this.ContourIines.Size = new System.Drawing.Size(52, 20);
            this.ContourIines.TabIndex = 0;
            this.ContourIines.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContourIines_KeyDown);
            // 
            // ComboBoxlines
            // 
            this.ComboBoxlines.FormattingEnabled = true;
            this.ComboBoxlines.Items.AddRange(new object[] {
            "none",
            "linear",
            "linear+spline"});
            this.ComboBoxlines.Location = new System.Drawing.Point(12, 233);
            this.ComboBoxlines.Name = "ComboBoxlines";
            this.ComboBoxlines.Size = new System.Drawing.Size(52, 21);
            this.ComboBoxlines.TabIndex = 3;
            this.ComboBoxlines.SelectedIndexChanged += new System.EventHandler(this.ComboBoxlines_SelectedIndexChanged);
            // 
            // TextBoxFormula
            // 
            this.TextBoxFormula.Location = new System.Drawing.Point(70, 11);
            this.TextBoxFormula.Name = "TextBoxFormula";
            this.TextBoxFormula.Size = new System.Drawing.Size(396, 20);
            this.TextBoxFormula.TabIndex = 4;
            this.TextBoxFormula.Text = "x * x + y * y";
            // 
            // ButtonApplication
            // 
            this.ButtonApplication.Location = new System.Drawing.Point(12, 11);
            this.ButtonApplication.Name = "ButtonApplication";
            this.ButtonApplication.Size = new System.Drawing.Size(52, 23);
            this.ButtonApplication.TabIndex = 5;
            this.ButtonApplication.Text = "Application";
            this.ButtonApplication.UseVisualStyleBackColor = true;
            this.ButtonApplication.Click += new System.EventHandler(this.ButtonApplication_Click);
            // 
            // ButtonMinus
            // 
            this.ButtonMinus.Location = new System.Drawing.Point(12, 384);
            this.ButtonMinus.Name = "ButtonMinus";
            this.ButtonMinus.Size = new System.Drawing.Size(52, 24);
            this.ButtonMinus.TabIndex = 7;
            this.ButtonMinus.Text = "-";
            this.ButtonMinus.UseVisualStyleBackColor = true;
            this.ButtonMinus.Click += new System.EventHandler(this.ButtonMinus_Click);
            // 
            // ButtonPlus
            // 
            this.ButtonPlus.Location = new System.Drawing.Point(12, 414);
            this.ButtonPlus.Name = "ButtonPlus";
            this.ButtonPlus.Size = new System.Drawing.Size(52, 24);
            this.ButtonPlus.TabIndex = 8;
            this.ButtonPlus.Text = "+";
            this.ButtonPlus.UseVisualStyleBackColor = true;
            this.ButtonPlus.Click += new System.EventHandler(this.ButtonPlus_Click);
            // 
            // ChartGraphic
            // 
            this.ChartGraphic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ChartGraphic.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.Shingle;
            this.ChartGraphic.CausesValidation = false;
            chartArea1.Name = "ChartArea1";
            this.ChartGraphic.ChartAreas.Add(chartArea1);
            this.ChartGraphic.IsSoftShadows = false;
            legend1.Name = "Legend1";
            this.ChartGraphic.Legends.Add(legend1);
            this.ChartGraphic.Location = new System.Drawing.Point(70, 37);
            this.ChartGraphic.Name = "ChartGraphic";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChartGraphic.Series.Add(series1);
            this.ChartGraphic.Size = new System.Drawing.Size(399, 401);
            this.ChartGraphic.TabIndex = 0;
            this.ChartGraphic.Text = "chart1";
            this.ChartGraphic.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.SystemDefault;
            this.ChartGraphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChartGraphic_MouseDown);
            this.ChartGraphic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChartGraphic_MouseMove);
            this.ChartGraphic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChartGraphic_MouseUp);
            // 
            // Isoline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(481, 450);
            this.Controls.Add(this.ChartGraphic);
            this.Controls.Add(this.ButtonPlus);
            this.Controls.Add(this.ButtonMinus);
            this.Controls.Add(this.ButtonApplication);
            this.Controls.Add(this.TextBoxFormula);
            this.Controls.Add(this.ComboBoxlines);
            this.Controls.Add(this.ContourIines);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Isoline";
            this.Text = "Isoline";
            ((System.ComponentModel.ISupportInitialize)(this.ChartGraphic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ContourIines;
        private System.Windows.Forms.ComboBox ComboBoxlines;
        private System.Windows.Forms.TextBox TextBoxFormula;
        private System.Windows.Forms.Button ButtonApplication;
        private System.Windows.Forms.Button ButtonMinus;
        private System.Windows.Forms.Button ButtonPlus;
        private System.Windows.Forms.ColorDialog ColorDialog;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartGraphic;
    }
}

