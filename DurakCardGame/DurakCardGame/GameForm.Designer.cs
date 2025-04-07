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
            SuspendLayout();
            // 
            // pnlPlayerHand
            // 
            pnlPlayerHand.BackColor = Color.ForestGreen;
            pnlPlayerHand.Location = new Point(101, 399);
            pnlPlayerHand.Name = "pnlPlayerHand";
            pnlPlayerHand.Size = new Size(600, 150);
            pnlPlayerHand.TabIndex = 1;
            // 
            // pnlComputerHand
            // 
            pnlComputerHand.BackColor = Color.ForestGreen;
            pnlComputerHand.Location = new Point(101, 4);
            pnlComputerHand.Name = "pnlComputerHand";
            pnlComputerHand.Size = new Size(600, 150);
            pnlComputerHand.TabIndex = 2;
            // 
            // pnlPlayArea
            // 
            pnlPlayArea.BackColor = Color.DarkGreen;
            pnlPlayArea.Location = new Point(221, 163);
            pnlPlayArea.Name = "pnlPlayArea";
            pnlPlayArea.Size = new Size(400, 200);
            pnlPlayArea.TabIndex = 3;
            // 
            // btnTakeCards
            // 
            btnTakeCards.Location = new Point(635, 237);
            btnTakeCards.Name = "btnTakeCards";
            btnTakeCards.Size = new Size(120, 40);
            btnTakeCards.TabIndex = 4;
            btnTakeCards.Text = "Take Cards";
            btnTakeCards.UseVisualStyleBackColor = true;
            // 
            // btnEndTurn
            // 
            btnEndTurn.Location = new Point(635, 283);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(120, 40);
            btnEndTurn.TabIndex = 5;
            btnEndTurn.Text = "End Turn";
            btnEndTurn.UseVisualStyleBackColor = true;
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(635, 192);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(120, 40);
            btnNewGame.TabIndex = 7;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // pnlDeck
            // 
            pnlDeck.BackColor = Color.Green;
            pnlDeck.Location = new Point(12, 162);
            pnlDeck.Name = "pnlDeck";
            pnlDeck.Size = new Size(183, 112);
            pnlDeck.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = SystemColors.ButtonShadow;
            lblStatus.Location = new Point(12, 277);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(203, 119);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Game Status";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
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
    }
}
