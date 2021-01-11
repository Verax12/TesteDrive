using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDrive.MasterDetail
{

    public class MasterDetailViewMasterMenuItem
    {
        public MasterDetailViewMasterMenuItem()
        {
            TargetType = typeof(MasterDetailViewMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        
        public Type TargetType { get; set; }

    }
}