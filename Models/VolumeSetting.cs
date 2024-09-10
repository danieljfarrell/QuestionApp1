using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionApp1.Models
{
    public class VolumeSetting
    {
        public int Volume { set; get; }

        public VolumeSetting(int volume)
        {
            Volume = volume;
        }
    }
}
