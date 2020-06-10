using gerappa_test01_api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerappa_test01_api.Controllers
{
    public interface ICrud<T> where T : CosmoEntity
    {
        Task<IActionResult> Create(T entity);
        Task<IActionResult> Update(T entity);
        Task<IActionResult> Get(string id);
        Task<IActionResult> GetAll();
        Task<IActionResult> Delete(string id);

    }
}
