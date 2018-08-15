using System;
using System.Collections.Generic;
using System.Text;
using PremierShipDb.App.Core.Dtos;

namespace PremierShipDb.App.Contracts
{
    public interface ITeamController
    {
        void AddTeam(TeamDto teamDto);

        void SetDateFound(int teamId, DateTime date);

        void SetAddress(int teamId, string address);

        TeamDto TeamInfo(int teamId);

        TeamPersonalInfoDto TeamPersonalInfo(int teamId);

    }
}
