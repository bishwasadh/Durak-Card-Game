﻿namespace DurakCardGame
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
            lblStatus = new Button();
            btnNewGame = new Button();
            pnlDeck = new Panel();
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
            pnlComputerHand.Location = new Point(101, 9);
            pnlComputerHand.Name = "pnlComputerHand";
            pnlComputerHand.Size = new Size(600, 150);
            pnlComputerHand.TabIndex = 2;
            // 
            // pnlPlayArea
            // 
            pnlPlayArea.BackColor = Color.DarkGreen;
            pnlPlayArea.Location = new Point(201, 179);
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
            // lblStatus
            // 
            lblStatus.Location = new Point(21, 359);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(760, 44);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Game Status";
            lblStatus.UseVisualStyleBackColor = true;
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(635, 192);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(120, 40);
            btnNewGame.TabIndex = 7;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            // 
            // pnlDeck
            // 
            pnlDeck.BackColor = Color.Green;
            pnlDeck.Location = new Point(20, 200);
            pnlDeck.Name = "pnlDeck";
            pnlDeck.Size = new Size(100, 150);
            pnlDeck.TabIndex = 8;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
            Controls.Add(btnNewGame);
            Controls.Add(pnlDeck);
            Controls.Add(lblStatus);
            Controls.Add(btnEndTurn);
            Controls.Add(btnTakeCards);
            Controls.Add(pnlPlayArea);
            Controls.Add(pnlComputerHand);
            Controls.Add(pnlPlayerHand);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Durak Card Game";
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlPlayerHand;
        private Panel pnlComputerHand;
        private Panel pnlPlayArea;
        private Button btnTakeCards;
        private Button btnEndTurn;
        private Button lblStatus;
        private Button btnNewGame;
        private Panel pnlDeck;
    }
}
