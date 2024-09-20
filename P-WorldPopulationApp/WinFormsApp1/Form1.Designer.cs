namespace WinFormsApp1
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
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            fromText = new TextBox();
            toText = new TextBox();
            button1 = new Button();
            legends = new Button();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(62, 93);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(669, 345);
            formsPlot1.TabIndex = 0;
            // 
            // fromText
            // 
            fromText.Location = new Point(40, 30);
            fromText.Name = "fromText";
            fromText.Size = new Size(100, 23);
            fromText.TabIndex = 1;
            // 
            // toText
            // 
            toText.Location = new Point(182, 30);
            toText.Name = "toText";
            toText.Size = new Size(100, 23);
            toText.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(319, 30);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // legends
            // 
            legends.Location = new Point(654, 20);
            legends.Name = "legends";
            legends.Size = new Size(115, 43);
            legends.TabIndex = 4;
            legends.Text = "Display Legends";
            legends.UseVisualStyleBackColor = true;
            legends.Click += legends_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(legends);
            Controls.Add(button1);
            Controls.Add(toText);
            Controls.Add(fromText);
            Controls.Add(formsPlot1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private TextBox fromText;
        private TextBox toText;
        private Button button1;
        private Button legends;
    }
}
