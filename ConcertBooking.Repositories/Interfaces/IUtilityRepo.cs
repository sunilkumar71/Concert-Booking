
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.ConcertBooking.Repositories
{
    public  interface IUtilityRepo
    {
        Task<string> SaveImage(string ContainerName,IFormFile file);
        Task<string> Edit(string ContainerName,IFormFile file,string dbPath);
        Task DeleteFile(string ContainerName,string dbPath);
       
    }
}
