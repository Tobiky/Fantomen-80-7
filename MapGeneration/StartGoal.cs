using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymnasieArbete1
{
    class StartGoal : Risk
    {
        bool isStart;

        public StartGoal() { }

        public StartGoal(bool isStart)
        {
            this.isStart = isStart;
        }

        public bool IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }
    }
}
