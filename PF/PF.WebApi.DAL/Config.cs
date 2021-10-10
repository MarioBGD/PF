using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL
{
    public static class Config
    {
        //local
        //public static string DbConnectionString { get; } = "Server=(localdb)\\mssqllocaldb;Database=PayFairDb;Trusted_Connection=True;MultipleActiveResultSets=true;";
        //test webio
        public static string DbConnectionString { get; } = "Server=mssql4.webio.pl,2401;Database=mariuszb1357_PF_DataBase;Uid=mariuszb1357_mainuser;Password=Usermain1@;";
    }
}
