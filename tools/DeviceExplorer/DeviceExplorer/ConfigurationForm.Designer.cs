namespace DeviceExplorer
{
    partial class ConfigurationForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deviceContentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.configurationComboBox = new System.Windows.Forms.ComboBox();
            this.addDeviceContentBtn = new System.Windows.Forms.Button();
            this.deleteConfigurationBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.targetConditionTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.configurationLabelComboBox = new System.Windows.Forms.ComboBox();
            this.configurationLabelValueTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Device Twin Content";
            // 
            // deviceContentRichTextBox
            // 
            this.deviceContentRichTextBox.Location = new System.Drawing.Point(48, 143);
            this.deviceContentRichTextBox.Name = "deviceContentRichTextBox";
            this.deviceContentRichTextBox.Size = new System.Drawing.Size(713, 258);
            this.deviceContentRichTextBox.TabIndex = 1;
            this.deviceContentRichTextBox.Text = "{\n    \"properties.desired.new_key2\": \"{\\\"properties.desired.new_key\\\":{\\\"new_key_" +
    "value\\\":\\\"value_1\\\"}}\"\n}";
            // 
            // configurationComboBox
            // 
            this.configurationComboBox.FormattingEnabled = true;
            this.configurationComboBox.Location = new System.Drawing.Point(131, 22);
            this.configurationComboBox.Name = "configurationComboBox";
            this.configurationComboBox.Size = new System.Drawing.Size(630, 21);
            this.configurationComboBox.TabIndex = 2;
            this.configurationComboBox.SelectedIndexChanged += new System.EventHandler(this.configurationCombo_changed);
            // 
            // addDeviceContentBtn
            // 
            this.addDeviceContentBtn.Location = new System.Drawing.Point(48, 415);
            this.addDeviceContentBtn.Name = "addDeviceContentBtn";
            this.addDeviceContentBtn.Size = new System.Drawing.Size(123, 23);
            this.addDeviceContentBtn.TabIndex = 3;
            this.addDeviceContentBtn.Text = "Add Configuration";
            this.addDeviceContentBtn.UseVisualStyleBackColor = true;
            this.addDeviceContentBtn.Click += new System.EventHandler(this.addDeviceContentBtn_Click);
            // 
            // deleteConfigurationBtn
            // 
            this.deleteConfigurationBtn.Location = new System.Drawing.Point(212, 415);
            this.deleteConfigurationBtn.Name = "deleteConfigurationBtn";
            this.deleteConfigurationBtn.Size = new System.Drawing.Size(123, 23);
            this.deleteConfigurationBtn.TabIndex = 3;
            this.deleteConfigurationBtn.Text = "Delete Configuration";
            this.deleteConfigurationBtn.UseVisualStyleBackColor = true;
            this.deleteConfigurationBtn.Click += new System.EventHandler(this.deleteDeviceContentBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target Condition";
            // 
            // targetConditionTextBox
            // 
            this.targetConditionTextBox.Location = new System.Drawing.Point(131, 61);
            this.targetConditionTextBox.Name = "targetConditionTextBox";
            this.targetConditionTextBox.Size = new System.Drawing.Size(630, 20);
            this.targetConditionTextBox.TabIndex = 5;
            this.targetConditionTextBox.Text = "tags.environment = \"this is not required\"";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Configuration Labels";
            // 
            // configurationLabelComboBox
            // 
            this.configurationLabelComboBox.FormattingEnabled = true;
            this.configurationLabelComboBox.Location = new System.Drawing.Point(172, 96);
            this.configurationLabelComboBox.Name = "configurationLabelComboBox";
            this.configurationLabelComboBox.Size = new System.Drawing.Size(258, 21);
            this.configurationLabelComboBox.TabIndex = 7;
            this.configurationLabelComboBox.SelectedIndexChanged += new System.EventHandler(this.configurationLabel_SelectIndxChange);
            // 
            // configurationLabelValueTextBox
            // 
            this.configurationLabelValueTextBox.Location = new System.Drawing.Point(437, 96);
            this.configurationLabelValueTextBox.Name = "configurationLabelValueTextBox";
            this.configurationLabelValueTextBox.Size = new System.Drawing.Size(324, 20);
            this.configurationLabelValueTextBox.TabIndex = 8;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.configurationLabelValueTextBox);
            this.Controls.Add(this.configurationLabelComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.targetConditionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deleteConfigurationBtn);
            this.Controls.Add(this.addDeviceContentBtn);
            this.Controls.Add(this.configurationComboBox);
            this.Controls.Add(this.deviceContentRichTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ConfigurationForm";
            this.Text = "Manage Device Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox deviceContentRichTextBox;
        private System.Windows.Forms.ComboBox configurationComboBox;
        private System.Windows.Forms.Button addDeviceContentBtn;
        private System.Windows.Forms.Button deleteConfigurationBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox targetConditionTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox configurationLabelComboBox;
        private System.Windows.Forms.TextBox configurationLabelValueTextBox;
    }
}