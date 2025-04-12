using Microsoft.Web.WebView2.WinForms;

namespace Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            dataGridView1 = new DataGridView();
            labDistrict = new Label();
            dataGridView2 = new DataGridView();
            webViewWeather = new WebView2();
            panelChatContainer = new Panel();
            userInput = new TextBox();
            sendButton = new Button();
            clearChatButton = new Button();
            chatHeader = new Panel();
            chatTitle = new Label();
            closeChat = new Label();
            resizeIcon = new PictureBox();
            chatContent = new RichTextBox();
            fab = new Label();
            fabTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewWeather).BeginInit();
            panelChatContainer.SuspendLayout();
            chatHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resizeIcon).BeginInit();
            fab.SuspendLayout();
            SuspendLayout();
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top;
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.Menu;
            label15.Location = new Point(673, 438);
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
            labTemperature.Location = new Point(209, 168);
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
            btnSearch.Location = new Point(1237, 79);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(121, 62);
            btnSearch.TabIndex = 53;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // TBCity
            // 
            TBCity.Anchor = AnchorStyles.Top;
            TBCity.BackColor = Color.White;
            TBCity.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TBCity.ForeColor = Color.Black;
            TBCity.Location = new Point(208, 79);
            TBCity.Margin = new Padding(3, 4, 3, 4);
            TBCity.Multiline = true;
            TBCity.Name = "TBCity";
            TBCity.Size = new Size(968, 62);
            TBCity.TabIndex = 52;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(38, 92);
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
            labDateTime2.Location = new Point(237, 18);
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
            picIcon.Location = new Point(610, 168);
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
            labDateTime.Location = new Point(38, 18);
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
            labTemp_min.Location = new Point(597, 440);
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
            labTemp_max.Location = new Point(690, 440);
            labTemp_max.Name = "labTemp_max";
            labTemp_max.Size = new Size(66, 35);
            labTemp_max.TabIndex = 50;
            labTemp_max.Text = "N/A";
            // 
            // labAdvice
            // 
            labAdvice.BackColor = Color.Transparent;
            labAdvice.Font = new Font("Times New Roman", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labAdvice.ForeColor = SystemColors.Menu;
            labAdvice.Location = new Point(888, 844);
            labAdvice.Name = "labAdvice";
            labAdvice.Size = new Size(470, 58);
            labAdvice.TabIndex = 62;
            labAdvice.Text = "Lời khuyên";
            // 
            // labDetail2
            // 
            labDetail2.AutoSize = true;
            labDetail2.BackColor = Color.Transparent;
            labDetail2.Font = new Font("Times New Roman", 19.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labDetail2.ForeColor = SystemColors.Menu;
            labDetail2.Location = new Point(201, 368);
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
            labFeels_like.Location = new Point(430, 440);
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
            label13.Location = new Point(202, 440);
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
            labHumidity.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labHumidity.ForeColor = SystemColors.Window;
            labHumidity.Location = new Point(221, 584);
            labHumidity.Name = "labHumidity";
            labHumidity.Size = new Size(51, 25);
            labHumidity.TabIndex = 69;
            labHumidity.Text = "N/A";
            // 
            // pictureBox6
            // 
            pictureBox6.Anchor = AnchorStyles.Top;
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.BackgroundImage = (Image)resources.GetObject("pictureBox6.BackgroundImage");
            pictureBox6.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox6.ErrorImage = null;
            pictureBox6.Location = new Point(610, 522);
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
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Cursor = Cursors.AppStarting;
            pictureBox2.ErrorImage = null;
            pictureBox2.Location = new Point(227, 522);
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
            labSunset.Location = new Point(38, 584);
            labSunset.Name = "labSunset";
            labSunset.Size = new Size(51, 25);
            labSunset.TabIndex = 9;
            labSunset.Text = "N/A";
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Top;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.BackgroundImage = (Image)resources.GetObject("pictureBox4.BackgroundImage");
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.ErrorImage = null;
            pictureBox4.Location = new Point(428, 522);
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
            labSunrise.Location = new Point(38, 448);
            labSunrise.Name = "labSunrise";
            labSunrise.Size = new Size(51, 25);
            labSunrise.TabIndex = 6;
            labSunrise.Text = "N/A";
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.ErrorImage = null;
            pictureBox3.Location = new Point(44, 522);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(54, 58);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Anchor = AnchorStyles.Top;
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.BackgroundImage = (Image)resources.GetObject("pictureBox5.BackgroundImage");
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox5.ErrorImage = null;
            pictureBox5.Location = new Point(44, 385);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(54, 58);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // labWindSpeed
            // 
            labWindSpeed.Anchor = AnchorStyles.Top;
            labWindSpeed.AutoSize = true;
            labWindSpeed.BackColor = Color.Transparent;
            labWindSpeed.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labWindSpeed.ForeColor = SystemColors.Window;
            labWindSpeed.Location = new Point(391, 584);
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
            labPressure.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labPressure.ForeColor = SystemColors.Window;
            labPressure.Location = new Point(583, 584);
            labPressure.Name = "labPressure";
            labPressure.Size = new Size(51, 25);
            labPressure.TabIndex = 13;
            labPressure.Text = "N/A";
            // 
            // btnLocation
            // 
            btnLocation.Anchor = AnchorStyles.Top;
            btnLocation.BackColor = Color.Transparent;
            btnLocation.FlatStyle = FlatStyle.System;
            btnLocation.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLocation.ForeColor = Color.White;
            btnLocation.Location = new Point(1412, 79);
            btnLocation.Margin = new Padding(3, 4, 3, 4);
            btnLocation.Name = "btnLocation";
            btnLocation.Size = new Size(117, 62);
            btnLocation.TabIndex = 70;
            btnLocation.Text = "Vị trí";
            btnLocation.UseVisualStyleBackColor = false;
            btnLocation.Click += btnLocation_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Bottom;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.ImeMode = ImeMode.On;
            dataGridView1.Location = new Point(887, 538);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(664, 293);
            dataGridView1.TabIndex = 72;
            // 
            // labDistrict
            // 
            labDistrict.Anchor = AnchorStyles.Top;
            labDistrict.AutoSize = true;
            labDistrict.BackColor = Color.Transparent;
            labDistrict.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labDistrict.ForeColor = Color.Transparent;
            labDistrict.Location = new Point(1221, 23);
            labDistrict.Name = "labDistrict";
            labDistrict.Size = new Size(87, 20);
            labDistrict.TabIndex = 73;
            labDistrict.Text = "Thành phố";
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(887, 209);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(664, 282);
            dataGridView2.TabIndex = 74;
            // 
            // webViewWeather
            // 
            webViewWeather.AllowExternalDrop = true;
            webViewWeather.CreationProperties = null;
            webViewWeather.DefaultBackgroundColor = Color.White;
            webViewWeather.Location = new Point(0, 0);
            webViewWeather.Name = "webViewWeather";
            webViewWeather.Size = new Size(0, 0);
            webViewWeather.TabIndex = 75;
            webViewWeather.ZoomFactor = 1D;
            // 
            // panelChatContainer
            // 
            panelChatContainer.BackColor = Color.White;
            panelChatContainer.BorderStyle = BorderStyle.FixedSingle;
            panelChatContainer.Controls.Add(chatContent);
            panelChatContainer.Controls.Add(userInput);
            panelChatContainer.Controls.Add(sendButton);
            panelChatContainer.Controls.Add(clearChatButton);
            panelChatContainer.Controls.Add(chatHeader);
            panelChatContainer.Controls.Add(resizeIcon);
            panelChatContainer.Location = new Point(1, 448);
            panelChatContainer.Name = "panelChatContainer";
            panelChatContainer.Size = new Size(400, 500);
            panelChatContainer.TabIndex = 0;
            panelChatContainer.Visible = false;
            // 
            // userInput
            // 
            userInput.Location = new Point(10, 430);
            userInput.Multiline = true;
            userInput.Name = "userInput";
            userInput.ScrollBars = ScrollBars.Vertical;
            userInput.Size = new Size(240, 40);
            userInput.TabIndex = 1;
            // 
            // sendButton
            // 
            sendButton.BackColor = Color.FromArgb(0, 51, 102);
            sendButton.ForeColor = Color.White;
            sendButton.Location = new Point(260, 430);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(60, 40);
            sendButton.TabIndex = 2;
            sendButton.Text = "GỬI";
            sendButton.UseVisualStyleBackColor = false;
            // 
            // clearChatButton
            // 
            clearChatButton.BackColor = Color.FromArgb(244, 67, 54);
            clearChatButton.ForeColor = Color.White;
            clearChatButton.Location = new Point(330, 430);
            clearChatButton.Name = "clearChatButton";
            clearChatButton.Size = new Size(60, 40);
            clearChatButton.TabIndex = 3;
            clearChatButton.Text = "Xóa";
            clearChatButton.UseVisualStyleBackColor = false;
            // 
            // chatHeader
            // 
            chatHeader.BackColor = Color.FromArgb(0, 51, 102);
            chatHeader.Controls.Add(chatTitle);
            chatHeader.Controls.Add(closeChat);
            chatHeader.Location = new Point(0, 0);
            chatHeader.Name = "chatHeader";
            chatHeader.Size = new Size(400, 40);
            chatHeader.TabIndex = 4;
            // 
            // chatTitle
            // 
            chatTitle.AutoSize = true;
            chatTitle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            chatTitle.ForeColor = Color.White;
            chatTitle.Location = new Point(10, 10);
            chatTitle.Name = "chatTitle";
            chatTitle.Size = new Size(74, 20);
            chatTitle.TabIndex = 0;
            chatTitle.Text = "Chatbot";
            // 
            // closeChat
            // 
            closeChat.AutoSize = true;
            closeChat.Cursor = Cursors.Hand;
            closeChat.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            closeChat.ForeColor = Color.White;
            closeChat.Location = new Point(370, 5);
            closeChat.Name = "closeChat";
            closeChat.Size = new Size(31, 31);
            closeChat.TabIndex = 1;
            closeChat.Text = "×";
            // 
            // resizeIcon
            // 
            resizeIcon.Location = new Point(0, 0);
            resizeIcon.Name = "resizeIcon";
            resizeIcon.Size = new Size(100, 50);
            resizeIcon.TabIndex = 5;
            resizeIcon.TabStop = false;
            // 
            // chatContent
            // 
            chatContent.BackColor = Color.FromArgb(249, 249, 249);
            chatContent.BorderStyle = BorderStyle.None;
            chatContent.Location = new Point(-1, 39);
            chatContent.Name = "chatContent";
            chatContent.ReadOnly = true;
            chatContent.Size = new Size(398, 380);
            chatContent.TabIndex = 0;
            chatContent.Text = "";
            // 
            // fab
            // 
            fab.BackColor = Color.White;
            fab.Controls.Add(fabTitle);
            fab.Cursor = Cursors.Hand;
            fab.Image = (Image)resources.GetObject("fab.Image");
            fab.Location = new Point(12, 862);
            fab.Name = "fab";
            fab.Size = new Size(60, 60);
            fab.TabIndex = 0;
            // 
            // fabTitle
            // 
            fabTitle.AutoSize = true;
            fabTitle.BackColor = Color.FromArgb(218, 0, 24);
            fabTitle.ForeColor = Color.White;
            fabTitle.Location = new Point(-70, 20);
            fabTitle.Name = "fabTitle";
            fabTitle.Size = new Size(75, 20);
            fabTitle.TabIndex = 1;
            fabTitle.Text = "Chat ngay";
            fabTitle.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaption;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1710, 951);
            Controls.Add(fab);
            Controls.Add(panelChatContainer);
            Controls.Add(dataGridView2);
            Controls.Add(labDistrict);
            Controls.Add(dataGridView1);
            Controls.Add(btnLocation);
            Controls.Add(labAdvice);
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
            Controls.Add(labFeels_like);
            Controls.Add(labDetail2);
            Controls.Add(label13);
            Controls.Add(webViewWeather);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewWeather).EndInit();
            panelChatContainer.ResumeLayout(false);
            panelChatContainer.PerformLayout();
            chatHeader.ResumeLayout(false);
            chatHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resizeIcon).EndInit();
            fab.ResumeLayout(false);
            fab.PerformLayout();
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labDistrict;
        private DataGridView dataGridView2;
        private WebView2 webViewWeather;

    }
}

