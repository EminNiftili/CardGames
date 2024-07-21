namespace Card.Pablo.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User emin = new User() { Id = 1 };
            User hajar = new User() { Id = 2 };

            Pablo pablo = new Pablo(2);
            pablo.JoinUser(emin);
            pablo.JoinUser(hajar);

            pablo.StartPablo();

            var eminsCard = pablo.GetUserCard(emin.Id);
            var hajarsCard = pablo.GetUserCard(hajar.Id);

            var round1 = pablo.Play(emin.Id, false);
            pablo.PlayUser(emin.Id, round1, eminsCard[0].Id, PabloAction.Exchange);


            var round2 = pablo.Play(hajar.Id, true);
            pablo.PlayUser(hajar.Id, round2, hajarsCard[0].Id, PabloAction.Exchange);
        }
    }
}