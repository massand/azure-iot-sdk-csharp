using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;

namespace DeviceExplorer
{
    public partial class ConfigurationForm : Form
    {
        private RegistryManager registryManager;
        private Dictionary<string, Configuration> configurationsMap;
        private Dictionary<string, string> configurationLabelsMap;
        private IEnumerable<Configuration> configurations;
        private Configuration currentConfiguration;

        public ConfigurationForm(RegistryManager registryManager)
        {
            InitializeComponent();

            this.registryManager = registryManager;

            initControlsAsync();
        }

        private async Task initControlsAsync()
        {
            configurationsMap = new Dictionary<string, Configuration>();
            configurationLabelsMap = new Dictionary<string, string>();

            try
            {
                configurations = await registryManager.GetConfigurationsAsync(5);
                foreach(var configuration in configurations)
                {
                    configurationsMap.Add(configuration.Id, configuration);
                }

                this.configurationComboBox.DataSource = configurationsMap.Keys.ToList();

                if (this.configurationComboBox.Items.Count > 0)
                {
                    updateTextBoxes();
                }

            }catch(Exception ex)
            {
                using (new CenterDialog(this))
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void updateTextBoxes()
        {
            configurationLabelsMap.Clear();
            configurationLabelComboBox.Text = "";
            configurationLabelValueTextBox.Clear();
            targetConditionTextBox.Clear();
            deviceContentRichTextBox.Clear();

            Configuration configuration = configurationsMap[configurationComboBox.SelectedItem.ToString()];
            this.currentConfiguration = configuration;

            deviceContentRichTextBox.Text = JsonConvert.SerializeObject(configuration.Content.DeviceContent);
            targetConditionTextBox.Text = configuration.TargetCondition;

            //TODO: Can this be done using LINQ?
            if (configuration.Labels != null)
            {
                foreach (var label in configuration.Labels)
                {
                    configurationLabelsMap.Add(label.Key, label.Value);
                }
            }

            configurationLabelComboBox.DataSource = configurationLabelsMap.Keys.ToList();
        }

        private async void addNewDeviceConfiguration(string json)
        {
            if (configurationComboBox.Text.Length == 0) configurationComboBox.Text = Guid.NewGuid().ToString();

            Configuration configuration = new Configuration(configurationComboBox.Text);
            configuration.TargetCondition = targetConditionTextBox.Text;
            configuration.Content = new ConfigurationContent();
            configuration.Content.DeviceContent = new Dictionary<string, object>();
            //configuration.Content.DeviceContent["properties.desired2.deviceContent_key"] = deviceContentRichTextBox.Text; //this works
            configuration.Content.DeviceContent["properties.desired.deviceContent_key"] = deviceContentRichTextBox.Text; //this works
            //configuration.Content.DeviceContent["properties.2.deviceContent_key"] = deviceContentRichTextBox.Text; //this doesn't work
            configuration = await registryManager.AddConfigurationAsync(configuration);
        }

        private void addDeviceContentBtn_Click(object sender, EventArgs e)
        {
            addNewDeviceConfiguration(deviceContentRichTextBox.Text);
        }

        private void configurationCombo_changed(object sender, EventArgs e)
        {
            updateTextBoxes();
        }

        private void deleteDeviceContentBtn_Click(object sender, EventArgs e)
        {
            deleteDeviceConfiguration(configurationComboBox.Text);
        }

        private void deleteDeviceConfiguration(string text)
        {
            Configuration selectedConfiguration = configurationsMap[text];
            this.registryManager.RemoveConfigurationAsync(selectedConfiguration);
            configurationsMap.Remove(text);
            updateTextBoxes();
        }

        private void configurationLabel_SelectIndxChange(object sender, EventArgs e)
        {
            if(this.currentConfiguration.Labels != null)
            {
                configurationLabelValueTextBox.Text = this.currentConfiguration.Labels[configurationLabelComboBox.Text];
            }
        }
    }
}
