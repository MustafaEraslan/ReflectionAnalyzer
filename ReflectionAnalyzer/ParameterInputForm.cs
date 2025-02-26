using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace ReflectionAnalyzer
{
    public partial class ParameterInputForm : Form
    {
        public object[] ParameterValues { get; private set; }
        private readonly ParameterInfo[] _parameters;

        public ParameterInputForm(ParameterInfo[] parameters)
        {
            InitializeComponent();
            _parameters = parameters;

            // Form Ayarları
            this.Text = "Enter Method Parameters";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            CreateInputFields(parameters);
        }

        private void CreateInputFields(ParameterInfo[] parameters)
        {
            int top = 10;

            foreach (var param in parameters)
            {
                var label = new Label
                {
                    Text = $"{param.Name} ({param.ParameterType.Name})",
                    Top = top,
                    Left = 10,
                    Width = 200
                };

                var textBox = new TextBox
                {
                    Name = param.Name,
                    Top = top,
                    Left = 220,
                    Width = 150
                };

                Controls.Add(label);
                Controls.Add(textBox);
                top += 30;
            }

            var submitButton = new Button
            {
                Text = "Submit",
                Top = top + 10,
                Left = 220,
                Width = 100
            };
            submitButton.Click += SubmitParameters;

            Controls.Add(submitButton);
        }

        private void SubmitParameters(object sender, EventArgs e)
        {
            var textBoxes = Controls.OfType<TextBox>().ToList();
            var values = new object[textBoxes.Count];

            for (int i = 0; i < textBoxes.Count; i++)
            {
                var paramType = _parameters[i].ParameterType;
                var input = textBoxes[i].Text;

                try
                {
                    values[i] = ConvertParameterValue(input, paramType); // Tür dönüşümü
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Parametre '{_parameters[i].Name}' için hata: {ex.Message}", "Dönüşüm Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hata varsa metot çağrısını durdur
                }
            }

            ParameterValues = values;
            DialogResult = DialogResult.OK;
            Close();
        }

        private object ConvertParameterValue(string input, Type targetType)
        {
            if (targetType == typeof(string))
                return input;

            if (targetType == typeof(int))
                return int.TryParse(input, out int intValue)
                    ? intValue
                    : throw new FormatException("Geçerli bir tam sayı girin. (örn: 42)");

            if (targetType == typeof(double))
                return double.TryParse(input, out double doubleValue)
                    ? doubleValue
                    : throw new FormatException("Geçerli bir ondalık sayı girin. (örn: 3.14)");

            if (targetType == typeof(bool))
                return bool.TryParse(input, out bool boolValue)
                    ? boolValue
                    : throw new FormatException("Geçerli bir boolean değeri girin. (true veya false)");

            if (targetType == typeof(DateTime))
                return DateTime.TryParse(input, out DateTime dateValue)
                    ? dateValue
                    : throw new FormatException("Geçerli bir tarih girin. (örn: 2024-02-26)");

            if (targetType.IsEnum)
            {
                var validValues = string.Join(", ", Enum.GetNames(targetType));
                return Enum.TryParse(targetType, input, ignoreCase: true, out object enumValue)
                    ? enumValue
                    : throw new FormatException($"Geçerli bir değer girin. Uygun değerler: {validValues}");
            }

            if (targetType == typeof(Font))
            {
                // Beklenen format: FontName, Size, [Style]
                var parts = input.Split(',');
                if (parts.Length < 2)
                    throw new FormatException("Font için format: FontName, Size [, Style] (örn: Arial,12 veya Arial,12,Bold)");

                var fontName = parts[0].Trim();
                if (!float.TryParse(parts[1].Trim(), out float fontSize))
                    throw new FormatException("Geçerli bir font boyutu girin. (örn: 12)");

                FontStyle fontStyle = FontStyle.Regular;
                if (parts.Length == 3 && !Enum.TryParse(parts[2].Trim(), true, out fontStyle))
                {
                    var validFontStyles = string.Join(", ", Enum.GetNames(typeof(FontStyle)));
                    throw new FormatException($"Geçerli bir font stili girin. Uygun değerler: {validFontStyles}");
                }

                return new Font(fontName, fontSize, fontStyle);
            }

            // 🚨 Desteklenmeyen parametre türü için açıklamalı hata mesajı
            throw new NotSupportedException(
                $"Desteklenmeyen parametre türü: {targetType.Name}. " +
                "Desteklenen türler: string, int, double, bool, DateTime, Enum ve Font.\n" +
                "Lütfen uygun bir değer girin."
            );
        }
    }
}