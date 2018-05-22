using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models.Manage.TeamModels
{
    public class TeamListViewModel
    {
        TeamListFilterModel m_filter = new TeamListFilterModel();
        TeamListResultModel m_result = new TeamListResultModel();

        public TeamListFilterModel Filter { get { return this.m_filter; } set { this.m_filter = value; } }
        public TeamListResultModel Result { get { return this.m_result; } set { this.m_result = value; } }
    }
}