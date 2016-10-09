namespace RacerMotors
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbMiniText = new System.Windows.Forms.RichTextBox();
            this.rtbFullText = new System.Windows.Forms.RichTextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.btnActualPrice = new System.Windows.Forms.Button();
            this.btnPrice = new System.Windows.Forms.Button();
            this.btnUpdateImg = new System.Windows.Forms.Button();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPasswords = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rtbMiniText
            // 
            this.rtbMiniText.Location = new System.Drawing.Point(9, 10);
            this.rtbMiniText.Margin = new System.Windows.Forms.Padding(2);
            this.rtbMiniText.Name = "rtbMiniText";
            this.rtbMiniText.Size = new System.Drawing.Size(523, 126);
            this.rtbMiniText.TabIndex = 0;
            this.rtbMiniText.Text = "";
            // 
            // rtbFullText
            // 
            this.rtbFullText.Location = new System.Drawing.Point(9, 140);
            this.rtbFullText.Margin = new System.Windows.Forms.Padding(2);
            this.rtbFullText.Name = "rtbFullText";
            this.rtbFullText.Size = new System.Drawing.Size(523, 126);
            this.rtbFullText.TabIndex = 1;
            this.rtbFullText.Text = "";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(9, 270);
            this.tbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(523, 20);
            this.tbTitle.TabIndex = 2;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(9, 292);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(523, 20);
            this.tbDescription.TabIndex = 3;
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(9, 315);
            this.tbKeywords.Margin = new System.Windows.Forms.Padding(2);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(523, 20);
            this.tbKeywords.TabIndex = 4;
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Location = new System.Drawing.Point(558, 292);
            this.btnSaveTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(122, 41);
            this.btnSaveTemplate.TabIndex = 5;
            this.btnSaveTemplate.Text = "Сохранить шаблон";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // btnActualPrice
            // 
            this.btnActualPrice.Location = new System.Drawing.Point(543, 21);
            this.btnActualPrice.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualPrice.Name = "btnActualPrice";
            this.btnActualPrice.Size = new System.Drawing.Size(126, 44);
            this.btnActualPrice.TabIndex = 6;
            this.btnActualPrice.Text = "Актуализировать сайт";
            this.btnActualPrice.UseVisualStyleBackColor = true;
            this.btnActualPrice.Click += new System.EventHandler(this.btnActualPrice_Click);
            // 
            // btnPrice
            // 
            this.btnPrice.Location = new System.Drawing.Point(543, 82);
            this.btnPrice.Name = "btnPrice";
            this.btnPrice.Size = new System.Drawing.Size(126, 29);
            this.btnPrice.TabIndex = 7;
            this.btnPrice.Text = "Обработать прайс";
            this.btnPrice.UseVisualStyleBackColor = true;
            this.btnPrice.Click += new System.EventHandler(this.btnPrice_Click);
            // 
            // btnUpdateImg
            // 
            this.btnUpdateImg.Location = new System.Drawing.Point(543, 124);
            this.btnUpdateImg.Name = "btnUpdateImg";
            this.btnUpdateImg.Size = new System.Drawing.Size(125, 29);
            this.btnUpdateImg.TabIndex = 8;
            this.btnUpdateImg.Text = "Обновить картинки";
            this.btnUpdateImg.UseVisualStyleBackColor = true;
            this.btnUpdateImg.Click += new System.EventHandler(this.btnUpdateImg_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(543, 159);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(100, 20);
            this.tbLogin.TabIndex = 9;
            // 
            // tbPasswords
            // 
            this.tbPasswords.Location = new System.Drawing.Point(543, 185);
            this.tbPasswords.Name = "tbPasswords";
            this.tbPasswords.Size = new System.Drawing.Size(100, 20);
            this.tbPasswords.TabIndex = 10;
            this.tbPasswords.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 349);
            this.Controls.Add(this.tbPasswords);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.btnUpdateImg);
            this.Controls.Add(this.btnPrice);
            this.Controls.Add(this.btnActualPrice);
            this.Controls.Add(this.btnSaveTemplate);
            this.Controls.Add(this.tbKeywords);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.rtbFullText);
            this.Controls.Add(this.rtbMiniText);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMiniText;
        private System.Windows.Forms.RichTextBox rtbFullText;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.Button btnActualPrice;
        private System.Windows.Forms.Button btnPrice;
        private System.Windows.Forms.Button btnUpdateImg;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbPasswords;
    }
}

