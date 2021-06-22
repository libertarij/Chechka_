using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chechka.Models
{
    public class MenuItem
    {
        //3.1.1 Является ли вызываемая ссылка страницей (Razor Page) или методом контроллера
        public bool IsPage { get; set; } = false;

        //3.1.1 Текст надписи
        public string Text { get; set; } = "";

        //3.1.1 Имя контроллера
        public string Controller { get; set; } = "";

        //3.1.1 Имя метода
        public string Action { get; set; } = "";

        //3.1.1 Имя страницы
        public string Page { get; set; } = "";

        //3.1.1 Имя боласти (Ares)
        public string Area { get; set; } = "";

        //3.1.1 Имя класса CSS для текущего (активного) пункта меню
        public string Active { get; set; } = "";


    }
}
