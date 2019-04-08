using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models
{
    public class ListViewBase
    {
        public ListFilterBase Filter { get; set; } = new ListFilterBase();
        public ListResultBase Result { get; set; } = new ListResultBase();
    }
}