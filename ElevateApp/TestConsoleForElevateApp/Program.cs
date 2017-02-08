using ElevateAppLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleForElevateApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var traMgr = new TrainerManager();

           // var trainers = traMgr.GetFullTrainerList();

            Console.WriteLine("There are " + traMgr.GetTrainerCount().ToString() + " trainers.\n");
            foreach (var tra in traMgr.ActiveTrainer)
            {
                Console.WriteLine(tra.TrainerFirstName + " \t" + tra.TrainerLastName);
            }
            Console.ReadKey();
        }
    }
}
