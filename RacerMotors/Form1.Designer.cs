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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNamePosition = new System.Windows.Forms.Label();
            this.lblRazdel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblVsegoRazdelov = new System.Windows.Forms.Label();
            this.ofdLoadPrice = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbChekedReplaceMiniText = new System.Windows.Forms.CheckBox();
            this.cbReplaceImagesProduct = new System.Windows.Forms.CheckBox();
            this.cbChekedReplaceFullText = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbMiniText
            // 
            this.rtbMiniText.Location = new System.Drawing.Point(9, 31);
            this.rtbMiniText.Margin = new System.Windows.Forms.Padding(2);
            this.rtbMiniText.Name = "rtbMiniText";
            this.rtbMiniText.Size = new System.Drawing.Size(523, 130);
            this.rtbMiniText.TabIndex = 0;
            this.rtbMiniText.Text = "";
            // 
            // rtbFullText
            // 
            this.rtbFullText.Location = new System.Drawing.Point(9, 161);
            this.rtbFullText.Margin = new System.Windows.Forms.Padding(2);
            this.rtbFullText.Name = "rtbFullText";
            this.rtbFullText.Size = new System.Drawing.Size(523, 130);
            this.rtbFullText.TabIndex = 1;
            this.rtbFullText.Text = "";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(9, 291);
            this.tbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(523, 20);
            this.tbTitle.TabIndex = 2;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(9, 313);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(523, 20);
            this.tbDescription.TabIndex = 3;
            // 
            // tbKeywords
            // 
            this.tbKeywords.Location = new System.Drawing.Point(9, 336);
            this.tbKeywords.Margin = new System.Windows.Forms.Padding(2);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(523, 20);
            this.tbKeywords.TabIndex = 4;
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Location = new System.Drawing.Point(543, 326);
            this.btnSaveTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(126, 30);
            this.btnSaveTemplate.TabIndex = 5;
            this.btnSaveTemplate.Text = "Сохранить шаблон";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // btnActualPrice
            // 
            this.btnActualPrice.Location = new System.Drawing.Point(543, 30);
            this.btnActualPrice.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualPrice.Name = "btnActualPrice";
            this.btnActualPrice.Size = new System.Drawing.Size(126, 45);
            this.btnActualPrice.TabIndex = 6;
            this.btnActualPrice.Text = "Актуализировать сайт";
            this.btnActualPrice.UseVisualStyleBackColor = true;
            this.btnActualPrice.Click += new System.EventHandler(this.btnActualPrice_Click);
            // 
            // btnPrice
            // 
            this.btnPrice.Location = new System.Drawing.Point(543, 79);
            this.btnPrice.Name = "btnPrice";
            this.btnPrice.Size = new System.Drawing.Size(126, 30);
            this.btnPrice.TabIndex = 7;
            this.btnPrice.Text = "Обработать прайс";
            this.btnPrice.UseVisualStyleBackColor = true;
            this.btnPrice.Click += new System.EventHandler(this.btnPrice_Click);
            // 
            // btnUpdateImg
            // 
            this.btnUpdateImg.Location = new System.Drawing.Point(543, 114);
            this.btnUpdateImg.Name = "btnUpdateImg";
            this.btnUpdateImg.Size = new System.Drawing.Size(125, 30);
            this.btnUpdateImg.TabIndex = 8;
            this.btnUpdateImg.Text = "Обновить картинки";
            this.btnUpdateImg.UseVisualStyleBackColor = true;
            this.btnUpdateImg.Click += new System.EventHandler(this.btnUpdateImg_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(543, 166);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(125, 20);
            this.tbLogin.TabIndex = 9;
            // 
            // tbPasswords
            // 
            this.tbPasswords.Location = new System.Drawing.Point(543, 205);
            this.tbPasswords.Name = "tbPasswords";
            this.tbPasswords.Size = new System.Drawing.Size(125, 20);
            this.tbPasswords.TabIndex = 10;
            this.tbPasswords.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(543, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Логин:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(543, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Пароль:";
            // 
            // lblNamePosition
            // 
            this.lblNamePosition.AutoSize = true;
            this.lblNamePosition.Location = new System.Drawing.Point(543, 228);
            this.lblNamePosition.Name = "lblNamePosition";
            this.lblNamePosition.Size = new System.Drawing.Size(47, 13);
            this.lblNamePosition.TabIndex = 13;
            this.lblNamePosition.Text = "Раздел ";
            // 
            // lblRazdel
            // 
            this.lblRazdel.AutoSize = true;
            this.lblRazdel.Location = new System.Drawing.Point(587, 228);
            this.lblRazdel.Name = "lblRazdel";
            this.lblRazdel.Size = new System.Drawing.Size(13, 13);
            this.lblRazdel.TabIndex = 14;
            this.lblRazdel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(543, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "из";
            // 
            // lblVsegoRazdelov
            // 
            this.lblVsegoRazdelov.AutoSize = true;
            this.lblVsegoRazdelov.Location = new System.Drawing.Point(568, 241);
            this.lblVsegoRazdelov.Name = "lblVsegoRazdelov";
            this.lblVsegoRazdelov.Size = new System.Drawing.Size(13, 13);
            this.lblVsegoRazdelov.TabIndex = 16;
            this.lblVsegoRazdelov.Text = "0";
            // 
            // ofdLoadPrice
            // 
            this.ofdLoadPrice.FileName = "openFileDialog1";
            this.ofdLoadPrice.Filter = "Excel|*.xlsx";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(846, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // cbChekedReplaceMiniText
            // 
            this.cbChekedReplaceMiniText.AutoSize = true;
            this.cbChekedReplaceMiniText.Location = new System.Drawing.Point(674, 33);
            this.cbChekedReplaceMiniText.Name = "cbChekedReplaceMiniText";
            this.cbChekedReplaceMiniText.Size = new System.Drawing.Size(165, 17);
            this.cbChekedReplaceMiniText.TabIndex = 18;
            this.cbChekedReplaceMiniText.Text = "Замена краткого описания";
            this.cbChekedReplaceMiniText.UseVisualStyleBackColor = true;
            // 
            // cbReplaceImagesProduct
            // 
            this.cbReplaceImagesProduct.AutoSize = true;
            this.cbReplaceImagesProduct.Location = new System.Drawing.Point(674, 79);
            this.cbReplaceImagesProduct.Name = "cbReplaceImagesProduct";
            this.cbReplaceImagesProduct.Size = new System.Drawing.Size(174, 17);
            this.cbReplaceImagesProduct.TabIndex = 19;
            this.cbReplaceImagesProduct.Text = "Замена изображений товара";
            this.cbReplaceImagesProduct.UseVisualStyleBackColor = true;
            // 
            // cbChekedReplaceFullText
            // 
            this.cbChekedReplaceFullText.AutoSize = true;
            this.cbChekedReplaceFullText.Location = new System.Drawing.Point(674, 56);
            this.cbChekedReplaceFullText.Name = "cbChekedReplaceFullText";
            this.cbChekedReplaceFullText.Size = new System.Drawing.Size(160, 17);
            this.cbChekedReplaceFullText.TabIndex = 20;
            this.cbChekedReplaceFullText.Text = "Замена полного описания";
            this.cbChekedReplaceFullText.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 372);
            this.Controls.Add(this.cbChekedReplaceFullText);
            this.Controls.Add(this.cbReplaceImagesProduct);
            this.Controls.Add(this.cbChekedReplaceMiniText);
            this.Controls.Add(this.lblVsegoRazdelov);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblRazdel);
            this.Controls.Add(this.lblNamePosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Racer Motors";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNamePosition;
        private System.Windows.Forms.Label lblRazdel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVsegoRazdelov;
        private System.Windows.Forms.OpenFileDialog ofdLoadPrice;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbChekedReplaceMiniText;
        private System.Windows.Forms.CheckBox cbReplaceImagesProduct;
        private System.Windows.Forms.CheckBox cbChekedReplaceFullText;
    }
}

