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
            searchButton = new Button();
            legends = new Button();
            checkBoxLock = new CheckBox();
            countryCheckBox = new CheckedListBox();
            resetButton = new Button();
            settingButton = new Button();
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
            // searchButton
            // 
            searchButton.Location = new Point(337, 34);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(75, 34);
            searchButton.TabIndex = 3;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // legends
            // 
            legends.Location = new Point(978, 34);
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
            checkBoxLock.Location = new Point(1186, 612);
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
            // resetButton
            // 
            resetButton.Location = new Point(1117, 34);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(120, 43);
            resetButton.TabIndex = 7;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // settingButton
            // 
            settingButton.Location = new Point(1036, 608);
            settingButton.Name = "settingButton";
            settingButton.Size = new Size(75, 23);
            settingButton.TabIndex = 8;
            settingButton.Text = "Réglage";
            settingButton.UseVisualStyleBackColor = true;
            settingButton.Click += settingButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 662);
            Controls.Add(settingButton);
            Controls.Add(resetButton);
            Controls.Add(countryCheckBox);
            Controls.Add(checkBoxLock);
            Controls.Add(legends);
            Controls.Add(searchButton);
            Controls.Add(toText);
            Controls.Add(fromText);
            Controls.Add(formsPlot1);
            Name = "Form1";
            Text = "Form1";
            Resize += Form1_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private TextBox fromText;
        private TextBox toText;
        private Button searchButton;
        private Button legends;
        private CheckBox checkBoxLock;
        private CheckedListBox countryCheckBox;
        private Button resetButton;
        private Button settingButton;
    }
}
