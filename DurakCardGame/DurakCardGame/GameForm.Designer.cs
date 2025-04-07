namespace DurakCardGame
{
    partial class GameForm
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
            pnlPlayerHand = new Panel();
            pnlComputerHand = new Panel();
            pnlPlayArea = new Panel();
            btnTakeCards = new Button();
            btnEndTurn = new Button();
            btnNewGame = new Button();
            pnlDeck = new Panel();
            lblStatus = new Label();
            lblTrumpSuit = new Label();
            lblTurnStatus = new Label();
            btnRestartGame = new Button();
            SuspendLayout();
            // 
            // pnlPlayerHand
            // 
            pnlPlayerHand.BackColor = Color.PaleGreen;
            pnlPlayerHand.BorderStyle = BorderStyle.Fixed3D;
            pnlPlayerHand.Location = new Point(135, 525);
            pnlPlayerHand.Name = "pnlPlayerHand";
            pnlPlayerHand.Size = new Size(720, 120);
            pnlPlayerHand.TabIndex = 1;
            // 
            // pnlComputerHand
            // 
            pnlComputerHand.BackColor = Color.PaleGreen;
            pnlComputerHand.BorderStyle = BorderStyle.Fixed3D;
            pnlComputerHand.Location = new Point(135, 37);
            pnlComputerHand.Name = "pnlComputerHand";
            pnlComputerHand.Size = new Size(720, 120);
            pnlComputerHand.TabIndex = 2;
            // 
            // pnlPlayArea
            // 
            pnlPlayArea.BackColor = Color.DarkGreen;
            pnlPlayArea.Location = new Point(235, 209);
            pnlPlayArea.Name = "pnlPlayArea";
            pnlPlayArea.Size = new Size(568, 258);
            pnlPlayArea.TabIndex = 3;
            // 
            // btnTakeCards
            // 
            btnTakeCards.Location = new Point(843, 245);
            btnTakeCards.Name = "btnTakeCards";
            btnTakeCards.Size = new Size(150, 30);
            btnTakeCards.TabIndex = 4;
            btnTakeCards.Text = "Take Cards";
            btnTakeCards.UseVisualStyleBackColor = true;
            btnTakeCards.Click += btnTakeCards_Click_1;
            // 
            // btnEndTurn
            // 
            btnEndTurn.Location = new Point(843, 317);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(150, 30);
            btnEndTurn.TabIndex = 5;
            btnEndTurn.Text = "End Turn";
            btnEndTurn.UseVisualStyleBackColor = true;
            btnEndTurn.Click += btnEndTurn_Click_1;
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(843, 209);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(150, 30);
            btnNewGame.TabIndex = 7;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // pnlDeck
            // 
            pnlDeck.BackColor = Color.CadetBlue;
            pnlDeck.BorderStyle = BorderStyle.Fixed3D;
            pnlDeck.Location = new Point(25, 298);
            pnlDeck.Name = "pnlDeck";
            pnlDeck.Size = new Size(183, 112);
            pnlDeck.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = SystemColors.ButtonShadow;
            lblStatus.Location = new Point(825, 378);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(198, 130);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Game Status";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTrumpSuit
            // 
            lblTrumpSuit.AutoSize = true;
            lblTrumpSuit.BackColor = Color.Chartreuse;
            lblTrumpSuit.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTrumpSuit.Location = new Point(25, 429);
            lblTrumpSuit.Name = "lblTrumpSuit";
            lblTrumpSuit.Size = new Size(178, 38);
            lblTrumpSuit.TabIndex = 10;
            lblTrumpSuit.Text = "Trump Suit :";
            // 
            // lblTurnStatus
            // 
            lblTurnStatus.AutoSize = true;
            lblTurnStatus.BackColor = Color.Chartreuse;
            lblTurnStatus.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTurnStatus.Location = new Point(25, 241);
            lblTurnStatus.Name = "lblTurnStatus";
            lblTurnStatus.Size = new Size(171, 31);
            lblTurnStatus.TabIndex = 11;
            lblTurnStatus.Text = "Turn : Waiting...";
            // 
            // btnRestartGame
            // 
            btnRestartGame.Location = new Point(843, 281);
            btnRestartGame.Name = "btnRestartGame";
            btnRestartGame.Size = new Size(150, 30);
            btnRestartGame.TabIndex = 12;
            btnRestartGame.Text = "Restart Game";
            btnRestartGame.UseVisualStyleBackColor = true;
            btnRestartGame.Click += btnRestartGame_Click_1;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            ClientSize = new Size(1050, 672);
            Controls.Add(btnRestartGame);
            Controls.Add(lblTurnStatus);
            Controls.Add(lblTrumpSuit);
            Controls.Add(lblStatus);
            Controls.Add(btnNewGame);
            Controls.Add(pnlDeck);
            Controls.Add(btnEndTurn);
            Controls.Add(btnTakeCards);
            Controls.Add(pnlPlayArea);
            Controls.Add(pnlComputerHand);
            Controls.Add(pnlPlayerHand);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Durak Card Game";
            Load += GameForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel pnlPlayerHand;
        private Panel pnlComputerHand;
        private Panel pnlPlayArea;
        private Button btnTakeCards;
        private Button btnEndTurn;
        private Button btnNewGame;
        private Panel pnlDeck;
        private Label lblStatus;
        private Label lblTrumpSuit;
        private Label lblTurnStatus;
        private Button btnRestartGame;
    }
}
