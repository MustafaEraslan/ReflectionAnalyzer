using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionAnalyzer
{
    public partial class MainForm : Form
    {
        private Assembly[] loadedAssemblies;
        public MainForm()
        {
            InitializeComponent();
            btnLoadDll.Click += btnLoadDll_Click;
            lstClasses.SelectedIndexChanged += lstClasses_SelectedIndexChanged;
            btnInvokeMethod.Click += btnInvokeMethod_Click;
            lstMethods.SelectedIndexChanged += lstMethods_SelectedIndexChanged;

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assemblyName = new AssemblyName(args.Name).Name + ".dll";
                var assemblyPath = loadedAssemblies
                    .Select(a => a.Location)
                    .FirstOrDefault(path => Path.GetFileName(path) == assemblyName);

                return assemblyPath != null ? Assembly.LoadFrom(assemblyPath) : null;
            };
        }

        private void btnInvokeMethod_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Invoke Method clicked!");
            InvokeSelectedMethod();
        }

        private void lstClasses_SelectedIndexChanged(object? sender, EventArgs e)
        {
            MessageBox.Show("Class selected!");
            var selectedClass = lstClasses.SelectedItem?.ToString();
            var type = loadedAssemblies.SelectMany(a => a.GetTypes())
                                       .FirstOrDefault(t => t.FullName == selectedClass);

            if (type != null)
            {
                ShowFormControlsAndProperties(type);
            }
            ListMethodsAndFields(type);
        }
        private void lstMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = lstMethods.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedItem) || !selectedItem.Contains("-")) return;

            var controlName = selectedItem.Split('-').Last().Trim();

            var selectedClass = lstClasses.SelectedItem?.ToString();
            var type = loadedAssemblies.SelectMany(a => a.GetTypes())
                                       .FirstOrDefault(t => t.FullName == selectedClass);

            if (type != null && type.IsSubclassOf(typeof(Form)))
            {
                var instance = Activator.CreateInstance(type) as Form;
                var control = instance?.Controls.Find(controlName, true).FirstOrDefault();

                if (control != null)
                {
                    propertyGridControls.SelectedObject = control;
                }
            }
        }

        private void btnLoadDll_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "DLL files (*.dll)|*.dll";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    lstClasses.Items.Clear();
                    var assemblies = new List<Assembly>();

                    foreach (var filePath in openFileDialog.FileNames)
                    {
                        try
                        {
                            var assembly = Assembly.LoadFrom(filePath);
                            assemblies.Add(assembly);  // Başarılı yüklenenleri listeye ekle
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"DLL yüklenemedi: {Path.GetFileName(filePath)}\nHata: {ex.Message}", "Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    loadedAssemblies = assemblies.ToArray();

                    foreach (var assembly in loadedAssemblies)
                    {
                        var classes = assembly.GetTypes().Where(t => t.IsClass);
                        foreach (var cls in classes)
                        {
                            lstClasses.Items.Add(cls.FullName);
                        }
                    }

                    ListFormDerivedClasses();  // DLL yüklendikten sonra çağır
                }
            }
        }
        private void ListFormDerivedClasses()
        {
            lstClasses.Items.Clear();

            foreach (var assembly in loadedAssemblies)
            {
                var formClasses = assembly.GetTypes()
                                          .Where(t => t.IsClass && t.IsSubclassOf(typeof(Form)));

                foreach (var formClass in formClasses)
                {
                    lstClasses.Items.Add(formClass.FullName);
                }
            }
        }
        private void ShowFormControlsAndProperties(Type formType)
        {
            if (!formType.IsSubclassOf(typeof(Form))) return;

            var instance = Activator.CreateInstance(formType) as Form;
            if (instance == null) return;

            var controls = instance.Controls.Cast<Control>().ToList();

            // Kontrolleri türlerine göre kategorize et
            var inputControls = controls.Where(c => c is TextBox || c is ComboBox || c is DateTimePicker).ToList();
            var containerControls = controls.Where(c => c is GroupBox || c is Panel || c is TabControl).ToList();
            var dataControls = controls.Where(c => c is DataGridView || c is ListView).ToList();

            lstMethods.Items.Clear();
            lstMethods.Items.Add($"--- Input Controls ({inputControls.Count}) ---");
            inputControls.ForEach(c => lstMethods.Items.Add($"{c.GetType().Name} - {c.Name}"));

            lstMethods.Items.Add($"--- Container Controls ({containerControls.Count}) ---");
            containerControls.ForEach(c => lstMethods.Items.Add($"{c.GetType().Name} - {c.Name}"));

            lstMethods.Items.Add($"--- Data Controls ({dataControls.Count}) ---");
            dataControls.ForEach(c => lstMethods.Items.Add($"{c.GetType().Name} - {c.Name}"));

            // İlk kontrolün property’lerini göster
            if (controls.Any())
            {
                propertyGridControls.SelectedObject = controls.First();
            }
        }
        private void ListMethodsAndFields(Type type)
        {
            lstMethods.Items.Clear();

            // Metotları listele
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods)
            {
                var access = method.IsPublic ? "public" : method.IsPrivate ? "private" : "protected";
                lstMethods.Items.Add($"Method: {access} {method.ReturnType.Name} {method.Name}()");
            }

            // Değişkenleri listele
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                var access = field.IsPublic ? "public" : field.IsPrivate ? "private" : "protected";
                lstMethods.Items.Add($"Field: {access} {field.FieldType.Name} {field.Name}");
            }
        }
        private void InvokeSelectedMethod()
        {
            var selectedClass = lstClasses.SelectedItem?.ToString();
            var selectedMethodText = lstMethods.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedClass) || string.IsNullOrEmpty(selectedMethodText)) return;

            var selectedMethod = selectedMethodText.Split(' ').Last().Replace("()", "");
            var type = loadedAssemblies.SelectMany(a => a.GetTypes())
                                       .FirstOrDefault(t => t.FullName == selectedClass);

            if (type != null)
            {
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                  .Where(m => m.Name == selectedMethod).ToArray();

                if (methods.Length == 0)
                {
                    MessageBox.Show("Metot bulunamadı.", "Hata");
                    return;
                }

                MethodInfo method = null;
                object[] parameterValues = null;

                var overloadMethods = methods.Where(m => m.GetParameters().Length > 0).ToList();

                if (overloadMethods.Any()) // Parametreli metot
                {
                    var targetMethod = overloadMethods.First(); // İlk parametreli metodu seç
                    var parameters = targetMethod.GetParameters();

                    using (var inputForm = new ParameterInputForm(parameters))
                    {
                        if (inputForm.ShowDialog() == DialogResult.OK)
                        {
                            parameterValues = inputForm.ParameterValues;
                            method = methods.FirstOrDefault(m => m.GetParameters().Length == parameterValues.Length);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else // Parametresiz metot
                {
                    method = methods.FirstOrDefault(m => m.GetParameters().Length == 0);
                }

                if (method != null)
                {
                    var instance = method.IsStatic ? null : Activator.CreateInstance(type);
                    var result = method.Invoke(instance, parameterValues);
                    MessageBox.Show(result?.ToString() ?? "Metot başarıyla çalıştırıldı.", "Sonuç");
                }
                else
                {
                    MessageBox.Show("Uygun metot bulunamadı.", "Hata");
                }
            }
        }
    }
}
