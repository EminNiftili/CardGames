using Card.Logic.Enums;
using Card.Logic.Models;

namespace Card.Logic.Settings
{
    public class CardOption
    {
        public CardOption(DeckCardSize cardSize, DeckCardSymbol cardSymbol, Func<int> ruleForValue, Action<DeckCard>? ruleForSpecialCase)
        {
            RuleForValue = ruleForValue;
            RuleForSpecialCase = ruleForSpecialCase;
            CardSymbol = cardSymbol;
            CardSize = cardSize;
        }

        public Func<int> RuleForValue { get; set; }
        public Action<DeckCard>? RuleForSpecialCase { get; set; }
        public DeckCardSize CardSize { get; set; }
        public DeckCardSymbol CardSymbol { get; set; }
    }
}
