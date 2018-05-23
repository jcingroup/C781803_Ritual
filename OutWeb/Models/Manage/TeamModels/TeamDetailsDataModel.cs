using OutWeb.Entities;
using System.Collections.Generic;

namespace OutWeb.Models.Manage.TeamModels
{
    public class TeamDetailsDataModel 
    {
 
        private TEAM m_details = new TEAM();

        public TEAM Data
        { get { return this.m_details; } set { this.m_details = value; } }
    }
}