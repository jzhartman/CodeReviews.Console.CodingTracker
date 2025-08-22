using CodingTracker.Services.Interfaces;


namespace CodingTracker.Views
{
    public class CodingSessionView
    {
        private readonly ICodingSessionService _codingSessionService;

        public CodingSessionView(ICodingSessionService codingSessionService)
        {
            _codingSessionService = codingSessionService;
        }

        public void RenderAllCodingSessions()
        {
            var sessions = _codingSessionService.GetAllCodingSessions();

            int count = 1;
            Console.WriteLine("A list of all coding sessions: ");

            foreach (var session in sessions)
            {
                Console.WriteLine($"{count}:\t{session.StartTime} to {session.EndTime} for a duration of {session.Duration}");
                count++;
            }
        }
    }
}
