using Microsoft.AspNetCore.Mvc;

namespace DefensiveProgramming.Controllers
{
    public class VotingController : Controller
    {
        public bool CanVote(int age)
        {
            if (age < 0)
                throw new ArgumentException("Age cannot be negative.");

            return age >= 18;
        }
    }
}
