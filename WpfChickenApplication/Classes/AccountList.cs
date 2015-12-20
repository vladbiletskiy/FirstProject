using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChickenApplication
{
    [Serializable]
    public class AccountList : List<Account>
    {
        public bool Contains(string name)
        {
            foreach (var element in this)
            {
                if (element.Name == name) return true;
            }
            return false;
        }
        public Account GetAccount(string name)
        {
            foreach (var element in this)
            {
                if (element.Name == name) return element;
            }
            return null;
        }
    }
}
