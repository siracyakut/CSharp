namespace Çekiliş_Uygulaması
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblMain = new System.Windows.Forms.Label();
            this.lblinfo = new System.Windows.Forms.Label();
            this.txtKatilimcilar = new System.Windows.Forms.TextBox();
            this.txtIsim = new System.Windows.Forms.TextBox();
            this.lblCekilisIsim = new System.Windows.Forms.Label();
            this.lblKazanacak = new System.Windows.Forms.Label();
            this.txtKazanacak = new System.Windows.Forms.TextBox();
            this.lblYedek = new System.Windows.Forms.Label();
            this.txtYedek = new System.Windows.Forms.TextBox();
            this.btnBelirle = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMain.Location = new System.Drawing.Point(12, 9);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(238, 40);
            this.lblMain.TabIndex = 0;
            this.lblMain.Text = "Katılımcı Listesi";
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.Location = new System.Drawing.Point(16, 66);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(217, 13);
            this.lblinfo.TabIndex = 1;
            this.lblinfo.Text = "Her satıra 1 katılımcı gelecek şekilde ekleyin.";
            // 
            // txtKatilimcilar
            // 
            this.txtKatilimcilar.Location = new System.Drawing.Point(19, 96);
            this.txtKatilimcilar.Multiline = true;
            this.txtKatilimcilar.Name = "txtKatilimcilar";
            this.txtKatilimcilar.Size = new System.Drawing.Size(298, 205);
            this.txtKatilimcilar.TabIndex = 2;
            // 
            // txtIsim
            // 
            this.txtIsim.Location = new System.Drawing.Point(19, 330);
            this.txtIsim.Name = "txtIsim";
            this.txtIsim.Size = new System.Drawing.Size(298, 20);
            this.txtIsim.TabIndex = 3;
            // 
            // lblCekilisIsim
            // 
            this.lblCekilisIsim.AutoSize = true;
            this.lblCekilisIsim.Location = new System.Drawing.Point(16, 312);
            this.lblCekilisIsim.Name = "lblCekilisIsim";
            this.lblCekilisIsim.Size = new System.Drawing.Size(116, 13);
            this.lblCekilisIsim.TabIndex = 4;
            this.lblCekilisIsim.Text = "Çekiliş İsmi (Opsiyonel):";
            // 
            // lblKazanacak
            // 
            this.lblKazanacak.AutoSize = true;
            this.lblKazanacak.Location = new System.Drawing.Point(16, 363);
            this.lblKazanacak.Name = "lblKazanacak";
            this.lblKazanacak.Size = new System.Drawing.Size(113, 13);
            this.lblKazanacak.TabIndex = 6;
            this.lblKazanacak.Text = "Kazanacak Kişi Sayısı:";
            // 
            // txtKazanacak
            // 
            this.txtKazanacak.Location = new System.Drawing.Point(19, 381);
            this.txtKazanacak.Name = "txtKazanacak";
            this.txtKazanacak.Size = new System.Drawing.Size(113, 20);
            this.txtKazanacak.TabIndex = 5;
            // 
            // lblYedek
            // 
            this.lblYedek.AutoSize = true;
            this.lblYedek.Location = new System.Drawing.Point(224, 365);
            this.lblYedek.Name = "lblYedek";
            this.lblYedek.Size = new System.Drawing.Size(90, 13);
            this.lblYedek.TabIndex = 8;
            this.lblYedek.Text = "Yedek Kişi Sayısı:";
            // 
            // txtYedek
            // 
            this.txtYedek.Location = new System.Drawing.Point(204, 381);
            this.txtYedek.Name = "txtYedek";
            this.txtYedek.Size = new System.Drawing.Size(113, 20);
            this.txtYedek.TabIndex = 7;
            // 
            // btnBelirle
            // 
            this.btnBelirle.Location = new System.Drawing.Point(19, 422);
            this.btnBelirle.Name = "btnBelirle";
            this.btnBelirle.Size = new System.Drawing.Size(298, 32);
            this.btnBelirle.TabIndex = 9;
            this.btnBelirle.Text = "KAZANANLARI BELİRLE!";
            this.btnBelirle.UseVisualStyleBackColor = true;
            this.btnBelirle.Click += new System.EventHandler(this.btnBelirle_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(249, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(338, 471);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnBelirle);
            this.Controls.Add(this.lblYedek);
            this.Controls.Add(this.txtYedek);
            this.Controls.Add(this.lblKazanacak);
            this.Controls.Add(this.txtKazanacak);
            this.Controls.Add(this.lblCekilisIsim);
            this.Controls.Add(this.txtIsim);
            this.Controls.Add(this.txtKatilimcilar);
            this.Controls.Add(this.lblinfo);
            this.Controls.Add(this.lblMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Çekiliş Uygulaması";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Label lblinfo;
        private System.Windows.Forms.TextBox txtKatilimcilar;
        private System.Windows.Forms.TextBox txtIsim;
        private System.Windows.Forms.Label lblCekilisIsim;
        private System.Windows.Forms.Label lblKazanacak;
        private System.Windows.Forms.TextBox txtKazanacak;
        private System.Windows.Forms.Label lblYedek;
        private System.Windows.Forms.TextBox txtYedek;
        private System.Windows.Forms.Button btnBelirle;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

