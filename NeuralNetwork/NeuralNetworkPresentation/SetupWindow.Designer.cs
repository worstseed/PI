namespace NeuralNetworkPresentation
{
    sealed partial class SetupWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xPositionTextBox = new System.Windows.Forms.TextBox();
            this.yPositionTextBox = new System.Windows.Forms.TextBox();
            this.numberOfEpochsTextBox = new System.Windows.Forms.TextBox();
            this.numberOfExpedicionsTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numberOfTestingStepsTextBox = new System.Windows.Forms.TextBox();
            this.numberOfExploringStepsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.startSimulationButton = new System.Windows.Forms.Button();
            this.batteryMaxCapacityTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.setHorizontalObstacleCheckBox = new System.Windows.Forms.CheckBox();
            this.setVerticalObstacleCheckBox = new System.Windows.Forms.CheckBox();
            this.setRandomObstacleCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set simulation parameters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(44, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start position:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(77, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Horizontal:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(77, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Vertical: ";
            // 
            // xPositionTextBox
            // 
            this.xPositionTextBox.Location = new System.Drawing.Point(143, 76);
            this.xPositionTextBox.Name = "xPositionTextBox";
            this.xPositionTextBox.Size = new System.Drawing.Size(68, 20);
            this.xPositionTextBox.TabIndex = 4;
            // 
            // yPositionTextBox
            // 
            this.yPositionTextBox.Location = new System.Drawing.Point(143, 105);
            this.yPositionTextBox.Name = "yPositionTextBox";
            this.yPositionTextBox.Size = new System.Drawing.Size(68, 20);
            this.yPositionTextBox.TabIndex = 5;
            // 
            // numberOfEpochsTextBox
            // 
            this.numberOfEpochsTextBox.Location = new System.Drawing.Point(397, 105);
            this.numberOfEpochsTextBox.Name = "numberOfEpochsTextBox";
            this.numberOfEpochsTextBox.Size = new System.Drawing.Size(68, 20);
            this.numberOfEpochsTextBox.TabIndex = 10;
            // 
            // numberOfExpedicionsTextBox
            // 
            this.numberOfExpedicionsTextBox.Location = new System.Drawing.Point(397, 79);
            this.numberOfExpedicionsTextBox.Name = "numberOfExpedicionsTextBox";
            this.numberOfExpedicionsTextBox.Size = new System.Drawing.Size(68, 20);
            this.numberOfExpedicionsTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(259, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Number of epochs:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(259, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Number of expedicions:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(226, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "Teaching parameters:";
            // 
            // numberOfTestingStepsTextBox
            // 
            this.numberOfTestingStepsTextBox.Location = new System.Drawing.Point(143, 200);
            this.numberOfTestingStepsTextBox.Name = "numberOfTestingStepsTextBox";
            this.numberOfTestingStepsTextBox.Size = new System.Drawing.Size(68, 20);
            this.numberOfTestingStepsTextBox.TabIndex = 15;
            // 
            // numberOfExploringStepsTextBox
            // 
            this.numberOfExploringStepsTextBox.Location = new System.Drawing.Point(143, 171);
            this.numberOfExploringStepsTextBox.Name = "numberOfExploringStepsTextBox";
            this.numberOfExploringStepsTextBox.Size = new System.Drawing.Size(68, 20);
            this.numberOfExploringStepsTextBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(77, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Testing: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(77, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "Exploring:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(44, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 19);
            this.label10.TabIndex = 11;
            this.label10.Text = "Number of steps:";
            // 
            // startSimulationButton
            // 
            this.startSimulationButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.startSimulationButton.Location = new System.Drawing.Point(279, 247);
            this.startSimulationButton.Name = "startSimulationButton";
            this.startSimulationButton.Size = new System.Drawing.Size(130, 45);
            this.startSimulationButton.TabIndex = 16;
            this.startSimulationButton.Text = "Simulate!";
            this.startSimulationButton.UseVisualStyleBackColor = true;
            this.startSimulationButton.Click += new System.EventHandler(this.startSimulationButton_Click);
            // 
            // batteryMaxCapacityTextBox
            // 
            this.batteryMaxCapacityTextBox.Location = new System.Drawing.Point(397, 171);
            this.batteryMaxCapacityTextBox.Name = "batteryMaxCapacityTextBox";
            this.batteryMaxCapacityTextBox.Size = new System.Drawing.Size(68, 20);
            this.batteryMaxCapacityTextBox.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(259, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "Battery max capacity:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(226, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 19);
            this.label13.TabIndex = 17;
            this.label13.Text = "Robot parameters:";
            // 
            // setHorizontalObstacleCheckBox
            // 
            this.setHorizontalObstacleCheckBox.AutoSize = true;
            this.setHorizontalObstacleCheckBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setHorizontalObstacleCheckBox.Location = new System.Drawing.Point(80, 278);
            this.setHorizontalObstacleCheckBox.Name = "setHorizontalObstacleCheckBox";
            this.setHorizontalObstacleCheckBox.Size = new System.Drawing.Size(104, 19);
            this.setHorizontalObstacleCheckBox.TabIndex = 21;
            this.setHorizontalObstacleCheckBox.Text = "One horizontal";
            this.setHorizontalObstacleCheckBox.UseVisualStyleBackColor = true;
            // 
            // setVerticalObstacleCheckBox
            // 
            this.setVerticalObstacleCheckBox.AutoSize = true;
            this.setVerticalObstacleCheckBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setVerticalObstacleCheckBox.Location = new System.Drawing.Point(80, 303);
            this.setVerticalObstacleCheckBox.Name = "setVerticalObstacleCheckBox";
            this.setVerticalObstacleCheckBox.Size = new System.Drawing.Size(90, 19);
            this.setVerticalObstacleCheckBox.TabIndex = 22;
            this.setVerticalObstacleCheckBox.Text = "One vertical";
            this.setVerticalObstacleCheckBox.UseVisualStyleBackColor = true;
            // 
            // setRandomObstacleCheckBox
            // 
            this.setRandomObstacleCheckBox.AutoSize = true;
            this.setRandomObstacleCheckBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setRandomObstacleCheckBox.Location = new System.Drawing.Point(80, 328);
            this.setRandomObstacleCheckBox.Name = "setRandomObstacleCheckBox";
            this.setRandomObstacleCheckBox.Size = new System.Drawing.Size(70, 19);
            this.setRandomObstacleCheckBox.TabIndex = 23;
            this.setRandomObstacleCheckBox.Text = "Random";
            this.setRandomObstacleCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(44, 247);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 19);
            this.label11.TabIndex = 25;
            this.label11.Text = "Type of obstacles:";
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cancelButton.Location = new System.Drawing.Point(279, 298);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(130, 45);
            this.cancelButton.TabIndex = 26;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SetupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.setRandomObstacleCheckBox);
            this.Controls.Add(this.setVerticalObstacleCheckBox);
            this.Controls.Add(this.setHorizontalObstacleCheckBox);
            this.Controls.Add(this.batteryMaxCapacityTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.startSimulationButton);
            this.Controls.Add(this.numberOfTestingStepsTextBox);
            this.Controls.Add(this.numberOfExploringStepsTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numberOfEpochsTextBox);
            this.Controls.Add(this.numberOfExpedicionsTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.yPositionTextBox);
            this.Controls.Add(this.xPositionTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupWindow";
            this.Text = "SetupWindow";
            this.Load += new System.EventHandler(this.SetupWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox xPositionTextBox;
        private System.Windows.Forms.TextBox yPositionTextBox;
        private System.Windows.Forms.TextBox numberOfEpochsTextBox;
        private System.Windows.Forms.TextBox numberOfExpedicionsTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox numberOfTestingStepsTextBox;
        private System.Windows.Forms.TextBox numberOfExploringStepsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button startSimulationButton;
        private System.Windows.Forms.TextBox batteryMaxCapacityTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox setHorizontalObstacleCheckBox;
        private System.Windows.Forms.CheckBox setVerticalObstacleCheckBox;
        private System.Windows.Forms.CheckBox setRandomObstacleCheckBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cancelButton;
    }
}