namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.D_text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Send_BT = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UDPchooseBT = new System.Windows.Forms.RadioButton();
            this.TCPchooseBT = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.Adr_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CreateS_BT = new System.Windows.Forms.Button();
            this.Port_text = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.CL_lbox = new System.Windows.Forms.Button();
            this.R_button = new System.Windows.Forms.Button();
            this.Gr_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Ani_BT = new System.Windows.Forms.Button();
            this.autoScrollChB = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // D_text
            // 
            this.D_text.Location = new System.Drawing.Point(71, 28);
            this.D_text.Multiline = true;
            this.D_text.Name = "D_text";
            this.D_text.Size = new System.Drawing.Size(269, 49);
            this.D_text.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Send_BT);
            this.groupBox1.Controls.Add(this.D_text);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 97);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Box";
            // 
            // Send_BT
            // 
            this.Send_BT.Location = new System.Drawing.Point(372, 28);
            this.Send_BT.Name = "Send_BT";
            this.Send_BT.Size = new System.Drawing.Size(73, 49);
            this.Send_BT.TabIndex = 4;
            this.Send_BT.Text = "Send";
            this.Send_BT.UseVisualStyleBackColor = true;
            this.Send_BT.Click += new System.EventHandler(this.Send_BT_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.UDPchooseBT);
            this.groupBox2.Controls.Add(this.TCPchooseBT);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.Adr_text);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CreateS_BT);
            this.groupBox2.Controls.Add(this.Port_text);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(486, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 152);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // UDPchooseBT
            // 
            this.UDPchooseBT.AutoSize = true;
            this.UDPchooseBT.Location = new System.Drawing.Point(131, 83);
            this.UDPchooseBT.Name = "UDPchooseBT";
            this.UDPchooseBT.Size = new System.Drawing.Size(48, 17);
            this.UDPchooseBT.TabIndex = 12;
            this.UDPchooseBT.Text = "UDP";
            this.UDPchooseBT.UseVisualStyleBackColor = true;
            // 
            // TCPchooseBT
            // 
            this.TCPchooseBT.AutoSize = true;
            this.TCPchooseBT.Checked = true;
            this.TCPchooseBT.Location = new System.Drawing.Point(6, 83);
            this.TCPchooseBT.Name = "TCPchooseBT";
            this.TCPchooseBT.Size = new System.Drawing.Size(46, 17);
            this.TCPchooseBT.TabIndex = 11;
            this.TCPchooseBT.TabStop = true;
            this.TCPchooseBT.Text = "TCP";
            this.TCPchooseBT.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(131, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 40);
            this.button2.TabIndex = 10;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Adr_text
            // 
            this.Adr_text.Enabled = false;
            this.Adr_text.Location = new System.Drawing.Point(64, 57);
            this.Adr_text.Name = "Adr_text";
            this.Adr_text.Size = new System.Drawing.Size(115, 20);
            this.Adr_text.TabIndex = 3;
            this.Adr_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Adr_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Adr_text_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Address";
            // 
            // CreateS_BT
            // 
            this.CreateS_BT.Location = new System.Drawing.Point(6, 106);
            this.CreateS_BT.Name = "CreateS_BT";
            this.CreateS_BT.Size = new System.Drawing.Size(86, 40);
            this.CreateS_BT.TabIndex = 2;
            this.CreateS_BT.Text = "Open server";
            this.CreateS_BT.UseVisualStyleBackColor = true;
            this.CreateS_BT.Click += new System.EventHandler(this.Creat_S_Click);
            // 
            // Port_text
            // 
            this.Port_text.Enabled = false;
            this.Port_text.Location = new System.Drawing.Point(64, 23);
            this.Port_text.Name = "Port_text";
            this.Port_text.Size = new System.Drawing.Size(78, 20);
            this.Port_text.TabIndex = 0;
            this.Port_text.Text = "80";
            this.Port_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Port";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 130);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(468, 303);
            this.listBox1.TabIndex = 4;
            // 
            // CL_lbox
            // 
            this.CL_lbox.Location = new System.Drawing.Point(512, 323);
            this.CL_lbox.Name = "CL_lbox";
            this.CL_lbox.Size = new System.Drawing.Size(126, 40);
            this.CL_lbox.TabIndex = 6;
            this.CL_lbox.Text = "Clear text";
            this.CL_lbox.UseVisualStyleBackColor = true;
            this.CL_lbox.Click += new System.EventHandler(this.CL_lbox_Click);
            // 
            // R_button
            // 
            this.R_button.Location = new System.Drawing.Point(512, 185);
            this.R_button.Name = "R_button";
            this.R_button.Size = new System.Drawing.Size(126, 40);
            this.R_button.TabIndex = 7;
            this.R_button.Text = "Listen client";
            this.R_button.UseVisualStyleBackColor = true;
            this.R_button.Click += new System.EventHandler(this.R_button_Click);
            // 
            // Gr_button
            // 
            this.Gr_button.Location = new System.Drawing.Point(512, 277);
            this.Gr_button.Name = "Gr_button";
            this.Gr_button.Size = new System.Drawing.Size(126, 40);
            this.Gr_button.TabIndex = 8;
            this.Gr_button.Text = "Graph";
            this.Gr_button.UseVisualStyleBackColor = true;
            this.Gr_button.Click += new System.EventHandler(this.Gr_button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Ani_BT
            // 
            this.Ani_BT.Location = new System.Drawing.Point(512, 231);
            this.Ani_BT.Name = "Ani_BT";
            this.Ani_BT.Size = new System.Drawing.Size(126, 40);
            this.Ani_BT.TabIndex = 9;
            this.Ani_BT.Text = "Animation";
            this.Ani_BT.UseVisualStyleBackColor = true;
            this.Ani_BT.Click += new System.EventHandler(this.Ani_BT_Click);
            // 
            // autoScrollChB
            // 
            this.autoScrollChB.AutoSize = true;
            this.autoScrollChB.Location = new System.Drawing.Point(512, 416);
            this.autoScrollChB.Name = "autoScrollChB";
            this.autoScrollChB.Size = new System.Drawing.Size(75, 17);
            this.autoScrollChB.TabIndex = 10;
            this.autoScrollChB.Text = "Auto scroll";
            this.autoScrollChB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 455);
            this.Controls.Add(this.autoScrollChB);
            this.Controls.Add(this.Ani_BT);
            this.Controls.Add(this.Gr_button);
            this.Controls.Add(this.R_button);
            this.Controls.Add(this.CL_lbox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox D_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button CreateS_BT;
        private System.Windows.Forms.TextBox Port_text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button CL_lbox;
        private System.Windows.Forms.TextBox Adr_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Send_BT;
        private System.Windows.Forms.Button R_button;
        private System.Windows.Forms.Button Gr_button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button Ani_BT;
        private System.Windows.Forms.RadioButton UDPchooseBT;
        private System.Windows.Forms.RadioButton TCPchooseBT;
        private System.Windows.Forms.CheckBox autoScrollChB;
    }
}

