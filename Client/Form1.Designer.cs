using System.Windows.Forms.DataVisualization.Charting;

namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
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
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            Series series1 = new Series();
            label15 = new Label();
            labTemperature = new Label();
            btnSearch = new Button();
            TBCity = new TextBox();
            label1 = new Label();
            labDateTime2 = new Label();
            picIcon = new PictureBox();
            labDateTime = new Label();
            labTemp_min = new Label();
            labTemp_max = new Label();
            labAdvice = new Label();
            labDetail2 = new Label();
            labFeels_like = new Label();
            label13 = new Label();
            labHumidity = new Label();
            pictureBox6 = new PictureBox();
            pictureBox2 = new PictureBox();
            labSunset = new Label();
            pictureBox4 = new PictureBox();
            labSunrise = new Label();
            pictureBox3 = new PictureBox();
            pictureBox5 = new PictureBox();
            labWindSpeed = new Label();
            labPressure = new Label();
            btnLocation = new Button();
            chart1 = new Chart();
            dataGridView1 = new DataGridView();
            labDistrict = new Label();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top;
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.Menu;
            label15.Location = new Point(278, 309);
            label15.Name = "label15";
            label15.Size = new Size(23, 35);
            label15.TabIndex = 67;
            label15.Text = "/";
            // 
            // labTemperature
            // 
            labTemperature.Anchor = AnchorStyles.Top;
            labTemperature.AutoSize = true;
            labTemperature.BackColor = Color.Transparent;
            labTemperature.Font = new Font("Times New Roman", 72F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labTemperature.ForeColor = Color.Transparent;
            labTemperature.Location = new Point(544, 110);
            labTemperature.Name = "labTemperature";
            labTemperature.Size = new Size(192, 135);
            labTemperature.TabIndex = 54;
            labTemperature.Text = "°C";
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.FlatStyle = FlatStyle.System;
            btnSearch.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(1072, 54);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(96, 44);
            btnSearch.TabIndex = 53;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // TBCity
            // 
            TBCity.Anchor = AnchorStyles.Top;
            TBCity.AutoCompleteCustomSource.AddRange(new string[] { "Quận 1", "Quận 2", "Quận 3", "Quận 4", "Quận 5", "Quận 6", "Quận 7", "Quận 8", "Quận 10", "Quận 11", "Quận 12", "Bình Tân", "Củ Chi", "Hóc Môn", "Bình Chánh", "Nhà Bè", "Cần Giờ", "Gò Vấp", "Bình Thạnh", "Tân Bình", "Tân Phú", "Phú Nhuận", "Thủ Đức", "An Giang", "Vũng Tàu", "Bạc Liêu", "Bắc Giang", "Bắc Kạn", "Bắc Ninh", "Bến Tre", "Bình Dương", "Bình Định", "Bình Phước", "Bình Thuận", "Cà Mau", "Cao Bằng", "Cần Thơ", "Đà Nẵng", "Đắk Lắk", "Đắk Nông", "Điện Biên", "Đồng Nai", "Đồng Tháp", "Gia Lai", "Hà Giang", "Hà Nam", "Hà Nội", "Hà Tĩnh", "Hải Dương", "Hải Phòng", "Hậu Giang", "Hòa Bình", "Hưng Yên", "Khánh Hòa", "Kiên Giang", "Kon Tum", "Lai Châu", "Lạng Sơn", "Lào Cai", "Lâm Đồng", "Tỉnh Long An", "Nam Định", "Tỉnh Nghệ An", "Tỉnh Ninh Bình", "Tỉnh Ninh Thuận", "Tỉnh Phú Thọ", "Tỉnh Phú Yên", "Tỉnh Quảng Bình", "Tỉnh Quảng Nam", "Tỉnh Quảng Ngãi", "Tỉnh Quảng Ninh", "Tỉnh Quảng Trị", "Tỉnh Sóc Trăng", "Sơn La", "Tây Ninh", "Tỉnh Thái Bình", "Thái Nguyên", "Thanh Hóa", "Thừa Thiên Huế", "Tỉnh Tiền Giang", "Thành Phố Hồ Chí Minh", "Tỉnh Trà Vinh", "Tỉnh Tuyên Quang", "Vĩnh Long", "Tỉnh Vĩnh Phúc", "Tỉnh Yên Bái", "============", "============", "Ấn Độ", "Aland Islands", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Áo", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Bỉ", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia ", "Bonaire, Sint Eustatius and Saba", "Bosnia  Herzegovina", "Botswana", "Brazil", "British Indian Ocean Territory", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cabo Verde", "Cambodia", "Cameroon", "Canada", "Cayman Islands", "Central African Republic", "Chad", "Chile", "Trung Quốc", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Cook Islands", "Costa Rica", "Croatia", "Cuba", "Curaçao", "Cyprus", "Cộng hòa Séc", "Côte d'Ivoire", "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Ai Cập", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Federated States of Micronesia", "Fiji", "Phần Lan", "Former Yugoslav Republic of Macedonia", "Pháp", "French Guiana", "French Polynesia", "French Southern Territories", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Holy See", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran ", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Nhật Bản", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Lào", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Triều Tiên", "Northern Mariana Islands", "Na Uy", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Ba Lan", "Bồ Đào Nha", "Puerto Rico", "Qatar", "Republic of the Congo", "Romania", "Nga", "Rwanda", "Réunion", "Saint Barthélemy", "Saint Helena, Ascension and Tristan da Cunha", "Saint Kitts and Nevis", "Saint Lucia", "Saint Martin", "Saint Pierre and Miquelon", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Sint Maarten", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "Nam Phi", "South Georgia and the South Sandwich Islands", "Hàn Quốc", "South Sudan", "Tây Ban Nha", "Sri Lanka", "State of Palestine", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Thổ Nhĩ Kì", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "Vương quốc Anh", "England", "Scotland", "Wales", "Northern Ireland", "United States Minor Outlying Islands", "Hoa Kỳ", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Việt Nam", "Virgin Islands ", "Virgin Islands", "Wallis and Futuna", "Western Sahara", "Yemen", "Zambia", "Zimbabwe" });
            TBCity.BackColor = Color.White;
            TBCity.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TBCity.ForeColor = Color.Black;
            TBCity.Location = new Point(366, 54);
            TBCity.Margin = new Padding(3, 4, 3, 4);
            TBCity.Multiline = true;
            TBCity.Name = "TBCity";
            TBCity.Size = new Size(686, 43);
            TBCity.TabIndex = 52;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(200, 66);
            label1.Name = "label1";
            label1.Size = new Size(128, 25);
            label1.TabIndex = 51;
            label1.Text = "Nơi cần tìm";
            // 
            // labDateTime2
            // 
            labDateTime2.AutoSize = true;
            labDateTime2.BackColor = Color.Transparent;
            labDateTime2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labDateTime2.ForeColor = Color.White;
            labDateTime2.Location = new Point(200, 18);
            labDateTime2.Name = "labDateTime2";
            labDateTime2.Size = new Size(72, 25);
            labDateTime2.TabIndex = 57;
            labDateTime2.Text = "Ngày:";
            // 
            // picIcon
            // 
            picIcon.Anchor = AnchorStyles.Top;
            picIcon.BackColor = Color.Transparent;
            picIcon.ErrorImage = null;
            picIcon.Location = new Point(869, 115);
            picIcon.Margin = new Padding(3, 4, 3, 4);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(180, 176);
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.TabIndex = 55;
            picIcon.TabStop = false;
            // 
            // labDateTime
            // 
            labDateTime.AutoSize = true;
            labDateTime.BackColor = Color.Transparent;
            labDateTime.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labDateTime.ForeColor = Color.White;
            labDateTime.Location = new Point(40, 18);
            labDateTime.Name = "labDateTime";
            labDateTime.Size = new Size(56, 25);
            labDateTime.TabIndex = 56;
            labDateTime.Text = "Giờ:";
            // 
            // labTemp_min
            // 
            labTemp_min.Anchor = AnchorStyles.Top;
            labTemp_min.AutoSize = true;
            labTemp_min.BackColor = Color.Transparent;
            labTemp_min.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labTemp_min.ForeColor = SystemColors.Menu;
            labTemp_min.Location = new Point(199, 309);
            labTemp_min.Name = "labTemp_min";
            labTemp_min.Size = new Size(66, 35);
            labTemp_min.TabIndex = 49;
            labTemp_min.Text = "N/A";
            // 
            // labTemp_max
            // 
            labTemp_max.Anchor = AnchorStyles.Top;
            labTemp_max.AutoSize = true;
            labTemp_max.BackColor = Color.Transparent;
            labTemp_max.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labTemp_max.ForeColor = SystemColors.Menu;
            labTemp_max.Location = new Point(296, 309);
            labTemp_max.Name = "labTemp_max";
            labTemp_max.Size = new Size(66, 35);
            labTemp_max.TabIndex = 50;
            labTemp_max.Text = "N/A";
            // 
            // labAdvice
            // 
            labAdvice.AutoSize = true;
            labAdvice.BackColor = Color.Transparent;
            labAdvice.Font = new Font("Times New Roman", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labAdvice.ForeColor = SystemColors.Menu;
            labAdvice.Location = new Point(188, 510);
            labAdvice.Name = "labAdvice";
            labAdvice.Size = new Size(235, 49);
            labAdvice.TabIndex = 62;
            labAdvice.Text = "Lời khuyên";
            // 
            // labDetail2
            // 
            labDetail2.AutoSize = true;
            labDetail2.BackColor = Color.Transparent;
            labDetail2.Font = new Font("Times New Roman", 19.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labDetail2.ForeColor = SystemColors.Menu;
            labDetail2.Location = new Point(189, 448);
            labDetail2.Name = "labDetail2";
            labDetail2.Size = new Size(73, 37);
            labDetail2.TabIndex = 4;
            labDetail2.Text = "N/A";
            // 
            // labFeels_like
            // 
            labFeels_like.Anchor = AnchorStyles.Top;
            labFeels_like.AutoSize = true;
            labFeels_like.BackColor = Color.Transparent;
            labFeels_like.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labFeels_like.ForeColor = SystemColors.Menu;
            labFeels_like.Location = new Point(386, 380);
            labFeels_like.Name = "labFeels_like";
            labFeels_like.Size = new Size(66, 35);
            labFeels_like.TabIndex = 15;
            labFeels_like.Text = "N/A";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top;
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = SystemColors.Menu;
            label13.Location = new Point(190, 380);
            label13.Name = "label13";
            label13.Size = new Size(200, 35);
            label13.TabIndex = 14;
            label13.Text = "Cảm giác như ";
            // 
            // labHumidity
            // 
            labHumidity.Anchor = AnchorStyles.Top;
            labHumidity.AutoSize = true;
            labHumidity.BackColor = Color.Transparent;
            labHumidity.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labHumidity.ForeColor = SystemColors.Window;
            labHumidity.Location = new Point(600, 380);
            labHumidity.Name = "labHumidity";
            labHumidity.Size = new Size(51, 25);
            labHumidity.TabIndex = 69;
            labHumidity.Text = "N/A";
            //
            // labWindSpeed
            // 
            labWindSpeed.Anchor = AnchorStyles.Top;
            labWindSpeed.AutoSize = true;
            labWindSpeed.BackColor = Color.Transparent;
            labWindSpeed.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labWindSpeed.ForeColor = SystemColors.Window;
            labWindSpeed.Location = new Point(600, 309);
            labWindSpeed.Name = "labWindSpeed";
            labWindSpeed.Size = new Size(51, 25);
            labWindSpeed.TabIndex = 11;
            labWindSpeed.Text = "N/A";
            // 
            // labPressure
            // 
            labPressure.Anchor = AnchorStyles.Top;
            labPressure.AutoSize = true;
            labPressure.BackColor = Color.Transparent;
            labPressure.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labPressure.ForeColor = SystemColors.Window;
            labPressure.Location = new Point(600, 460);
            labPressure.Name = "labPressure";
            labPressure.Size = new Size(51, 25);
            labPressure.TabIndex = 13;
            labPressure.Text = "N/A";
            // 
            // 
            // pictureBox6
            // 
            pictureBox6.Anchor = AnchorStyles.Top;
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox6.ErrorImage = null;
            pictureBox6.Location = new Point(912, 378);
            pictureBox6.Margin = new Padding(3, 4, 3, 4);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(58, 58);
            pictureBox6.TabIndex = 20;
            pictureBox6.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Cursor = Cursors.AppStarting;
            pictureBox2.ErrorImage = null;
            pictureBox2.Location = new Point(624, 378);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(52, 58);
            pictureBox2.TabIndex = 68;
            pictureBox2.TabStop = false;
            // 
            // labSunset
            // 
            labSunset.Anchor = AnchorStyles.Top;
            labSunset.AutoSize = true;
            labSunset.BackColor = Color.Transparent;
            labSunset.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labSunset.ForeColor = SystemColors.Window;
            labSunset.Location = new Point(320, 242);
            labSunset.Name = "labSunset";
            labSunset.Size = new Size(51, 25);
            labSunset.TabIndex = 9;
            labSunset.Text = "N/A";
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Top;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.ErrorImage = null;
            pictureBox4.Location = new Point(771, 378);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(52, 58);
            pictureBox4.TabIndex = 10;
            pictureBox4.TabStop = false;
            // 
            // labSunrise
            // 
            labSunrise.Anchor = AnchorStyles.Top;
            labSunrise.AutoSize = true;
            labSunrise.BackColor = Color.Transparent;
            labSunrise.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labSunrise.ForeColor = SystemColors.Window;
            labSunrise.Location = new Point(191, 242);
            labSunrise.Name = "labSunrise";
            labSunrise.Size = new Size(51, 25);
            labSunrise.TabIndex = 6;
            labSunrise.Text = "N/A";
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.ErrorImage = null;
            pictureBox3.Location = new Point(316, 181);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(52, 58);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Anchor = AnchorStyles.Top;
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox5.ErrorImage = null;
            pictureBox5.Location = new Point(197, 181);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(54, 58);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // btnLocation
            // 
            btnLocation.Anchor = AnchorStyles.Top;
            btnLocation.BackColor = Color.Transparent;
            btnLocation.FlatStyle = FlatStyle.System;
            btnLocation.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLocation.ForeColor = Color.White;
            btnLocation.Location = new Point(1174, 54);
            btnLocation.Margin = new Padding(3, 4, 3, 4);
            btnLocation.Name = "btnLocation";
            btnLocation.Size = new Size(96, 44);
            btnLocation.TabIndex = 70;
            btnLocation.Text = "Vị trí";
            btnLocation.UseVisualStyleBackColor = false;
            btnLocation.Click += btnLocation_Click;
            // 
            // chart1
            // 
            chart1.Anchor = AnchorStyles.Bottom;
            chart1.BackImageTransparentColor = Color.Transparent;
            chart1.BackSecondaryColor = Color.Transparent;
            chart1.BorderlineColor = Color.Transparent;
            chartArea1.AlignmentOrientation = AreaAlignmentOrientations.Vertical | AreaAlignmentOrientations.Horizontal;
            chartArea1.BackColor = Color.Transparent;
            chartArea1.BackImageTransparentColor = Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Cursor = Cursors.Hand;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(12, 610);
            chart1.Margin = new Padding(3, 4, 3, 4);
            chart1.Name = "chart1";
            chart1.Palette = ChartColorPalette.None;
            chart1.RightToLeft = RightToLeft.No;
            series1.BackGradientStyle = GradientStyle.TopBottom;
            series1.BackImageTransparentColor = Color.Transparent;
            series1.BackSecondaryColor = Color.Transparent;
            series1.BorderColor = Color.Transparent;
            series1.ChartArea = "ChartArea1";
            series1.Color = Color.DodgerBlue;
            series1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            series1.LabelBackColor = Color.Transparent;
            series1.LabelBorderColor = Color.Transparent;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = Color.Transparent;
            series1.MarkerColor = Color.Transparent;
            series1.MarkerImageTransparentColor = Color.Transparent;
            series1.Name = "Nhiệt độ";
            series1.ShadowColor = Color.Transparent;
            chart1.Series.Add(series1);
            chart1.Size = new Size(827, 346);
            chart1.TabIndex = 71;
            chart1.Text = "chart1";
            chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Bottom;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.ImeMode = ImeMode.On;
            dataGridView1.Location = new Point(845, 610);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(565, 346);
            dataGridView1.TabIndex = 72;
            // 
            // labDistrict
            // 
            labDistrict.Anchor = AnchorStyles.Top;
            labDistrict.AutoSize = true;
            labDistrict.BackColor = Color.Transparent;
            labDistrict.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labDistrict.ForeColor = Color.Transparent;
            labDistrict.Location = new Point(1170, 11);
            labDistrict.Name = "labDistrict";
            labDistrict.Size = new Size(87, 20);
            labDistrict.TabIndex = 73;
            labDistrict.Text = "Thành phố";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaption;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1430, 960);
            Controls.Add(labDistrict);
            Controls.Add(dataGridView1);
            Controls.Add(chart1);
            Controls.Add(btnLocation);
            Controls.Add(labHumidity);
            Controls.Add(pictureBox6);
            Controls.Add(label15);
            Controls.Add(pictureBox2);
            Controls.Add(labSunset);
            Controls.Add(labTemperature);
            Controls.Add(pictureBox4);
            Controls.Add(btnSearch);
            Controls.Add(labSunrise);
            Controls.Add(TBCity);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(pictureBox5);
            Controls.Add(labWindSpeed);
            Controls.Add(labDateTime2);
            Controls.Add(labPressure);
            Controls.Add(picIcon);
            Controls.Add(labDateTime);
            Controls.Add(labTemp_min);
            Controls.Add(labTemp_max);
            Controls.Add(labAdvice);
            Controls.Add(labFeels_like);
            Controls.Add(labDetail2);
            Controls.Add(label13);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WeatherApplication";
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labTemperature;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox TBCity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labDateTime2;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label labDateTime;
        private System.Windows.Forms.Label labTemp_min;
        private System.Windows.Forms.Label labTemp_max;
        private System.Windows.Forms.Label labAdvice;
        private System.Windows.Forms.Label labFeels_like;
        private System.Windows.Forms.Label labDetail2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label labHumidity;
        private System.Windows.Forms.Label labSunset;
        private System.Windows.Forms.Label labSunrise;
        private System.Windows.Forms.Label labWindSpeed;
        private System.Windows.Forms.Label labPressure;
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.Label labDistrict;

    }
}

