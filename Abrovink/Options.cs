using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abrovink
{
    public partial class Options : Form
    {
        Properties.Settings settings = Properties.Settings.Default;
        KeysConverter kc = new KeysConverter();
        Dictionary<WidgetType, OptionsData> options = new Dictionary<WidgetType, OptionsData>();

        public Options()
        {
            InitializeComponent();

            for(int i=1; i<16; i+=2)
                EyedropperResolution0.Items.Add(new KeyValuePair<int, string>(i, i + "x" + i));

            EyedropperResolution0.SelectedIndex = 0;

            LoadOptions();

            txtCredits.ContextMenu = new ContextMenu();
            txtCredits.Enter += TxtCredits_Enter;
            txtCredits.Cursor = Cursors.Default;
            txtCredits.Text = settings["Credits"].ToString();
        }

        private void TxtCredits_Enter(object sender, EventArgs e)
        {
            lblCreditsHeader.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveOptions();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void Hotkey_KeyDown(object sender, KeyEventArgs e)
        {
            var textbox = (sender as TextBox);
            var tag = -1;

            if (textbox.Tag != null)
            {
                if (int.TryParse(textbox.Tag.ToString(), out tag))
                {
                    switch ((WidgetType)(int.Parse(textbox.Tag.ToString())))
                    {
                        case WidgetType.EyeDropper:
                            EyedropperHotkey.Tag = kc.ConvertToString(e.KeyCode);
                            break;

                        case WidgetType.Ruler:
                            RulerHotkey.Tag = kc.ConvertToString(e.KeyCode);
                            break;

                        default:
                            break;
                    }
                }
            }

            e.Handled = true;
        }

        private void Hotkey_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textbox = (sender as TextBox);
            var tag = -1;

            if (textbox.Tag != null)
            {
                if (int.TryParse(textbox.Tag.ToString(), out tag))
                {
                    switch ((WidgetType)(int.Parse(textbox.Tag.ToString())))
                    {
                        case WidgetType.EyeDropper:
                            textbox.Text = EyedropperHotkey.Tag.ToString();
                            break;

                        case WidgetType.Ruler:
                            textbox.Text = RulerHotkey.Tag.ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
            e.Handled = true;
        }


        private void LoadOptions()
        {
            options = new Dictionary<WidgetType, OptionsData>();

            OptionsData data;

            foreach (WidgetType type in Enum.GetValues(typeof(WidgetType)))
            {
                var strData = settings["Options_" + (int)type].ToString();

                if (string.IsNullOrEmpty(strData))
                {
                    // Initialize default options data
                    data = new OptionsData();

                    switch (type)
                    {
                        case WidgetType.EyeDropper:
                            data.StringVals["hotkey"] = "Ctrl+Alt+1";
                            data.IntVals["resolution"] = 3;
                            break;

                        case WidgetType.Ruler:
                            data.StringVals["hotkey"] = "Ctrl+Alt+2";
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    // Load custom options
                    data = strData.XmlDeserializeFromString<OptionsData>();
                }

                // Initialize options data variables
                options.Add(type, data);

                string[] hotkey = data.StringVals["hotkey"].ToString().Split('+');

                // Populate controls with data
                switch (type)
                {
                    case WidgetType.EyeDropper:
                        if(hotkey.Length < 3)
                        {
                            EyedropperHotkey0.Text = "";
                            EyedropperHotkey0.Text = hotkey[1];
                            EyedropperHotkey2.Text = hotkey[2];
                        }
                        else
                        {
                            EyedropperHotkey0.Text = hotkey[0];
                            EyedropperHotkey1.Text = hotkey[1];
                            EyedropperHotkey2.Text = hotkey[2];
                        }
                        EyedropperResolution0.Text = data.IntVals["resolution"].ToString() + "x" + data.IntVals["resolution"].ToString();
                        break;

                    case WidgetType.Ruler:
                        //RulerHotkey0.Text = hotkey[0];
                        //RulerHotkey1.Text = hotkey[1];
                        break;

                    default:
                        break;
                }

            }
        }

        private void SaveOptions()
        {
            foreach (WidgetType type in Enum.GetValues(typeof(WidgetType)))
            {
                switch (type)
                {
                    case WidgetType.EyeDropper:
                        options[type].StringVals["hotkey"] = (EyedropperHotkey0.Text + "+" + EyedropperHotkey1.Text + "+" + EyedropperHotkey2.Text).Trim('+');
                        options[type].IntVals["resolution"] = ((KeyValuePair<int,string>)EyedropperResolution0.SelectedItem).Key;
                        break;

                    case WidgetType.Ruler:
                        //options[type].StringVals["hotkey"] = RulerHotkey0.Text + "+" + RulerHotkey1.Text;
                        break;

                    default:
                        break;
                }

                settings["Options_" + (int)type] = options[type].XmlSerializeToString();
            }

            settings.Save();
        }

        private void EyedropperHotkey_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = (sender as ComboBox);
            if(EyedropperHotkey0.Text == EyedropperHotkey1.Text)
            {
                MessageBox.Show("You can't assign two modifier keys of the same type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                box.SelectedIndex = string.IsNullOrEmpty(EyedropperHotkey0.Text) ? 1 : 0;
            }
        }
    }
}
