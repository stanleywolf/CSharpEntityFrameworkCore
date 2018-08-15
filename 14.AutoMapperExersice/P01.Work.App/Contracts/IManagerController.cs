using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Core.Dtos;

namespace P01.Work.App.Contracts
{
    public interface IManagerController
    {
        void SetManager(int empId, int managerId);

        ManagerDto GetManagerInfo(int emplId);
    }
}
