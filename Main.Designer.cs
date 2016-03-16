namespace qtp
{
    partial class Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblServerTime = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBooking = new System.Windows.Forms.TabPage();
            this.txtBoxMarket = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnInsertBook = new System.Windows.Forms.Button();
            this.txtBoxWritePath = new System.Windows.Forms.TextBox();
            this.checkIfWriteFile = new System.Windows.Forms.CheckBox();
            this.checkIfMQ = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBoxMaxBars = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBoxInterval = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ddInterval = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBoxSymbol = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ddRegion = new System.Windows.Forms.ComboBox();
            this.gridBooking = new System.Windows.Forms.DataGridView();
            this.tabSymbolWatch = new System.Windows.Forms.TabPage();
            this.AddSymbols = new System.Windows.Forms.Button();
            this.gridStocks = new System.Windows.Forms.DataGridView();
            this.btnQueryStocks = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStocks = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTickWatch = new System.Windows.Forms.TabPage();
            this.GridBest5Bid = new System.Windows.Forms.DataGridView();
            this.GridBest5Ask = new System.Windows.Forms.DataGridView();
            this.GridTick = new System.Windows.Forms.DataGridView();
            this.btnTick = new System.Windows.Forms.Button();
            this.txtTick = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabProductList = new System.Windows.Forms.TabPage();
            this.listStockInfo = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnEnterMonitor = new System.Windows.Forms.Button();
            this.btnLeaveMonitor = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblConnect = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.timerServerTime = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.lblSendRequestServerTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGetServerTime = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBooking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooking)).BeginInit();
            this.tabSymbolWatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStocks)).BeginInit();
            this.tabTickWatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Bid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Ask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTick)).BeginInit();
            this.tabProductList.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPassword.Location = new System.Drawing.Point(30, 117);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(97, 19);
            this.lblPassword.TabIndex = 15;
            this.lblPassword.Text = "Password：";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(109, 113);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(175, 30);
            this.txtPassWord.TabIndex = 2;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAccount.Location = new System.Drawing.Point(30, 85);
            this.lblAccount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(89, 19);
            this.lblAccount.TabIndex = 13;
            this.lblAccount.Text = "Account：";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAccount.Location = new System.Drawing.Point(109, 78);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(5);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(175, 30);
            this.txtAccount.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblServerTime);
            this.groupBox1.Location = new System.Drawing.Point(470, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 62);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server時間";
            // 
            // lblServerTime
            // 
            this.lblServerTime.AutoSize = true;
            this.lblServerTime.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblServerTime.Location = new System.Drawing.Point(40, 27);
            this.lblServerTime.Name = "lblServerTime";
            this.lblServerTime.Size = new System.Drawing.Size(0, 24);
            this.lblServerTime.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBooking);
            this.tabControl1.Controls.Add(this.tabSymbolWatch);
            this.tabControl1.Controls.Add(this.tabTickWatch);
            this.tabControl1.Controls.Add(this.tabProductList);
            this.tabControl1.Location = new System.Drawing.Point(24, 157);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(840, 407);
            this.tabControl1.TabIndex = 17;
            // 
            // tabBooking
            // 
            this.tabBooking.Controls.Add(this.txtBoxMarket);
            this.tabBooking.Controls.Add(this.label12);
            this.tabBooking.Controls.Add(this.btnInsertBook);
            this.tabBooking.Controls.Add(this.txtBoxWritePath);
            this.tabBooking.Controls.Add(this.checkIfWriteFile);
            this.tabBooking.Controls.Add(this.checkIfMQ);
            this.tabBooking.Controls.Add(this.label11);
            this.tabBooking.Controls.Add(this.txtBoxMaxBars);
            this.tabBooking.Controls.Add(this.label10);
            this.tabBooking.Controls.Add(this.txtBoxInterval);
            this.tabBooking.Controls.Add(this.label9);
            this.tabBooking.Controls.Add(this.ddInterval);
            this.tabBooking.Controls.Add(this.label8);
            this.tabBooking.Controls.Add(this.txtBoxSymbol);
            this.tabBooking.Controls.Add(this.label7);
            this.tabBooking.Controls.Add(this.label6);
            this.tabBooking.Controls.Add(this.ddRegion);
            this.tabBooking.Controls.Add(this.gridBooking);
            this.tabBooking.Location = new System.Drawing.Point(4, 28);
            this.tabBooking.Name = "tabBooking";
            this.tabBooking.Padding = new System.Windows.Forms.Padding(3);
            this.tabBooking.Size = new System.Drawing.Size(832, 375);
            this.tabBooking.TabIndex = 4;
            this.tabBooking.Text = "商品訂閱管理";
            this.tabBooking.UseVisualStyleBackColor = true;
            // 
            // txtBoxMarket
            // 
            this.txtBoxMarket.Location = new System.Drawing.Point(321, 11);
            this.txtBoxMarket.Name = "txtBoxMarket";
            this.txtBoxMarket.Size = new System.Drawing.Size(100, 30);
            this.txtBoxMarket.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(239, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 19);
            this.label12.TabIndex = 17;
            this.label12.Text = "市場代碼";
            // 
            // btnInsertBook
            // 
            this.btnInsertBook.Location = new System.Drawing.Point(635, 13);
            this.btnInsertBook.Name = "btnInsertBook";
            this.btnInsertBook.Size = new System.Drawing.Size(75, 23);
            this.btnInsertBook.TabIndex = 16;
            this.btnInsertBook.Text = "新增";
            this.btnInsertBook.UseVisualStyleBackColor = true;
            this.btnInsertBook.Click += new System.EventHandler(this.btnInsertBook_Click);
            // 
            // txtBoxWritePath
            // 
            this.txtBoxWritePath.Location = new System.Drawing.Point(273, 77);
            this.txtBoxWritePath.Name = "txtBoxWritePath";
            this.txtBoxWritePath.Size = new System.Drawing.Size(525, 30);
            this.txtBoxWritePath.TabIndex = 15;
            // 
            // checkIfWriteFile
            // 
            this.checkIfWriteFile.AutoSize = true;
            this.checkIfWriteFile.Location = new System.Drawing.Point(181, 81);
            this.checkIfWriteFile.Name = "checkIfWriteFile";
            this.checkIfWriteFile.Size = new System.Drawing.Size(107, 23);
            this.checkIfWriteFile.TabIndex = 14;
            this.checkIfWriteFile.Text = "寫出檔案";
            this.checkIfWriteFile.UseVisualStyleBackColor = true;
            // 
            // checkIfMQ
            // 
            this.checkIfMQ.AutoSize = true;
            this.checkIfMQ.Location = new System.Drawing.Point(34, 82);
            this.checkIfMQ.Name = "checkIfMQ";
            this.checkIfMQ.Size = new System.Drawing.Size(164, 23);
            this.checkIfMQ.TabIndex = 13;
            this.checkIfMQ.Text = "推播至訊息佇列";
            this.checkIfMQ.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(603, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 19);
            this.label11.TabIndex = 12;
            // 
            // txtBoxMaxBars
            // 
            this.txtBoxMaxBars.Location = new System.Drawing.Point(431, 47);
            this.txtBoxMaxBars.Name = "txtBoxMaxBars";
            this.txtBoxMaxBars.Size = new System.Drawing.Size(61, 30);
            this.txtBoxMaxBars.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(284, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(174, 19);
            this.label10.TabIndex = 10;
            this.label10.Text = "策略最大使用K棒數";
            // 
            // txtBoxInterval
            // 
            this.txtBoxInterval.Location = new System.Drawing.Point(97, 47);
            this.txtBoxInterval.Name = "txtBoxInterval";
            this.txtBoxInterval.Size = new System.Drawing.Size(39, 30);
            this.txtBoxInterval.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(239, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 19);
            this.label9.TabIndex = 8;
            // 
            // ddInterval
            // 
            this.ddInterval.FormattingEnabled = true;
            this.ddInterval.Location = new System.Drawing.Point(142, 47);
            this.ddInterval.Name = "ddInterval";
            this.ddInterval.Size = new System.Drawing.Size(121, 26);
            this.ddInterval.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "分析週期";
            // 
            // txtBoxSymbol
            // 
            this.txtBoxSymbol.Location = new System.Drawing.Point(512, 11);
            this.txtBoxSymbol.Name = "txtBoxSymbol";
            this.txtBoxSymbol.Size = new System.Drawing.Size(100, 30);
            this.txtBoxSymbol.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(439, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 4;
            this.label7.Text = "商品代碼";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 19);
            this.label6.TabIndex = 3;
            this.label6.Text = "  地區別";
            // 
            // ddRegion
            // 
            this.ddRegion.FormattingEnabled = true;
            this.ddRegion.Location = new System.Drawing.Point(97, 13);
            this.ddRegion.Name = "ddRegion";
            this.ddRegion.Size = new System.Drawing.Size(121, 26);
            this.ddRegion.TabIndex = 2;
            // 
            // gridBooking
            // 
            this.gridBooking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBooking.Location = new System.Drawing.Point(16, 107);
            this.gridBooking.Name = "gridBooking";
            this.gridBooking.RowTemplate.Height = 24;
            this.gridBooking.Size = new System.Drawing.Size(786, 248);
            this.gridBooking.TabIndex = 1;
            // 
            // tabSymbolWatch
            // 
            this.tabSymbolWatch.Controls.Add(this.AddSymbols);
            this.tabSymbolWatch.Controls.Add(this.gridStocks);
            this.tabSymbolWatch.Controls.Add(this.btnQueryStocks);
            this.tabSymbolWatch.Controls.Add(this.label2);
            this.tabSymbolWatch.Controls.Add(this.txtStocks);
            this.tabSymbolWatch.Controls.Add(this.label1);
            this.tabSymbolWatch.Location = new System.Drawing.Point(4, 28);
            this.tabSymbolWatch.Name = "tabSymbolWatch";
            this.tabSymbolWatch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSymbolWatch.Size = new System.Drawing.Size(832, 375);
            this.tabSymbolWatch.TabIndex = 0;
            this.tabSymbolWatch.Text = "報價";
            this.tabSymbolWatch.UseVisualStyleBackColor = true;
            // 
            // AddSymbols
            // 
            this.AddSymbols.Location = new System.Drawing.Point(586, 18);
            this.AddSymbols.Name = "AddSymbols";
            this.AddSymbols.Size = new System.Drawing.Size(105, 22);
            this.AddSymbols.TabIndex = 5;
            this.AddSymbols.Text = "新增商品";
            this.AddSymbols.UseVisualStyleBackColor = true;
            this.AddSymbols.Click += new System.EventHandler(this.AddSymbols_Click);
            // 
            // gridStocks
            // 
            this.gridStocks.AllowUserToAddRows = false;
            this.gridStocks.AllowUserToDeleteRows = false;
            this.gridStocks.AllowUserToResizeRows = false;
            this.gridStocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStocks.Location = new System.Drawing.Point(7, 47);
            this.gridStocks.Name = "gridStocks";
            this.gridStocks.ReadOnly = true;
            this.gridStocks.RowHeadersVisible = false;
            this.gridStocks.RowTemplate.Height = 24;
            this.gridStocks.Size = new System.Drawing.Size(697, 322);
            this.gridStocks.TabIndex = 4;
            this.gridStocks.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridStocks_CellPainting);
            // 
            // btnQueryStocks
            // 
            this.btnQueryStocks.Location = new System.Drawing.Point(495, 18);
            this.btnQueryStocks.Name = "btnQueryStocks";
            this.btnQueryStocks.Size = new System.Drawing.Size(75, 23);
            this.btnQueryStocks.TabIndex = 3;
            this.btnQueryStocks.Text = "查詢";
            this.btnQueryStocks.UseVisualStyleBackColor = true;
            this.btnQueryStocks.Click += new System.EventHandler(this.btnQueryStocks_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "( 多筆以逗號{,}區隔 )";
            // 
            // txtStocks
            // 
            this.txtStocks.Location = new System.Drawing.Point(79, 16);
            this.txtStocks.Name = "txtStocks";
            this.txtStocks.Size = new System.Drawing.Size(243, 30);
            this.txtStocks.TabIndex = 1;
            this.txtStocks.Text = "TX00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品代碼";
            // 
            // tabTickWatch
            // 
            this.tabTickWatch.Controls.Add(this.GridBest5Bid);
            this.tabTickWatch.Controls.Add(this.GridBest5Ask);
            this.tabTickWatch.Controls.Add(this.GridTick);
            this.tabTickWatch.Controls.Add(this.btnTick);
            this.tabTickWatch.Controls.Add(this.txtTick);
            this.tabTickWatch.Controls.Add(this.label3);
            this.tabTickWatch.Location = new System.Drawing.Point(4, 28);
            this.tabTickWatch.Name = "tabTickWatch";
            this.tabTickWatch.Padding = new System.Windows.Forms.Padding(3);
            this.tabTickWatch.Size = new System.Drawing.Size(832, 375);
            this.tabTickWatch.TabIndex = 1;
            this.tabTickWatch.Text = "Tick & Best5";
            this.tabTickWatch.UseVisualStyleBackColor = true;
            // 
            // GridBest5Bid
            // 
            this.GridBest5Bid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridBest5Bid.Location = new System.Drawing.Point(495, 192);
            this.GridBest5Bid.MultiSelect = false;
            this.GridBest5Bid.Name = "GridBest5Bid";
            this.GridBest5Bid.RowHeadersVisible = false;
            this.GridBest5Bid.RowTemplate.Height = 24;
            this.GridBest5Bid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GridBest5Bid.Size = new System.Drawing.Size(131, 159);
            this.GridBest5Bid.TabIndex = 6;
            // 
            // GridBest5Ask
            // 
            this.GridBest5Ask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridBest5Ask.Location = new System.Drawing.Point(495, 21);
            this.GridBest5Ask.MultiSelect = false;
            this.GridBest5Ask.Name = "GridBest5Ask";
            this.GridBest5Ask.RowHeadersVisible = false;
            this.GridBest5Ask.RowTemplate.Height = 24;
            this.GridBest5Ask.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GridBest5Ask.Size = new System.Drawing.Size(131, 159);
            this.GridBest5Ask.TabIndex = 5;
            // 
            // GridTick
            // 
            this.GridTick.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridTick.Location = new System.Drawing.Point(72, 49);
            this.GridTick.MultiSelect = false;
            this.GridTick.Name = "GridTick";
            this.GridTick.RowHeadersVisible = false;
            this.GridTick.RowTemplate.Height = 24;
            this.GridTick.Size = new System.Drawing.Size(390, 302);
            this.GridTick.TabIndex = 4;
            // 
            // btnTick
            // 
            this.btnTick.Location = new System.Drawing.Point(185, 17);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(75, 23);
            this.btnTick.TabIndex = 3;
            this.btnTick.Text = "接收報價";
            this.btnTick.UseVisualStyleBackColor = true;
            this.btnTick.Click += new System.EventHandler(this.btnTick_Click);
            // 
            // txtTick
            // 
            this.txtTick.Location = new System.Drawing.Point(79, 18);
            this.txtTick.Name = "txtTick";
            this.txtTick.Size = new System.Drawing.Size(100, 30);
            this.txtTick.TabIndex = 2;
            this.txtTick.Text = "TX00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "商品代碼";
            // 
            // tabProductList
            // 
            this.tabProductList.Controls.Add(this.listStockInfo);
            this.tabProductList.Controls.Add(this.button2);
            this.tabProductList.Location = new System.Drawing.Point(4, 28);
            this.tabProductList.Name = "tabProductList";
            this.tabProductList.Padding = new System.Windows.Forms.Padding(3);
            this.tabProductList.Size = new System.Drawing.Size(832, 375);
            this.tabProductList.TabIndex = 3;
            this.tabProductList.Text = "取得商品";
            this.tabProductList.UseVisualStyleBackColor = true;
            // 
            // listStockInfo
            // 
            this.listStockInfo.FormattingEnabled = true;
            this.listStockInfo.ItemHeight = 18;
            this.listStockInfo.Location = new System.Drawing.Point(79, 51);
            this.listStockInfo.Name = "listStockInfo";
            this.listStockInfo.Size = new System.Drawing.Size(565, 292);
            this.listStockInfo.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(217, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 36);
            this.button2.TabIndex = 0;
            this.button2.Text = "查詢";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnEnterMonitor
            // 
            this.btnEnterMonitor.Location = new System.Drawing.Point(292, 79);
            this.btnEnterMonitor.Name = "btnEnterMonitor";
            this.btnEnterMonitor.Size = new System.Drawing.Size(83, 26);
            this.btnEnterMonitor.TabIndex = 18;
            this.btnEnterMonitor.Text = "報價連線";
            this.btnEnterMonitor.UseVisualStyleBackColor = true;
            this.btnEnterMonitor.Click += new System.EventHandler(this.btnEnterMonitor_Click);
            // 
            // btnLeaveMonitor
            // 
            this.btnLeaveMonitor.Location = new System.Drawing.Point(292, 112);
            this.btnLeaveMonitor.Name = "btnLeaveMonitor";
            this.btnLeaveMonitor.Size = new System.Drawing.Size(83, 26);
            this.btnLeaveMonitor.TabIndex = 19;
            this.btnLeaveMonitor.Text = "中斷報價";
            this.btnLeaveMonitor.UseVisualStyleBackColor = true;
            this.btnLeaveMonitor.Click += new System.EventHandler(this.btnLeaveMonitor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblConnect);
            this.groupBox2.Location = new System.Drawing.Point(618, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(71, 62);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "訊號";
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblConnect.Location = new System.Drawing.Point(19, 24);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(39, 27);
            this.lblConnect.TabIndex = 0;
            this.lblConnect.Text = "●";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblMessage);
            this.groupBox3.Location = new System.Drawing.Point(33, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 61);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "訊息";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 27);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(47, 19);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "訊息";
            // 
            // timerServerTime
            // 
            this.timerServerTime.Interval = 60000;
            this.timerServerTime.Tick += new System.EventHandler(this.timerServerTime_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 19);
            this.label4.TabIndex = 23;
            this.label4.Text = "送出要求時間：";
            // 
            // lblSendRequestServerTime
            // 
            this.lblSendRequestServerTime.AutoSize = true;
            this.lblSendRequestServerTime.Location = new System.Drawing.Point(242, 187);
            this.lblSendRequestServerTime.Name = "lblSendRequestServerTime";
            this.lblSendRequestServerTime.Size = new System.Drawing.Size(0, 19);
            this.lblSendRequestServerTime.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(381, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 19);
            this.label5.TabIndex = 25;
            this.label5.Text = "收到Server時間CALLBACK：";
            // 
            // lblGetServerTime
            // 
            this.lblGetServerTime.AutoSize = true;
            this.lblGetServerTime.Location = new System.Drawing.Point(580, 116);
            this.lblGetServerTime.Name = "lblGetServerTime";
            this.lblGetServerTime.Size = new System.Drawing.Size(0, 19);
            this.lblGetServerTime.TabIndex = 26;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 634);
            this.Controls.Add(this.lblGetServerTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblSendRequestServerTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLeaveMonitor);
            this.Controls.Add(this.btnEnterMonitor);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.txtAccount);
            this.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuoteTester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabBooking.ResumeLayout(false);
            this.tabBooking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooking)).EndInit();
            this.tabSymbolWatch.ResumeLayout(false);
            this.tabSymbolWatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStocks)).EndInit();
            this.tabTickWatch.ResumeLayout(false);
            this.tabTickWatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Bid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridBest5Ask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTick)).EndInit();
            this.tabProductList.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSymbolWatch;
        private System.Windows.Forms.TabPage tabTickWatch;
        private System.Windows.Forms.Button btnEnterMonitor;
        private System.Windows.Forms.Button btnLeaveMonitor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStocks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQueryStocks;
        private System.Windows.Forms.Label lblServerTime;
        private System.Windows.Forms.Timer timerServerTime;
        private System.Windows.Forms.DataGridView gridStocks;
        private System.Windows.Forms.Button btnTick;
        private System.Windows.Forms.TextBox txtTick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView GridTick;
        private System.Windows.Forms.DataGridView GridBest5Ask;
        private System.Windows.Forms.DataGridView GridBest5Bid;
        private System.Windows.Forms.TabPage tabProductList;
        private System.Windows.Forms.ListBox listStockInfo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSendRequestServerTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGetServerTime;
        private System.Windows.Forms.TabPage tabBooking;
        private System.Windows.Forms.ComboBox ddRegion;
        private System.Windows.Forms.DataGridView gridBooking;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxSymbol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkIfMQ;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBoxMaxBars;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBoxInterval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxWritePath;
        private System.Windows.Forms.CheckBox checkIfWriteFile;
        private System.Windows.Forms.Button btnInsertBook;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBoxMarket;
        private System.Windows.Forms.Button AddSymbols;
    }
}

