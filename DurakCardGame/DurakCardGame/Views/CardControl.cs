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
    }
}