namespace DurakCardGame
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTestCardControl = new Button();
            pnlCardTest = new Panel();
            SuspendLayout();
            // 
            // btnTestCardControl
            // 
            btnTestCardControl.Location = new Point(10, 10);
            btnTestCardControl.Name = "btnTestCardControl";
            btnTestCardControl.Size = new Size(94, 29);
            btnTestCardControl.TabIndex = 0;
            btnTestCardControl.Text = "Test Card Control";
            btnTestCardControl.UseVisualStyleBackColor = true;
            btnTestCardControl.Click += btnTestCardControl_Click;
            // 
            // pnlCardTest
            // 
            pnlCardTest.BackColor = Color.Green;
            pnlCardTest.Location = new Point(10, 50);
            pnlCardTest.Name = "pnlCardTest";
            pnlCardTest.Size = new Size(300, 200);
            pnlCardTest.TabIndex = 1;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlCardTest);
            Controls.Add(btnTestCardControl);
            Name = "GameForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnTestCardControl;
        private Panel pnlCardTest;
    }
}
