using Chechka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Chechka.Controllers
{
    public class MenuViewComponent : ViewComponent
    {
        //3.1.1 Коллекция исходных данных
        private List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem{ Controller="Home", Action="Index", Text="Lab 3" },
            new MenuItem{ Controller="Product", Action="Index", Text="Каталог" },
            new MenuItem{ IsPage=true, Area="Admin", Page="/Index", Text="Администрирование"}
        };

        //3.1.1 Метод
        public IViewComponentResult Invoke()
        {
            //Получение значений сегментов маршрута
            var controller = ViewContext.RouteData.Values["controller"];
            var page = ViewContext.RouteData.Values["page"];
            var area = ViewContext.RouteData.Values["area"];

            foreach (var item in _menuItems)
            {
                //Название контроллера совпадает?
                var _matchController = controller?.Equals(item.Controller) ?? false;

                //Название области совпадает?
                var _matchArea = area?.Equals(item.Area) ?? false;

                //Если есть совпадение, то сделать элемент меню активным
                //(применить соответствующий класс CSS)
                if (_matchController || _matchArea)
                {
                    item.Active = "active";
                }
            }
            return View(_menuItems);
        }
    }
}
