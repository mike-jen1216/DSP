using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSP.Models.Entity;

namespace DSP.Interface.Repo
{
    public interface IDesignPatternRepo
    {
        DesignPatternEntity GetEntity(int id);//
    }
}