﻿namespace DNS_Changer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            activeInterfaceName = new Label();
            label4 = new Label();
            CurrentDnsNameTxt = new Label();
            label2 = new Label();
            currentDns2 = new TextBox();
            currentDns1 = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            setDnsButton = new Button();
            DnsList = new ListBox();
            pictureBox1 = new PictureBox();
            linkLabel1 = new LinkLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(linkLabel1);
            groupBox1.Controls.Add(activeInterfaceName);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(CurrentDnsNameTxt);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(currentDns2);
            groupBox1.Controls.Add(currentDns1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(288, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(260, 158);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Current Settings";
            // 
            // activeInterfaceName
            // 
            activeInterfaceName.AutoSize = true;
            activeInterfaceName.Location = new Point(119, 103);
            activeInterfaceName.Name = "activeInterfaceName";
            activeInterfaceName.Size = new Size(36, 15);
            activeInterfaceName.TabIndex = 6;
            activeInterfaceName.Text = "None";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 103);
            label4.Name = "label4";
            label4.Size = new Size(92, 15);
            label4.TabIndex = 5;
            label4.Text = "Active Interface:";
            // 
            // CurrentDnsNameTxt
            // 
            CurrentDnsNameTxt.AutoSize = true;
            CurrentDnsNameTxt.Location = new Point(119, 127);
            CurrentDnsNameTxt.Name = "CurrentDnsNameTxt";
            CurrentDnsNameTxt.Size = new Size(36, 15);
            CurrentDnsNameTxt.TabIndex = 4;
            CurrentDnsNameTxt.Text = "None";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 127);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 3;
            label2.Text = "Setting: ";
            // 
            // currentDns2
            // 
            currentDns2.BackColor = SystemColors.ActiveCaption;
            currentDns2.Location = new Point(119, 50);
            currentDns2.Name = "currentDns2";
            currentDns2.ReadOnly = true;
            currentDns2.Size = new Size(135, 23);
            currentDns2.TabIndex = 2;
            // 
            // currentDns1
            // 
            currentDns1.BackColor = SystemColors.ActiveCaption;
            currentDns1.Location = new Point(119, 21);
            currentDns1.Name = "currentDns1";
            currentDns1.ReadOnly = true;
            currentDns1.Size = new Size(135, 23);
            currentDns1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 24);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "DNS domains";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(setDnsButton);
            groupBox2.Controls.Add(DnsList);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(260, 262);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Set Dns";
            // 
            // setDnsButton
            // 
            setDnsButton.Location = new Point(168, 107);
            setDnsButton.Name = "setDnsButton";
            setDnsButton.Size = new Size(86, 51);
            setDnsButton.TabIndex = 1;
            setDnsButton.Text = "Set DNS";
            setDnsButton.UseVisualStyleBackColor = true;
            setDnsButton.Click += setDnsButton_Click;
            // 
            // DnsList
            // 
            DnsList.FormattingEnabled = true;
            DnsList.ItemHeight = 15;
            DnsList.Location = new Point(6, 22);
            DnsList.Name = "DnsList";
            DnsList.Size = new Size(156, 229);
            DnsList.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(366, 178);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(77, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(56, 127);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(46, 15);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Refresh";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 288);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "DNS Changer";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label CurrentDnsNameTxt;
        private Label label2;
        private TextBox currentDns2;
        private TextBox currentDns1;
        private Label label1;
        private GroupBox groupBox2;
        private Button setDnsButton;
        private ListBox DnsList;
        private PictureBox pictureBox1;
        private Label activeInterfaceName;
        private Label label4;
        private LinkLabel linkLabel1;
    }
}
