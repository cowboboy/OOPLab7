namespace OOPLab4._1
{
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
            this.PaintBox = new System.Windows.Forms.Panel();
            this.checkBoxCtrl = new System.Windows.Forms.CheckBox();
            this.checkBoxCollision = new System.Windows.Forms.CheckBox();
            this.setFigure = new System.Windows.Forms.ComboBox();
            this.setColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PaintBox
            // 
            this.PaintBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PaintBox.AutoSize = true;
            this.PaintBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PaintBox.Location = new System.Drawing.Point(74, 82);
            this.PaintBox.Name = "PaintBox";
            this.PaintBox.Size = new System.Drawing.Size(734, 393);
            this.PaintBox.TabIndex = 0;
            this.PaintBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBox_Paint);
            this.PaintBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PaintBox_MouseClick);
            this.PaintBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PaintBox_MouseMove);
            this.PaintBox.Resize += new System.EventHandler(this.PaintBox_Resize);
            // 
            // checkBoxCtrl
            // 
            this.checkBoxCtrl.AutoSize = true;
            this.checkBoxCtrl.Location = new System.Drawing.Point(79, 32);
            this.checkBoxCtrl.Name = "checkBoxCtrl";
            this.checkBoxCtrl.Size = new System.Drawing.Size(102, 24);
            this.checkBoxCtrl.TabIndex = 1;
            this.checkBoxCtrl.Text = "Ctrl Button";
            this.checkBoxCtrl.UseVisualStyleBackColor = true;
            this.checkBoxCtrl.CheckedChanged += new System.EventHandler(this.checkBoxCtrl_CheckedChanged);
            // 
            // checkBoxCollision
            // 
            this.checkBoxCollision.AutoSize = true;
            this.checkBoxCollision.Location = new System.Drawing.Point(187, 32);
            this.checkBoxCollision.Name = "checkBoxCollision";
            this.checkBoxCollision.Size = new System.Drawing.Size(145, 24);
            this.checkBoxCollision.TabIndex = 2;
            this.checkBoxCollision.Text = "Multiple collision";
            this.checkBoxCollision.UseVisualStyleBackColor = true;
            this.checkBoxCollision.CheckedChanged += new System.EventHandler(this.checkBoxCollision_CheckedChanged);
            // 
            // setFigure
            // 
            this.setFigure.FormattingEnabled = true;
            this.setFigure.Location = new System.Drawing.Point(550, 16);
            this.setFigure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.setFigure.Name = "setFigure";
            this.setFigure.Size = new System.Drawing.Size(138, 28);
            this.setFigure.TabIndex = 3;
            this.setFigure.SelectedIndexChanged += new System.EventHandler(this.setFigure_SelectedIndexChanged);
            // 
            // setColor
            // 
            this.setColor.FormattingEnabled = true;
            this.setColor.Location = new System.Drawing.Point(393, 16);
            this.setColor.Name = "setColor";
            this.setColor.Size = new System.Drawing.Size(151, 28);
            this.setColor.TabIndex = 4;
            this.setColor.SelectedIndexChanged += new System.EventHandler(this.setColor_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(756, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 517);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setColor);
            this.Controls.Add(this.setFigure);
            this.Controls.Add(this.checkBoxCollision);
            this.Controls.Add(this.checkBoxCtrl);
            this.Controls.Add(this.PaintBox);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel PaintBox;
        private CheckBox checkBoxCtrl;
        private CheckBox checkBoxCollision;
        private ComboBox setFigure;
        private ComboBox setColor;
        private Label label1;
    }
}