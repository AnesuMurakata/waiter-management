namespace INF2011S_Workshop8_WaS7_PIISOL.PresentationLayer
{
    partial class ShiftsForm
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
            this.shiftsMonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.calendarMessageLabel = new System.Windows.Forms.Label();
            this.waitersComboBox = new System.Windows.Forms.ComboBox();
            this.dayShiftsLabel = new System.Windows.Forms.Label();
            this.eveningShiftsLabel = new System.Windows.Forms.Label();
            this.dayKeyLabel = new System.Windows.Forms.Label();
            this.eveningKeyLabel = new System.Windows.Forms.Label();
            this.waitersLabel = new System.Windows.Forms.Label();
            this.eventsMessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // shiftsMonthCalendar
            // 
            this.shiftsMonthCalendar.Location = new System.Drawing.Point(30, 39);
            this.shiftsMonthCalendar.Name = "shiftsMonthCalendar";
            this.shiftsMonthCalendar.TabIndex = 0;
            // 
            // calendarMessageLabel
            // 
            this.calendarMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.calendarMessageLabel.Location = new System.Drawing.Point(30, 257);
            this.calendarMessageLabel.Name = "calendarMessageLabel";
            this.calendarMessageLabel.Size = new System.Drawing.Size(262, 38);
            this.calendarMessageLabel.TabIndex = 1;
            // 
            // waitersComboBox
            // 
            this.waitersComboBox.FormattingEnabled = true;
            this.waitersComboBox.Location = new System.Drawing.Point(726, 60);
            this.waitersComboBox.Name = "waitersComboBox";
            this.waitersComboBox.Size = new System.Drawing.Size(322, 24);
            this.waitersComboBox.TabIndex = 2;
            this.waitersComboBox.SelectedIndexChanged += new System.EventHandler(this.waitersComboBox_SelectedIndexChanged);
            // 
            // dayShiftsLabel
            // 
            this.dayShiftsLabel.BackColor = System.Drawing.Color.Transparent;
            this.dayShiftsLabel.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayShiftsLabel.Location = new System.Drawing.Point(722, 123);
            this.dayShiftsLabel.Name = "dayShiftsLabel";
            this.dayShiftsLabel.Size = new System.Drawing.Size(64, 28);
            this.dayShiftsLabel.TabIndex = 3;
            this.dayShiftsLabel.Text = "Day";
            // 
            // eveningShiftsLabel
            // 
            this.eveningShiftsLabel.BackColor = System.Drawing.Color.Transparent;
            this.eveningShiftsLabel.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eveningShiftsLabel.Location = new System.Drawing.Point(880, 122);
            this.eveningShiftsLabel.Name = "eveningShiftsLabel";
            this.eveningShiftsLabel.Size = new System.Drawing.Size(96, 28);
            this.eveningShiftsLabel.TabIndex = 4;
            this.eveningShiftsLabel.Text = "Evening";
            // 
            // dayKeyLabel
            // 
            this.dayKeyLabel.BackColor = System.Drawing.Color.Yellow;
            this.dayKeyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dayKeyLabel.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayKeyLabel.Location = new System.Drawing.Point(792, 123);
            this.dayKeyLabel.Name = "dayKeyLabel";
            this.dayKeyLabel.Size = new System.Drawing.Size(64, 28);
            this.dayKeyLabel.TabIndex = 5;
            // 
            // eveningKeyLabel
            // 
            this.eveningKeyLabel.BackColor = System.Drawing.Color.MediumAquamarine;
            this.eveningKeyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eveningKeyLabel.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eveningKeyLabel.Location = new System.Drawing.Point(984, 122);
            this.eveningKeyLabel.Name = "eveningKeyLabel";
            this.eveningKeyLabel.Size = new System.Drawing.Size(64, 28);
            this.eveningKeyLabel.TabIndex = 6;
            // 
            // waitersLabel
            // 
            this.waitersLabel.BackColor = System.Drawing.Color.Transparent;
            this.waitersLabel.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitersLabel.Location = new System.Drawing.Point(825, 18);
            this.waitersLabel.Name = "waitersLabel";
            this.waitersLabel.Size = new System.Drawing.Size(128, 28);
            this.waitersLabel.TabIndex = 7;
            this.waitersLabel.Text = "Waiters";
            // 
            // eventsMessageLabel
            // 
            this.eventsMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.eventsMessageLabel.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventsMessageLabel.Location = new System.Drawing.Point(393, 56);
            this.eventsMessageLabel.Name = "eventsMessageLabel";
            this.eventsMessageLabel.Size = new System.Drawing.Size(268, 94);
            this.eventsMessageLabel.TabIndex = 8;
            // 
            // ShiftsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 573);
            this.Controls.Add(this.eventsMessageLabel);
            this.Controls.Add(this.waitersLabel);
            this.Controls.Add(this.eveningKeyLabel);
            this.Controls.Add(this.dayKeyLabel);
            this.Controls.Add(this.eveningShiftsLabel);
            this.Controls.Add(this.dayShiftsLabel);
            this.Controls.Add(this.waitersComboBox);
            this.Controls.Add(this.calendarMessageLabel);
            this.Controls.Add(this.shiftsMonthCalendar);
            this.Name = "ShiftsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar shiftsMonthCalendar;
        private System.Windows.Forms.Label calendarMessageLabel;
        private System.Windows.Forms.ComboBox waitersComboBox;
        private System.Windows.Forms.Label dayShiftsLabel;
        private System.Windows.Forms.Label eveningShiftsLabel;
        private System.Windows.Forms.Label dayKeyLabel;
        private System.Windows.Forms.Label eveningKeyLabel;
        private System.Windows.Forms.Label waitersLabel;
        private System.Windows.Forms.Label eventsMessageLabel;
    }
}