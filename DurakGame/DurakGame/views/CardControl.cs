using System;
using System.Windows.Forms;
using DurakGame.Classes;


namespace DurakGame.Views
{
    public class CardControl : Button
    {
        private Card card;

        public CardControl(Card card)
        {
            this.card = card;
            Text = card.ToString();
            Click += Card_Click;
        }

        private void Card_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"You clicked {card}");
        }
    }
}
