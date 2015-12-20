using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChickenApplication
{
[Serializable]
    public class Account
    {
    public string Name { set; get; }
    public byte Level_Avalible { set; get; }
    public int Task_Avalible { set; get; }
    public Account(string n)
    {
        Name = n;
        Level_Avalible = 0;
        Task_Avalible = 1;
    }
    }
}
