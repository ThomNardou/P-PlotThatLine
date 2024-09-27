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
            checkBoxLock = new CheckBox();
            countryCheckBox = new CheckedListBox();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(96, 115);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1015, 493);
            formsPlot1.TabIndex = 0;
            // 
            // fromText
            // 
            fromText.Location = new Point(96, 41);
            fromText.Name = "fromText";
            fromText.Size = new Size(100, 23);
            fromText.TabIndex = 1;
            fromText.KeyPress += checkOnlyNumbers;
            // 
            // toText
            // 
            toText.Location = new Point(220, 41);
            toText.Name = "toText";
            toText.Size = new Size(100, 23);
            toText.TabIndex = 2;
            toText.KeyPress += checkOnlyNumbers;
            // 
            // button1
            // 
            button1.Location = new Point(337, 34);
            button1.Name = "button1";
            button1.Size = new Size(75, 34);
            button1.TabIndex = 3;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // legends
            // 
            legends.Location = new Point(996, 30);
            legends.Name = "legends";
            legends.Size = new Size(115, 43);
            legends.TabIndex = 4;
            legends.Text = "Display Legends";
            legends.UseVisualStyleBackColor = true;
            legends.Click += legends_Click;
            // 
            // checkBoxLock
            // 
            checkBoxLock.AutoSize = true;
            checkBoxLock.Location = new Point(1028, 614);
            checkBoxLock.Name = "checkBoxLock";
            checkBoxLock.Size = new Size(51, 19);
            checkBoxLock.TabIndex = 5;
            checkBoxLock.Text = "Lock";
            checkBoxLock.UseVisualStyleBackColor = true;
            checkBoxLock.CheckedChanged += checkBoxLock_CheckedChanged;
            // 
            // countryCheckBox
            // 
            countryCheckBox.FormattingEnabled = true;
            countryCheckBox.Location = new Point(1117, 127);
            countryCheckBox.Name = "countryCheckBox";
            countryCheckBox.Size = new Size(120, 454);
            countryCheckBox.TabIndex = 6;
            countryCheckBox.ItemCheck += countryCheckBox_ItemCheck;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 662);
            Controls.Add(countryCheckBox);
            Controls.Add(checkBoxLock);
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
        private CheckBox checkBoxLock;
        private CheckedListBox countryCheckBox;
    }
}
