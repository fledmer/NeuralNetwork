
namespace WindowsFormsApp5
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Logs = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.structBox = new System.Windows.Forms.TextBox();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.eBox = new System.Windows.Forms.TextBox();
            this.aBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.inBox = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.outBox = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.pathBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.outLabel = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(274, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(102, 20);
            this.textBox1.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(274, 204);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(274, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(274, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Logs
            // 
            this.Logs.AutoSize = true;
            this.Logs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Logs.Location = new System.Drawing.Point(810, 12);
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(0, 20);
            this.Logs.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(527, 127);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Обучение";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // structBox
            // 
            this.structBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.structBox.Location = new System.Drawing.Point(527, 83);
            this.structBox.Name = "structBox";
            this.structBox.Size = new System.Drawing.Size(231, 38);
            this.structBox.TabIndex = 8;
            this.structBox.Text = "4096 256 64 26";
            // 
            // pathBox
            // 
            this.pathBox.Location = new System.Drawing.Point(527, 156);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(100, 20);
            this.pathBox.TabIndex = 9;
            this.pathBox.Text = "test.txt";
            // 
            // eBox
            // 
            this.eBox.Location = new System.Drawing.Point(527, 28);
            this.eBox.Name = "eBox";
            this.eBox.Size = new System.Drawing.Size(60, 20);
            this.eBox.TabIndex = 10;
            this.eBox.Text = "0,7";
            // 
            // aBox
            // 
            this.aBox.Location = new System.Drawing.Point(628, 28);
            this.aBox.Name = "aBox";
            this.aBox.Size = new System.Drawing.Size(60, 20);
            this.aBox.TabIndex = 11;
            this.aBox.Text = "0,3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(494, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "E:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(605, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "A:";
            // 
            // inBox
            // 
            this.inBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inBox.Location = new System.Drawing.Point(527, 302);
            this.inBox.Name = "inBox";
            this.inBox.Size = new System.Drawing.Size(231, 31);
            this.inBox.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(527, 339);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(231, 39);
            this.button5.TabIndex = 15;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // outBox
            // 
            this.outBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outBox.Location = new System.Drawing.Point(527, 384);
            this.outBox.Name = "outBox";
            this.outBox.Size = new System.Drawing.Size(231, 31);
            this.outBox.TabIndex = 16;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(527, 182);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 23);
            this.button6.TabIndex = 17;
            this.button6.Text = "Загрузка весов";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // pathBox2
            // 
            this.pathBox2.Location = new System.Drawing.Point(527, 211);
            this.pathBox2.Name = "pathBox2";
            this.pathBox2.Size = new System.Drawing.Size(100, 20);
            this.pathBox2.TabIndex = 18;
            this.pathBox2.Text = "weights.txt";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(649, 127);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(161, 23);
            this.button7.TabIndex = 19;
            this.button7.Text = "Создать нейросеть";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // outLabel
            // 
            this.outLabel.AutoSize = true;
            this.outLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outLabel.Location = new System.Drawing.Point(387, 14);
            this.outLabel.Name = "outLabel";
            this.outLabel.Size = new System.Drawing.Size(0, 24);
            this.outLabel.TabIndex = 20;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(15, 275);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(253, 23);
            this.button8.TabIndex = 21;
            this.button8.Text = "USE";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1316, 700);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.outLabel);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.pathBox2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.outBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.inBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.aBox);
            this.Controls.Add(this.eBox);
            this.Controls.Add(this.pathBox);
            this.Controls.Add(this.structBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label Logs;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox structBox;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.TextBox eBox;
        private System.Windows.Forms.TextBox aBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox outBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox pathBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label outLabel;
        private System.Windows.Forms.Button button8;
    }
}

