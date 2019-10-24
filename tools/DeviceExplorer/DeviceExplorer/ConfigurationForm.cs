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

            deviceContentRichTextBox.Text = JsonConvert.SerializeObject(configuration.Content.ModulesContent);
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

            CreateModulesContent(configuration, configuration.Id);

            //configuration.Content.ModulesContent = new Dictionary<string, IDictionary<string, object>>();
            ////configuration.Content.DeviceContent["properties.desired2.deviceContent_key"] = deviceContentRichTextBox.Text; //this works
            //configuration.Content.ModulesContent["properties.desired.deviceContent_key"] = new Dictionary<string, object> { { "runtime", "value" }, { "runtime2", "value2" } }; ; //this works
            //configuration.Content.ModulesContent["properties.desired.deviceContent_int"] = new Dictionary<string, object> { ["runtime"] = 1, 
            //                                                                                                               ["runtime2"] = 2 }; ; //this works
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

        public void CreateDeviceContent(Configuration configuration, string configurationId)
        {
            configuration.Content = new ConfigurationContent();
            configuration.Content.DeviceContent = new Dictionary<string, object>();
            configuration.Content.DeviceContent["properties.desired.deviceContent_key"] = "deviceContent_value-" + configurationId;
        }

        public void CreateModulesContent(Configuration configuration, string configurationId)
        {
            configuration.Content.ModulesContent = new Dictionary<string, IDictionary<string, object>>();
            IDictionary<string, object> modules_value = new Dictionary<string, object>();
            IDictionary<string, object> samplemodules_value = new Dictionary<string, object>();
            samplemodules_value["type"] = "docker";
            modules_value["properties.desired.modules.SampleModule"] = samplemodules_value;
            configuration.Content.ModulesContent["$edgeAgent"] = modules_value;
        }

        public void CreateMetricsAndTargetCondition(Configuration configuration, string configurationId)
        {
            configuration.Metrics.Queries.Add("waterSettingsPending", "SELECT deviceId FROM devices WHERE properties.reported.chillerWaterSettings.status=\'pending\'");
            configuration.TargetCondition = "properties.reported.chillerProperties.model=\'4000x\'";
            configuration.Priority = 20;
        }

        public void PrintConfiguration(Configuration configuration)
        {
            Console.WriteLine("Configuration Id: " + configuration.Id);
            Console.WriteLine("Configuration SchemaVersion: " + configuration.SchemaVersion);

            Console.WriteLine("Configuration Labels: " + configuration.Labels);

            PrintContent(configuration.ContentType, configuration.Content);

            Console.WriteLine("Configuration TargetCondition: " + configuration.TargetCondition);
            Console.WriteLine("Configuration CreatedTimeUtc: " + configuration.CreatedTimeUtc);
            Console.WriteLine("Configuration LastUpdatedTimeUtc: " + configuration.LastUpdatedTimeUtc);

            Console.WriteLine("Configuration Priority: " + configuration.Priority);

            PrintConfigurationMetrics(configuration.SystemMetrics, "SystemMetrics");
            PrintConfigurationMetrics(configuration.Metrics, "Metrics");

            Console.WriteLine("Configuration ETag: " + configuration.ETag);
            Console.WriteLine("------------------------------------------------------------");
        }

        private void PrintContent(string contentType, ConfigurationContent configurationContent)
        {
            Console.WriteLine($"Configuration Content [type = {contentType}]");

            Console.WriteLine("ModuleContent:");
            foreach (string modulesContentKey in configurationContent.ModulesContent.Keys)
            {
                foreach (string key in configurationContent.ModulesContent[modulesContentKey].Keys)
                {
                    Console.WriteLine($"\t\t{key} = {configurationContent.ModulesContent[modulesContentKey][key]}");
                }
            }

            Console.WriteLine("DeviceContent:");
            foreach (string key in configurationContent.DeviceContent.Keys)
            {
                Console.WriteLine($"\t{key} = {configurationContent.DeviceContent[key]}");
            }
        }

        private void PrintConfigurationMetrics(ConfigurationMetrics metrics, string title)
        {
            Console.WriteLine($"{title} Results: ({metrics.Results.Count})");
            foreach (string key in metrics.Results.Keys)
            {
                Console.WriteLine($"\t{key} = {metrics.Results[key]}");
            }

            Console.WriteLine($"{title} Queries: ({metrics.Queries.Count})");
            foreach (string key in metrics.Queries.Keys)
            {
                Console.WriteLine($"\t{key} = {metrics.Queries[key]}");
            }
        }
    }
}
