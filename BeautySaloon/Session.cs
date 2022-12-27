using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySaloon.Models;

namespace BeautySaloon
{
    public class Session
    {
        private Session()
        {
            context = new SemerovaBeautySaloonContext();
        }

        private static Session? instance;
        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Session();
                }
                return instance;
            }
        }

        private SemerovaBeautySaloonContext context;
        public SemerovaBeautySaloonContext Context => context;
    }
}
