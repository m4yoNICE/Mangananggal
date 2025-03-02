namespace Mangananggal.Forms.Authentication
{
    partial class ForgetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgetForm));
            this.lblError = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.txtOTPcode = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblCheckUsername = new System.Windows.Forms.Label();
            this.btnOTPsendcode = new System.Windows.Forms.Button();
            this.lblOTPcountdown = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBack = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(230, 387);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 18);
            this.lblError.TabIndex = 10;
            // 
            // btnVerify
            // 
            this.btnVerify.BackColor = System.Drawing.Color.Black;
            this.btnVerify.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerify.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerify.ForeColor = System.Drawing.Color.White;
            this.btnVerify.Location = new System.Drawing.Point(143, 525);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(353, 57);
            this.btnVerify.TabIndex = 9;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = false;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // txtOTPcode
            // 
            this.txtOTPcode.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOTPcode.Location = new System.Drawing.Point(140, 353);
            this.txtOTPcode.Name = "txtOTPcode";
            this.txtOTPcode.Size = new System.Drawing.Size(356, 52);
            this.txtOTPcode.TabIndex = 8;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(140, 249);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(356, 52);
            this.txtUsername.TabIndex = 11;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged_1);
            // 
            // lblCheckUsername
            // 
            this.lblCheckUsername.AutoSize = true;
            this.lblCheckUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckUsername.ForeColor = System.Drawing.Color.White;
            this.lblCheckUsername.Location = new System.Drawing.Point(337, 228);
            this.lblCheckUsername.Name = "lblCheckUsername";
            this.lblCheckUsername.Size = new System.Drawing.Size(0, 18);
            this.lblCheckUsername.TabIndex = 12;
            // 
            // btnOTPsendcode
            // 
            this.btnOTPsendcode.BackColor = System.Drawing.Color.Black;
            this.btnOTPsendcode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.btnOTPsendcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOTPsendcode.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOTPsendcode.ForeColor = System.Drawing.Color.White;
            this.btnOTPsendcode.Location = new System.Drawing.Point(140, 411);
            this.btnOTPsendcode.Name = "btnOTPsendcode";
            this.btnOTPsendcode.Size = new System.Drawing.Size(90, 25);
            this.btnOTPsendcode.TabIndex = 13;
            this.btnOTPsendcode.Text = "Send Code";
            this.btnOTPsendcode.UseVisualStyleBackColor = false;
            this.btnOTPsendcode.Click += new System.EventHandler(this.btnOTPsendcode_Click);
            // 
            // lblOTPcountdown
            // 
            this.lblOTPcountdown.AutoSize = true;
            this.lblOTPcountdown.BackColor = System.Drawing.Color.Transparent;
            this.lblOTPcountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOTPcountdown.ForeColor = System.Drawing.Color.White;
            this.lblOTPcountdown.Location = new System.Drawing.Point(297, 418);
            this.lblOTPcountdown.Name = "lblOTPcountdown";
            this.lblOTPcountdown.Size = new System.Drawing.Size(0, 18);
            this.lblOTPcountdown.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(37)))), ((int)(((byte)(83)))));
            this.panel1.Location = new System.Drawing.Point(108, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 134);
            this.panel1.TabIndex = 15;
            // 
            // lblBack
            // 
            this.lblBack.AutoSize = true;
            this.lblBack.BackColor = System.Drawing.Color.Transparent;
            this.lblBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBack.ForeColor = System.Drawing.Color.White;
            this.lblBack.Location = new System.Drawing.Point(12, 9);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(69, 73);
            this.lblBack.TabIndex = 6;
            this.lblBack.Text = "<";
            this.lblBack.Click += new System.EventHandler(this.lblBack_Click);
            // 
            // ForgetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(653, 681);
            this.Controls.Add(this.lblBack);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblOTPcountdown);
            this.Controls.Add(this.btnOTPsendcode);
            this.Controls.Add(this.lblCheckUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.txtOTPcode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ForgetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForgetForm";
            this.Load += new System.EventHandler(this.ForgetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.TextBox txtOTPcode;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblCheckUsername;
        private System.Windows.Forms.Button btnOTPsendcode;
        private System.Windows.Forms.Label lblOTPcountdown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBack;
    }
}