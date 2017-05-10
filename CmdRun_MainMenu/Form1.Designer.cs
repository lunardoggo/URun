namespace CmdRun_MainMenu
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.bOptions = new System.Windows.Forms.Button();
            this.bEnd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(522, 107);
            this.label1.TabIndex = 0;
            this.label1.Text = "CMD_RUN";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(541, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 107);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(161, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 44);
            this.label2.TabIndex = 2;
            this.label2.Text = " - Herzlich Willkommen -";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 40);
            this.label3.TabIndex = 3;
            this.label3.Text = "Hauptmenü";
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.Color.LimeGreen;
            this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStart.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bStart.Location = new System.Drawing.Point(109, 279);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(380, 56);
            this.bStart.TabIndex = 4;
            this.bStart.Text = "Spiel starten";
            this.bStart.UseVisualStyleBackColor = false;
            // 
            // bOptions
            // 
            this.bOptions.BackColor = System.Drawing.SystemColors.ControlDark;
            this.bOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOptions.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOptions.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bOptions.Location = new System.Drawing.Point(109, 341);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(380, 56);
            this.bOptions.TabIndex = 5;
            this.bOptions.Text = "Optionen und Einstellungen";
            this.bOptions.UseVisualStyleBackColor = false;
            // 
            // bEnd
            // 
            this.bEnd.BackColor = System.Drawing.Color.Crimson;
            this.bEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEnd.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bEnd.Location = new System.Drawing.Point(109, 403);
            this.bEnd.Name = "bEnd";
            this.bEnd.Size = new System.Drawing.Size(380, 56);
            this.bEnd.TabIndex = 6;
            this.bEnd.Text = "Spiel beenden";
            this.bEnd.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(757, 472);
            this.Controls.Add(this.bEnd);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CMD_RUN";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bOptions;
        private System.Windows.Forms.Button bEnd;
    }
}

