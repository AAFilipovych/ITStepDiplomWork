namespace Seller
{
    partial class SellerForm
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.Go_button = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 56);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(260, 199);
            this.checkedListBox1.TabIndex = 0;
            // 
            // Go_button
            // 
            this.Go_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Go_button.ForeColor = System.Drawing.Color.SeaGreen;
            this.Go_button.Location = new System.Drawing.Point(12, 278);
            this.Go_button.Name = "Go_button";
            this.Go_button.Size = new System.Drawing.Size(260, 51);
            this.Go_button.TabIndex = 1;
            this.Go_button.Text = "Выполнено!";
            this.Go_button.UseVisualStyleBackColor = true;
            this.Go_button.Click += new System.EventHandler(this.Go_button_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_refresh.ForeColor = System.Drawing.Color.Red;
            this.button_refresh.Location = new System.Drawing.Point(12, 12);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(260, 30);
            this.button_refresh.TabIndex = 2;
            this.button_refresh.Text = "ОБНОВИТЬ    АКТУАЛЬНЫЕ    ЗАКАЗЫ";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // SellerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(284, 341);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.Go_button);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "SellerForm";
            this.Text = "Заказы в работе";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button Go_button;
        private System.Windows.Forms.Button button_refresh;
    }
}

