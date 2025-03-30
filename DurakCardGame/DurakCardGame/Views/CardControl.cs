using System;
using System.Drawing;
using System.Windows.Forms;
using DurakCardGame.Models;

namespace DurakCardGame.Views
{
    // This is a custom control that inherits from UserControl
    public class CardControl : UserControl
    {
        // Private fields to store card information
        private Card _card;
        private bool _faceUp = true;
        private bool _isSelected = false;

        // Public property for the card
        public Card Card
        {
            get { return _card; }
            set
            {
                _card = value;
                Invalidate(); // Force the control to redraw when card changes
            }
        }

        // Public property to determine if card is face up or face down
        public bool FaceUp
        {
            get { return _faceUp; }
            set
            {
                _faceUp = value;
                Invalidate(); // Force redraw when face up/down changes
            }
        }

        // Public property to determine if card is selected
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                Invalidate(); // Force redraw when selection changes
            }
        }

        // Constructor
        public CardControl()
        {
            // Set default size
            Size = new Size(80, 120);

            // Enable double buffering to prevent flickering
            DoubleBuffered = true;
        }

        // This method is called when the control needs to be redrawn
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            // Draw card background
            Rectangle cardRect = new Rectangle(0, 0, Width - 1, Height - 1);

            if (_faceUp && _card != null)
            {
                // Draw a face-up card
                g.FillRectangle(Brushes.White, cardRect);

                // Get suit color (red or black)
                Brush textBrush = (_card.CardSuit == Card.Suit.Hearts || _card.CardSuit == Card.Suit.Diamonds)
                    ? Brushes.Red
                    : Brushes.Black;

                // Draw rank in top-left corner
                string rank = GetRankSymbol();
                g.DrawString(rank, Font, textBrush, new Point(5, 5));

                // Draw suit in top-left corner
                string suitSymbol = GetSuitSymbol();
                g.DrawString(suitSymbol, Font, textBrush, new Point(5, 20));

                // Draw large suit symbol in center
                Font largeFont = new Font(Font.FontFamily, 24);
                g.DrawString(suitSymbol, largeFont, textBrush, new Point(Width / 2 - 15, Height / 2 - 20));
            }
            else
            {
                // Draw a face-down card
                g.FillRectangle(Brushes.LightBlue, cardRect);
                g.DrawRectangle(Pens.Blue, cardRect);

                // Draw pattern on back of card
                for (int x = 5; x < Width; x += 10)
                {
                    for (int y = 5; y < Height; y += 10)
                    {
                        g.DrawRectangle(Pens.Blue, x, y, 4, 4);
                    }
                }
            }

            // Draw card border
            Pen borderPen = _isSelected ? new Pen(Color.Gold, 3) : Pens.Black;
            g.DrawRectangle(borderPen, cardRect);

            // If selected, draw a highlight
            if (_isSelected)
            {
                Rectangle highlightRect = new Rectangle(2, 2, Width - 5, Height - 5);
                g.DrawRectangle(new Pen(Color.Gold, 2), highlightRect);
            }
        }

        // Helper method to get appropriate symbol for rank
        private string GetRankSymbol()
        {
            switch (_card.CardRank)
            {
                case Card.Rank.Six: return "6";
                case Card.Rank.Seven: return "7";
                case Card.Rank.Eight: return "8";
                case Card.Rank.Nine: return "9";
                case Card.Rank.Ten: return "10";
                case Card.Rank.Jack: return "J";
                case Card.Rank.Queen: return "Q";
                case Card.Rank.King: return "K";
                case Card.Rank.Ace: return "A";
                default: return "?";
            }
        }

        // Helper method to get appropriate symbol for suit
        private string GetSuitSymbol()
        {
            switch (_card.CardSuit)
            {
                case Card.Suit.Clubs: return "♣";
                case Card.Suit.Diamonds: return "♦";
                case Card.Suit.Hearts: return "♥";
                case Card.Suit.Spades: return "♠";
                default: return "?";
            }
        }
    }
}