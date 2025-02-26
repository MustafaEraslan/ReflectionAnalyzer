namespace ReflectionAnalyzer
{
    partial class MainForm
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
            propertyGridControls = new PropertyGrid();
            btnInvokeMethod = new Button();
            lstMethods = new ListBox();
            lstClasses = new ListBox();
            btnLoadDll = new Button();
            SuspendLayout();
            // 
            // propertyGridControls
            // 
            propertyGridControls.Location = new Point(43, 636);
            propertyGridControls.Name = "propertyGridControls";
            propertyGridControls.Size = new Size(1209, 473);
            propertyGridControls.TabIndex = 14;
            // 
            // btnInvokeMethod
            // 
            btnInvokeMethod.Location = new Point(624, 106);
            btnInvokeMethod.Name = "btnInvokeMethod";
            btnInvokeMethod.Size = new Size(302, 58);
            btnInvokeMethod.TabIndex = 13;
            btnInvokeMethod.Text = "\"Invoke Method\"";
            btnInvokeMethod.UseVisualStyleBackColor = true;
            // 
            // lstMethods
            // 
            lstMethods.FormattingEnabled = true;
            lstMethods.ItemHeight = 41;
            lstMethods.Location = new Point(23, 200);
            lstMethods.Name = "lstMethods";
            lstMethods.Size = new Size(661, 414);
            lstMethods.TabIndex = 12;
            // 
            // lstClasses
            // 
            lstClasses.FormattingEnabled = true;
            lstClasses.ItemHeight = 41;
            lstClasses.Location = new Point(722, 200);
            lstClasses.Name = "lstClasses";
            lstClasses.Size = new Size(567, 414);
            lstClasses.TabIndex = 11;
            // 
            // btnLoadDll
            // 
            btnLoadDll.Location = new Point(206, 106);
            btnLoadDll.Name = "btnLoadDll";
            btnLoadDll.Size = new Size(311, 58);
            btnLoadDll.TabIndex = 10;
            btnLoadDll.Text = "\"Load DLL(s)\"";
            btnLoadDll.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1301, 1175);
            Controls.Add(propertyGridControls);
            Controls.Add(btnInvokeMethod);
            Controls.Add(lstMethods);
            Controls.Add(lstClasses);
            Controls.Add(btnLoadDll);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private PropertyGrid propertyGridControls;
        private Button btnInvokeMethod;
        private ListBox lstMethods;
        private ListBox lstClasses;
        private Button btnLoadDll;
    }
}