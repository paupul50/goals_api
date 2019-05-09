using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Constants
{
    public static class RandomGoals
    {

        public static string GetRandomGoal()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            switch (number)
            {
                case 1:
                    return "Tik baltyminis maistas.";
                case 2:
                    return "Salotadienis.";
                case 3:
                    return "Meditacija.";
                case 4:
                    return "Paskaityti knygą.";
                case 5:
                    return "Užsiimti hobiu.";

                default:
                    return "Parašyti bakalaurą.";
            }
        }
    }
}