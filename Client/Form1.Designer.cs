namespace WeatherClient
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtCity = new TextBox();
            btnGetWeather = new Button();
            panelWeather = new Panel();
            lblCity = new Label();
            lblTemp = new Label();
            lblHumidity = new Label();
            lblDescription = new Label();
            lblDate = new Label();
            pictureBox1 = new PictureBox();
            panelWeather.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtCity
            // 
            txtCity.Font = new Font("Segoe UI", 10F);
            txtCity.Location = new Point(23, 27);
            txtCity.Margin = new Padding(3, 4, 3, 4);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "Nhập tên thành phố";
            txtCity.Size = new Size(228, 30);
            txtCity.TabIndex = 2;
            // 
            // btnGetWeather
            // 
            btnGetWeather.BackColor = Color.SteelBlue;
            btnGetWeather.FlatAppearance.BorderSize = 0;
            btnGetWeather.FlatStyle = FlatStyle.Flat;
            btnGetWeather.Font = new Font("Segoe UI", 9F);
            btnGetWeather.ForeColor = Color.White;
            btnGetWeather.Location = new Point(263, 27);
            btnGetWeather.Margin = new Padding(3, 4, 3, 4);
            btnGetWeather.Name = "btnGetWeather";
            btnGetWeather.Size = new Size(114, 33);
            btnGetWeather.TabIndex = 1;
            btnGetWeather.Text = "Xem thời tiết";
            btnGetWeather.UseVisualStyleBackColor = false;
            btnGetWeather.Click += BtnGetWeather_Click;
            // 
            // panelWeather
            // 
            panelWeather.BackColor = Color.AliceBlue;
            panelWeather.BorderStyle = BorderStyle.FixedSingle;
            panelWeather.Controls.Add(lblCity);
            panelWeather.Controls.Add(lblTemp);
            panelWeather.Controls.Add(lblHumidity);
            panelWeather.Controls.Add(lblDescription);
            panelWeather.Controls.Add(lblDate);
            panelWeather.Controls.Add(pictureBox1);
            panelWeather.Location = new Point(23, 80);
            panelWeather.Margin = new Padding(3, 4, 3, 4);
            panelWeather.Name = "panelWeather";
            panelWeather.Size = new Size(612, 340);
            panelWeather.TabIndex = 0;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCity.Location = new Point(23, 20);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(0, 32);
            lblCity.TabIndex = 0;
            // 
            // lblTemp
            // 
            lblTemp.AutoSize = true;
            lblTemp.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTemp.Location = new Point(23, 60);
            lblTemp.Name = "lblTemp";
            lblTemp.Size = new Size(0, 54);
            lblTemp.TabIndex = 1;
            // 
            // lblHumidity
            // 
            lblHumidity.AutoSize = true;
            lblHumidity.Font = new Font("Segoe UI", 11F);
            lblHumidity.Location = new Point(23, 113);
            lblHumidity.Name = "lblHumidity";
            lblHumidity.Size = new Size(0, 25);
            lblHumidity.TabIndex = 2;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 11F);
            lblDescription.Location = new Point(23, 147);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(0, 25);
            lblDescription.TabIndex = 3;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F);
            lblDate.Location = new Point(23, 180);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(0, 20);
            lblDate.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(473, 20);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(91, 107);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1061, 540);
            Controls.Add(panelWeather);
            Controls.Add(btnGetWeather);
            Controls.Add(txtCity);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Weather App";
            panelWeather.ResumeLayout(false);
            panelWeather.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCity;
        private Button btnGetWeather;
        private Panel panelWeather;
        private Label lblCity;
        private Label lblTemp;
        private Label lblHumidity;
        private Label lblDescription;
        private Label lblDate;
        private PictureBox pictureBox1;
    }
}