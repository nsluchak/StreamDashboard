namespace StreamApp
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            this.LEDProjector = new System.Windows.Forms.PictureBox();
            this.AudioInputSelector = new System.Windows.Forms.ListBox();
            this.ListenButton = new System.Windows.Forms.Button();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.useDBCheckbox = new System.Windows.Forms.CheckBox();
            this.ceilingInput = new System.Windows.Forms.TextBox();
            this.peakInput = new System.Windows.Forms.TextBox();
            this.ceilingLabel = new System.Windows.Forms.Label();
            this.activatedThresholdLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.grayscaleCheckBox = new System.Windows.Forms.CheckBox();
            this.listeningThresholdInput = new System.Windows.Forms.TextBox();
            this.listeningThresholdLabel = new System.Windows.Forms.Label();
            this.beatSensitivityInput = new System.Windows.Forms.TextBox();
            this.beatSensitivityLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LEDProjector)).BeginInit();
            this.SuspendLayout();
            // 
            // LEDProjector
            // 
            this.LEDProjector.Location = new System.Drawing.Point(100, 100);
            this.LEDProjector.Name = "LEDProjector";
            this.LEDProjector.Size = new System.Drawing.Size(600, 50);
            this.LEDProjector.TabIndex = 0;
            this.LEDProjector.TabStop = false;
            // 
            // AudioInputSelector
            // 
            this.AudioInputSelector.FormattingEnabled = true;
            this.AudioInputSelector.Location = new System.Drawing.Point(282, 12);
            this.AudioInputSelector.Name = "AudioInputSelector";
            this.AudioInputSelector.Size = new System.Drawing.Size(234, 82);
            this.AudioInputSelector.TabIndex = 1;
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(522, 43);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(75, 23);
            this.ListenButton.TabIndex = 2;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 10;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // useDBCheckbox
            // 
            this.useDBCheckbox.AutoSize = true;
            this.useDBCheckbox.Location = new System.Drawing.Point(153, 180);
            this.useDBCheckbox.Name = "useDBCheckbox";
            this.useDBCheckbox.Size = new System.Drawing.Size(61, 17);
            this.useDBCheckbox.TabIndex = 3;
            this.useDBCheckbox.Text = "Use dB";
            this.useDBCheckbox.UseVisualStyleBackColor = true;
            this.useDBCheckbox.CheckedChanged += new System.EventHandler(this.useDBCheckbox_CheckedChanged);
            // 
            // ceilingInput
            // 
            this.ceilingInput.Location = new System.Drawing.Point(153, 203);
            this.ceilingInput.Name = "ceilingInput";
            this.ceilingInput.Size = new System.Drawing.Size(100, 20);
            this.ceilingInput.TabIndex = 4;
            this.ceilingInput.Text = "5000";
            this.ceilingInput.TextChanged += new System.EventHandler(this.ceilingInputBox_TextChanged);
            // 
            // peakInput
            // 
            this.peakInput.Location = new System.Drawing.Point(153, 229);
            this.peakInput.Name = "peakInput";
            this.peakInput.Size = new System.Drawing.Size(100, 20);
            this.peakInput.TabIndex = 5;
            this.peakInput.Text = "5000";
            this.peakInput.TextChanged += new System.EventHandler(this.peakInputBox_TextChanged);
            // 
            // ceilingLabel
            // 
            this.ceilingLabel.AutoSize = true;
            this.ceilingLabel.Location = new System.Drawing.Point(259, 206);
            this.ceilingLabel.Name = "ceilingLabel";
            this.ceilingLabel.Size = new System.Drawing.Size(91, 13);
            this.ceilingLabel.TabIndex = 6;
            this.ceilingLabel.Text = "Ceiling Magnitude";
            // 
            // activatedThresholdLabel
            // 
            this.activatedThresholdLabel.AutoSize = true;
            this.activatedThresholdLabel.Location = new System.Drawing.Point(259, 232);
            this.activatedThresholdLabel.Name = "activatedThresholdLabel";
            this.activatedThresholdLabel.Size = new System.Drawing.Size(104, 13);
            this.activatedThresholdLabel.TabIndex = 7;
            this.activatedThresholdLabel.Text = "Activation Threshold";
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            // 
            // grayscaleCheckBox
            // 
            this.grayscaleCheckBox.AutoSize = true;
            this.grayscaleCheckBox.Location = new System.Drawing.Point(153, 157);
            this.grayscaleCheckBox.Name = "grayscaleCheckBox";
            this.grayscaleCheckBox.Size = new System.Drawing.Size(73, 17);
            this.grayscaleCheckBox.TabIndex = 8;
            this.grayscaleCheckBox.Text = "Grayscale";
            this.grayscaleCheckBox.UseVisualStyleBackColor = true;
            this.grayscaleCheckBox.CheckedChanged += new System.EventHandler(this.grayscaleCheckBox_CheckedChanged);
            // 
            // listeningThresholdInput
            // 
            this.listeningThresholdInput.Location = new System.Drawing.Point(376, 203);
            this.listeningThresholdInput.Name = "listeningThresholdInput";
            this.listeningThresholdInput.Size = new System.Drawing.Size(100, 20);
            this.listeningThresholdInput.TabIndex = 9;
            this.listeningThresholdInput.Text = "0";
            this.listeningThresholdInput.TextChanged += new System.EventHandler(this.listeningThresholdInput_TextChanged);
            // 
            // listeningThresholdLabel
            // 
            this.listeningThresholdLabel.AutoSize = true;
            this.listeningThresholdLabel.Location = new System.Drawing.Point(482, 206);
            this.listeningThresholdLabel.Name = "listeningThresholdLabel";
            this.listeningThresholdLabel.Size = new System.Drawing.Size(99, 13);
            this.listeningThresholdLabel.TabIndex = 10;
            this.listeningThresholdLabel.Text = "Listening Threshold";
            // 
            // beatSensitivityInput
            // 
            this.beatSensitivityInput.Location = new System.Drawing.Point(376, 229);
            this.beatSensitivityInput.Name = "beatSensitivityInput";
            this.beatSensitivityInput.Size = new System.Drawing.Size(100, 20);
            this.beatSensitivityInput.TabIndex = 11;
            this.beatSensitivityInput.Text = "1.35";
            this.beatSensitivityInput.TextChanged += new System.EventHandler(this.beatSensitivityInput_TextChanged);
            // 
            // beatSensitivityLabel
            // 
            this.beatSensitivityLabel.AutoSize = true;
            this.beatSensitivityLabel.Location = new System.Drawing.Point(482, 232);
            this.beatSensitivityLabel.Name = "beatSensitivityLabel";
            this.beatSensitivityLabel.Size = new System.Drawing.Size(79, 13);
            this.beatSensitivityLabel.TabIndex = 12;
            this.beatSensitivityLabel.Text = "Beat Sensitivity";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.beatSensitivityLabel);
            this.Controls.Add(this.beatSensitivityInput);
            this.Controls.Add(this.listeningThresholdLabel);
            this.Controls.Add(this.listeningThresholdInput);
            this.Controls.Add(this.grayscaleCheckBox);
            this.Controls.Add(this.activatedThresholdLabel);
            this.Controls.Add(this.ceilingLabel);
            this.Controls.Add(this.peakInput);
            this.Controls.Add(this.ceilingInput);
            this.Controls.Add(this.useDBCheckbox);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.AudioInputSelector);
            this.Controls.Add(this.LEDProjector);
            this.Name = "Dashboard";
            this.Text = "RAVEGOD99 Stream Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LEDProjector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LEDProjector;
        private System.Windows.Forms.ListBox AudioInputSelector;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Timer DisplayTimer;
        private System.Windows.Forms.CheckBox useDBCheckbox;
        private System.Windows.Forms.TextBox ceilingInput;
        private System.Windows.Forms.TextBox peakInput;
        private System.Windows.Forms.Label ceilingLabel;
        private System.Windows.Forms.Label activatedThresholdLabel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckBox grayscaleCheckBox;
        private System.Windows.Forms.TextBox listeningThresholdInput;
        private System.Windows.Forms.Label listeningThresholdLabel;
        private System.Windows.Forms.TextBox beatSensitivityInput;
        private System.Windows.Forms.Label beatSensitivityLabel;
    }
}

