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
            this.ResultLabel = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
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
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(271, 192);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(0, 13);
            this.ResultLabel.TabIndex = 3;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 10;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.ResultLabel);
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
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Timer DisplayTimer;
    }
}

