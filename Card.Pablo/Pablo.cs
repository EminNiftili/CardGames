using Card.Logic.Enums;
using Card.Logic.Models;
using Card.Core.Extensions;
using Card.Logic.Settings;

namespace Card.Pablo
{
    public class Pablo
    {
        private object _lockJoinUser = new object();
        private Deck _deck;
        private int _maximumUser;
        private List<DeckCard> _playedCards;
        private List<DeckCard> _cardsInTable;
        private Dictionary<int, User> _userQueue;
        private Dictionary<int, List<DeckCard>> _cardsInUserHand;

        public Pablo(int maximumUser)
        {
            CardBuilderOption cardBuilder = new CardBuilderOption();
            cardBuilder.AddRuleForCalculatingValue(DeckCardSize.A, () => 1);
            cardBuilder.AddRuleForCalculatingValue(DeckCardSize.K, () => 10);
            cardBuilder.AddRuleForCalculatingValue(DeckCardSize.Q, () => 10);
            cardBuilder.AddRuleForCalculatingValue(DeckCardSize.J, () => 10);
            cardBuilder.AddRuleForCalculatingValue(DeckCardSize.Joker, () => 0);
            _deck = Deck.Create(DeckStyle.Big, cardBuilder, true);
            _maximumUser = maximumUser;
            _playedCards = new List<DeckCard>();
            _cardsInTable = new List<DeckCard>();
            _cardsInUserHand = new Dictionary<int, List<DeckCard>>();
            _userQueue = new Dictionary<int, User>();
            for (int i = 1; i <= _maximumUser; i++)
            {
                _userQueue.Add(i, null);
            }
        }

        public bool JoinUser(User user)
        {
            Monitor.Enter(_lockJoinUser);
            try
            {
                if (_userQueue.Where(x => x.Value != null).Count() < _maximumUser)
                {
                    var queueId = GetUserQueue();
                    _userQueue.Remove(queueId);
                    _userQueue.Add(queueId, user);
                    return true;
                }
                return false;
            }
            finally
            {
                Monitor.Exit(_lockJoinUser);
            }
        }

        public bool LeaveUser(int userId)
        {
            if(_userQueue.Any(x => x.Value.Id == userId))
            {
                var userQueue = _userQueue.First(x => x.Value.Id == userId);
                _userQueue.Remove(userQueue.Key);
                _userQueue.Add(userQueue.Key, null);
                return true;
            }
            return false;
        }

        public List<DeckCard> GetUserCard(int userId)
        {
            if (_userQueue.Any(x => x.Value.Id == userId))
            {
                var userQueue = _userQueue.First(x => x.Value.Id == userId);
                return _cardsInUserHand[userId];
            }
            return null;
        }

        public void StartPablo()
        {
            _cardsInTable = _deck.Shuffle();
            foreach(var userQueue in _userQueue)
            {
                var userHand = _cardsInTable.GetLastAndRemove(4);
                _cardsInUserHand.Add(userQueue.Key, userHand);
            }
        }

        public TakenCard Play(int userId, bool isFromPlayed)
        {
            if (_userQueue.Any(x => x.Value.Id == userId))
            {
                var userQueue = _userQueue.First(x => x.Value.Id == userId);
                TakenCard takenCard = new TakenCard();
                if (isFromPlayed)
                {
                    takenCard.Card = _playedCards.GetLastAndRemove();
                    takenCard.Actions = new List<PabloAction>
                    {
                        PabloAction.Exchange,
                        PabloAction.BurnOut
                    };
                }
                else
                {
                    takenCard.Card =  _cardsInTable.GetLastAndRemove();
                    takenCard.Actions = new List<PabloAction>
                    {
                        PabloAction.Exchange,
                        PabloAction.BurnOut,
                        PabloAction.View,
                        PabloAction.Swap,
                        PabloAction.Discard
                    };
                }
                return takenCard;
            }
            return null;
        }

        public bool PlayUser(int userId, TakenCard takenCard, int handCardId, PabloAction action)
        {
            if (_userQueue.Any(x => x.Value.Id == userId) && takenCard.Actions.Contains(action))
            {
                var userQueue = _userQueue.First(x => x.Value.Id == userId);
                switch (action)
                {
                    case PabloAction.Exchange:
                        {
                            var removedCard = _cardsInUserHand[userId].FirstOrDefault(x => x.Id == handCardId);
                            _cardsInUserHand[userId].Remove(removedCard);
                            _playedCards.Add(removedCard);
                            var anotherSameRemovable = _cardsInUserHand[userId].Where(x => x.CardSize == removedCard.CardSize).ToList();
                            foreach(var removable in anotherSameRemovable)
                            {
                                _cardsInUserHand[userId].Remove(removable);
                                _playedCards.Add(removable);
                            }
                            _cardsInUserHand[userId].Add(takenCard.Card);
                            break;
                        }
                }

                return true;
            }
            return false;
        }

        private int GetUserQueue()
        {
            foreach(var user in _userQueue)
            {
                if(user.Value is null)
                {
                    return user.Key;
                }
            }
            return -1;
        }
    }
}
