using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Chechka.DAL.Entities;
using System.Linq;
using Chechka.Models;
using Chechka.Extensions;
using Chechka.DAL.Data; //Lb8.4.2

namespace Chechka.Controllers
{
    public class ProductController : Controller
    {

        ////Lb8.4.2--
        //public List<ComputerPart> _computerParts;
        //List<ComputerPartGroup> _computerPartGroups;
        ////Lb8.4.2--

        //Lb8.4.2{
        ApplicationDbContext _context;
        //Lb8.4.2}

        int _pageSize;

        ////Lb8.4.2--
        //public ProductController()
        //{
        //    _pageSize = 3;
        //    SetupData();
        //}
        //////Lb8.4.2--

        public ProductController(ApplicationDbContext context)
        {
            _pageSize = 3;
            _context = context;
        }

        //Lb7.4.6{
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        //Lb7.4.6}

        public IActionResult Index(/*Lb6.4.6.2{*/int? group/*Lb6.4.6.2}*/, int pageNo = 1)
        {
            //Lb6.4.4.3--
            //var items = _cmputerParts
            //                .Skip((pageNo - 1) * _pageSize)
            //                .Take(_pageSize)
            //                .ToList();
            //return View(items);
            //Lb6.4.4.3--

            //Lb6.4.6.2
            //Добавим фильтр
                ////Lb8.4.2--
                //var computerPartsFiltered = _computerParts.Where(d => !group.HasValue || d.ComputerPartGroupId == group.Value);
                ////Lb8.4.2--

                //Lb8.4.2{
                var computerPartsFiltered = _context.ComputerParts.Where(d => !group.HasValue || d.ComputerPartGroupId == group.Value);
                //Lb8.4.2}
            //Lb6.4.6.2

            //Lb6.4.6.2{
            // Поместить список групп во ViewData
                ////Lb8.4.2--
                //ViewData["Groups"] = _computerPartGroups;
                ////Lb8.4.2--

                //Lb8.4.2{
                ViewData["Groups"] = _context.ComputerPartGroups;
            //Lb8.4.2}
            // Получить id текущей группы и поместить в ViewData
            ViewData["CurrentGroup"] = group ?? 0;
            //Lb6.4.6.2

            //Lb6.4.6.2--
            //return View(ListViewModel<ComputerPart>.GetModel(_computerParts, pageNo, _pageSize));

            //Lb6.4.6.2
            //Lb7.4.3.3--
            //return View(ListViewModel<ComputerPart>.GetModel(computerPartsFiltered, pageNo, _pageSize));

            //Lb7.4.3.3{
            var model = ListViewModel<ComputerPart>.GetModel(computerPartsFiltered, pageNo, _pageSize);
            //Lb7.4.5--            
            //if (Request.Headers["x-requested-with"].ToString().ToLower().Equals("xmlhttprequest"))
            //    return PartialView("_listpartial", model);
            //Lb7.4.5--
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
            //Lb7.4.3.3}
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            //_computerPartGroups = new List<ComputerPartGroup>
            //{
            //    new ComputerPartGroup {ComputerPartGroupId=1, GroupName="Процессоры"},
            //    new ComputerPartGroup {ComputerPartGroupId=2, GroupName="Видеоакарты"}
            //    };


            //_computerParts = new List<ComputerPart>
            //{
            //    new ComputerPart {
            //        ComputerPartId = 1,
            //        ComputerPartName="Процессор AMD Ryzen 5 3600",
            //        Description="Socet AM4, 3.6/4.2 ГГц, 6 ядер, многопоточный, 65 Вт, 7 нм",
            //        Price=455,
            //        ComputerPartGroupId=1,
            //        Image="cpu_amd_ryzen_5_3600.jpeg"
            //    },
            //    new ComputerPart {
            //        ComputerPartId = 2,
            //        ComputerPartName="Процессор AMD Ryzen 5 5600X",
            //        Description="Socet AM4, 3.7/4.6 ГГц, 6 ядер, многопоточный, 65 Вт, 7 нм",
            //        Price=816,
            //        ComputerPartGroupId=1,
            //        Image="cpu_amd_ryzen_5_5600X.jpeg"
            //    },
            //    new ComputerPart {
            //        ComputerPartId = 3,
            //        ComputerPartName="Процессор Intel Core i5-10400F",
            //        Description="Socet LGA1200, 2.9/4.3 ГГц, 6 ядер, многопоточный, 65 Вт, 14 нм",
            //        Price=431,
            //        ComputerPartGroupId=1,
            //        Image="cpu_Intel_core_i5_10400.jpeg"
            //    },
            //    new ComputerPart {
            //        ComputerPartId = 4,
            //        ComputerPartName="Видеокарта Gigabyte GeForce GTX 1660 OC 6GB GDDR5 GV-N1660OC-6GD",
            //        Description="Частота ядра 1530/1830 МГц, 6 ГБ GDDR5, частота памяти 8 000 МГц, HDMI/DisplayPort(3)",
            //        Price=1700,
            //        ComputerPartGroupId=2,
            //        Image="videocard_gigabyte_geforce_gtx_1660_oc_6gb_gddr5_gv-n1660oc-6gd.jpeg"
            //    },
            //    new ComputerPart {
            //        ComputerPartId = 5,
            //        ComputerPartName="Видеокарта ASUS Cerberus GeForce GTX 1050 Ti OC Edition 4GB GDDR5",
            //        Description="Частота ядра 1392/1450 МГц, 4 ГБ GDDR5, частота памяти 7 008 МГц, DVI/HDMI/DisplayPort(3)",
            //        Price=716,
            //        ComputerPartGroupId=2,
            //        Image="videocard_asus_cerberus_geforce_gtx_1050_ti_oc_edition_4gb_gddr5.jpeg"
            //    },
            //    new ComputerPart {
            //        ComputerPartId = 6,
            //        ComputerPartName="Gigabyte Radeon RX 580 Gaming 8GB GDDR5 GV-RX580GAMING-8GD rev. 1.0",
            //        Description="Частота ядра 1257/2304 МГц, 8 ГБ GDDR5, частота памяти 8 000 МГц, DVI/HDMI/DisplayPort(3)",
            //        Price=2893,
            //        ComputerPartGroupId=2,
            //        Image="videocard_gigabyte_radeon_rx_580_gaming_8gb_gddr5_gv-rx580gaming-8gd_rev._1.0.jpeg"
            //    },
            //};
        }
    }
}
