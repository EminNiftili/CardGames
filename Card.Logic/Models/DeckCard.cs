using Card.Logic.Enums;
using Card.Logic.Settings;

namespace Card.Logic.Models
{
    public class DeckCard
    {
        private CardOption _cardOption;
        public event Action<DeckCard>? OnHandling;
        public DeckCard(CardOption option)
        {
            CloneCardOption(option);
            CardSize = _cardOption.CardSize;
            CardSymbol = _cardOption.CardSymbol;
            Value = _cardOption.RuleForValue is null ? (int)_cardOption.CardSymbol : _cardOption.RuleForValue.Invoke();
            OnHandling += _cardOption.RuleForSpecialCase;
        }

        public DeckCard(DeckCardSize cardSize, DeckCardSymbol cardSymbol, int value)
        {
            CardSize = cardSize;
            CardSymbol = cardSymbol;
            Value = value;
            _cardOption = new CardOption(cardSize, cardSymbol, () => value, null);
        }
        public DeckCardSymbol CardSymbol { get; private set; }
        public  DeckCardSize CardSize { get; private set; }
        public int Value { get; private set; }
        public int Id { get; set; }


        private void CloneCardOption(CardOption option)
        {
            if(option == null)
            {
                return;
            }
            _cardOption = new(option.CardSize, option.CardSymbol, option.RuleForValue, option.RuleForSpecialCase);
        }

        public override string ToString()
        {
            return $"{CardSize} {CardSymbol} {Value}";
        }
    }
}
