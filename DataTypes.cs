using System;
using System.Collections.Generic;

namespace FormFun
{
    [Serializable]
    class Form
    {
        public string description;

        public List<Tab> tabs = new List<Tab>();

        public Form(string _description)
        {
            description = _description;
        }
    }

    [Serializable]
    class Tab
    {
        public string description;

        public Tab(string _description)
        {
            description = _description;
        }

    }
}
