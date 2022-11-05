
namespace Integral
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbB = new System.Windows.Forms.TextBox();
            this.tbA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.zgIntegral = new ZedGraph.ZedGraphControl();
            this.cbVariants = new System.Windows.Forms.ComboBox();
            this.tbRes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbErmit = new System.Windows.Forms.RadioButton();
            this.rbLagrange = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(324, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Рассчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(56, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tbB
            // 
            this.tbB.Location = new System.Drawing.Point(103, 14);
            this.tbB.Name = "tbB";
            this.tbB.Size = new System.Drawing.Size(79, 20);
            this.tbB.TabIndex = 2;
            this.tbB.Text = "1";
            // 
            // tbA
            // 
            this.tbA.Location = new System.Drawing.Point(103, 182);
            this.tbA.Name = "tbA";
            this.tbA.Size = new System.Drawing.Size(79, 20);
            this.tbA.TabIndex = 2;
            this.tbA.Text = "-1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(68, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "b =";
            // 
            // tbN
            // 
            this.tbN.Location = new System.Drawing.Point(149, 238);
            this.tbN.Name = "tbN";
            this.tbN.Size = new System.Drawing.Size(79, 20);
            this.tbN.TabIndex = 2;
            this.tbN.Text = "4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(18, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "количество узлов";
            // 
            // zgIntegral
            // 
            this.zgIntegral.Dock = System.Windows.Forms.DockStyle.Right;
            this.zgIntegral.Location = new System.Drawing.Point(415, 0);
            this.zgIntegral.Name = "zgIntegral";
            this.zgIntegral.ScrollGrace = 0D;
            this.zgIntegral.ScrollMaxX = 0D;
            this.zgIntegral.ScrollMaxY = 0D;
            this.zgIntegral.ScrollMaxY2 = 0D;
            this.zgIntegral.ScrollMinX = 0D;
            this.zgIntegral.ScrollMinY = 0D;
            this.zgIntegral.ScrollMinY2 = 0D;
            this.zgIntegral.Size = new System.Drawing.Size(501, 463);
            this.zgIntegral.TabIndex = 5;
            this.zgIntegral.UseExtendedPrintDialog = true;
            // 
            // cbVariants
            // 
            this.cbVariants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVariants.FormattingEnabled = true;
            this.cbVariants.Items.AddRange(new object[] {
            "x^2",
            "Sin(x)",
            "x^2 + 2x - 5",
            "8x - 3"});
            this.cbVariants.Location = new System.Drawing.Point(197, 98);
            this.cbVariants.Name = "cbVariants";
            this.cbVariants.Size = new System.Drawing.Size(121, 21);
            this.cbVariants.TabIndex = 6;
            // 
            // tbRes
            // 
            this.tbRes.BackColor = System.Drawing.SystemColors.Info;
            this.tbRes.Location = new System.Drawing.Point(121, 280);
            this.tbRes.Name = "tbRes";
            this.tbRes.ReadOnly = true;
            this.tbRes.Size = new System.Drawing.Size(107, 20);
            this.tbRes.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(20, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Результат";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "I = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(68, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "a =";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLagrange);
            this.groupBox1.Controls.Add(this.rbErmit);
            this.groupBox1.Location = new System.Drawing.Point(23, 320);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 69);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Метод";
            // 
            // rbErmit
            // 
            this.rbErmit.AutoSize = true;
            this.rbErmit.Checked = true;
            this.rbErmit.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbErmit.Location = new System.Drawing.Point(3, 16);
            this.rbErmit.Name = "rbErmit";
            this.rbErmit.Size = new System.Drawing.Size(220, 17);
            this.rbErmit.TabIndex = 0;
            this.rbErmit.TabStop = true;
            this.rbErmit.Text = "Гаусса-Эрмита";
            this.rbErmit.UseVisualStyleBackColor = true;
            // 
            // rbLagrange
            // 
            this.rbLagrange.AutoSize = true;
            this.rbLagrange.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbLagrange.Location = new System.Drawing.Point(3, 33);
            this.rbLagrange.Name = "rbLagrange";
            this.rbLagrange.Size = new System.Drawing.Size(220, 17);
            this.rbLagrange.TabIndex = 1;
            this.rbLagrange.TabStop = true;
            this.rbLagrange.Text = "Гаусса-Лагранжа";
            this.rbLagrange.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 463);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbRes);
            this.Controls.Add(this.cbVariants);
            this.Controls.Add(this.zgIntegral);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbN);
            this.Controls.Add(this.tbA);
            this.Controls.Add(this.tbB);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Интеграл";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbB;
        private System.Windows.Forms.TextBox tbA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbN;
        private System.Windows.Forms.Label label3;
        private ZedGraph.ZedGraphControl zgIntegral;
        private System.Windows.Forms.ComboBox cbVariants;
        private System.Windows.Forms.TextBox tbRes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbErmit;
        private System.Windows.Forms.RadioButton rbLagrange;
    }
}

