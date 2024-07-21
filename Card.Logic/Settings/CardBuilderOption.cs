using Card.Logic.Enums;

namespace Card.Logic.Settings
{
    public class CardBuilderOption
    {
        private List<CardOption> _options;

        public CardBuilderOption()
        {
            this._options = new List<CardOption>();
        }

        public void AddRuleForCalculatingValue(DeckCardSize cardSize, DeckCardSymbol cardSymbol, Func<int> returnValue)
        {
            if(returnValue == null)
            {
                throw new ArgumentNullException(nameof(returnValue));
            }
            var option = _options.FirstOrDefault(x => x.CardSize == cardSize && x.CardSymbol == cardSymbol);
            if (option == null)
            {
                _options.Add(new CardOption(cardSize, cardSymbol, returnValue, null));
            }
            else
            {
                option.RuleForValue = returnValue;
            }
        }
        public void AddRuleForCalculatingValue(DeckCardSize cardSize, Func<int> returnValue)
        {
            var cardSymbols = Enum.GetValues<DeckCardSymbol>()?.ToList();
            cardSymbols ??= new List<DeckCardSymbol>();
            cardSymbols.Remove(DeckCardSymbol.None);
            foreach(var cardSymbol in cardSymbols)
            {
                AddRuleForCalculatingValue(cardSize, cardSymbol, returnValue);
            }
        }


        public CardOption GetOrCreateOption(DeckCardSize cardSize, DeckCardSymbol cardSymbol)
        {
            var option = _options.FirstOrDefault(x => x.CardSize == cardSize && x.CardSymbol == cardSymbol);
            option ??= new CardOption(cardSize, cardSymbol, () => (int)cardSize, null);
            return option;
        }
    }
}
