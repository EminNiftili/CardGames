using Card.Logic.Enums;
using Card.Logic.Settings;
using System.Collections.Generic;

namespace Card.Logic.Models
{
    public class Deck
    {
        public List<DeckCard> Cards { get; set; }

        public Deck(DeckStyle deckStyle, CardBuilderOption option, bool includeJoker)
        {
            Cards = new List<DeckCard>();
            switch (deckStyle)
            {
                case DeckStyle.Small:
                    {
                        CreateSmallDeck(option);
                        break;
                    }
                case DeckStyle.Medium:
                    {
                        CreateMediumDeck(option);
                        break;
                    }
                case DeckStyle.Big:
                    {
                        CreateBigDeck(option);
                        break;
                    }
                default:
                    {
                        throw new InvalidDataException(nameof(deckStyle));
                    }
            }
            if (includeJoker)
            {
                AddJokers(option, 2);
            }
            int i = 1;
            Cards.ForEach(x =>
            {
                x.Id = i;
                i++;
            });
        }
        public Deck(DeckStyle deckStyle, CardBuilderOption option) : this(deckStyle, option, false)
        {
        }
        public Deck(DeckStyle deckStyle, bool includeJoker) : this(deckStyle, new CardBuilderOption(), includeJoker)
        {
        }

        public List<DeckCard> Shuffle()
        {
            Random rng = new Random();
            List<DeckCard> newList = new(Cards); // Orijinal listeyi kopyala
            int n = newList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                DeckCard value = newList[k];
                newList[k] = newList[n];
                newList[n] = value;
            }
            return newList;
        }

        public static Deck Create(DeckStyle deckStyle)
        {
            return new Deck(deckStyle, new CardBuilderOption());
        }

        public static Deck Create(DeckStyle deckStyle, CardBuilderOption option, bool includeJoker = false)
        {
            return new Deck(deckStyle, option, includeJoker);
        }

        private void CreateSmallDeck(CardBuilderOption cardBuilder)
        {
            Cards.AddRange(new[]
            {
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.A, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.K, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Q, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.J, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Ten, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Nine, DeckCardSymbol.RedHeart)),
            });

            Cards.AddRange(new[]
            {
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.A, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.K, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Q, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.J, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Ten, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Nine, DeckCardSymbol.BlackHeart)),
            });

            Cards.AddRange(new[]
            {

                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.A, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.K, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Q, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.J, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Ten, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Nine, DeckCardSymbol.RedDiamond)),
            });

            Cards.AddRange(new[]
            {
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.A, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.K, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Q, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.J, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Ten, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Nine, DeckCardSymbol.BlackClub)),
            });
        }

        private void CreateMediumDeck(CardBuilderOption cardBuilder)
        {
            CreateSmallDeck(cardBuilder);

            Cards.AddRange(new[]
            {
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Eight, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Seven, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Six, DeckCardSymbol.RedHeart)),


                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Eight, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Seven, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Six, DeckCardSymbol.BlackHeart)),


                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Eight, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Seven, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Six, DeckCardSymbol.RedDiamond)),


                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Eight, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Seven, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Six, DeckCardSymbol.BlackClub)),
            });
        }

        private void CreateBigDeck(CardBuilderOption cardBuilder)
        {
            CreateMediumDeck(cardBuilder);

            Cards.AddRange(new[]
            {
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Five, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Four, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Three, DeckCardSymbol.RedHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Two, DeckCardSymbol.RedHeart)),


                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Five,  DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Four,  DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Three, DeckCardSymbol.BlackHeart)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Two, DeckCardSymbol.BlackHeart)),


                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Five, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Four, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Three, DeckCardSymbol.RedDiamond)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Two, DeckCardSymbol.RedDiamond)),


                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Five, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Four, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Three, DeckCardSymbol.BlackClub)),
                new DeckCard(cardBuilder.GetOrCreateOption(DeckCardSize.Two, DeckCardSymbol.BlackClub)),
            });
        }

        private void AddJokers(CardBuilderOption option, int jokerCount)
        {
            for(int i = 1; i <= jokerCount; i++)
            {
                Cards.Add(new DeckCard(option.GetOrCreateOption(DeckCardSize.Joker, DeckCardSymbol.None)));
            }
        }
    }
}
